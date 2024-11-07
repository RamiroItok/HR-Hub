<%@ Page Title="ListadoUsuarios" Language="C#" AutoEventWireup="true" CodeBehind="ListadoUsuarios.aspx.cs" Inherits="GUI.ListadoUsuarios" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><asp:Literal ID="litPageTitle" runat="server" Text="HR Hub - Listado de Usuarios"></asp:Literal></title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/ListarUsuarios.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="containerListado">
            <div class="table-container animate__animated animate__fadeIn">
                <h2><asp:Literal ID="litTitle" runat="server" Text="Listado de Usuarios"></asp:Literal></h2>

                <div class="search-container">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="" />
                    <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="button cancel" Text="" OnClick="btnCancelar_Click" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

                <div style="overflow-x: auto;">
                    <asp:GridView ID="dataGridUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" GridLines="None" AutoPostBack="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Nombre" HeaderText="" />
                            <asp:BoundField DataField="Apellido" HeaderText="" />
                            <asp:BoundField DataField="Email" HeaderText="" />
                            <asp:BoundField DataField="FechaIngreso" HeaderText="" />
                            <asp:BoundField DataField="Puesto" HeaderText="" />
                            <asp:BoundField DataField="Area" HeaderText="" />
                            <asp:BoundField DataField="FechaNacimiento" HeaderText="" />
                            <asp:BoundField DataField="Genero" HeaderText="" />
                            <asp:BoundField DataField="Direccion" HeaderText="" />
                            <asp:BoundField DataField="NumeroDireccion" HeaderText="" />
                            <asp:BoundField DataField="Departamento" HeaderText="" />
                            <asp:BoundField DataField="CodigoPostal" HeaderText="" />
                            <asp:BoundField DataField="Ciudad" HeaderText="" />
                            <asp:BoundField DataField="Provincia" HeaderText="" />
                            <asp:BoundField DataField="Pais" HeaderText="" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div id="userForm" class="form-container">
                <h3><asp:Literal ID="litFormTitle" runat="server" Text="Modificar Datos de Usuario"></asp:Literal></h3>
                <asp:Label ID="lblMensajeModificacion" runat="server" CssClass="validation-message-failed" Text="" ></asp:Label>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtNombre"><asp:Literal ID="litNombreLabel" runat="server" Text="Nombre"></asp:Literal>:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenNombre" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellido"><asp:Literal ID="litApellidoLabel" runat="server" Text="Apellido"></asp:Literal>:</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenApellido" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtEmail"><asp:Literal ID="litEmailLabel" runat="server" Text="Email"></asp:Literal>:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenEmail" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtFechaIngreso"><asp:Literal ID="litFechaIngresoLabel" runat="server" Text="Fecha de Ingreso"></asp:Literal>:</label>
                        <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenFechaIngreso" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="DropDownPuesto" ID="litPuestoLabel" Text="Puesto" CssClass="form-label"></asp:Label>
                        <asp:DropDownList runat="server" ID="DropDownPuesto" CssClass="form-control"></asp:DropDownList>
                        <asp:HiddenField ID="hiddenDropDownPuesto" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="DropDownArea" ID="litAreaLabel" Text="Área" CssClass="form-label"></asp:Label>
                        <asp:DropDownList runat="server" ID="DropDownArea" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtFechaNacimiento"><asp:Literal ID="litFechaNacimientoLabel" runat="server" Text="Fecha de Nacimiento"></asp:Literal>:</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenFechaNacimiento" runat="server" />
                    </div>
                    <div class="form-group">
                    <asp:Label ID="lblGenero" runat="server" AssociatedControlID="drpGenero" />
                    <asp:DropDownList ID="drpGenero" runat="server" CssClass="form-control" />
                </div>
                </div>

               <div class="form-row">
                    <div class="form-group">
                        <label for="txtDireccion"><asp:Literal ID="litDireccionLabel" runat="server" Text="Dirección"></asp:Literal>:</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>
                    <div class="form-group">
                        <label for="txtNumeroDireccion"><asp:Literal ID="litNumeroDireccionLabel" runat="server" Text="Número de Dirección"></asp:Literal>:</label>
                        <asp:TextBox ID="txtNumeroDireccion" runat="server" CssClass="form-control" onkeypress="return soloNumeros(event);" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtDepartamento"><asp:Literal ID="litDepartamentoLabel" runat="server" Text="Departamento"></asp:Literal>:</label>
                        <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" onkeypress="return letrasYNumeros(event);" />
                    </div>
                    <div class="form-group">
                        <label for="txtCodigoPostal"><asp:Literal ID="litCodigoPostalLabel" runat="server" Text="Código Postal"></asp:Literal>:</label>
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" onkeypress="return letrasYNumeros(event);" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtCiudad"><asp:Literal ID="litCiudadLabel" runat="server" Text="Ciudad"></asp:Literal>:</label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>
                    <div class="form-group">
                        <label for="txtProvincia"><asp:Literal ID="litProvinciaLabel" runat="server" Text="Provincia"></asp:Literal>:</label>
                        <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtPais"><asp:Literal ID="litPaisLabel" runat="server" Text="País"></asp:Literal>:</label>
                        <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" onkeypress="return soloLetras(event);" />
                    </div>
                </div>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelarModificacion" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarModificacion_Click" />
            </div>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var rows = document.querySelectorAll("#<%= dataGridUsuarios.ClientID %> tbody tr");

            rows.forEach(function (row) {
                row.addEventListener("click", function () {
                    rows.forEach(function (r) {
                        var checkbox = r.querySelector("input[type='checkbox']");
                        if (checkbox) checkbox.checked = false;
                        r.classList.remove("selected-row");
                    });

                    var checkbox = row.querySelector("input[type='checkbox']");
                    if (checkbox) checkbox.checked = true;

                    row.classList.add("selected-row");

                    var nombre = row.cells[1].innerText;
                    var apellido = row.cells[2].innerText;
                    var email = row.cells[3].innerText;
                    var fechaIngreso = row.cells[4].innerText;
                    var puesto = row.cells[5].innerText;
                    var area = row.cells[6].innerText;
                    var fechaNacimiento = row.cells[7].innerText;
                    var genero = row.cells[8].innerText;
                    var direccion = row.cells[9].innerText;
                    var numeroDireccion = row.cells[10].innerText;
                    var departamento = row.cells[11].innerText;
                    var codigoPostal = row.cells[12].innerText;
                    var ciudad = row.cells[13].innerText;
                    var provincia = row.cells[14].innerText;
                    var pais = row.cells[15].innerText;

                    document.getElementById("<%= txtNombre.ClientID %>").value = nombre;
                    document.getElementById("<%= txtApellido.ClientID %>").value = apellido;
                    document.getElementById("<%= txtEmail.ClientID %>").value = email;
                    document.getElementById("<%= txtFechaIngreso.ClientID %>").value = fechaIngreso;
                    document.getElementById("<%= txtFechaNacimiento.ClientID %>").value = fechaNacimiento;
                    document.getElementById("<%= txtDireccion.ClientID %>").value = direccion;
                    document.getElementById("<%= txtNumeroDireccion.ClientID %>").value = numeroDireccion;
                    document.getElementById("<%= txtDepartamento.ClientID %>").value = departamento;
                    document.getElementById("<%= txtCodigoPostal.ClientID %>").value = codigoPostal;
                    document.getElementById("<%= txtCiudad.ClientID %>").value = ciudad;
                    document.getElementById("<%= txtProvincia.ClientID %>").value = provincia;
                    document.getElementById("<%= txtPais.ClientID %>").value = pais;
                    document.getElementById("<%= hiddenNombre.ClientID %>").value = nombre;
                    document.getElementById("<%= hiddenApellido.ClientID %>").value = apellido;
                    document.getElementById("<%= hiddenEmail.ClientID %>").value = email;
                    document.getElementById("<%= hiddenFechaIngreso.ClientID %>").value = fechaIngreso;
                    document.getElementById("<%= hiddenFechaNacimiento.ClientID %>").value = fechaNacimiento;

                    var dropDownGenero = document.getElementById("<%= drpGenero.ClientID %>");
                    for (var i = 0; i < dropDownGenero.options.length; i++) {
                        if (dropDownGenero.options[i].text === genero) {
                            dropDownGenero.selectedIndex = i;
                            break;
                        }
                    }

                    var dropDownPuesto = document.getElementById("<%= DropDownPuesto.ClientID %>");
                    var hiddenField = document.getElementById("<%= hiddenDropDownPuesto.ClientID %>");

                    for (var i = 0; i < dropDownPuesto.options.length; i++) {
                        if (dropDownPuesto.options[i].text === puesto) {
                            dropDownPuesto.selectedIndex = i;
                            hiddenField.value = dropDownPuesto.options[i].value;
                            break;
                        }
                    }
    
                    var dropDownArea = document.getElementById("<%= DropDownArea.ClientID %>");
                    for (var i = 0; i < dropDownArea.options.length; i++) {
                        if (dropDownArea.options[i].text === area) {
                            dropDownArea.selectedIndex = i;
                            break;
                        }
                    }
                });
            });
        });
    </script>

    <script type="text/javascript">
        function soloLetras(e) {
            var key = e.keyCode || e.which;
            var tecla = String.fromCharCode(key).toLowerCase();
            var letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
            var especiales = "8-37-39-46";

            var tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

        function letrasYNumeros(e) {
            var key = e.keyCode || e.which;
            var tecla = String.fromCharCode(key).toLowerCase();
            var letrasYNumeros = " áéíóúabcdefghijklmnñopqrstuvwxyz0123456789";
            var especiales = "8-37-39-46";

            var tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letrasYNumeros.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

        function soloNumeros(e) {
            var key = e.keyCode || e.which;
            var tecla = String.fromCharCode(key);
            var numeros = "0123456789";
            var especiales = "8-37-39-46";

            var tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (numeros.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
    </script>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>