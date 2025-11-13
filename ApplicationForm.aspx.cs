using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using iTextSharp.text.log;

namespace JobApplicationForm
{
    public partial class ApplicationForm : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["JobAppConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int applicantId = SaveApplicant();
                SaveEducation(applicantId, "Secondary/Intermediate", "SSC");
                SaveEducation(applicantId, "University", "Uni");
                SaveEducation(applicantId, "PostDoc", "PostDoc");
                SaveStudentGuided(applicantId);
                SaveDistinctions(applicantId);
                SaveCourses(applicantId);
                SaveResearch(applicantId);
                SavePublications(applicantId, "HEC", "HEC Journal");
                SavePublications(applicantId, "INT", "International Journal");
                SavePublications(applicantId, "Book", "Books");
                SavePublications(applicantId, "Pro", "Proceedings");
                SavePublications(applicantId, "Manual", "Manuals");
                SavePublications(applicantId, "Mono", "Monographs");
                SavePublications(applicantId, "bookisbn", "Book Chapters");
                SaveProjects(applicantId, "propi", "Project PI");
                SaveProjects(applicantId, "procopi", "Project Co-PI");
                SaveMembership(applicantId);
                SaveLanguages(applicantId);
                SaveEmployment(applicantId);
                SaveCountriesVisited(applicantId);
                SaveDeclarations(applicantId);
                SaveBankSlip(applicantId);

                Session["ApplicantID"] = applicantId;
                Response.Redirect("ApplicationPreview.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Error: " + ex.Message;
                lblMessage.CssClass = "text-danger fw-bold";
            }

        }


        //protected void BtnDownloadPdf_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int applicantId = SaveApplicant();
        //        SaveEducation(applicantId, "Secondary/Intermediate", "SSC");
        //        SaveEducation(applicantId, "University", "Uni");
        //        SaveEducation(applicantId, "PostDoc", "PostDoc");
        //        SaveResearch(applicantId);
        //        SaveEmployment(applicantId);
        //        SaveDeclarations(applicantId);
        //        SaveBankSlip(applicantId);

        //        // ✅ Save ID in session for preview
        //        Session["ApplicantID"] = applicantId;
        //        Response.Redirect("ApplicationPreview.aspx");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "❌ Error: " + ex.Message;
        //        lblMessage.CssClass = "text-danger fw-bold";
        //    }
        //}

        // ------------------ APPLICANT ------------------
        private int SaveApplicant()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Applicants
        (UserID, PostName, Department, Campus, FullName, CNIC, Sex, DOB, FatherName, 
         PresentAddress, PermanentAddress, TelResident, Mobile, WhatsApp, Email, 
         Quota, Domicile, NationalitySelf, NationalitySpouse)
        OUTPUT INSERTED.ApplicantID
        VALUES (@UserID, @PostName, @Department, @Campus, @FullName, @CNIC, @Sex, @DOB, @FatherName, 
                @PresentAddress, @PermanentAddress, @TelResident, @Mobile, @WhatsApp, @Email, 
                @Quota, @Domicile, @NationalitySelf, @NationalitySpouse)", conn))
            {
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]); // ✅ link applicant with logged-in user
                cmd.Parameters.AddWithValue("@PostName", ddlPostName.SelectedValue);
                cmd.Parameters.AddWithValue("@Department", txtDepartment?.Text ?? "");
                cmd.Parameters.AddWithValue("@Campus", ddlCampus.SelectedValue);
                cmd.Parameters.AddWithValue("@FullName", txtFullName?.Text ?? "");
                cmd.Parameters.AddWithValue("@CNIC", txtCNIC?.Text ?? "");
                cmd.Parameters.AddWithValue("@Sex", rblSex.SelectedValue);
                cmd.Parameters.AddWithValue("@DOB", DateTime.TryParse(txtDOB.Text, out DateTime dob) ? (object)dob : DBNull.Value);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName?.Text ?? "");
                cmd.Parameters.AddWithValue("@PresentAddress", txtPresentAddress?.Text ?? "");
                cmd.Parameters.AddWithValue("@PermanentAddress", txtPermanentAddress?.Text ?? "");
                cmd.Parameters.AddWithValue("@TelResident", txtTelResident?.Text ?? "");
                cmd.Parameters.AddWithValue("@Mobile", txtMobile?.Text ?? "");
                cmd.Parameters.AddWithValue("@WhatsApp", txtWhatsApp?.Text ?? "");
                cmd.Parameters.AddWithValue("@Email", txtEmail?.Text ?? "");
                cmd.Parameters.AddWithValue("@Quota", txtQuota?.Text ?? "");
                cmd.Parameters.AddWithValue("@Domicile", txtDomicile?.Text ?? "");
                cmd.Parameters.AddWithValue("@NationalitySelf", txtNationality?.Text ?? "");
                cmd.Parameters.AddWithValue("@NationalitySpouse", txtSpouse?.Text ?? "");

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        // ------------------ EDUCATION ------------------
        private void SaveEducation(int applicantId, string level, string prefix)
        {
            string[] certs = Request.Form.GetValues(prefix + "_Cert[]");
            if (certs == null) return; // no entries

            string[] institutes = Request.Form.GetValues(prefix + "_Institute[]");
            string[] regNos = Request.Form.GetValues(prefix + "_RegNo[]");
            string[] years = Request.Form.GetValues(prefix + "_Years[]");
            string[] divisions = Request.Form.GetValues(prefix + "_Division[]");
            string[] marksObt = Request.Form.GetValues(prefix + "_MarksObt[]");
            string[] totalMarks = Request.Form.GetValues(prefix + "_TotalMarks[]");
            string[] subjects = Request.Form.GetValues(prefix + "_Subject[]");

            for (int i = 0; i < certs.Length; i++)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Education (ApplicantID, Level, Certificate, Institute, RegistrationNo, Years, Division, MarksObt, TotalMarks, Subject)
                    VALUES (@ApplicantID, @Level, @Certificate, @Institute, @RegNo, @Years, @Division, @MarksObt, @TotalMarks, @Subject)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Level", level);
                    cmd.Parameters.AddWithValue("@Certificate", certs[i]);
                    cmd.Parameters.AddWithValue("@Institute", institutes?[i] ?? "");
                    cmd.Parameters.AddWithValue("@RegNo", regNos?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Years", years?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Division", divisions?[i] ?? "");
                    cmd.Parameters.AddWithValue("@MarksObt", marksObt?[i] ?? "");
                    cmd.Parameters.AddWithValue("@TotalMarks", totalMarks?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Subject", subjects?[i] ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ------------------ Student Guide ------------------
        private void SaveStudentGuided(int applicantId)
        {
            string[] levels = Request.Form.GetValues("StudentGuide_Level[]");
            if (levels == null) return;

            string[] msc = Request.Form.GetValues("StudentGuide_MSC[]");  
            string[] mphil = Request.Form.GetValues("StudentGuide_MPhill[]");
            string[] phd = Request.Form.GetValues("StudentGuide_PhD[]");

            for (int i = 0; i < levels.Length; i++)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO StudentsGuided (ApplicantID, Level, MSC, Mphill, Phd)
            VALUES (@ApplicantID, @Level, @MSC, @Mphill, @Phd)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Level", levels?[i] ?? "");
                    cmd.Parameters.AddWithValue("@MSC", msc?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Mphill", mphil?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Phd", phd?[i] ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ------------------ Research ------------------
        private void SaveResearch(int applicantId)
        {
            string[] titles = Request.Form.GetValues("Research_Title[]");
            if (titles == null) return;

            string[] fromDates = Request.Form.GetValues("Research_From[]");
            string[] toDates = Request.Form.GetValues("Research_To[]");
            string[] professors = Request.Form.GetValues("Research_Professor[]");
            string[] institutions = Request.Form.GetValues("Research_Institution[]");

            for (int i = 0; i < titles.Length; i++)
            {
                string period = "";
                if (fromDates != null && toDates != null)
                {
                    period = (fromDates[i] ?? "") + " to " + (toDates[i] ?? "");
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Research (ApplicantID, Title, Period, Professor, Institution)
            VALUES (@ApplicantID, @Title, @Period, @Professor, @Institution)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Title", titles[i] ?? "");
                    cmd.Parameters.AddWithValue("@Period", period);
                    cmd.Parameters.AddWithValue("@Professor", professors?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Institution", institutions?[i] ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ------------------ Distinctions ------------------
        private void SaveDistinctions(int applicantId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Distinctions 
            (ApplicantID, Medal, Professional, Sports, Extra)
            VALUES (@ApplicantID, @Medal, @Professional, @Sports, @Extra)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Medal", Request.Form["Medal"]  ?? "");
                    cmd.Parameters.AddWithValue("@Professional", Request.Form["Society"] ?? "");
                    cmd.Parameters.AddWithValue("@Sports", Request.Form["Sports"] ?? "");
                    cmd.Parameters.AddWithValue("@Extra", Request.Form["Extra"] ?? "");
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ------------------ Course Taught ------------------
        private void SaveCourses(int applicantId)
        {
            string[] title = Request.Form.GetValues("Course_Title[]");
            if (title == null) return;

            string[] year = Request.Form.GetValues("Course_Year[]");   // ✅ rename keys without dots
            string[] independent = Request.Form.GetValues("Course_Independent[]");
            string[] joint = Request.Form.GetValues("Course_Joint[]");

            for (int i = 0; i < title.Length; i++)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO CoursesTaught (ApplicantID, CourseTitle, Year, Independent, Joint)
            VALUES (@ApplicantID, @CourseTitle, @Year, @Independent, @Joint)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@CourseTitle", title?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Year", year?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Independent", independent?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Joint", joint?[i] ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



        // ------------------ Publications------------------
       

        private void SavePublications(int applicantId, string prefix, string category)
        {
            string[] authors = Request.Form.GetValues(prefix + "_Authors[]");
            if (authors == null) return;

            string[] years = Request.Form.GetValues(prefix + "_Year[]");
            string[] titles = Request.Form.GetValues(prefix + "_Title[]");
            string[] journals = Request.Form.GetValues(prefix + "_Name[]");  // Journals only
            string[] issns = Request.Form.GetValues(prefix + "_ISSN[]");
            string[] impacts = Request.Form.GetValues(prefix + "_Imp[]");
            string[] isbns = Request.Form.GetValues(prefix + "_Issbn[]");
            string[] publishers = Request.Form.GetValues(prefix + "_Publisher[]");
            string[] projecttitle = Request.Form.GetValues(prefix + "_ProjectTitle[]");
            string[] funds = Request.Form.GetValues(prefix + "_Funding[]");
            string[] amounts = Request.Form.GetValues(prefix + "_Amount[]");
            string[] durations = Request.Form.GetValues(prefix + "_Duration[]");
            string[] roles = Request.Form.GetValues(prefix + "_PI[]");   // PI / Co-PI
            string[] statuses = Request.Form.GetValues(prefix + "_Status[]");

            for (int i = 0; i < authors.Length; i++)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Publications 
            (ApplicantID, Category, Authors, Year, Title, Journal, ISSN, ImpactFactor, ISBN, Publisher, ProjectTitle, FundingAgency, Amount, Duration, Role, PresentStatus)
            VALUES (@ApplicantID, @Category, @Authors, @Year, @Title, @Journal, @ISSN, @Impact, @ISBN, @Publisher, @ProjectTitle, @Funding, @Amount, @Duration, @Role, @Status)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Category", category);

                    cmd.Parameters.AddWithValue("@Authors", authors?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Year", years?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Title", titles?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Journal", journals != null && i < journals.Length ? journals[i] : "");
                    cmd.Parameters.AddWithValue("@ISSN", issns != null && i < issns.Length ? issns[i] : "");
                    cmd.Parameters.AddWithValue("@Impact", impacts != null && i < impacts.Length ? impacts[i] : "");
                    cmd.Parameters.AddWithValue("@ISBN", isbns != null && i < isbns.Length ? isbns[i] : "");
                    cmd.Parameters.AddWithValue("@Publisher", publishers != null && i < publishers.Length ? publishers[i] : "");
                    cmd.Parameters.AddWithValue("@ProjectTitle", projecttitle != null && i < projecttitle.Length ? projecttitle[i] : "");
                    cmd.Parameters.AddWithValue("@Funding", funds != null && i < funds.Length ? funds[i] : "");
                    cmd.Parameters.AddWithValue("@Amount", amounts != null && i < amounts.Length ? amounts[i] : "");
                    cmd.Parameters.AddWithValue("@Duration", durations != null && i < durations.Length ? durations[i] : "");
                    cmd.Parameters.AddWithValue("@Role", roles != null && i < roles.Length ? roles[i] : "");
                    cmd.Parameters.AddWithValue("@Status", statuses != null && i < statuses.Length ? statuses[i] : "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ------------------ Projects ------------------
        private void SaveProjects(int applicantId, string prefix, string category)
        {
            string[] projecttitle = Request.Form.GetValues(prefix + "_ProjectTitle[]");
            if (projecttitle == null) return;

            
            
            string[] funds = Request.Form.GetValues(prefix + "_Funding[]");
            string[] amounts = Request.Form.GetValues(prefix + "_Amount[]");
            string[] durations = Request.Form.GetValues(prefix + "_Duration[]");
            string[] roles = Request.Form.GetValues(prefix + "_PI[]");   // PI / Co-PI
            string[] statuses = Request.Form.GetValues(prefix + "_Status[]");

            for (int i = 0; i < projecttitle.Length; i++)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Projects
            (ApplicantID, Category, ProjectTitle,FundingAgency, Amount, Duration, Role, PresentStatus)
            VALUES (@ApplicantID, @Category, @ProjectTitle, @Funding, @Amount, @Duration, @Role, @PresentStatus)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Category", category);

                    
                    cmd.Parameters.AddWithValue("@ProjectTitle", projecttitle != null && i < projecttitle.Length ? projecttitle[i] : "");
                    cmd.Parameters.AddWithValue("@Funding", funds != null && i < funds.Length ? funds[i] : "");
                    cmd.Parameters.AddWithValue("@Amount", amounts != null && i < amounts.Length ? amounts[i] : "");
                    cmd.Parameters.AddWithValue("@Duration", durations != null && i < durations.Length ? durations[i] : "");
                    cmd.Parameters.AddWithValue("@Role", roles != null && i < roles.Length ? roles[i] : "");
                    cmd.Parameters.AddWithValue("@PresentStatus", statuses != null && i < statuses.Length ? statuses[i] : "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ------------------ Membership ------------------
        private void SaveMembership(int applicantId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Membership
            (ApplicantID, Name, Nature)
            VALUES (@ApplicantID, @Name, @Nature)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    
                    cmd.Parameters.AddWithValue("@Name", Request.Form["Name"] ?? "");
                    cmd.Parameters.AddWithValue("@Nature", Request.Form["Nature"] ?? "");
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ------------------ Languages ------------------
        private void SaveLanguages(int applicantId)
        {
            string[] titles = Request.Form.GetValues("Lang_Title[]");
            if (titles == null) return;

            string[] reading = Request.Form.GetValues("Lang_Reading[]");
            string[] writing = Request.Form.GetValues("Lang_Writing[]");
            string[] spoken = Request.Form.GetValues("Lang_Spoken[]");
            

            for (int i = 0; i < titles.Length; i++)
            {
               
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Languages (ApplicantID, Language, Reading, Writing, Spoken)
            VALUES (@ApplicantID, @Language, @Reading, @Writing, @Spoken)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@Language", titles[i] ?? "");
                    cmd.Parameters.AddWithValue("@Reading", reading[i] ?? "");
                    cmd.Parameters.AddWithValue("@Writing", writing?[i] ?? "");
                    cmd.Parameters.AddWithValue("@Spoken", spoken?[i] ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ------------------ EMPLOYMENT ------------------
        private void SaveEmployment(int applicantId)
        {
            string[] posts = Request.Form.GetValues("Emp_Post[]");
            if (posts == null) return;

            string[] wheres = Request.Form.GetValues("Emp_Where[]");
            string[] scales = Request.Form.GetValues("Emp_Scale[]");
            string[] lastPays = Request.Form.GetValues("Emp_LastPay[]");
            string[] fromDates = Request.Form.GetValues("Emp_From[]");
            string[] toDates = Request.Form.GetValues("Emp_To[]");
            string[] years = Request.Form.GetValues("Emp_Years[]");
            string[] months = Request.Form.GetValues("Emp_Months[]");
            string[] days = Request.Form.GetValues("Emp_Days[]");
            string[] leavings = Request.Form.GetValues("Emp_Leaving[]");
            string[] jobDescs = Request.Form.GetValues("Emp_JobDesc[]");

            for (int i = 0; i < posts.Length; i++)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO EmploymentHistory
                    (ApplicantID, PostHeld, WhereEmployed, ScaleOfPay, LastPay, DurationFrom, DurationTo, TotalYears, TotalMonths, TotalDays, CauseOfLeaving, JobDescription)
                    VALUES (@ApplicantID, @PostHeld, @WhereEmployed, @ScaleOfPay, @LastPay, @DurationFrom, @DurationTo, @TotalYears, @TotalMonths, @TotalDays, @CauseOfLeaving, @JobDescription)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@PostHeld", posts[i]);
                    cmd.Parameters.AddWithValue("@WhereEmployed", wheres?[i] ?? "");
                    cmd.Parameters.AddWithValue("@ScaleOfPay", scales?[i] ?? "");
                    cmd.Parameters.AddWithValue("@LastPay", lastPays?[i] ?? "");
                    cmd.Parameters.AddWithValue("@DurationFrom", fromDates?[i] ?? "");
                    cmd.Parameters.AddWithValue("@DurationTo", toDates?[i] ?? "");
                    cmd.Parameters.AddWithValue("@TotalYears", years?[i] ?? "0");
                    cmd.Parameters.AddWithValue("@TotalMonths", months?[i] ?? "0");
                    cmd.Parameters.AddWithValue("@TotalDays", days?[i] ?? "0");
                    cmd.Parameters.AddWithValue("@CauseOfLeaving", leavings?[i] ?? "");
                    cmd.Parameters.AddWithValue("@JobDescription", jobDescs?[i] ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }




        // ------------------ DECLARATIONS ------------------
        private void SaveDeclarations(int applicantId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Declarations 
            (ApplicantID, HasAllQualifications, MinPayAcceptable, PhysicalDisability, Liability, 
             EmployerPermission, EmployerNameDesignation, JoiningTime, AttachedDocuments, CertifyStatement)
            VALUES (@ApplicantID, @HasAllQualifications, @MinPayAcceptable, @PhysicalDisability, @Liability, 
             @EmployerPermission, @EmployerNameDesignation, @JoiningTime, @AttachedDocuments, @CertifyStatement)", conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                    cmd.Parameters.AddWithValue("@HasAllQualifications", Request.Form["HasAllQualifications"] ?? "0");
                    cmd.Parameters.AddWithValue("@MinPayAcceptable", Request.Form["MinPayAcceptable"] ?? "");
                    cmd.Parameters.AddWithValue("@PhysicalDisability", Request.Form["PhysicalDisabilityYN"] ?? "No");
                    cmd.Parameters.AddWithValue("@Liability", Request.Form["Liability"] ?? "");
                    cmd.Parameters.AddWithValue("@EmployerPermission", Request.Form["EmployerPermission"] ?? "0");
                    cmd.Parameters.AddWithValue("@EmployerNameDesignation", Request.Form["EmployerNameDesignation"] ?? "");
                    cmd.Parameters.AddWithValue("@JoiningTime", Request.Form["JoiningTime"] ?? "");
                    cmd.Parameters.AddWithValue("@AttachedDocuments", Request.Form["AttachedDocuments"] ?? "");
                    cmd.Parameters.AddWithValue("@CertifyStatement", Request.Form["CertifyStatement"] != null ? "1" : "0");

                    cmd.ExecuteNonQuery();
                }
            }
        }



        // ------------------ BANK SLIP ------------------
        private void SaveBankSlip(int applicantId)
        {
            var files = Request.Files;
            string[] amounts = Request.Form.GetValues("Bank_Amount[]");
            string[] slipNos = Request.Form.GetValues("Bank_SlipNo[]");
            string[] dates = Request.Form.GetValues("Bank_Date[]");
            string[] bankNames = Request.Form.GetValues("Bank_BankName[]");
           // string[] branches = Request.Form.GetValues("Bank_Branch[]"); // may not exist
           
            HttpFileCollection uploadedFiles = Request.Files;

            if (amounts == null) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                for (int i = 0; i < amounts.Length; i++)
                {
                    string fileName = null;
                    if (uploadedFiles.Count > i && uploadedFiles[i] != null && uploadedFiles[i].ContentLength > 0)
                    {
                        fileName = Path.GetFileName(uploadedFiles[i].FileName);
                        string savePath = Server.MapPath("~/Uploads/" + fileName);
                        uploadedFiles[i].SaveAs(savePath);
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO BankSlip (ApplicantID, Amount, SlipNo, SlipDate, BankName, FilePath) 
                VALUES (@ApplicantID, @Amount, @SlipNo, @SlipDate, @BankName, @FilePath)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                        cmd.Parameters.AddWithValue("@Amount", amounts[i] ?? "");
                        cmd.Parameters.AddWithValue("@SlipNo", slipNos[i] ?? "");
                        cmd.Parameters.AddWithValue("@SlipDate", dates[i] ?? "");
                        cmd.Parameters.AddWithValue("@BankName", bankNames[i] ?? "");
                        cmd.Parameters.AddWithValue("@FilePath", (object)fileName ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //Country Visited
        private void SaveCountriesVisited(int applicantId)
        {
            string[] countries = Request.Form.GetValues("Visit_Country[]");
            string[] fromDates = Request.Form.GetValues("Visit_From[]");
            string[] toDates = Request.Form.GetValues("Visit_To[]");
            string[] purposes = Request.Form.GetValues("Visit_Purpose[]");

            if (countries == null) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                for (int i = 0; i < countries.Length; i++)
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO CountriesVisited (ApplicantID, Country, VisitFrom, VisitTo, Purpose)
                VALUES (@ApplicantID, @Country, @VisitFrom, @VisitTo, @Purpose)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                        cmd.Parameters.AddWithValue("@Country", (object)countries[i] ?? DBNull.Value);

                        if (DateTime.TryParse(fromDates?[i], out DateTime fDate))
                            cmd.Parameters.AddWithValue("@VisitFrom", fDate);
                        else
                            cmd.Parameters.AddWithValue("@VisitFrom", DBNull.Value);

                        if (DateTime.TryParse(toDates?[i], out DateTime tDate))
                            cmd.Parameters.AddWithValue("@VisitTo", tDate);
                        else
                            cmd.Parameters.AddWithValue("@VisitTo", DBNull.Value);

                        cmd.Parameters.AddWithValue("@Purpose", (object)purposes?[i] ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
