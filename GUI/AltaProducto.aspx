﻿<%@ Page Title="AltaProducto" Language="C#" AutoEventWireup="true" CodeBehind="AltaProducto.aspx.cs" Inherits="GUI.AltaProducto" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/AltaProducto.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0; display: flex; align-items: center; justify-content: center;">
    <form id="form1" runat="server" style="display: flex; justify-content: center; align-items: center; min-height: 100vh;">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="containerEmpresas">
            <div class="content-box">
                <h2 class="text-center mb-4" runat="server" id="tituloRegistro">Alta de Producto</h2>

                <div class="form-group">
                    <label for="txtNombreProducto" runat="server" id="lblNombreProducto">Nombre del Producto:</label>
                    <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" placeholder="Ingrese el nombre" MaxLength="50"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="DropDownEmpresa" ID="lblEmpresa" CssClass="form-label">Empresa:</asp:Label>
                    <asp:DropDownList runat="server" ID="DropDownEmpresa" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label for="fileProducto" runat="server" id="lblImagenProducto">Imagen del Producto:</label>
                    <asp:FileUpload ID="fileProducto" runat="server" CssClass="form-control-file" />
                </div>

                <div class="form-group">
                    <label for="txtDescripcion" runat="server" id="lblDescripcion">Descripcion:</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ingrese una descripcion" MaxLength="50"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="DropDownTipoProducto" ID="lblTipo" CssClass="form-label">Tipo:</asp:Label>
                    <asp:DropDownList runat="server" ID="DropDownTipoProducto" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label for="txtCantidad" runat="server" id="lblCantidad">Cantidad:</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" placeholder="Ingrese la cantidad" TextMode="Number"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtPrecioUnitario" runat="server" id="lblPrecioUnitario">Precio Unitario:</label>
                    <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" placeholder="Ingrese el precio unitario" TextMode="Number"></asp:TextBox>
                </div>

                <div class="text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Registrar" CssClass="btn btn-primary mr-2" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" OnClick="btnCancel_Click" />
                </div>

                <div class="text-center mt-4">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                </div>
            </div>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const cantidadInput = document.getElementById("<%= txtCantidad.ClientID %>");
            const precioUnitarioInput = document.getElementById("<%= txtPrecioUnitario.ClientID %>");

            function validarNumeroPositivo(event) {
                if (event.target.value < 0) {
                    event.target.value = "";
                }
            }

            cantidadInput.addEventListener("input", validarNumeroPositivo);
            precioUnitarioInput.addEventListener("input", validarNumeroPositivo);
        });
    </script>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
