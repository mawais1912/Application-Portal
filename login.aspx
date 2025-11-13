<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniversityPortal.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <title>University Portal — Login & Sign Up</title>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap" rel="stylesheet">
    <style>
        :root {
            --brand: #0b62ff;
            --brand-600: #0a56df;
            --text: #0f172a;
            --muted: #64748b;
            --bg: #f8fafc;
            --card: #ffffff;
            --ring: rgba(11,98,255,.15);
            --success: #16a34a;
            --danger: #dc2626;
        }
        * { box-sizing: border-box; }
        html, body { height: 100%; margin: 0; font-family: 'Inter', sans-serif; }
        body {
            color: var(--text);
            background: radial-gradient(1200px 600px at 80% -10%, #dbeafe 0%, rgba(219,234,254,0) 60%),
                        radial-gradient(800px 400px at -10% 110%, #dcfce7 0%, rgba(220,252,231,0) 60%),
                        var(--bg);
        }
        .shell { min-height: 100%; display: grid; grid-template-columns: 1.3fr 1fr; }
        .panel { display: grid; place-items: center; padding: 40px 24px; }
        .card {
            width: min(520px, 100%);
            background: var(--card);
            border-radius: 24px;
            box-shadow: 0 20px 60px rgba(2,6,23,.08), 0 1px 0 rgba(2,6,23,.06);
            padding: 28px;
            border: 1px solid rgba(15,23,42,.06);
        }
        .tabs { display: grid; grid-template-columns: 1fr 1fr; gap: 6px; background: #f1f5f9; padding: 6px; border-radius: 14px; margin-bottom: 20px; }
        .tab {
            appearance: none; border: 0; background: transparent; padding: 12px 10px;
            border-radius: 10px; font-weight: 600; cursor: pointer; color: var(--muted);
        }
        .tab:hover { background: #e2e8f0; }
        .field { display: grid; gap: 8px; margin-bottom: 16px; }
        .label { font-size: 13px; color: var(--muted); font-weight: 600; }
        .input {
            width: 100%; padding: 14px;
            border-radius: 12px; border: 1px solid #e2e8f0;
            background: #fff; font-size: 15px;
            transition: 0.2s ease;
        }
        .input:focus { border-color: var(--brand); box-shadow: 0 0 0 4px var(--ring); outline: none; }
        .actions { display: flex; align-items: center; justify-content: space-between; margin-top: 12px; }
        .btn {
            border: 0; cursor: pointer; font-weight: 700;
            padding: 14px 16px; border-radius: 12px; font-size: 15px;
        }
        .btn-primary { background: var(--brand); color: #fff; transition: 0.2s; }
        .btn-primary:hover { background: var(--brand-600); }
        .hint { font-size: 12px; color: var(--muted); }
        .alert {
            border-radius: 12px; padding: 12px 14px; font-size: 14px;
            margin-bottom: 12px; display: block;
        }
        .alert.success { background: #ecfdf5; color: var(--success); border: 1px solid #bbf7d0; }
        .alert.error { background: #fef2f2; color: var(--danger); border: 1px solid #fecaca; }
        footer { margin-top: 16px; text-align: center; color: var(--muted); font-size: 12px; }
        @media (max-width: 960px) {
            .shell { grid-template-columns: 1fr; }
            .panel { padding: 24px 16px; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="shell">
            <main class="panel">
                <section class="card">
                    <!-- Tabs -->
                    <div class="tabs">
                        <asp:Button ID="btnTabLogin" runat="server" Text="Log In" CssClass="tab" OnClick="SwitchToLogin" CausesValidation="false" OnClientClick="clearAlerts()" />
                        <asp:Button ID="btnTabSignUp" runat="server" Text="Create Account" CssClass="tab" OnClick="SwitchToSignup" CausesValidation="false" OnClientClick="clearAlerts()" />
                    </div>

                    <!-- Alerts (shared) -->
                    <asp:Label ID="lblSuccess" runat="server" CssClass="alert success" Visible="false"></asp:Label>
                    <asp:Label ID="lblError" runat="server" CssClass="alert error" Visible="false"></asp:Label>

                    <!-- Login Panel -->
                    <asp:Panel ID="pnlLogin" runat="server">
                        <div class="field">
                            <label class="label">Email</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="field">
                            <label class="label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="actions">
                            <asp:CheckBox ID="chkRemember" runat="server" Text="Remember me" />
                            <asp:Button ID="btnLogin" runat="server" Text="Log In" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                        </div>
                    </asp:Panel>

                    <!-- Signup Panel -->
                    <asp:Panel ID="pnlSignup" runat="server" Visible="false">
                        <div class="field"><label>First Name</label><asp:TextBox ID="txtFirstName" runat="server" CssClass="input"></asp:TextBox></div>
                        <div class="field"><label>Last Name</label><asp:TextBox ID="txtLastName" runat="server" CssClass="input"></asp:TextBox></div>
                        <div class="field"><label>Email</label><asp:TextBox ID="txtEmail" runat="server" CssClass="input" TextMode="Email"></asp:TextBox></div>
                        <div class="field"><label>Phone Number</label><asp:TextBox ID="txtDept" runat="server" CssClass="input"></asp:TextBox></div>
                        <div class="field"><label>Create Password</label><asp:TextBox ID="txtPassword1" runat="server" CssClass="input" TextMode="Password"></asp:TextBox></div>
                        <div class="field"><label>Confirm Password</label><asp:TextBox ID="txtPassword2" runat="server" CssClass="input" TextMode="Password"></asp:TextBox></div>
                        <div class="actions">
                            <span class="hint">By creating an account, you agree to the Terms & Privacy.</span>
                            <asp:Button ID="btnSignup" runat="server" Text="Create Account" CssClass="btn btn-primary" OnClick="btnSignup_Click" />
                        </div>
                    </asp:Panel>

                    <footer>© <%: DateTime.Now.Year %> University Portal</footer>
                </section>
            </main>
        </div>
    </form>

    <script>
        function clearAlerts() {
            document.getElementById('<%= lblError.ClientID %>').style.display = "none";
            document.getElementById('<%= lblSuccess.ClientID %>').style.display = "none";
        }
    </script>
</body>
</html>
