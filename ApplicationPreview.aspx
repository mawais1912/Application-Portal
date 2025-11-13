<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPreview.aspx.cs" Inherits="JobApplicationForm.ApplicationPreview" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Application Preview</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <h2 class="mb-4 text-primary text-center">📑 Application Preview</h2>

        <!-- ✅ Info Banner -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info fw-bold text-center mb-4">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- Personal Info -->
        <div class="card shadow mb-4">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Personal Information</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litApplicantInfo" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Secondary / Intermediate -->
        <div class="card shadow mb-4">
            <div class="card-header bg-info text-white">
                <h5 class="mb-0">Secondary & Intermediate</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litSecondary" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- University -->
        <div class="card shadow mb-4">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">University</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litUniversity" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Post-Doctorate -->
        <div class="card shadow mb-4">
            <div class="card-header bg-warning text-dark">
                <h5 class="mb-0">Post-Doctorate</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litPostDoc" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Research -->
        <div class="card shadow mb-4">
            <div class="card-header bg-secondary text-white">
                <h5 class="mb-0">Research</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litResearch" runat="server"></asp:Literal>
            </div>
        </div>

         <!-- Student Guide -->
 <div class="card shadow mb-4">
     <div class="card-header bg-secondary text-white">
         <h5 class="mb-0">Student Guide</h5>
     </div>
     <div class="card-body">
         <asp:Literal ID="litStudentGuide" runat="server"></asp:Literal>
     </div>
 </div>
         <!-- Distinctions -->
        <div class="card shadow mb-4">
    <div class="card-header bg-secondary text-white">
        <h5 class="mb-0">Distinctions</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="litDistinctions" runat="server"></asp:Literal>
    </div>
</div>
                                 <!-- Courses Taught -->
        <div class="card shadow mb-4">
    <div class="card-header bg-secondary text-white">
        <h5 class="mb-0">Courses Taught</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="litCourses" runat="server"></asp:Literal>
    </div>
</div>

        <!-- Hec -->
<div class="card shadow mb-4">
    <div class="card-header bg-info text-white">
        <h5 class="mb-0"> Published in HEC</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="hecpub" runat="server"></asp:Literal>
    </div>
</div>

<!-- International -->
<div class="card shadow mb-4">
    <div class="card-header bg-success text-white">
        <h5 class="mb-0"> International Research Papers</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="litinter" runat="server"></asp:Literal>
    </div>
</div>

<!-- List of Books -->
<div class="card shadow mb-4">
    <div class="card-header bg-warning text-dark">
        <h5 class="mb-0">List of Books</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="litbook" runat="server"></asp:Literal>
    </div>
</div>

        <!-- Proceedings -->
<div class="card shadow mb-4">
    <div class="card-header bg-info text-white">
        <h5 class="mb-0"> List of Proceedings</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="proceeding" runat="server"></asp:Literal>
    </div>
</div>

<!-- List of Manuals -->
<div class="card shadow mb-4">
    <div class="card-header bg-success text-white">
        <h5 class="mb-0"> List of Manuals</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="manual" runat="server"></asp:Literal>
    </div>
</div>

<!--List of Monographs -->
<div class="card shadow mb-4">
    <div class="card-header bg-warning text-dark">
        <h5 class="mb-0">List of Monographs</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="mono" runat="server"></asp:Literal>
    </div>
</div>

                <!-- List of Book Chapters bearing ISBN No. ( -->
<div class="card shadow mb-4">
    <div class="card-header bg-info text-white">
        <h5 class="mb-0"> List of Book Chapters bearing ISBN No. (</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="isbn" runat="server"></asp:Literal>
    </div>
</div>

<!-- List of Research Projects completed as P.I. -->
<div class="card shadow mb-4">
    <div class="card-header bg-success text-white">
        <h5 class="mb-0"> List of Research Projects completed as P.I.</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="pi" runat="server"></asp:Literal>
    </div>
</div>

<!--List of Research Projects completed as Co-PI  -->
<div class="card shadow mb-4">
    <div class="card-header bg-warning text-dark">
        <h5 class="mb-0">List of Research Projects completed as Co-PI </h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="copi" runat="server"></asp:Literal>
    </div>
</div>

         <!-- Foreign Languages -->
 <div class="card shadow mb-4">
     <div class="card-header bg-dark text-white">
         <h5 class="mb-0">Foreign Languages</h5>
     </div>
     <div class="card-body">
         <asp:Literal ID="litlanguage" runat="server"></asp:Literal>
     </div>
 </div>

                <!-- MemeberShip Languages -->
<div class="card shadow mb-4">
    <div class="card-header bg-dark text-white">
        <h5 class="mb-0">MemberShip of Learned Socities</h5>
    </div>
    <div class="card-body">
        <asp:Literal ID="litmembership" runat="server"></asp:Literal>
    </div>
</div>
        <!-- Employment History -->
        <div class="card shadow mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">Employment History</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litEmployment" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Countries Visited -->
        <div class="card shadow mb-4">
            <div class="card-header bg-secondary text-white">
                <h5 class="mb-0">Countries Visited</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litCountries" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Declarations -->
        <div class="card shadow mb-4">
            <div class="card-header bg-light text-dark">
                <h5 class="mb-0">Declarations & Other Information</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litDeclarations" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Bank Credit Slip -->
        <div class="card shadow mb-4">
            <div class="card-header bg-info text-white">
                <h5 class="mb-0">Bank Credit Slip</h5>
            </div>
            <div class="card-body">
                <asp:Literal ID="litBankSlip" runat="server"></asp:Literal>
            </div>
        </div>

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

        <!-- Buttons -->
        <div class="d-flex justify-content-between mt-4">
            <asp:Button ID="btnBack" runat="server" Text="⬅ Back to Form" CssClass="btn btn-outline-primary btn-lg" PostBackUrl="~/ApplicationForm.aspx" />
            <asp:Button ID="btnDownloadPdf" runat="server" Text="⬇ Download PDF" CssClass="btn btn-success btn-lg" OnClick="btnDownloadPdf_Click" />
        </div>
    </form>
</body>
</html>
