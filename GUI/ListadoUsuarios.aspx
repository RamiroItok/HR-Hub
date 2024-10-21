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

        <div class="full-width-container animate__animated animate__fadeIn">
            <div class="inner-container">
                <h2>Listado de Usuarios</h2>

                <asp:GridView ID="dataGridUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" />
                        <asp:BoundField DataField="Puesto" HeaderText="Puesto" />
                        <asp:BoundField DataField="Area" HeaderText="Area" />
                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" />
                        <asp:BoundField DataField="Genero" HeaderText="Genero" />
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                        <asp:BoundField DataField="NumeroDireccion" HeaderText="Numero de Direccion" />
                        <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                        <asp:BoundField DataField="CodigoPostal" HeaderText="Codigo Postal" />
                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                        <asp:BoundField DataField="Pais" HeaderText="Pais" />
                        <asp:BoundField DataField="Idioma" HeaderText="Idioma" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
