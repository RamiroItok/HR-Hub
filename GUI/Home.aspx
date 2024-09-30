<%@ Page Title="Home" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GUI.Home" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Recursos Humanos</title>
    <link href="~/Style/Default.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/SiteMaster.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="header">
            <div class="header-content">
                <img src="Content/imagenes/Logo_HrHub.jpg" alt="HR Hub Logo" class="logo" />
                <asp:Label ID="lblBienvenido" runat="server" CssClass="welcome-message" />
            </div>
        </div>

        <div class="highlight-section">
            <div class="highlight-content">
                <i class="fas fa-briefcase fa-3x"></i>
                <h2>Gestión Integral de Recursos Humanos</h2>
                <p>Descubra cómo optimizar la gestión de su personal con nuestras herramientas avanzadas.</p>
            </div>
        </div>

        <section class="about-us animate__animated animate__fadeInUp">
            <h2>¿Quiénes Somos?</h2>
            <p>HR Hub es una software factory especializada en soluciones innovadoras para la gestión de recursos humanos, 
            marcando la diferencia en el mercado por su enfoque único en la comunicación y el uso de inteligencia artificial 
            para el análisis de datos.</p>
        </section>

        <section class="our-pillars animate__animated animate__fadeInUp">
            <h2>Nuestros Pilares</h2>
            <div class="pillar">
                <h3>Compromiso con la Excelencia</h3>
                <p>Nuestra misión es ofrecer soluciones de alta calidad que superen las expectativas de nuestros clientes.</p>
            </div>
            <div class="pillar">
                <h3>Innovación Continua</h3>
                <p>Nos esforzamos por estar a la vanguardia de la tecnología y las tendencias del sector.</p>
            </div>
            <div class="pillar">
                <h3>Enfoque en el Cliente</h3>
                <p>Nuestro objetivo es entender y satisfacer las necesidades únicas de cada cliente.</p>
            </div>
        </section>

        <div class="footer">
            <p>&copy; 2024 HR Hub. Todos los derechos reservados.</p>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
