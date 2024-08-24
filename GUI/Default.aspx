﻿<%@ Page Title="Inicio" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GUI._Default" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Recursos Humanos</title>
    <link href="Style/Default.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Incluir el NavBar -->
        <uc:NavBar runat="server" ID="NavBarControl" />

        <!-- Contenedor de Flexbox para logo y bienvenida -->
        <div class="header">
            <div class="header-content">
                <img src="Content/imagenes/Logo_HrHub.jpg" alt="HR Hub Logo" class="logo" />
                <h1>Bienvenidos a HR Hub</h1>
            </div>
        </div>

        <!-- Sección Prominente para "Gestión Integral de Recursos Humanos" -->
        <div class="highlight-section">
            <div class="highlight-content">
                <i class="fas fa-briefcase fa-3x"></i> <!-- Icono añadido -->
                <h2>Gestión Integral de Recursos Humanos</h2>
                <p>Descubra cómo optimizar la gestión de su personal con nuestras herramientas avanzadas.</p>
            </div>
        </div>

        <!-- Sección Quiénes Somos -->
        <section class="about-us animate__animated animate__fadeInUp">
            <h2>¿Quiénes Somos?</h2>
            <p>HR Hub es una software factory especializada en soluciones innovadoras para la gestión de recursos humanos, 
            marcando la diferencia en el mercado por su enfoque único en la comunicación y el uso de inteligencia artificial 
            para el análisis de datos.</p>
        </section>

        <!-- Sección Nuestros Pilares -->
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

        <!-- Footer -->
        <div class="footer">
            <p>&copy; 2024 HR Hub. Todos los derechos reservados.</p>
        </div>
    </form>

    <!-- Scripts -->
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
