<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GUI.Registro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Registro de Usuario</h2>
        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtNombre" Text="Nombre:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtApellido" Text="Apellido:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtContraseña" Text="Contraseña:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtContraseña" CssClass="form-control" TextMode="Password" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="DropDownPuesto" Text="Puesto:" CssClass="form-label"></asp:Label>
            <asp:DropDownList runat="server" ID="DropDownPuesto" CssClass="form-control">
                <asp:ListItem Text="Empleado" Value="Empleado"></asp:ListItem>
                <asp:ListItem Text="RRHH" Value="RRHH"></asp:ListItem>
                <asp:ListItem Text="Líder" Value="Lider"></asp:ListItem>
            </asp:DropDownList>
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="txtArea" Text="Area:" CssClass="form-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtArea" CssClass="form-control" />
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="btnRegistrar_Click"/>
        </asp:Panel>

        <asp:Panel runat="server" CssClass="form-group">
            <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>