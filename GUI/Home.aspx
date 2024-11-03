<%@ Page Title="Home" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GUI.Home" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Recursos Humanos</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/Home.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-background" 
             style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); 
                    background-size: cover; 
                    background-position: center; 
                    background-attachment: fixed;">
            
            <!-- Hero Section -->
            <section class="hero-section">
                <img src="Content/imagenes/Logo_HrHub.jpg" alt="HR Hub Logo" class="hero-logo" />
                <h1>Bienvenido a HR Hub</h1>
                <p>La solución integral para gestionar y optimizar tus recursos humanos.</p>
                <div class="hero-buttons">
                    <a href="Login.aspx" class="cta-button">Inicia sesión ahora</a>
                    <button type="button" class="info-button" onclick="scrollToServices()">¿Quieres saber más información?</button>
                    <a href="Contact.aspx" class="contact-button">Comunícate con nosotros</a>
                </div>
            </section>

            <hr class="section-divider"> <!-- Línea divisoria -->

            <!-- Sección de Pilares -->
            <section class="pillars-section">
                <h2 class="section-title">Nuestros Pilares</h2>
                <div class="pillars-container">
                    <div class="pillar">
                        <i class="fas fa-award fa-2x"></i>
                        <h3>Compromiso con la Excelencia</h3>
                        <p>Ofrecemos soluciones de alta calidad que superan las expectativas de nuestros clientes.</p>
                    </div>
                    <div class="pillar">
                        <i class="fas fa-lightbulb fa-2x"></i>
                        <h3>Innovación Continua</h3>
                        <p>Mantenemos a HR Hub a la vanguardia de la tecnología y las tendencias del sector.</p>
                    </div>
                    <div class="pillar">
                        <i class="fas fa-user-friends fa-2x"></i>
                        <h3>Enfoque en el Cliente</h3>
                        <p>Entendemos y atendemos las necesidades únicas de cada cliente.</p>
                    </div>
                </div>
            </section>

            <hr class="section-divider"> <!-- Línea divisoria -->

            <!-- Sección de Servicios -->
            <section id="services" class="services-section">
                <h2 class="section-title">Nuestros Servicios</h2>
                <div class="services-grid">
                    <div class="service">
                        <h4>Gestión de Documentación</h4>
                        <p>Automatización y organización de todos los documentos esenciales de RRHH.</p>
                    </div>
                    <div class="service">
                        <h4>Capacitación y Desarrollo</h4>
                        <p>Gestión de programas de capacitación para el crecimiento del talento.</p>
                    </div>
                    <div class="service">
                        <h4>Beneficios y Compensaciones</h4>
                        <p>Optimización de beneficios y bonificaciones para empleados.</p>
                    </div>
                    <div class="service">
                        <h4>Reclutamiento</h4>
                        <p>Facilita el proceso de reclutamiento y selección de nuevos talentos.</p>
                    </div>
                    <div class="service">
                        <h4>Comunicación Interna</h4>
                        <p>Herramientas para mejorar la comunicación y cohesión en la empresa.</p>
                    </div>
                    <div class="service">
                        <h4>Análisis de Datos</h4>
                        <p>Uso de IA para analizar datos y obtener insights para la toma de decisiones.</p>
                    </div>
                </div>
                <p class="more-services">Y muchos servicios más para cubrir todas tus necesidades de Recursos Humanos.</p>
            </section>
        </div>

        <!-- Footer -->
        <footer class="footer">
            <p>&copy; 2024 HR Hub. Todos los derechos reservados.</p>
        </footer>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        function scrollToServices() {
            const servicesSection = document.getElementById("services");
            servicesSection.scrollIntoView({ behavior: "smooth" });
        }
    </script>
</body>
</html>
