<%@ Page Title="Contact" Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="GUI.Contact" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Contacto</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="~/Style/Contact.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="container mt-5">
            <div class="card contact-card shadow-lg p-4 animate__animated animate__fadeIn">
                <h2 class="text-center mb-4 text-uppercase font-weight-bold text-dark">
                    <asp:Literal ID="litContactTitle" runat="server" Text="Contacto"></asp:Literal>
                </h2>

                <div class="contact-info text-center">
                    <p><i class="fas fa-map-marker-alt icon-style"></i> <asp:Literal ID="litAddress" runat="server" Text="Dirección: El Salvador 5847"></asp:Literal></p>
                    <p><i class="fas fa-city icon-style"></i> <asp:Literal ID="litCity" runat="server" Text="Ciudad: Buenos Aires"></asp:Literal></p>
                    <p><i class="fas fa-map icon-style"></i> <asp:Literal ID="litProvince" runat="server" Text="Provincia: Buenos Aires"></asp:Literal></p>
                    <p><i class="fas fa-globe-americas icon-style"></i> <asp:Literal ID="litCountry" runat="server" Text="País: Argentina"></asp:Literal></p>
                    <p><i class="fas fa-phone icon-style"></i> <asp:Literal ID="litPhone" runat="server" Text="Teléfono: +54 11 1234-5678"></asp:Literal></p>
                    <p><i class="fas fa-envelope icon-style"></i> <asp:Literal ID="litEmailLabel" runat="server" Text="Email:"></asp:Literal> 
                        <a href="mailto:hrhub@gmail.com" class="contact-link">hrhub@gmail.com</a>
                    </p>
                </div>
            </div>
        </div>

        <!-- Selector de idioma -->
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
