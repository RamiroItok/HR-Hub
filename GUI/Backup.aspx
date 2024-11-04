<%@ Page Title="Backup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="GUI.Backup" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

    <div class="container mt-5">
        <h2 class="text-center">
            <asp:Label ID="lblTituloBackup" runat="server" Text="Realizar Backup"></asp:Label>
        </h2>

        <div class="form-group">
            <asp:Label ID="lblRutaBackup" runat="server" AssociatedControlID="txtRuta" Text="Ruta del Backup:"></asp:Label>
            <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" placeholder="Ingrese la ruta completa"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblNombreBackup" runat="server" AssociatedControlID="txtNombre" Text="Nombre del Backup:"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del backup"></asp:TextBox>
        </div>

        <div class="form-group text-center">
            <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-primary" Text="Realizar Backup" OnClick="btnBackup_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary ml-3" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
        
        <div class="form-group text-center mt-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
