<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GUI.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Inicio de Sesión</title>
    <link rel="stylesheet" href="Style/Login.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Iniciar Sesión</h2>
            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email:" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Contraseña:" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Button runat="server" ID="btnLogin" Text="Iniciar Sesión" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                    <asp:HyperLink ID="lnkRecuperarContraseña" runat="server" NavigateUrl="ForgotPassword.aspx" CssClass="forgot-password">
                        ¿Olvidaste tu contraseña?
                    </asp:HyperLink>

                </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                    <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>                
            </asp:Panel>
        </div>
    </form>
</body>
</html>
