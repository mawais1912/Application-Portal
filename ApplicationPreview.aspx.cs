using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace JobApplicationForm
{
    public partial class ApplicationPreview : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["JobAppConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ApplicantID"] == null)
                {
                    Response.Redirect("ApplicationForm.aspx");
                    return;
                }

                if (Session["InfoMessage"] != null)
                {
                    lblMessage.Text = Session["InfoMessage"].ToString();
                    pnlMessage.Visible = true;
                    Session["InfoMessage"] = null; // clear after showing once
                }

                int applicantId = Convert.ToInt32(Session["ApplicantID"]);
                LoadPreview(applicantId);
            }
        }

        private void LoadPreview(int applicantId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    litApplicantInfo.Text = GetApplicantInfo(conn, applicantId);
                    litSecondary.Text = GenerateEducationTable(conn, applicantId, "Secondary/Intermediate");
                    litUniversity.Text = GenerateEducationTable(conn, applicantId, "University");
                    litPostDoc.Text = GenerateEducationTable(conn, applicantId, "PostDoc");
                    hecpub.Text = GeneratePublicationTable(conn, applicantId, "HEC Journal");
                    litinter.Text = GeneratePublicationTable(conn, applicantId, "International Journal");
                    litbook.Text = GeneratePublicationTable(conn, applicantId, "Books");
                    proceeding.Text = GeneratePublicationTable(conn, applicantId, "Proceedings");
                    manual.Text = GeneratePublicationTable(conn, applicantId, "Manuals");
                    mono.Text = GeneratePublicationTable(conn, applicantId, "Monographs");
                    isbn.Text = GeneratePublicationTable(conn, applicantId, "Book Chapters");
                    pi.Text = GenerateProjectTable(conn, applicantId, "Project PI");
                    copi.Text = GenerateProjectTable(conn, applicantId, "Project Co-PI");
                    litStudentGuide.Text = GetStudentGuideTable(conn,applicantId);
                    litDistinctions.Text= GetDistinctionsTable(conn, applicantId);
                    litCourses.Text = GetCourses(conn, applicantId);
                    litResearch.Text = GetResearchTable(conn, applicantId);
                    litlanguage.Text = GetLanguage(conn, applicantId);
                    litmembership.Text = GetMembership(conn, applicantId);
                    litEmployment.Text = GetEmploymentTable(conn, applicantId);
                    litCountries.Text = GetCountriesTable(conn, applicantId);
                    litDeclarations.Text = GetDeclarations(conn, applicantId);
                    litBankSlip.Text = GetBankSlipTable(conn, applicantId);
                }
            }
            catch (Exception ex)
            {
                litApplicantInfo.Text = $"<div class='alert alert-danger'>❌ Error loading preview: {HttpUtility.HtmlEncode(ex.Message)}</div>";
            }
        }

        private string H(object o) => HttpUtility.HtmlEncode(Convert.ToString(o) ?? "");
        private string SafeGet(SqlDataReader r, string col) => r.IsDBNull(r.GetOrdinal(col)) ? "" : H(r[col]);
        private string SafeDate(SqlDataReader r, string col)
        {
            int idx = r.GetOrdinal(col);
            if (r.IsDBNull(idx)) return "";
            if (DateTime.TryParse(Convert.ToString(r.GetValue(idx)), out DateTime dt))
                return dt.ToString("yyyy-MM-dd");
            return H(r.GetValue(idx));
        }

        // ---------------- Applicant Info ----------------
        private string GetApplicantInfo(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applicants WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return $@"
                <div class='table-responsive'>
                <table class='table table-bordered table-striped'>
                    <tr><th>Appication ID</th><td>{SafeGet(dr, "ApplicantID")}</td></tr>
                    <tr><th>Full Name</th><td>{SafeGet(dr, "FullName")}</td></tr>
                    <tr><th>CNIC</th><td>{SafeGet(dr, "CNIC")}</td></tr>
                    <tr><th>Sex</th><td>{SafeGet(dr, "Sex")}</td></tr>
                    <tr><th>Date of Birth</th><td>{SafeGet(dr, "DOB")}</td></tr>
                    <tr><th>Email</th><td>{SafeGet(dr, "Email")}</td></tr>
                    <tr><th>Mobile</th><td>{SafeGet(dr, "Mobile")}</td></tr>
                    <tr><th>WhatsApp</th><td>{SafeGet(dr, "WhatsApp")}</td></tr>
                    <tr><th>Telephone (Res.)</th><td>{SafeGet(dr, "TelResident")}</td></tr>
                    <tr><th>Post Applied</th><td>{SafeGet(dr, "PostName")}</td></tr>
                    <tr><th>Department</th><td>{SafeGet(dr, "Department")}</td></tr>
                    <tr><th>Campus</th><td>{SafeGet(dr, "Campus")}</td></tr>
                    <tr><th>Father's Name</th><td>{SafeGet(dr, "FatherName")}</td></tr>
                    <tr><th>Present Address</th><td>{SafeGet(dr, "PresentAddress")}</td></tr>
                    <tr><th>Permanent Address</th><td>{SafeGet(dr, "PermanentAddress")}</td></tr>
                    <tr><th>Quota</th><td>{SafeGet(dr, "Quota")}</td></tr>
                </table>
                </div>";
                    }
                }
            }
            return "<p class='text-muted'>No applicant info found.</p>";
        }

        // ---------------- Education ----------------
        private string GenerateEducationTable(SqlConnection conn, int applicantId, string level)
        {
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Certificate, Institute, RegistrationNo, Years, Division, MarksObt, TotalMarks, Subject " +
                "FROM Education WHERE ApplicantID=@ID AND Level=@Level", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                cmd.Parameters.AddWithValue("@Level", level);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-bordered table-striped'>
                    <thead><tr>
                    <th>Certificate/Degree</th><th>Institute</th><th>Reg. No</th><th>Years</th>
                    <th>Division/CGPA</th><th>Marks Obtained</th><th>Total Marks</th><th>Subject</th>
                    </tr></thead><tbody>";

                    while (r.Read())
                    {
                        html += "<tr>" +
                                $"<td>{SafeGet(r, "Certificate")}</td>" +
                                $"<td>{SafeGet(r, "Institute")}</td>" +
                                $"<td>{SafeGet(r, "RegistrationNo")}</td>" +
                                $"<td>{SafeGet(r, "Years")}</td>" +
                                $"<td>{SafeGet(r, "Division")}</td>" +
                                $"<td>{SafeGet(r, "MarksObt")}</td>" +
                                $"<td>{SafeGet(r, "TotalMarks")}</td>" +
                                $"<td>{SafeGet(r, "Subject")}</td>" +
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }

        // ---------------- Research ----------------
        private string GetResearchTable(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Title, Period, Professor, Institution FROM Research WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No research records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-striped'>
                <thead><tr><th>Title</th><th>Period</th><th>Professor</th><th>Institution</th></tr></thead><tbody>";
                    while (r.Read())
                    {
                        html += "<tr>" +
                                $"<td>{H(r["Title"])}</td>" +
                                $"<td>{H(r["Period"])}</td>" +
                                $"<td>{H(r["Professor"])}</td>" +
                                $"<td>{H(r["Institution"])}</td>" +
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }

        // ---------------- Student Guide ----------------
        private string GetStudentGuideTable(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Level, MSC, Mphill, Phd FROM StudentsGuided WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No research records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-striped'>
                <thead><tr><th>Level</th><th>M.Sc.equivalent</th><th>M.Sc.(Hons.)/M.Phil</th><th>PHD</th></tr></thead><tbody>";
                    while (r.Read())
                    {
                        html += "<tr>" +
                                $"<td>{H(r["Level"])}</td>" +
                                $"<td>{H(r["MSC"])}</td>" +
                                $"<td>{H(r["Mphill"])}</td>" +
                                $"<td>{H(r["Phd"])}</td>" +
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }
        // ---------------- Distinctions ----------------
        private string GetDistinctionsTable(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Medal, Professional, Sports, Extra
          FROM Distinctions WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return "<p class='text-muted'>No Distinctions found.</p>";

                   

                    return $@"
                <div class='table-responsive'>
                <table class='table table-bordered'>
                    <tr><th>Position/Medal/Award (Academic/Civil)</th><td>{H(r["Medal"])}</td></tr>
                    <tr><th>Professional awards (Govt./Institution/Society)</th><td>{H(r["Professional"])}</td></tr>
                    <tr><th>Sports (Intervarsity/National/International)</th><td>{H(r["Sports"])}</td></tr>
                    <tr><th>Extra Curricular</th><td>{H(r["Extra"])}</td></tr>
                    
                </table>
                </div>";
                }
            }
        }


        // ----------------Courses Taught ----------------
        //private string GetCourses(SqlConnection conn, int applicantId)
        //{
        //    using (SqlCommand cmd = new SqlCommand(
        //        @"SELECT CourseTitle, Year, Independent, Joint
        //  FROM CoursesTaught WHERE ApplicantID=@ID", conn))
        //    {
        //        cmd.Parameters.AddWithValue("@ID", applicantId);
        //        using (SqlDataReader r = cmd.ExecuteReader())
        //        {
        //            if (!r.Read()) return "<p class='text-muted'>No Courses Taught found.</p>";




        //            string html = @"<div class='table-responsive'><table class='table table-striped'>
        //        <thead><tr><th>Course No/ Title</th><th>Years</th><th>Independent</th><th>Joint</th></tr></thead><tbody>";
        //            while (r.Read())
        //            {
        //                html += "<tr>" +
        //                        $"<td>{H(r["CourseTitle"])}</td>" +
        //                        $"<td>{H(r["Year"])}</td>" +
        //                        $"<td>{H(r["Independent"])}</td>" +
        //                        $"<td>{H(r["Joint"])}</td>" +
        //                        "</tr>";
        //            }
        //            html += "</tbody></table></div>";
        //            return html;


        //        }
        //    }
        //}


        // ---------------- Publications ----------------
        private string GetCourses(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT CourseTitle, Year, Independent, Joint
          FROM CoursesTaught WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows)
                        return "<p class='text-muted'>No Courses Taught found.</p>";

                    string html = @"<div class='table-responsive'>
                            <table class='table table-striped'>
                            <thead><tr>
                                <th>Course No/ Title</th>
                                <th>Years</th>
                                <th>Independent</th>
                                <th>Joint</th>
                            </tr></thead><tbody>";

                    while (r.Read())   // ✅ will include the first row now
                    {
                        html += "<tr>" +
                                $"<td>{H(r["CourseTitle"])}</td>" +
                                $"<td>{H(r["Year"])}</td>" +
                                $"<td>{H(r["Independent"])}</td>" +
                                $"<td>{H(r["Joint"])}</td>" +
                                "</tr>";
                    }

                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }

        private string GeneratePublicationTable(SqlConnection conn, int applicantId, string category)
        {
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT Authors, Year, Title, Journal, ISSN, ImpactFactor, ISBN, Publisher, 
               FundingAgency, Amount, Duration, Role, PresentStatus
        FROM Publications 
        WHERE ApplicantID=@ID AND Category=@Category", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                cmd.Parameters.AddWithValue("@Category", category);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-bordered table-striped'>
                <thead><tr>
                <th>Authors</th><th>Year</th><th>Title</th><th>Journal</th><th>ISSN</th>
                <th>Impact Factor</th><th>ISBN</th><th>Publisher</th>
                </tr></thead><tbody>";

                    while (r.Read())
                    {
                        html += "<tr>" +
                                $"<td>{SafeGet(r, "Authors")}</td>" +
                                $"<td>{SafeGet(r, "Year")}</td>" +
                                $"<td>{SafeGet(r, "Title")}</td>" +
                                $"<td>{SafeGet(r, "Journal")}</td>" +
                                $"<td>{SafeGet(r, "ISSN")}</td>" +
                                $"<td>{SafeGet(r, "ImpactFactor")}</td>" +
                                $"<td>{SafeGet(r, "ISBN")}</td>" +
                                $"<td>{SafeGet(r, "Publisher")}</td>" +
                               //// $"<td>{SafeGet(r, "Project Title")}</td>" +
                               // $"<td>{SafeGet(r, "FundingAgency")}</td>" +
                               // $"<td>{SafeGet(r, "Amount")}</td>" +
                               // $"<td>{SafeGet(r, "Duration")}</td>" +
                               // $"<td>{SafeGet(r, "Role")}</td>" +
                               // $"<td>{SafeGet(r, "PresentStatus")}</td>" +
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }


        // ---------------- Projects ----------------
        private string GenerateProjectTable(SqlConnection conn, int applicantId, string category)
        {
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT ProjectTitle, 
               FundingAgency, Amount, Duration, Role, PresentStatus
        FROM Projects 
        WHERE ApplicantID=@ID AND Category=@Category", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                cmd.Parameters.AddWithValue("@Category", category);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-bordered table-striped'>
                <thead><tr>
                <th>Project Title</th><th>Funding Agency</th><th>Amount</th><th>Duration</th>
                <th>Role</th><th>PresentStatus</th>
                </tr></thead><tbody>";

                    while (r.Read())
                    {
                        html += "<tr>" +
                               
                                $"<td>{SafeGet(r, "ProjectTitle")}</td>" +
                                $"<td>{SafeGet(r, "FundingAgency")}</td>" +
                                $"<td>{SafeGet(r, "Amount")}</td>" +
                                $"<td>{SafeGet(r, "Duration")}</td>" +
                                $"<td>{SafeGet(r, "Role")}</td>" +
                                $"<td>{SafeGet(r, "PresentStatus")}</td>" +
                                
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }


        // ---------------- Memebership ----------------
        private string GetMembership(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Name, Nature
          FROM Membership WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return "<p class='text-muted'>No declarations found.</p>";

                  //  string yesNo(object o) => (Convert.ToString(o) == "1") ? "Yes" : "No";

                    return $@"
                <div class='table-responsive'>
                <table class='table table-bordered'>
                    
                    <tr><th>Name</th><td>{H(r["Name"])}</td></tr>
                    <tr><th>Nature</th><td>{H(r["Nature"])}</td></tr>
                   
                </table>
                </div>";
                }
            }
        }


        // ---------------- Foreign Language ----------------
        private string GetLanguage(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Language, Reading, Writing, Spoken
          FROM Languages WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return "<p class='text-muted'>No declarations found.</p>";

                    //  string yesNo(object o) => (Convert.ToString(o) == "1") ? "Yes" : "No";

                    return $@"
                <div class='table-responsive'>
                <table class='table table-bordered'>y7u
                    
                    <tr><th>Name</th><td>{H(r["Language"])}</td></tr>
                    <tr><th>Nature</th><td>{H(r["Reading"])}</td></tr>
                    <tr><th>Nature</th><td>{H(r["Writing"])}</td></tr>
                    <tr><th>Nature</th><td>{H(r["Spoken"])}</td></tr>
                   
                </table>
                </div>";
                }
            }
        }

        // ---------------- Employment ----------------
        private string GetEmploymentTable(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT PostHeld, WhereEmployed, ScaleOfPay, LastPay, DurationFrom, DurationTo, 
                         TotalYears, TotalMonths, TotalDays, CauseOfLeaving, JobDescription
                  FROM EmploymentHistory WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No employment records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-bordered table-striped'>
                    <thead><tr>
                    <th>Post Held</th><th>Where Employed</th><th>Scale of Pay</th><th>Last Pay</th>
                    <th>From</th><th>To</th><th>Y</th><th>M</th><th>D</th><th>Cause of Leaving</th><th>Job Description</th>
                    </tr></thead><tbody>";

                    while (r.Read())
                    {
                        html += "<tr>" +
                                $"<td>{SafeGet(r, "PostHeld")}</td>" +
                                $"<td>{SafeGet(r, "WhereEmployed")}</td>" +
                                $"<td>{SafeGet(r, "ScaleOfPay")}</td>" +
                                $"<td>{SafeGet(r, "LastPay")}</td>" +
                                $"<td>{SafeDate(r, "DurationFrom")}</td>" +
                                $"<td>{SafeDate(r, "DurationTo")}</td>" +
                                $"<td>{SafeGet(r, "TotalYears")}</td>" +
                                $"<td>{SafeGet(r, "TotalMonths")}</td>" +
                                $"<td>{SafeGet(r, "TotalDays")}</td>" +
                                $"<td>{SafeGet(r, "CauseOfLeaving")}</td>" +
                                $"<td>{SafeGet(r, "JobDescription")}</td>" +
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }

        // ---------------- Countries ----------------
        private string GetCountriesTable(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Country, VisitFrom, VisitTo, Purpose FROM CountriesVisited WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No country records found.</p>";

                    string html = @"<div class='table-responsive'><table class='table table-bordered'>
                    <thead><tr><th>Country</th><th>From</th><th>To</th><th>Purpose</th></tr></thead><tbody>";

                    while (r.Read())
                    {
                        html += "<tr>" +
                                $"<td>{SafeGet(r, "Country")}</td>" +
                                $"<td>{SafeDate(r, "VisitFrom")}</td>" +
                                $"<td>{SafeDate(r, "VisitTo")}</td>" +
                                $"<td>{SafeGet(r, "Purpose")}</td>" +
                                "</tr>";
                    }
                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }

        // ---------------- Declarations ----------------
        private string GetDeclarations(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT HasAllQualifications, MinPayAcceptable, PhysicalDisability, PhysicalDisabilityFile,
                 Liability, EmployerPermission, EmployerNameDesignation, JoiningTime, AttachedDocuments, CertifyStatement
          FROM Declarations WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return "<p class='text-muted'>No declarations found.</p>";

                    string yesNo(object o) => (Convert.ToString(o) == "1") ? "Yes" : "No";

                    return $@"
                <div class='table-responsive'>
                <table class='table table-bordered'>
                    <tr><th>All Qualifications Met?</th><td>{yesNo(r["HasAllQualifications"])}</td></tr>
                    <tr><th>Minimum Pay Acceptable</th><td>{H(r["MinPayAcceptable"])}</td></tr>
                    <tr><th>Physical Disability</th><td>{yesNo(r["PhysicalDisability"])}</td></tr>
                    <tr><th>Disability File</th><td>{H(r["PhysicalDisabilityFile"])}</td></tr>
                    <tr><th>Financial Liability</th><td>{H(r["Liability"])}</td></tr>
                    <tr><th>Employer Permission</th><td>{yesNo(r["EmployerPermission"])}</td></tr>
                    <tr><th>Confidential Record Authority</th><td>{H(r["EmployerNameDesignation"])}</td></tr>
                    <tr><th>Time to Join</th><td>{H(r["JoiningTime"])}</td></tr>
                    <tr><th>Attached Documents</th><td>{H(r["AttachedDocuments"])}</td></tr>
                    <tr><th>Certification Signed?</th><td>{yesNo(r["CertifyStatement"])}</td></tr>
                </table>
                </div>";
                }
            }
        }

        // ---------------- Bank Credit Slip ----------------
        private string GetBankSlipTable(SqlConnection conn, int applicantId)
        {
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Amount, SlipNo, SlipDate, BankName, FilePath FROM BankSlip WHERE ApplicantID=@ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", applicantId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows) return "<p class='text-muted'>No bank slip records found.</p>";

                    var html = @"<div class='table-responsive'><table class='table table-bordered'>
                        <thead>
                        <tr>
                            <th>Amount</th><th>Slip No</th><th>Date</th><th>Bank</th><th>File</th>
                        </tr>
                        </thead><tbody>";

                    while (r.Read())
                    {
                        string fileCell = "—";
                        if (!string.IsNullOrEmpty(Convert.ToString(r["FilePath"])))
                        {
                            fileCell = $"<a href='/Uploads/{H(r["FilePath"])}' target='_blank' class='btn btn-sm btn-outline-primary'>View File</a>";
                        }

                        html += "<tr>" +
                                $"<td>{H(r["Amount"])}</td>" +
                                $"<td>{H(r["SlipNo"])}</td>" +
                                $"<td>{H(r["SlipDate"])}</td>" +
                                $"<td>{H(r["BankName"])}</td>" +
                                
                                $"<td>{fileCell}</td>" +
                                "</tr>";
                    }

                    html += "</tbody></table></div>";
                    return html;
                }
            }
        }

        // ---------------- Generate PDF ----------------
        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            if (Session["ApplicantID"] == null)
            {
                Response.Write("<script>alert('Session expired. Please resubmit the form.');</script>");
                return;
            }

            int applicantId = Convert.ToInt32(Session["ApplicantID"]);

            try
            {
                using (Document doc = new Document(PageSize.A4, 36, 36, 54, 54))
                {
                    // ✅ Write PDF directly to Response.OutputStream
                    PdfWriter.GetInstance(doc, Response.OutputStream);

                    doc.Open();

                    // Fonts
                   // var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, BaseColor.WHITE);
                    var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
                    var normalFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);

                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100;
                    headerTable.SetWidths(new float[] { 1f, 3f });
                    headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

                    // Logo cell
                    string logoPath = Server.MapPath("~/Uploads/small_image1.png");
                    if (File.Exists(logoPath))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                        logo.ScaleToFit(60f, 60f);
                        PdfPCell logoCell = new PdfPCell(logo);
                        logoCell.Border = Rectangle.NO_BORDER;
                        logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        headerTable.AddCell(logoCell);
                    }
                    else
                    {
                        headerTable.AddCell(new PdfPCell(new Phrase("[Logo Missing]")) { Border = Rectangle.NO_BORDER });
                    }

                    // Text cell
                    Font uniFont = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
                    PdfPCell textCell = new PdfPCell(new Phrase("University of Agriculture Faisalabad", uniFont));
                    textCell.Border = Rectangle.NO_BORDER;
                    textCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    textCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    textCell.PaddingLeft = 10;
                    headerTable.AddCell(textCell);

                    doc.Add(headerTable);
                    doc.Add(new Paragraph("\n"));

                    // 🔹 Title Bar (already in your code)
                    PdfPTable titleTable = new PdfPTable(1);
                    titleTable.WidthPercentage = 100;
                    Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, BaseColor.WHITE);

                    PdfPCell titleCell = new PdfPCell(new Phrase("University Job Application Form", titleFont))
                    {
                        BackgroundColor = new BaseColor(33, 150, 243), // blue
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 12
                    };
                    titleTable.AddCell(titleCell);

                    doc.Add(titleTable);
                    doc.Add(new Paragraph("\n"));

                    // 🔹 Sections
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        void AddSection(string sectionTitle, string htmlContent)
                        {
                            doc.Add(new Paragraph(sectionTitle, headerFont));
                            doc.Add(new Paragraph("\n"));
                            PdfPTable tbl = ConvertHtmlToTable(htmlContent, normalFont);
                            doc.Add(tbl);
                            doc.Add(new Paragraph("\n"));
                        }

                        AddSection("Personal Information", GetApplicantInfo(conn, applicantId));
                        AddSection("Education - Secondary/Intermediate", GenerateEducationTable(conn, applicantId, "Secondary/Intermediate"));
                        AddSection("Education - University", GenerateEducationTable(conn, applicantId, "University"));
                        AddSection("Education - PostDoc", GenerateEducationTable(conn, applicantId, "PostDoc"));
                        AddSection("Publications - HEC Journals", GeneratePublicationTable(conn, applicantId, "HEC Journal"));
                        AddSection("Publications - International Journals", GeneratePublicationTable(conn, applicantId, "International Journal"));
                        AddSection("Publications - Books", GeneratePublicationTable(conn, applicantId, "Books"));
                        AddSection("Publications - Proceedings", GeneratePublicationTable(conn, applicantId, "Proceedings"));
                        AddSection("Publications - Manuals", GeneratePublicationTable(conn, applicantId, "Manuals"));
                        AddSection("Publications - Monographs", GeneratePublicationTable(conn, applicantId, "Monographs"));
                        AddSection("Publications - Book Chapters", GeneratePublicationTable(conn, applicantId, "Book Chapters"));

                        AddSection("Research Projects (PI)", GenerateProjectTable(conn, applicantId, "Project PI"));
                        AddSection("Research Projects (Co-PI)", GenerateProjectTable(conn, applicantId, "Project Co-PI"));

                        AddSection("Students Guided", GetStudentGuideTable(conn, applicantId));
                        AddSection("Distinctions", GetDistinctionsTable(conn, applicantId));
                        AddSection("Courses Taught", GetCourses(conn, applicantId));
                        AddSection("Foreign Languages", GetLanguage(conn, applicantId));
                        AddSection("Membership of Learned Societies", GetMembership(conn, applicantId));

                        AddSection("Research Work", GetResearchTable(conn, applicantId));
                        AddSection("Employment History", GetEmploymentTable(conn, applicantId));
                        AddSection("Countries Visited", GetCountriesTable(conn, applicantId));
                        AddSection("Bank Credit Slip", GetBankSlipTable(conn, applicantId));
                        AddSection("Declarations", GetDeclarations(conn, applicantId));
                    }

                    // 🔹 Signature
                    doc.Add(new Paragraph("\n\n"));
                    doc.Add(new Paragraph("Applicant Signature", headerFont));
                    doc.Add(new Paragraph("\n"));

                    string signaturePath = Server.MapPath("~/Uploads/Signature_" + applicantId + ".png");
                    if (File.Exists(signaturePath))
                    {
                        iTextSharp.text.Image signature = iTextSharp.text.Image.GetInstance(signaturePath);
                        signature.Alignment = Element.ALIGN_LEFT;
                        signature.ScaleToFit(150f, 60f);
                        doc.Add(signature);
                    }
                    else
                    {
                        PdfPTable signTable = new PdfPTable(1) { WidthPercentage = 50 };
                        PdfPCell signCell = new PdfPCell(new Phrase("(Signature not provided)", normalFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            PaddingTop = 20,
                            BorderWidthTop = 1,
                            BorderWidthLeft = 0,
                            BorderWidthRight = 0,
                            BorderWidthBottom = 0
                        };
                        signTable.AddCell(signCell);
                        doc.Add(signTable);
                    }

                    doc.Close();
                }

                // ✅ Send PDF headers AFTER generating doc
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Application_" + applicantId + ".pdf");
                Response.Flush();
                Response.End();

               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('❌ Error generating PDF: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }


        private PdfPTable ConvertHtmlToTable(string html, Font cellFont)
        {
            var rows = System.Text.RegularExpressions.Regex.Matches(html, "<tr.*?>(.*?)</tr>", System.Text.RegularExpressions.RegexOptions.Singleline);
            if (rows.Count == 0) return new PdfPTable(1);

            var firstRowCols = System.Text.RegularExpressions.Regex.Matches(rows[0].Value, "<t[dh].*?>(.*?)</t[dh]>", System.Text.RegularExpressions.RegexOptions.Singleline);
            PdfPTable pdfTable = new PdfPTable(firstRowCols.Count);
            pdfTable.WidthPercentage = 100;

            foreach (System.Text.RegularExpressions.Match row in rows)
            {
                var cols = System.Text.RegularExpressions.Regex.Matches(row.Value, "<t[dh].*?>(.*?)</t[dh]>", System.Text.RegularExpressions.RegexOptions.Singleline);
                foreach (System.Text.RegularExpressions.Match col in cols)
                {
                    string text = System.Text.RegularExpressions.Regex.Replace(col.Groups[1].Value, "<.*?>", "").Trim();
                    PdfPCell cell = new PdfPCell(new Phrase(text, cellFont));
                    cell.Padding = 5;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;

                    if (col.Value.Contains("<th"))
                    {
                        cell.BackgroundColor = new BaseColor(220, 220, 220);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Phrase.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
                    }

                    pdfTable.AddCell(cell);
                }
            }
            return pdfTable;
        }
    }
}
