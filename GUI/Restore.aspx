<%@ Page Title="Restore" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="GUI.Restore" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Realizar Restore</h2>

        <div class="form-group">
            <label for="fileBackup">Seleccione el archivo de Backup:</label>
            <!-- Control para cargar el archivo -->
            <asp:FileUpload ID="fileBackup" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group text-center">
            <asp:Button ID="btnRestore" runat="server" CssClass="btn btn-primary" Text="Realizar Restore" OnClick="btnRestore_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary ml-3" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>

        <div class="form-group text-center mt-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>