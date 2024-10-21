<%@ Page Title="ListadoUsuarios" Language="C#" AutoEventWireup="true" CodeBehind="ListadoUsuarios.aspx.cs" Inherits="GUI.ListadoUsuarios" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Listado de Usuarios</title>
    
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
                <h2>Listado de Usuarios</h2>

                
                   
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
                                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" />
                                    <asp:BoundField DataField="Genero" HeaderText="Género" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                    <asp:BoundField DataField="NumeroDireccion" HeaderText="Número de Dirección" />
                                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                    <asp:BoundField DataField="CodigoPostal" HeaderText="Código Postal" />
                                    <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                    <asp:BoundField DataField="Pais" HeaderText="País" />
                                    <asp:BoundField DataField="Idioma" HeaderText="Idioma" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    
            </div>

            <div id="userForm" runat="server" class="form-container" style="display: none;">
                <h3>Modificar Datos de Usuario</h3>
            
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            
                <label for="txtApellido">Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var rows = document.querySelectorAll("#<%= dataGridUsuarios.ClientID %> tbody tr");

        rows.forEach(function (row) {
            row.addEventListener("click", function () {
                var checkbox = row.querySelector("input[type='checkbox']");

                var allCheckboxes = document.querySelectorAll("#<%= dataGridUsuarios.ClientID %> input[type='checkbox']");
                allCheckboxes.forEach(function (cb) {
                    cb.checked = false;
                });

                if (checkbox) {
                    checkbox.checked = true;

                    var nombre = row.cells[1].innerText;
                    var apellido = row.cells[2].innerText;
                    var email = row.cells[3].innerText;

                    document.getElementById("<%= txtNombre.ClientID %>").value = nombre;
                    document.getElementById("<%= txtApellido.ClientID %>").value = apellido;
                    document.getElementById("<%= txtEmail.ClientID %>").value = email;

                    document.getElementById("userForm").style.display = "block";
                }
            });
        });
    });
    </script>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>