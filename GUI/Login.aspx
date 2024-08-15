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
            <h2>Inicio de Sesión</h2>
            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email:" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" Width="344px" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Contraseña:" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" Width="343px" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Button runat="server" ID="btnLogin" Text="Iniciar Sesión" CssClass="btn btn-primary" OnClick="btnLogin_Click" Width="364px" />
            </asp:Panel>

            <!-- Label para mostrar mensajes de error o éxito -->
            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>