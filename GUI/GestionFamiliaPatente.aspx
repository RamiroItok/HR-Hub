<%@ Page Title="Gestión Familia Patente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionFamiliaPatente.aspx.cs" Inherits="GUI.GestionFamiliaPatente" EnableEventValidation="false" %>

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

        <h3>Permisos no asignados</h3>
        <div class="form-group">
            <asp:GridView ID="gvPermisosNoAsignados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" EmptyDataText="No hay permisos disponibles para asignar." DataKeyNames="Id">
                <Columns>
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="selectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Permiso" HeaderText="Permiso" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="form-group">
            <asp:Button ID="btn_AsignarPermiso" runat="server" Text="Asignar Permiso a Familia" CssClass="btn btn-success" OnClick="btn_AsignarPermiso_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblMensajeAsignacion" runat="server" CssClass="text-success"></asp:Label>
        </div>

        <h3>Permisos asignados</h3>
        <div class="form-group">
            <asp:GridView ID="gvPermisosAsignados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" EmptyDataText="No hay permisos disponibles para asignar." DataKeyNames="Id">
                <Columns>
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSeleccionar2" runat="server" onclick="selectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Permiso" HeaderText="Permiso" ItemStyle-Width="200px" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="form-group">
            <asp:Button ID="btn_EliminarPatente" runat="server" Text="Eliminar Patente" CssClass="btn btn-danger" OnClick="btn_EliminarPatente_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblMensajeEliminar" runat="server" CssClass="text-success"></asp:Label>
        </div>

        <script type="text/javascript">
            function selectRow(checkbox) {
                var row = checkbox.parentElement.parentElement;

                if (checkbox.checked) {
                    row.style.backgroundColor = '#c3e6cb';
                } else {
                    row.style.backgroundColor = '';
                }
            }
        </script>
    </div>
</asp:Content>
