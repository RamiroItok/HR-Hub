<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorDigitoVerificador.aspx.cs" Inherits="GUI.ErrorDigitoVerificador" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <title>Dígito Verificador</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Style/NavBar.css" rel="stylesheet" />
    <link href="Style/ErrorDigitoVerificador.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="container body-content digito-verificador-page">
            <div class="text-center my-5">
                <h1 class="animate__animated animate__fadeInDown">Error en los Dígitos Verificadores</h1>
                <div class="alert alert-warning text-center" role="alert">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-label" />
                </div>
                <p class="lead">Por favor comunicate con un usuario WebMaster.</p>
                <div class="text-center mt-4">
                    <asp:Button ID="btnOk" runat="server" Text="Aceptar" OnClick="btnOk_Click" CssClass="btn btn-success btn-lg" />
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</body>
</html>