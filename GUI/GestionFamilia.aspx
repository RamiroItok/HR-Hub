<%@ Page Title="Gestión de Familias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionFamilia.aspx.cs" Inherits="GUI.GestionFamilia" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblNombreFamilia" runat="server" Text="Nombre de familia"></asp:Label>
                <asp:TextBox ID="txtFamilia" runat="server" CssClass="form-control" />
                <br />
                <asp:Button ID="btnAceptar" runat="server" Text="Alta" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClick="btnCancelar_Click" />
                <br />
            </div>

            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            </div>

            <div class="col-md-8">
                <h3>Listado de familias</h3>
                <asp:GridView ID="gridFamilia" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" Width="70%">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Hijos" HeaderText="Hijos" Visible="False" />
                        <asp:BoundField DataField="Permiso" HeaderText="Permiso" Visible="False" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
