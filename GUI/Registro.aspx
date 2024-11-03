<%@ Page Title="Registro" Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GUI.Registro" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>
<%@ Register Src="~/Controls/ValidarEmail.ascx" TagPrefix="uc" TagName="ValidarEmail" %>
<%@ Register Src="~/Controls/ValidarRegistroUsuarioDatos.ascx" TagPrefix="uc" TagName="ValidarRegistroUsuarioDatos" %>


<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registro de Usuario</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/Registro.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <link href="~/Style/NavBar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="bitacora-page">
            <div class="container-registro">
            <h2>Registro de Usuario</h2>
            <div class="filter-container">
                <div class="form-group">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" AssociatedControlID="txtNombre" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Ingrese su nombre"/>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido:" AssociatedControlID="txtApellido" />
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Placeholder="Ingrese su apellido"/>
                </div>
            
                <div class="form-group">
                    <uc:ValidarEmail runat="server" ID="ValidarEmail" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña:" AssociatedControlID="txtContraseña" />
                    <div class="input-container">
                        <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" ReadOnly="True" />
                        <asp:HiddenField ID="hiddenContraseña" runat="server" />
                        <asp:Button ID="btnGenerarPassword" runat="server" Text="Generar contraseña" CssClass="generate-password-button" OnClick="btnGenerarPassword_Click" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="DropDownArea" Text="Area:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList runat="server" ID="DropDownArea" CssClass="form-control"></asp:DropDownList>
                </div>
                        
                <div class="form-group">
                    <label for="txtFechaNac">Fecha Nacimiento:</label>
                    <div class="input-container">
                        <input type="text" ID="txtFechaNac" runat="server" CssClass="form-control" Placeholder="Ingrese su fecha de nacimiento"/>
                        
                        <button type="button" class="calendar-button" onclick="openFlatpickr('txtFechaNac')">
                            <i class="fas fa-calendar-alt"></i>
                        </button>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblGenero" runat="server" Text="Genero:" AssociatedControlID="drpGenero" />
                    <asp:DropDownList ID="drpGenero" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione un genero</asp:ListItem>
                        <asp:ListItem Value="1">Masculino</asp:ListItem>
                        <asp:ListItem Value="2">Femenino</asp:ListItem>
                        <asp:ListItem Value="3">No especifica</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                   <uc:ValidarRegistroUsuarioDatos runat="server" ID="ValidarRegistroUsuarioDatosControl" />
                </div>

                <div class="button-group">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
                </div>
            </div>
                <asp:Panel runat="server" CssClass="form-group">
                    <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>                
            </asp:Panel>

        </div>
        </div>
    </form>
<script>
    var fpFechaNac;

    document.addEventListener("DOMContentLoaded", function () {
        var today = new Date();

        var minAdultAgeDate = new Date(today.getFullYear() - 100, today.getMonth(), today.getDate());
        var maxDate18YearsAgo = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());

        fpFechaNac = flatpickr("#<%= txtFechaNac.ClientID %>", {
            enableTime: false,
            dateFormat: "Y-m-d",
            maxDate: maxDate18YearsAgo,
            minDate: minAdultAgeDate,
        });
    });

    function openFlatpickr(fieldId) {
        if (fieldId === 'txtFechaNac') {
            fpFechaNac.open();
        }
    }
</script>



    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>