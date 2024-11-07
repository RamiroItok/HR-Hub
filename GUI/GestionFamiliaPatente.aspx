<%@ Page Title="Gestión Familia Patente" Language="C#" AutoEventWireup="true" CodeBehind="GestionFamiliaPatente.aspx.cs" Inherits="GUI.GestionFamiliaPatente" EnableEventValidation="true" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Permisos de Usuario</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; max-width: 1000px; margin: 0 auto;">
        <uc:NavBar ID="NavBar" runat="server" />

        <h1 class="title">
            <asp:Literal ID="litTituloPagina" runat="server" Text="Gestión Familia Patente"></asp:Literal>
        </h1>

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

        <h3><asp:Label ID="lblPermisosNoAsignadosTitle" runat="server" Text="Permisos no asignados"></asp:Label></h3>
        <div class="form-group">
            <asp:GridView ID="gvPermisosNoAsignados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="Id">
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

        <h3><asp:Label ID="lblPermisosAsignadosTitle" runat="server" Text="Permisos asignados"></asp:Label></h3>
        <div class="form-group">
            <asp:GridView ID="gvPermisosAsignados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="Id">
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

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function selectRow(checkbox) {
            var row = checkbox.parentElement.parentElement;
            row.style.backgroundColor = checkbox.checked ? '#c3e6cb' : '';
        }
    </script>
</body>
</html>
