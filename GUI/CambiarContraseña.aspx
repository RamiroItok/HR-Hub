<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="GUI.CambiarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container cambiar-password-container">
        <h2>Cambiar Contraseña</h2>
        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPasswordActual" Text="Contraseña Actual:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtPasswordActual" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPasswordNueva" Text="Nueva Contraseña:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtPasswordNueva" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPasswordConfirmar" Text="Confirmar Nueva Contraseña:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtPasswordConfirmar" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Button runat="server" ID="btnCambiar" Text="Cambiar Contraseña" CssClass="btn btn-primary" OnClick="btnCambiar_Click" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
