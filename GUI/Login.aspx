<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GUI.Login" %>

<%@ Register Src="~/Controls/ValidarEmail.ascx" TagPrefix="uc" TagName="ValidarEmail" %>
<%@ Register Src="~/Controls/ValidarContraseña.ascx" TagName="ValidarContraseña" TagPrefix="uc" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>
        <asp:Literal ID="litTituloPagina" runat="server"></asp:Literal></title>
    <link rel="stylesheet" href="Style/Login.css">
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>
                <asp:Literal ID="litTituloFormulario" runat="server"></asp:Literal></h2>

            <asp:Panel runat="server" CssClass="form-group">
                <uc:ValidarEmail ID="ValidarEmailControl" runat="server" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <uc:ValidarContraseña ID="PasswordValidator" runat="server" />
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                <asp:HyperLink ID="lnkRecuperarContraseña" runat="server" NavigateUrl="RecuperarContraseña.aspx" CssClass="forgot-password">
                    <asp:Literal ID="litRecuperarContraseña" runat="server"></asp:Literal>
                </asp:HyperLink>
            </asp:Panel>

            <asp:Panel runat="server" CssClass="form-group">
                <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
            </asp:Panel>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
            <asp:ListItem Value="es">Español</asp:ListItem>
            <asp:ListItem Value="en">English</asp:ListItem>
        </asp:DropDownList>
    </form>
</body>
</html>
