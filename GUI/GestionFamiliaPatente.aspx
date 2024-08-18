<%@ Page Title="Gestión Familia Patente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionFamiliaPatente.aspx.cs" Inherits="GUI.GestionFamiliaPatente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Gestión Familia Patente</h2>
        <div class="form-group">
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="lblFamilia1" runat="server" Text="Familias" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="cmb_Familia1" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                <asp:ListItem Text="Selecciona una opción..." Value="" />
            </asp:DropDownList>
            <asp:Button ID="btn_Listar" runat="server" Text="Listar" CssClass="btn btn-primary" OnClick="btn_Listar_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblPatentes" runat="server" Text="Patentes" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="cmb_Patentes" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                <asp:ListItem Text="Selecciona una opción..." Value="" />
            </asp:DropDownList>
            <asp:Button ID="btn_AgregarPatente" runat="server" Text="Agregar Patente" CssClass="btn btn-primary" OnClick="btn_AgregarPatente_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblFamilia2" runat="server" Text="Familias para agregar" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="cmb_Familia2" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                <asp:ListItem Text="Selecciona una opción..." Value="" />
            </asp:DropDownList>
            <asp:Button ID="btn_AgregarFamilia" runat="server" Text="Agregar Familia" CssClass="btn btn-primary" OnClick="btn_AgregarFamilia_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblTreeView" runat="server" Text="Árbol de Patentes/Familias" CssClass="form-label"></asp:Label>
            <asp:TreeView ID="treePatenteFamilia" runat="server" CssClass="treeview-control">
            </asp:TreeView>
        </div>

        <div class="form-group">
            <asp:Button ID="btn_Guardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btn_Guardar_Click" />
            <asp:Button ID="btn_EliminarPatente" runat="server" Text="Eliminar Patente" CssClass="btn btn-danger" OnClick="btn_EliminarPatente_Click" />
            <asp:Button ID="btn_Cancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btn_Cancelar_Click" />
        </div>
    </div>
</asp:Content>
