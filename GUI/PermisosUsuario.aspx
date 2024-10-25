<%@ Page Title="PermisosUsuario" Language="C#" AutoEventWireup="true" CodeBehind="PermisosUsuario.aspx.cs" Inherits="GUI.PermisosUsuario" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Listado de Usuarios - Permisos</title>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/ListarUsuarios.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="containerListado">
            <div class="table-container animate__animated animate__fadeIn">
                <h2>Listado de Usuarios - Permisos</h2>

                <div class="search-container">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar usuario..." />
                    <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="button cancel" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="validation-message" Text="" ></asp:Label>

                <div style="overflow-x: auto;">
                    <asp:GridView ID="dataGridUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" GridLines="None" AutoPostBack="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" />
                            <asp:BoundField DataField="Puesto" HeaderText="Puesto" />
                            <asp:BoundField DataField="Area" HeaderText="Área" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div id="userForm" class="form-container">
                <h3>Modificar Datos de Usuario</h3>
                <asp:Label ID="lblMensajeModificacion" runat="server" CssClass="validation-message-failed" Text="" ></asp:Label>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtNombre">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenNombre" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellido">Apellido:</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenApellido" runat="server" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtEmail">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hiddenEmail" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="DropDownPuesto" Text="Puesto:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList runat="server" ID="DropDownPuesto" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelarModificacion" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarModificacion_Click" />
            </div>
        </div>
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
                    var puesto = row.cells[4].innerText;

                    document.getElementById("<%= txtNombre.ClientID %>").value = nombre;
                    document.getElementById("<%= txtApellido.ClientID %>").value = apellido;
                    document.getElementById("<%= txtEmail.ClientID %>").value = email;

                    document.getElementById("<%= hiddenNombre.ClientID %>").value = nombre;
                    document.getElementById("<%= hiddenApellido.ClientID %>").value = apellido;
                    document.getElementById("<%= hiddenEmail.ClientID %>").value = email;

                    var dropDownPuesto = document.getElementById("<%= DropDownPuesto.ClientID %>");
                    for (var i = 0; i < dropDownPuesto.options.length; i++) {
                        if (dropDownPuesto.options[i].text === area) {
                            dropDownPuesto.selectedIndex = i;
                            break;
                        }
                    }
                });
            });
        });
    </script>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
