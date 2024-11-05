<%@ Page Title="Backup" Language="C#" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="GUI.Backup" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Backup</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/Backup.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="containerBackup mt-5">
            <h2 class="text-center">
                <asp:Label ID="lblTituloBackup" runat="server" Text="Realizar Backup"></asp:Label>
            </h2>

            <div class="form-group">
                <asp:Label ID="lblRutaBackup" runat="server" AssociatedControlID="txtRuta" Text="Ruta del Backup:"></asp:Label>
                <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" placeholder="Ingrese la ruta completa"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lblNombreBackup" runat="server" AssociatedControlID="txtNombre" Text="Nombre del Backup:"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del backup"></asp:TextBox>
            </div>

            <div class="form-group text-center">
                <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-primary" Text="Realizar Backup" OnClick="btnBackup_Click" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary ml-3" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>

            <div class="form-group text-center mt-3">
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
            </div>
        </div>
        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
    </form>
</body>
</html>
