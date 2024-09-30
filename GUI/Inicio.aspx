<%@ Page Title="Inicio" Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GUI.Inicio" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Recursos Humanos</title>
    <link href="~/Style/Inicio.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <br />
        <div class="welcome-section">
            <div class="welcome-content">
                <h1>Bienvenido <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>. Nos alegra que estés aquí. 👋</h1>
            </div>
        </div>

        <div class="container quick-actions">
            <div class="card">
                <div class="card-body">
                    <h3 class="card-title">
                        <asp:Label ID="lblNombreUsuarioProfile" runat="server"></asp:Label>
                    </h3>
                    <p class="card-text">Gestiona tu perfil y solicitudes</p>
                    <button class="btn btn-primary">
                        <i class="fas fa-user-circle"></i> Ir a mi perfil
                    </button>
                    <button class="btn btn-outline-primary">
                        <i class="fas fa-coffee"></i> Solicitar tiempo de descanso
                    </button>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <h3 class="card-title">Mis tareas</h3>
                    <p class="card-text">Consulta tus tareas pendientes</p>
                    <button class="btn btn-primary">
                        <i class="fas fa-tasks"></i> Ver mis tareas
                    </button>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <h3 class="card-title">Mis documentos</h3>
                    <p class="card-text">Accede a tus documentos importantes</p>
                    <button class="btn btn-primary">
                        <i class="fas fa-file-alt"></i> Ver mis documentos
                    </button>
                </div>
            </div>
        </div>

        <div class="footer">
            <p>&copy; 2024 HR Hub. Todos los derechos reservados.</p>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
