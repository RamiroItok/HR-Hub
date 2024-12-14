<%@ Page Title="Mi Cuenta" Language="C#" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="GUI.MiCuenta" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Mi Cuenta</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/MiCuenta.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body style="background-image: url('Content/imagenes/Fondo.jpg'); background-size: cover; background-repeat: no-repeat; background-attachment: fixed; background-position: center center;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

            <div id="userForm" class="form-container">
                <h3><asp:Literal ID="litFormTitle" runat="server" Text="Modificar Datos de Usuario"></asp:Literal></h3>
                <asp:Label ID="lblMensajeModificacion" runat="server" CssClass="validation-message-failed" Text="" ></asp:Label>

                <asp:Panel runat="server" CssClass="form-group">
                    <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
                </asp:Panel>

                    <div class="form-group">
                        <label for="txtNombre"><asp:Literal ID="litNombreLabel" runat="server" Text="Nombre"></asp:Literal>:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellido"><asp:Literal ID="litApellidoLabel" runat="server" Text="Apellido"></asp:Literal>:</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblGenero" runat="server" AssociatedControlID="drpGenero" />
                        <asp:DropDownList ID="drpGenero" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label for="txtDireccion"><asp:Literal ID="litDireccionLabel" runat="server" Text="Dirección"></asp:Literal>:</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>
                    <div class="form-group">
                        <label for="txtNumeroDireccion"><asp:Literal ID="litNumeroDireccionLabel" runat="server" Text="Número de Dirección"></asp:Literal>:</label>
                        <asp:TextBox ID="txtNumeroDireccion" runat="server" CssClass="form-control" onkeypress="return soloNumeros(event);" />
                    </div>

                    <div class="form-group">
                        <label for="txtDepartamento"><asp:Literal ID="litDepartamentoLabel" runat="server" Text="Departamento"></asp:Literal>:</label>
                        <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" onkeypress="return letrasYNumeros(event);" />
                    </div>
                    <div class="form-group">
                        <label for="txtCodigoPostal"><asp:Literal ID="litCodigoPostalLabel" runat="server" Text="Código Postal"></asp:Literal>:</label>
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" onkeypress="return letrasYNumeros(event);" />
                    </div>

                    <div class="form-group">
                        <label for="txtCiudad"><asp:Literal ID="litCiudadLabel" runat="server" Text="Ciudad"></asp:Literal>:</label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>
                    <div class="form-group">
                        <label for="txtProvincia"><asp:Literal ID="litProvinciaLabel" runat="server" Text="Provincia"></asp:Literal>:</label>
                        <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>

                    <div class="form-group">
                        <label for="txtPais"><asp:Literal ID="litPaisLabel" runat="server" Text="País"></asp:Literal>:</label>
                        <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelarModificacion" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarModificacion_Click" />
            </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
    </form>
</body>
</html>