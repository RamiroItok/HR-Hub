<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaDocumento.aspx.cs" Inherits="GUI.CargaDocumento" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>
        <asp:Literal ID="litPageTitle" runat="server" Text="HR Hub - Cargar Documento"></asp:Literal>
    </title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="~/Style/CargaDocumento.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0;">
    <form id="form1" runat="server" style="display: flex; justify-content: center; align-items: center; min-height: 100vh;">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <div class="card text-center animate__animated animate__fadeInUp" style="padding: 20px; background-color: rgba(255, 255, 255, 0.9); width: 400px; border-radius: 8px;">
            <h2 class="card-title" id="lblTitulo" style="margin-bottom: 20px;">
                <asp:Label ID="lblTitulo" runat="server" Text="Cargar documento"></asp:Label>
            </h2>

            <div class="form-group">
                <asp:Label ID="lblNombreArchivo" runat="server" AssociatedControlID="txtNombreArchivo" Text="Nombre del archivo"></asp:Label>
                <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del archivo"></asp:TextBox>
            </div>

            <div class="form-group" style="margin-top: 20px;">
                <asp:Label ID="lblArchivo" runat="server" AssociatedControlID="fileUpload" Text="Archivo"></asp:Label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" />
            </div>

            <div class="form-group" style="margin-top: 20px;">
                <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-primary" Text="Cargar archivo" OnClick="BtnCargar_Click" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary ml-3" Text="Cancelar" OnClick="BtnCancelar_Click" />
            </div>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script>
        function mostrarPopup(mensaje, tipo) {
            let titulo = tipo === 'error' ? 'Error' : 'Confirmación';
            let icono = tipo === 'error' ? 'error' : 'success';

            Swal.fire({
                title: titulo,
                text: mensaje,
                icon: icono,
                confirmButtonText: 'OK'
            });
        }

        <% if (Session["PopupMessage"] != null && Session["PopupType"] != null) { %>
        mostrarPopup('<%= Session["PopupMessage"] %>', '<%= Session["PopupType"] %>');
            <% Session.Remove("PopupMessage"); %>
            <% Session.Remove("PopupType"); %>
        <% } %>
</script>
</body>
</html>