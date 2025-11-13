using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace UniversityPortal
{
    public partial class Login : Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["JobAppConn"].ConnectionString;

        // Utility: show exactly one alert
        private void ShowSuccess(string msg)
        {
            lblError.Visible = false;
            lblSuccess.Text = msg;
            lblSuccess.Visible = true;
        }

        private void ShowError(string msg)
        {
            lblSuccess.Visible = false;
            lblError.Text = msg;
            lblError.Visible = true;
        }

        // ------------------ PAGE LOAD ------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hide messages on first load
                lblError.Visible = false;
                lblSuccess.Visible = false;

                // 🔹 Show a friendly message if redirected from another page
                if (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]))
                {
                    lblError.Text = "🔒 Please log in to access the requested page.";
                    lblError.Visible = true;
                }
            }
            else
            {
                // On postback, reset messages (avoid old ones persisting)
                lblError.Visible = false;
                lblSuccess.Visible = false;
            }
        }

        // ------------------ LOGIN ------------------
        // ------------------ LOGIN ------------------
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();   // Email
            string pass = txtPassword.Text.Trim();
            string hash = HashPassword(pass);

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT UserID, FirstName, LastName, Email, Department
        FROM dbo.Users 
        WHERE Email = @user AND PasswordHash = @pass;", conn))
            {
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", hash);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // ✅ Save User Info in Session
                        int userId = Convert.ToInt32(dr["UserID"]);
                        Session["UserID"] = userId;
                        Session["UserEmail"] = dr["Email"];
                        Session["UserName"] = dr["FirstName"] + " " + dr["LastName"];
                        Session["Department"] = dr["Department"];
                    }
                    else
                    {
                        ShowError("❌ Invalid username or password.");
                        return;
                    }
                }
            }

            // ✅ After login, check if user already has an application
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand checkApp = new SqlCommand(@"
        SELECT TOP 1 ApplicantID 
        FROM Applicants 
        WHERE UserID = @uid;", conn))
            {
                checkApp.Parameters.AddWithValue("@uid", Session["UserID"]);

                conn.Open();
                object appId = checkApp.ExecuteScalar();

                if (appId != null)
                {
                    // 🔹 User already submitted application → Go to Preview
                    Session["ApplicantID"] = Convert.ToInt32(appId);
                    ShowSuccess("ℹ You have already submitted your application.");
                    Response.Redirect("ApplicationPreview.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    // 🔹 No application yet → Go to Application Form
                    Response.Redirect("ApplicationForm.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        // ------------------ SIGNUP ------------------
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (txtPassword1.Text != txtPassword2.Text)
            {
                ShowError("❌ Passwords do not match.");
                return;
            }

            string fn = txtFirstName.Text.Trim();
            string ln = txtLastName.Text.Trim();
            string em = txtEmail.Text.Trim();
            string dept = txtDept.Text.Trim();
            string hash = HashPassword(txtPassword1.Text);

            if (string.IsNullOrEmpty(fn) || string.IsNullOrEmpty(ln) ||
                string.IsNullOrEmpty(em) || string.IsNullOrEmpty(dept))
            {
                ShowError("❌ Please fill out all fields.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO dbo.Users (FirstName, LastName, Email, Department, PasswordHash)
                OUTPUT INSERTED.UserID
                VALUES (@fn, @ln, @em, @dept, @pw);", conn))
            {
                cmd.Parameters.AddWithValue("@fn", fn);
                cmd.Parameters.AddWithValue("@ln", ln);
                cmd.Parameters.AddWithValue("@em", em);
                cmd.Parameters.AddWithValue("@dept", dept);
                cmd.Parameters.AddWithValue("@pw", hash);

                conn.Open();
                try
                {
                    object newId = cmd.ExecuteScalar();   // returns new UserID

                    // ✅ Save UserID in Session immediately
                    Session["UserID"] = Convert.ToInt32(newId);
                    Session["UserEmail"] = em;
                    Session["UserName"] = fn + " " + ln;
                    Session["Department"] = dept;

                    // ✅ Redirect directly to ApplicationForm
                    Response.Redirect("ApplicationForm.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        ShowError("❌ This email is already registered.");
                    }
                    else
                    {
                        ShowError("❌ Error creating account: " + ex.Message);
                    }
                }
            }
        }

        // ------------------ PASSWORD HASHING ------------------
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes); // ~44 chars
            }
        }

        // ------------------ TAB SWITCHING ------------------
        protected void SwitchToLogin(object sender, EventArgs e)
        {
            pnlLogin.Visible = true;
            pnlSignup.Visible = false;

            // Clear messages
            lblError.Visible = false;
            lblSuccess.Visible = false;
        }

        protected void SwitchToSignup(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlSignup.Visible = true;

            // Clear messages
            lblError.Visible = false;
            lblSuccess.Visible = false;
        }
    }
}
