<%@ Page Title="Backup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="GUI.Backup" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Realizar Backup</h2>
        <div class="form-group">
            <label for="txtRuta">Ruta del Backup:</label>
            <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" placeholder="Ingrese la ruta completa"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtNombre">Nombre del Backup:</label>
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
