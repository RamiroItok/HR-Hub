<%@ Page Title="Home" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GUI.Home" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestion de Recursos Humanos</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/Home.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <!-- DropDownList de idioma en la esquina superior derecha -->
        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
            <asp:ListItem Value="es">Español</asp:ListItem>
            <asp:ListItem Value="en">English</asp:ListItem>
        </asp:DropDownList>

        <div class="main-background"
            style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); 
                   background-size: cover; 
                   background-position: center; 
                   background-attachment: fixed;">

            <!-- Hero Section -->
            <section class="hero-section">
                <img src="Content/imagenes/Logo_HrHub.jpg" alt="HR Hub Logo" class="hero-logo" />
                <h1><asp:Literal ID="litWelcomeTitle" runat="server"></asp:Literal></h1>
                <p><asp:Literal ID="litWelcomeDescription" runat="server"></asp:Literal></p>

                <div class="hero-buttons">
                    <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="Login.aspx" CssClass="cta-button"></asp:HyperLink>
                    <asp:Button ID="btnInfo" runat="server" CssClass="info-button" OnClientClick="scrollToServices(); return false;" />
                    <asp:HyperLink ID="hlContact" runat="server" NavigateUrl="Contact.aspx" CssClass="contact-button"></asp:HyperLink>
                </div>
            </section>

            <hr class="section-divider" />

            <!-- Pillars Section -->
            <section class="pillars-section">
                <h2 class="section-title"><asp:Literal ID="litPillarsTitle" runat="server"></asp:Literal></h2>
                <div class="pillars-container">
                    <div class="pillar">
                        <i class="fas fa-award fa-2x"></i>
                        <h3><asp:Literal ID="litPillar1Title" runat="server"></asp:Literal></h3>
                        <p><asp:Literal ID="litPillar1Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="pillar">
                        <i class="fas fa-lightbulb fa-2x"></i>
                        <h3><asp:Literal ID="litPillar2Title" runat="server"></asp:Literal></h3>
                        <p><asp:Literal ID="litPillar2Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="pillar">
                        <i class="fas fa-user-friends fa-2x"></i>
                        <h3><asp:Literal ID="litPillar3Title" runat="server"></asp:Literal></h3>
                        <p><asp:Literal ID="litPillar3Description" runat="server"></asp:Literal></p>
                    </div>
                </div>
            </section>

            <hr class="section-divider" />

            <!-- Services Section -->
            <section id="services" class="services-section">
                <h2 class="section-title"><asp:Literal ID="litServicesTitle" runat="server"></asp:Literal></h2>
                <div class="services-grid">
                    <div class="service">
                        <h4><asp:Literal ID="litService1Title" runat="server"></asp:Literal></h4>
                        <p><asp:Literal ID="litService1Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="service">
                        <h4><asp:Literal ID="litService2Title" runat="server"></asp:Literal></h4>
                        <p><asp:Literal ID="litService2Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="service">
                        <h4><asp:Literal ID="litService3Title" runat="server"></asp:Literal></h4>
                        <p><asp:Literal ID="litService3Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="service">
                        <h4><asp:Literal ID="litService4Title" runat="server"></asp:Literal></h4>
                        <p><asp:Literal ID="litService4Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="service">
                        <h4><asp:Literal ID="litService5Title" runat="server"></asp:Literal></h4>
                        <p><asp:Literal ID="litService5Description" runat="server"></asp:Literal></p>
                    </div>
                    <div class="service">
                        <h4><asp:Literal ID="litService6Title" runat="server"></asp:Literal></h4>
                        <p><asp:Literal ID="litService6Description" runat="server"></asp:Literal></p>
                    </div>
                </div>
                <p class="more-services"><asp:Literal ID="litMoreServices" runat="server"></asp:Literal></p>
            </section>
        </div>

        <!-- Footer -->
        <footer class="footer">
            <p><asp:Literal ID="litFooterText" runat="server"></asp:Literal></p>
        </footer>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        // Funcion para scroll suave a la seccion de servicios
        function scrollToServices() {
            const servicesSection = document.getElementById("services");
            servicesSection.scrollIntoView({ behavior: "smooth" });
        }
    </script>
</body>
</html>
