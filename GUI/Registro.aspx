<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GUI.Registro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center">Registrar usuario</h2>
    <p class="text-center">Esta parte de la página es para registrarse como usuario</p>
    <p class="text-center">Nombre:
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </p>
    <p class="text-center">Apellido:
        <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
    </p>
    <p class="text-center">Email:
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    </p>
    <p class="text-center">Contraseña:
        <asp:TextBox ID="txtContraseña" runat="server"></asp:TextBox>
    </p>
    <p class="text-center">Puesto:
        <asp:DropDownList ID="DropDownPuesto" runat="server">
        </asp:DropDownList>
    </p>
    <p class="text-center">
        <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" Text="Registrarse" />
    </p>
    <p class="text-center">
        &nbsp;</p>
    <p class="text-center">
        <asp:Label ID="lblResultado" runat="server"></asp:Label>
    </p>
</asp:Content>
