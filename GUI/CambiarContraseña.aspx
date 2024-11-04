<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="GUI.CambiarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

    <div class="container cambiar-password-container">
        <h2><asp:Literal ID="litTitulo" runat="server">Cambiar Contraseña</asp:Literal></h2>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPasswordActual" ID="lblPasswordActual" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtPasswordActual" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPasswordNueva" ID="lblPasswordNueva" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtPasswordNueva" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPasswordConfirmar" ID="lblPasswordConfirmar" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtPasswordConfirmar" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Button runat="server" ID="btnCambiar" CssClass="btn btn-primary" OnClick="btnCambiar_Click" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
