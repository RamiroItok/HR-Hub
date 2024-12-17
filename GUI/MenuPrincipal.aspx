<%@ Page Title="MenuPrincipal" Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="GUI.MenuPrincipal" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>
        <asp:Literal ID="litTituloPagina" runat="server"></asp:Literal></title>
    <link href="~/Style/MenuPrincipal.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <br />
        <div class="welcome-section">
            <div class="welcome-content">
                <h1>
                    <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>👋</h1>
            </div>
        </div>

        <div class="container quick-actions">
            <div class="card">
                <div class="card-body">
                    <h3 class="card-title">
                        <asp:Label ID="lblNombreUsuarioProfile" runat="server"></asp:Label>
                    </h3>
                    <p class="card-text">
                        <asp:Literal ID="litGestionaPerfil" runat="server"></asp:Literal></p>
                    <button id="btnIrPerfil" runat="server" class="btn btn-primary" onserverclick="btnIrPerfil_ServerClick">
                        <i class="fas fa-user-circle"></i>
                        <asp:Literal ID="litIrPerfil" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <h3 class="card-title">
                        <asp:Literal ID="litMisDocumentos" runat="server"></asp:Literal></h3>
                    <p class="card-text">
                        <asp:Literal ID="litAccedeDocumentos" runat="server"></asp:Literal></p>
                    <button id="btnVerDocumentos" runat="server" class="btn btn-primary" onserverclick="btnVerDocumentos_ServerClick">
                        <i class="fas fa-file-alt"></i>
                        <asp:Literal ID="litVerDocumentos" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <div class="footer">
            <p>
                <asp:Literal ID="litFooterText" runat="server"></asp:Literal></p>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
