<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationForm.aspx.cs" Inherits="JobApplicationForm.ApplicationForm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Job Application Form</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background:#eef2f7; font-family:"Segoe UI", Tahoma, Geneva, Verdana, sans-serif; }
        .form-container { max-width:1100px; margin:40px auto; background:#fff; padding:35px; border-radius:15px; box-shadow:0px 10px 30px rgba(0,0,0,0.1); }
        h2 { color:#0d6efd; font-weight:bold; }
        h5 { color:#495057; font-weight:600; }
        .accordion-button:not(.collapsed){ background:#0d6efd; color:#fff; }
        table th { background:#f8f9fa; text-align:center; }
        table td input, table td textarea { border-radius:6px; }
        .section-card { border:1px solid #dee2e6; border-radius:10px; padding:20px; background:#fdfdfd; box-shadow:0px 4px 12px rgba(0,0,0,0.05); }
    </style>
</head>
<body>
<form id="form1" runat="server" class="form-container needs-validation" enctype="multipart/form-data" novalidate>
    <h2 class="text-center mb-4">📋 Job Application Form</h2>

    <div class="accordion" id="applicationFormAccordion">

        <!-- ✅ Personal Information -->
        <div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#personalInfo">
                    👤 Personal Information
                </button>
            </h2>
            <div id="personalInfo" class="accordion-collapse collapse show" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">

                    <!-- Name of Post -->
                    <div class="mb-3">
                        <label class="form-label">1. Name of Post *</label>
                        <asp:DropDownList ID="ddlPostName" runat="server" CssClass="form-select" required>
                            <asp:ListItem Text="-- Select Post --" Value="" />
                            <asp:ListItem Text="Professor (BPS-21)" Value="Professor (BPS-21)" />
                            <asp:ListItem Text="Associate Professor (BPS-20)" Value="Associate Professor (BPS-20)" />
                            <asp:ListItem Text="Assistant Professor (BPS-19)" Value="Assistant Professor (BPS-19)" />
                        </asp:DropDownList>
                    </div>

                    <!-- Department -->
                    <div class="mb-3">
                        <label class="form-label">2. Department *</label>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" required />
                    </div>

                    <!-- Campus -->
                    <div class="mb-3">
                        <label class="form-label">3. Campus *</label>
                        <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-select" required>
                            <asp:ListItem Text="-- Select Campus --" Value="" />
                            <asp:ListItem Text="Main Campus" Value="Main Campus" />
                            <asp:ListItem Text="Depalpur Campus" Value="Depalpur Campus" />
                            <asp:ListItem Text="Burewala Campus" Value="Burewala Campus" />
                            <asp:ListItem Text="Toba Tek Singh Campus" Value="Toba Tek Singh Campus" />
                        </asp:DropDownList>
                    </div>

                    <!-- Other Personal Info -->
                    <div class="mb-3"><label class="form-label">4. Full Name *</label><asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" required /></div>
                    <div class="mb-3"><label class="form-label">5. CNIC *</label><asp:TextBox ID="txtCNIC" runat="server" CssClass="form-control" placeholder="xxxxx-xxxxxxx-x" required /></div>
                    <div class="mb-3"><label class="form-label">6. Sex *</label>
                        <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="form-check-inline" required>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="mb-3"><label class="form-label">7. Date of Birth *</label><asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-control" required /></div>
                    <div class="mb-3"><label class="form-label">8. Father’s Name *</label><asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" required /></div>
                    <div class="mb-3">
                        <label class="form-label">9. Present Address *</label><asp:TextBox ID="txtPresentAddress" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control mb-2" required />
                        <label class="form-label">Permanent Address *</label><asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control mb-2" required />
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-6"><label class="form-label">Telephone</label><asp:TextBox ID="txtTelResident" runat="server" CssClass="form-control" /></div>
                        <div class="col-md-6"><label class="form-label">Mobile *</label><asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="03XXXXXXXXX" required /></div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-6"><label class="form-label">WhatsApp</label><asp:TextBox ID="txtWhatsApp" runat="server" CssClass="form-control" placeholder="03XXXXXXXXX" /></div>
                        <div class="col-md-6"><label class="form-label">Email *</label><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" required /></div>
                    </div>
                    <div class="mb-3"><label class="form-label">10. Quota (if any)</label><asp:TextBox ID="txtQuota" runat="server" CssClass="form-control" /></div>
                    <div class="mb-3"><label class="form-label">Domicile</label><asp:TextBox ID="txtDomicile" runat="server" CssClass="form-control" /></div>
                    <div class="mb-3"><label class="form-label">Nationality (Self)</label><asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" /></div>
                    <div class="mb-3"><label class="form-label">Nationality (Spouse)</label><asp:TextBox ID="txtSpouse" runat="server" CssClass="form-control" /></div>
                </div>

            </div>
        </div>

        <!-- ✅ Education -->
        <div class="accordion-item">
            <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#educationSection">🎓 Educational Qualifications</button></h2>
            <div id="educationSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">
                    <!-- Secondary -->
                    <h5>Secondary & Intermediate</h5>
                    <table class="table table-bordered table-striped" id="tblSecondary">
                        <thead><tr><th>Certificate</th><th>Institute</th><th>Reg No.</th><th>Years</th><th>Division</th><th>%</th><th>Marks Obt</th><th>Total Marks</th><th>Subject</th></tr></thead>
                        <tbody><tr>
                            <td><input class="form-control" name="SSC_Cert[]" /></td> 
                            <td><input class="form-control" name="SSC_Institute[]" /></td>
                            <td><input class="form-control" name="SSC_RegNo[]" /></td>
                            <td><input class="form-control" name="SSC_Years[]" /></td>
                            <td><input class="form-control" name="SSC_Division[]" /></td>
                            <td><input class="form-control" name="SSC_Percentage[]" /></td>
                            <td><input class="form-control" name="SSC_MarksObt[]" /></td>
                            <td><input class="form-control" name="SSC_TotalMarks[]" /></td>
                            <td><input class="form-control" name="SSC_Subject[]" /></td>
                        </tr></tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblSecondary')">+ Add Row</button>

                    <!-- University -->
                    <h5 class="mt-4">University</h5>
                    <table class="table table-bordered table-striped" id="tblUniversity">
                        <thead><tr><th>Degree</th><th>Institute</th><th>Reg No.</th><th>Years</th><th>CGPA/Div</th><th>Marks Obt</th><th>Total Marks</th><th>Subject</th></tr></thead>
                        <tbody><tr>
                            <td><input class="form-control" name="Uni_Cert[]" /></td>
                            <td><input class="form-control" name="Uni_Institute[]" /></td>
                            <td><input class="form-control" name="Uni_RegNo[]" /></td>
                            <td><input class="form-control" name="Uni_Years[]" /></td>
                            <td><input class="form-control" name="Uni_Division[]" /></td>
                            <td><input class="form-control" name="Uni_MarksObt[]" /></td>
                            <td><input class="form-control" name="Uni_TotalMarks[]" /></td>
                            <td><input class="form-control" name="Uni_Subject[]" /></td>
                        </tr></tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblUniversity')">+ Add Row</button>

                    <!-- PostDoc -->
                    <h5 class="mt-4">Post-Doctorate</h5>
                    <table class="table table-bordered table-striped" id="tblPostDoc">
                        <thead><tr><th>Certificate</th><th>Institute</th><th>Years</th><th>CGPA/Div</th><th>Marks Obt</th><th>Total Marks</th><th>Specialization</th></tr></thead>
                        <tbody><tr>
                            <td><input class="form-control" name="PostDoc_Cert[]" /></td>
                            <td><input class="form-control" name="PostDoc_Institute[]" /></td>
                            <td><input class="form-control" name="PostDoc_Years[]" /></td>
                            <td><input class="form-control" name="PostDoc_Division[]" /></td>
                            <td><input class="form-control" name="PostDoc_MarksObt[]" /></td>
                            <td><input class="form-control" name="PostDoc_TotalMarks[]" /></td>
                            <td><input class="form-control" name="PostDoc_Subject[]" /></td>
                        </tr></tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblPostDoc')">+ Add Row</button>


                </div>
            </div>
        </div>


         <!-- ✅ Student Guided -->
        <div class="accordion-item">
    <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#studentguidedSection">🧑‍🎓 Number of Student Guided</button></h2>
    <div id="studentguidedSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
        <div class="accordion-body section-card">
            <table class="table table-bordered table-striped" id="tblStudentguided">
                <thead><tr><th>Level</th><th>M.Sc. or equivalent</th><th>M.Sc. (Hons.)/M.Phill</th><th>Ph.D</th><tr><th>Major Supervisor/committee Member </th></tr></thead>
                <tbody><tr>
                    <td><input class="form-control" name="StudentGuide_Level[]" /></td>
                    <td><input class="form-control" name="StudentGuide_MSC" /></td>
                    <td><input  class="form-control" name="StudentGuide_MPhill[]" /></td>
                    <td><input  class="form-control" name="StudentGuide_PhD[]" /></td>
                    
                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblStudentguided')">+ Add Row</button>
        </div>
    </div>
</div>

        <!--Distictions-->
        <div class="accordion-item">
    <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#distintsSection">🏅 Distinctions</button></h2>
    <div id="distintsSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
        <div class="accordion-body section-card">
         
            <div class="mb-3"><label class="form-label">i) Position/Medal/Award (Academic/Civil)</label><textarea class="form-control" name="Medal"></textarea></div>
            <div class="mb-3"><label class="form-label">ii) Professional awards (Govt./Institution/Society)</label><textarea class="form-control" name="Society"></textarea></div>
            <div class="mb-3"><label class="form-label">iii) Sports (Intervarsity/National/International)</label><textarea class="form-control" name="Sports"></textarea></div>
            <div class="mb-3"><label class="form-label">iv) Extra Curricular</label><textarea class="form-control" name="Extra"></textarea></div>
           
           
        </div>
    </div>
</div>

                <!--Course Taught-->
                <div class="accordion-item">
    <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#CourseSection">📕 Courses Taught</button></h2>
    <div id="CourseSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
        <div class="accordion-body section-card">
            <table class="table table-bordered table-striped" id="tblCoursesTaught">
                <thead><tr><th>Courses No/ Title</th><th>Year</th><th>Independent</th><th>Joint</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="Course_Title[]" /></td>
                    <td><input class="form-control" name="Course_Year[]" /></td>
                    <td><input  class="form-control" name="Course_Independent[]" /></td>
                    <td><input  class="form-control" name="Course_Joint[]" /></td>
                    
                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblCoursesTaught')">+ Add Row</button>
        </div>
    </div>
</div>

        <!--Publications-->
    <div class="accordion-item">
    <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#PubSection">📰 Publications</button></h2>
    <div id="PubSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
        <div class="accordion-body section-card">
            <!-- HEC-->
            <h5> (A) List of Research Papers Published in HEC Recognized Journals only</h5>
            <table class="table table-bordered table-striped" id="tblhec">
                <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>Name of Journal with Volume and Page No. </th><th>ISSN No</th><th>Category</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="HEC_Authors[]" /></td> 
                    <td><input class="form-control" name="HEC_Year[]" /></td>
                    <td><input class="form-control" name="HEC_Title[]" /></td>
                    <td><input class="form-control" name="HEC_Name[]" /></td>
                    <td><input class="form-control" name="HEC_ISSN[]" /></td>
                    <td><input class="form-control" name="HEC_Category[]" /></td>
                    
                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblhec')">+ Add Row</button>

            <!-- International -->
            <h5 class="mt-4"> (B) List of International Research Papers Published</h5>
            <table class="table table-bordered table-striped" id="tblIntPub">
                      <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>Name of Journal with Volume and Page No. </th><th>ISSN No</th><th>Impact Factor</th></thead>
                <tbody><tr>
                   <td><input class="form-control" name="INT_Authors[]" /></td> 
                    <td><input class="form-control" name="INT_Year[]" /></td>
                    <td><input class="form-control" name="INT_Title[]" /></td>
                    <td><input class="form-control" name="INT_Name[]" /></td>
                    <td><input class="form-control" name="INT_ISSN[]" /></td>
                    <td><input class="form-control" name="INT_Imp[]" /></td>
                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblIntPub')">+ Add Row</button>

            <!-- Books -->
            <h5 class="mt-4"> (C) List of Books </h5>
            <table class="table table-bordered table-striped" id="tblbooks">
                                               <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>ISSBN NO </th><th>Name of Publisher</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="Book_Authors[]" /></td> 
                    <td><input class="form-control" name="Book_Year[]" /></td>
                    <td><input class="form-control" name="Book_Title[]" /></td>
                    <td><input class="form-control" name="Book_Issbn[]" /></td>
                    <td><input class="form-control" name="Book_Publisher[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblbooks')">+ Add Row</button>

                        <!-- Proceedings -->
            <h5 class="mt-4"> (C) List of Proceedings </h5>
            <table class="table table-bordered table-striped" id="tblproceedings">
                                               <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>Name of Publisher</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="Pro_Authors[]" /></td> 
                    <td><input class="form-control" name="Pro_Year[]" /></td>
                    <td><input class="form-control" name="Pro_Title[]" /></td>
                    <td><input class="form-control" name="Pro_Publisher[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblproceedings')">+ Add Row</button>


                        <!-- Manauals -->
            <h5 class="mt-4"> (C) List of Manuals </h5>
            <table class="table table-bordered table-striped" id="tblManual">
                                               <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>Name of Publisher</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="Manual_Authors[]" /></td> 
                    <td><input class="form-control" name="Manual_Year[]" /></td>
                    <td><input class="form-control" name="Manual_Title[]" /></td>
                    <td><input class="form-control" name="Manual_Publisher[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblManual')">+ Add Row</button>


                                    <!-- Monographs -->
            <h5 class="mt-4"> (C) List of Monographs </h5>
            <table class="table table-bordered table-striped" id="tblMono">
                                               <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>Name of Publisher</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="Mono_Authors[]" /></td> 
                    <td><input class="form-control" name="Mono_Year[]" /></td>
                    <td><input class="form-control" name="Mono_Title[]" /></td>
                    <td><input class="form-control" name="Mono_Publisher[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblMono')">+ Add Row</button>

            <!--List of Books Published chapters-->
                        <h5 class="mt-4"> (C)List of Book Chapters bearing ISBN No.</h5>
            <table class="table table-bordered table-striped" id="tblbookisbn">
                          <thead><tr><th>Authors</th><th>Year</th><th>Title</th><th>ISBN NO.</th><th>Name of Publisher</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="bookisbn_Authors[]" /></td> 
                    <td><input class="form-control" name="bookisbn_Year[]" /></td>
                    <td><input class="form-control" name="bookisbn_Title[]" /></td>
                    <td><input class="form-control" name="bookisbn_Issbn[]" /></td>
                    <td><input class="form-control" name="bookisbn_Publisher[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblbookisbn')">+ Add Row</button>


                        <!--List of Research Projects completed as P.I-->
                        <h5 class="mt-4"> List of Research Projects completed as P.I</h5>
            <table class="table table-bordered table-striped" id="tblpropi">
                    <thead><tr><th>Projrct Title</th><th>Funding Agency</th><th>Amount</th><th>Duration</th><th>As PI/Co-PI</th><th>Present Status</th></thead>
                <tbody><tr>
                    <td><input class="form-control" name="propi_ProjectTitle[]" /></td> 
                    <td><input class="form-control" name="propi_Funding[]" /></td>
                    <td><input class="form-control" name="propi_Amount[]" /></td>
                    <td><input class="form-control" name="propi_Duration[]" /></td>
                    <td><input class="form-control" name="propi_PI[]"/></td>
                    <td><input class="form-control" name="propi_Status[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblpropi')">+ Add Row</button>

                                    <!--List of Research Projects completed as  Co-PI-->
                        <h5 class="mt-4"> List of Research Projects completed as  Co-PI</h5>
            <table class="table table-bordered table-striped" id="tblprocopi">
                                               <thead><tr><th>Projrct Title</th><th>Funding Agency</th><th>Amount</th><th>Duration</th><th>As PI/Co-PI</th><th>Present Status</th></thead>
                <tbody><tr>
                            <td><input class="form-control" name="procopi_ProjectTitle[]" /></td> 
                            <td><input class="form-control" name="procopi_Funding[]" /></td>
                            <td><input class="form-control" name="procopi_Amount[]" /></td>
                            <td><input class="form-control" name="procopi_Duration[]" /></td>
                            <td><input class="form-control" name="procopi_PI[]"/></td>
                            <td><input class="form-control" name="procopi_Status[]"/></td>

                </tr></tbody>
            </table>
            <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblprocopi')">+ Add Row</button>

        </div>
    </div>
</div>


       <!--Membership-->
        <div class="accordion-item">
            <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#membershipSection">🏴󠁥󠁳󠁶󠁣󠁿 Membership</button></h2>
            <div id="membershipSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">
                    <table class="table table-bordered table-striped" id="tblMembership">
                        <thead><tr><th> Name</th><th> Nature</th></tr></thead>
                        <tbody><tr>
                            <td><div class="mb-3"><label class="form-label">Name</label><textarea class="form-control" name="Name"></textarea></div></td>
                            <td><div class="mb-3"><label class="form-label">Nature</label><textarea class="form-control" name="Nature"></textarea></div></td>
                            
                        </tr></tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblMembership')">+ Add Row</button>
                </div>
            </div>
        </div>


         <!-- ✅ Foreign Langugages -->
 <div class="accordion-item">
     <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#langugageSection">🗣️ Foreign Languages</button></h2>
     <div id="langugageSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
         <div class="accordion-body section-card">
             <table class="table table-bordered table-striped" id="tblLanguage">
                 <thead><tr><th>Language</th><th>Reading</th><th>Writing</th><th>Spoken</th></tr></thead>
                 <tbody><tr>
                     <td><input class="form-control" name="Lang_Title[]" /></td>
                     <td><input  class="form-control" name="Lang_Reading[]" /></td>
                     <td><input  class="form-control" name="Lang_Writing[]" /></td>
                     <td><input class="form-control" name="Lang_Spoken[]" /></td>
                    
                 </tr></tbody>
             </table>
             <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblLanguage')">+ Add Row</button>
         </div>
     </div>
 </div>

        <!-- ✅ Research Work -->
        <div class="accordion-item">
            <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#researchSection">📖 Research Work</button></h2>
            <div id="researchSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">
                    <table class="table table-bordered table-striped" id="tblResearch">
                        <thead><tr><th>Title</th><th>From</th><th>To</th><th>Professor</th><th>Institution</th></tr></thead>
                        <tbody><tr>
                            <td><input class="form-control" name="Research_Title[]" /></td>
                            <td><input type="date" class="form-control" name="Research_From[]" /></td>
                            <td><input type="date" class="form-control" name="Research_To[]" /></td>
                            <td><input class="form-control" name="Research_Professor[]" /></td>
                            <td><input class="form-control" name="Research_Institution[]" /></td>
                        </tr></tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblResearch')">+ Add Row</button>
                </div>
            </div>
        </div>

        <!-- ✅ Employment -->
        <div class="accordion-item">
            <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#employmentSection">💼 Employment History</button></h2>
            <div id="employmentSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">
                    <table class="table table-bordered table-striped" id="tblEmployment">
                        <thead><tr><th>Post Held</th><th>Where</th><th>Scale</th><th>Last Pay</th><th>From</th><th>To</th><th>Y</th><th>M</th><th>D</th><th>Leaving</th><th>Description</th></tr></thead>

                        <tbody>
                            <tr>
                           
                            <td><input class="form-control" name="Emp_Post[]" /></td>
                            <td><input class="form-control" name="Emp_Where[]" /></td>
                            <td><input class="form-control" name="Emp_Scale[]" /></td>
                            <td><input class="form-control" name="Emp_LastPay[]" /></td>
                            <td><input type="date" class="form-control emp-from" name="Emp_From[]" onchange="calcDuration(this)" /></td>
                            <td><input type="date" class="form-control emp-to" name="Emp_To[]" onchange="calcDuration(this)" /></td>
                            <td><input type="number" class="form-control emp-years" name="Emp_Years[]" readonly /></td>
                            <td><input type="number" class="form-control emp-months" name="Emp_Months[]" readonly /></td>
                            <td><input type="number" class="form-control emp-days" name="Emp_Days[]" readonly /></td>
                            <td><input class="form-control" name="Emp_Leaving[]" /></td>
                            <td><input class="form-control" name="Emp_JobDesc[]" /></td>
                        </tr>
            
                        </tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblEmployment')">+ Add Row</button>
                </div>
            </div>
        </div>

        <!-- ✅ Countries Visited -->
        <div class="accordion-item">
            <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#countriesVisitedSection">🌍 Countries Visited</button></h2>
            <div id="countriesVisitedSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">
                    <table class="table table-bordered table-striped" id="tblCountriesVisited">
                        <thead><tr><th>Country</th><th>From</th><th>To</th><th>Purpose</th></tr></thead>
                        <tbody><tr>
                            <td><input class="form-control" name="Visit_Country[]" /></td>
                            <td><input type="date" class="form-control" name="Visit_From[]" /></td>
                            <td><input type="date" class="form-control" name="Visit_To[]" /></td>
                            <td><input class="form-control" name="Visit_Purpose[]" /></td>
                        </tr></tbody>
                    </table>
                    <button type="button" class="btn btn-sm btn-outline-primary" onclick="addRow('tblCountriesVisited')">+ Add Row</button>
                </div>
            </div>
        </div>

        <!-- ✅ Declarations -->
        <div class="accordion-item">
            <h2 class="accordion-header"><button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#declarationsSection">📑 Declarations & Other Information</button></h2>
            <div id="declarationsSection" class="accordion-collapse collapse" data-bs-parent="#applicationFormAccordion">
                <div class="accordion-body section-card">
                  <div class="mb-3">
                            <label class="form-label d-block">23. Do you possess all qualifications mentioned?</label>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="HasAllQualifications" value="1" required>
                                <label class="form-check-label">Yes</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="HasAllQualifications" value="0" required>
                                <label class="form-check-label">No</label>
                            </div>
                        </div>
                    <div class="mb-3"><label class="form-label">24. Minimum Pay Acceptable</label><input class="form-control" name="MinPayAcceptable" /></div>
                    <div class="mb-3">
                        <label class="form-label">25. Physical Disability?</label><br />
                        <input type="radio" name="PhysicalDisabilityYN" value="0" checked onclick="toggleDisabilityFile(false)"> No
                        <input type="radio" name="PhysicalDisabilityYN" value="1" onclick="toggleDisabilityFile(true)"> Yes
                        <input type="file" id="disabilityFile" name="DisabilityFile" class="form-control mt-2" disabled />
                    </div>

                    <div class="mb-3"><label class="form-label">26. Liability</label><textarea class="form-control" name="Liability"></textarea></div>
                   <div class="mb-3">
                        <label class="form-label">27. Employer's Permission</label><br />
                        <input type="radio" name="EmployerPermission" value="1"> Yes
                        <input type="radio" name="EmployerPermission" value="0"> No
                    </div>

                    <div class="mb-3"><label class="form-label">28. Employer Name & Designation</label><input class="form-control" name="EmployerNameDesignation" /></div>
                    <div class="mb-3"><label class="form-label">29. Time before joining</label><input class="form-control" name="JoiningTime" /></div>
                    <div class="mb-3"><label class="form-label">30. Documents Attached</label><textarea class="form-control" name="AttachedDocuments"></textarea></div>

                    <!-- Bank Slip -->
                    <!-- ✅ Bank Credit Slip with file -->
                        <h5 class="mt-4">Bank Credit Slip (Attach Original)</h5>
                        <table class="table table-bordered table-striped" id="tblBankSlip">
                            <thead>
                                <tr><th>Amount</th><th>Credit Slip No.</th><th>Date</th><th>Bank Name</th><th>Attachment</th></tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><input type="number" class="form-control" name="Bank_Amount[]" /></td>
                                    <td><input type="text" class="form-control" name="Bank_SlipNo[]" /></td>
                                    <td><input type="date" class="form-control" name="Bank_Date[]" /></td>
                                    <td><input type="text" class="form-control" name="Bank_BankName[]" /></td>
                                    <%--<td><input type="text" class="form-control" name="Bank_Branch[]" /></td>
                                    <td><input type="file" class="form-control" name="Bank_File[]" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <button type="button" class="btn btn-sm btn-outline-primary mb-3" onclick="addRow('tblBankSlip')">+ Add Row</button>


                    <!-- Certification -->
                   <!-- ✅ Certification -->
                    <div class="alert alert-secondary mt-4" style="line-height:1.6;">
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" id="CertifyStatement" name="CertifyStatement" value="1">
                            <label class="form-check-label" for="CertifyStatement">
                                I certify that the statement made by me in this application are true to the best of my knowledge
                                and belief, and that I hold myself responsible for any discrepancy.
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Submit -->
    <div class="d-grid mt-4">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Application" CssClass="btn btn-primary btn-lg" OnClick="btnSubmit_Click" />
    </div>
    <div class="text-center mt-3">
        <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold"></asp:Label>
    </div>
</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
<script>
    function addRow(tableId) {
        var table = document.getElementById(tableId).getElementsByTagName('tbody')[0];
        var newRow = table.rows[0].cloneNode(true);
        var inputs = newRow.querySelectorAll('input, textarea');
        inputs.forEach(i => i.value = "");
        table.appendChild(newRow);
    }

    // Employment duration auto-calc
    function calcDuration(el) {
        var row = el.closest("tr");
        var from = new Date(row.querySelector(".emp-from").value);
        var to = new Date(row.querySelector(".emp-to").value);
        if (!isNaN(from) && !isNaN(to) && to >= from) {
            var years = to.getFullYear() - from.getFullYear();
            var months = to.getMonth() - from.getMonth();
            var days = to.getDate() - from.getDate();
            if (days < 0) { months--; days += new Date(to.getFullYear(), to.getMonth(), 0).getDate(); }
            if (months < 0) { years--; months += 12; }
            row.querySelector(".emp-years").value = years;
            row.querySelector(".emp-months").value = months;
            row.querySelector(".emp-days").value = days;
        }
    }

    function toggleDisabilityFile(enable) {
        var fileInput = document.getElementById("disabilityFile");
        fileInput.disabled = !enable;
        if (!enable) fileInput.value = "";
    }
</script>
</body>
</html>
