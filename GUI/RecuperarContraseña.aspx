<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="GUI.RecuperarContraseña" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Recuperar Contraseña</title>
    <link rel="stylesheet" href="Style/Login.css">
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Recuperar Contraseña</h2>
            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email:" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Button runat="server" ID="btnRecuperar" Text="Recuperar Contraseña" CssClass="btn btn-primary" OnClick="btnRecuperar_Click" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Button runat="server" ID="btnVolver" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
            </asp:Panel>


        </div>
    </form>
</body>
</html>
