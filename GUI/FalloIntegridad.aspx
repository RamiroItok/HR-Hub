<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FalloIntegridad.aspx.cs" Inherits="GUI.FalloIntegridad" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <title>Fallo de Integridad</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Style/FalloIntegridad.css" rel="stylesheet" />
    <link href="Style/NavBar.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="fallo-container mt-5">
            <h2 class="fallo-title text-center">Fallo de Integridad en el Sistema</h2>
            <div class="fallo-message text-center mt-3">
                <asp:Label ID="lblTablaFallo" runat="server" CssClass="fallo-status-label fallo-status-fallo"></asp:Label>
            </div>

            <h3 class="fallo-subtitle text-center mt-5">Recalcular dígitos verificadores</h3>

            <div class="fallo-status-container text-center mt-4">
                <asp:Label ID="lblEstadoIntegridad" runat="server" CssClass="fallo-status-label"></asp:Label>
            </div>

            <div class="fallo-recalcular-container text-center mt-3">
                <asp:Button ID="btnRecalcular" runat="server" CssClass="fallo-btn-primary" Text="Recalcular Dígitos Verificadores" OnClick="btnRecalcular_Click" />
            </div>
            <br />
            <h3 class="fallo-subtitle text-center mt-5">Restaurar el sistema</h3>
            <!-- Subtítulo para la selección del archivo de Backup -->
            <div class="fallo-form-group">
                <label for="fileBackup" class="fallo-label">Seleccione el archivo de Backup:</label>
                <asp:FileUpload ID="fileBackup" runat="server" CssClass="fallo-form-control" />
            </div>

            <!-- Botones -->
            <div class="fallo-buttons text-center">
                <asp:Button ID="btnRestore" runat="server" CssClass="fallo-btn-primary" Text="Realizar Restore" OnClick="btnRestore_Click" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="fallo-btn-secondary ml-3" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>

            <!-- Mensaje de éxito o error -->
            <div class="fallo-message text-center mt-3">
                <asp:Label ID="lblMensaje" runat="server" CssClass="fallo-text-success" Visible="false"></asp:Label>
            </div>
        </div>
    </form>

    <!-- Scripts de Bootstrap -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</body>
</html>