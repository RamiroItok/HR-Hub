<%@ Page Title="GestionPermisosUsuario" Language="C#" AutoEventWireup="true" CodeBehind="GestionPermisosUsuario.aspx.cs" Inherits="GUI.GestionPermisosUsuario" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Permisos de Usuario</title>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/GestionPermisosUsuario.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <uc:NavBar ID="NavBar" runat="server" />

        <div class="container mt-5">
            <h2>Gestión de Permisos de Usuario</h2>
            
            <div class="form-group">
                <label for="drpUsuarios">Seleccionar Usuario:</label>
                <asp:DropDownList ID="drpUsuarios" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpUsuarios_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <!-- Tabla de Permisos No Asignados -->
            <asp:UpdatePanel ID="UpdatePanelNoAsignados" runat="server">
                <ContentTemplate>
                    <h4>Permisos No Asignados</h4>
                    <asp:Label ID="lblNoPermisosNoAsignados" runat="server" Text="No hay permisos no asignados para este usuario." Visible="false" CssClass="text-muted"></asp:Label>
                    <asp:GridView ID="gvPermisosNoAsignados" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate><input type="checkbox" id="checkAllNoAsignados" onclick="toggleCheckboxes(this, 'gvPermisosNoAsignados')" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        </Columns>
                    </asp:GridView>
                    
                    <div class="text-right mt-3">
                        <asp:Button ID="btnAsignarPermisos" runat="server" Text="Asignar Permisos" CssClass="btn btn-success" OnClick="btnAsignarPermisos_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Tabla de Permisos Asignados -->
            <asp:UpdatePanel ID="UpdatePanelAsignados" runat="server">
                <ContentTemplate>
                    <h4>Permisos Asignados</h4>
                    <asp:Label ID="lblNoPermisosAsignados" runat="server" Text="No hay permisos asignados a este usuario." Visible="false" CssClass="text-muted"></asp:Label>
                    <asp:GridView ID="gvPermisosAsignados" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate><input type="checkbox" id="checkAllAsignados" onclick="toggleCheckboxes(this, 'gvPermisosAsignados')" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        </Columns>
                    </asp:GridView>

                    <div class="text-right mt-3">
                        <asp:Button ID="btnQuitarPermisos" runat="server" Text="Quitar Permisos" CssClass="btn btn-danger" OnClick="btnQuitarPermisos_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        function toggleCheckboxes(source, gridId) {
            const checkboxes = document.querySelectorAll(`#${gridId} input[type="checkbox"]`);
            for (const checkbox of checkboxes) {
                checkbox.checked = source.checked;
            }
        }
    </script>
</body>
</html>