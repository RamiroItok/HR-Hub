<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GUI.Registro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center">Registrar usuario</h2>

    <div class="form-group">
        <label for="nombre">Nombre</label>
        <input type="nombre" id="txtNombre" name="nombre" required runat="server">
    </div>
    <div class="form-group">
        <label for="apellido">Apellido</label>
        <input type="apellido" id="txtApellido" name="apellido" required runat="server">
    </div>
    <div class="form-group">
        <label for="email">Email</label>
        <input type="email" id="txtEmail" name="email" required runat="server">
    </div>
    <div class="form-group">
        <label for="contraseña">Contraseña</label>
        <input type="contraseña" id="txtContraseña" name="contraseña" required runat="server">
    </div>
    <p class="text-center">Puesto:
        <asp:DropDownList ID="DropDownPuesto" runat="server">
        </asp:DropDownList>
    </p>
    <asp:Button type="submit" id="btnRegistrar" class="submit-btn" runat="server" Text="Registrar" OnClick="btnRegistrar_Click"></asp:Button>
    <p class="text-center">
        &nbsp;</p>
    <p class="text-center">
        <asp:Label ID="lblResultado" runat="server"></asp:Label>
    </p>
</asp:Content>
