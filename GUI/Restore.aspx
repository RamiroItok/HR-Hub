<%@ Page Title="Restore" Language="C#" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="GUI.Restore" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><asp:Literal ID="litPageTitle" runat="server" /></title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/Restore.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="containerRestore mt-5 animate__animated animate__fadeIn">
            <h2><asp:Label ID="lblTituloRestore" runat="server" /></h2>
            <p class="description"><asp:Label ID="lblDescripcionRestore" runat="server" /></p>

            <div class="form-group">
                <label for="fileBackup"><i class="fas fa-file-upload"></i> <asp:Label ID="lblSeleccionarArchivo" runat="server" /></label>
                <asp:FileUpload ID="fileBackup" runat="server" CssClass="form-control file-upload" />
            </div>

            <div class="form-group text-center">
                <asp:Button ID="btnRestore" runat="server" CssClass="btn btn-primary btn-action" OnClick="btnRestore_Click" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary btn-action" OnClick="btnCancelar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert-message" Visible="false"></asp:Label>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>