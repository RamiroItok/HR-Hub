<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorDigitoVerificador.aspx.cs" Inherits="GUI.ErrorDigitoVerificador" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><asp:Literal ID="litPageTitle" runat="server" Text="Dígito Verificador - Error"></asp:Literal></title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Style/NavBar.css" rel="stylesheet" />
    <link href="Style/ErrorDigitoVerificador.css" rel="stylesheet" />
</head>

<body class="error-page" style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="container error-content text-center animate__animated animate__fadeIn">
            <h1 class="error-title animate__animated animate__fadeInDown">
                <asp:Literal ID="litErrorTitle" runat="server" Text="¡Error en los Dígitos Verificadores!"></asp:Literal>
            </h1>
            <div class="alert alert-warning error-alert animate__animated animate__fadeInUp" role="alert">
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-label" Text="Algo salió mal."></asp:Label>
            </div>
            <p class="lead error-message">
                <asp:Literal ID="litContactMessage" runat="server" Text="Por favor, comunícate con un administrador o usuario WebMaster para resolver este problema."></asp:Literal>
            </p>
            <asp:Button ID="btnOk" runat="server" CssClass="btn btn-primary btn-lg mt-4 animate__animated animate__pulse" OnClick="btnOk_Click" />
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>