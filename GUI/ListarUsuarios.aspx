<%@ Page Title="Lista de usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="GUI.ListarUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/Content/ListarUsuarios.css" rel="stylesheet" type="text/css" />

    <div class="full-width-container">
        <div class="inner-container">
            <h2>Lista de Usuarios</h2>
            <asp:GridView ID="dataGridUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Puesto" HeaderText="Puesto" SortExpression="Puesto" />
                    <asp:BoundField DataField="Area" HeaderText="Area" SortExpression="Area" />
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" SortExpression="FechaIngreso" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
