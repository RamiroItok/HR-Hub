<%@ Page Title="GestionPermisosUsuario" Language="C#" AutoEventWireup="true" CodeBehind="GestionPermisosUsuario.aspx.cs" Inherits="GUI.GestionPermisosUsuario" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Permisos de Usuario</title>
    
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/GestionPermisosUsuario.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; max-width: 1000px; margin: 0 auto;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <uc:NavBar ID="NavBar" runat="server" />

        <div class="container-permissions">
            <h2 class="title">Gestión de Permisos de Usuario</h2>
            
            <div class="form-group">
                <label for="drpUsuarios" class="label">Seleccionar Usuario:</label>
                <asp:DropDownList ID="drpUsuarios" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpUsuarios_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <!-- Tabla de Permisos No Asignados -->
            <asp:UpdatePanel ID="UpdatePanelNoAsignados" runat="server">
                <ContentTemplate>
                    <h4 class="subtitle">Permisos No Asignados</h4>
                    <asp:Label ID="lblNoPermisosNoAsignados" runat="server" Text="No hay permisos no asignados para este usuario." Visible="false" CssClass="text-muted"></asp:Label>
                    <asp:GridView ID="gvPermisosNoAsignados" runat="server" CssClass="table table-permissions" AutoGenerateColumns="False" DataKeyNames="Id">
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
                    
                    <div class="button-container">
                        <asp:Button ID="btnAsignarPermisos" runat="server" Text="Asignar Permisos" CssClass="btn btn-assign" OnClick="btnAsignarPermisos_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Tabla de Permisos Asignados -->
            <asp:UpdatePanel ID="UpdatePanelAsignados" runat="server">
                <ContentTemplate>
                    <h4 class="subtitle">Permisos Asignados</h4>
                    <asp:Label ID="lblNoPermisosAsignados" runat="server" Text="No hay permisos asignados a este usuario." Visible="false" CssClass="text-muted"></asp:Label>
                    <asp:GridView ID="gvPermisosAsignados" runat="server" CssClass="table table-permissions" AutoGenerateColumns="False" DataKeyNames="Id">
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

                    <div class="button-container">
                        <asp:Button ID="btnQuitarPermisos" runat="server" Text="Quitar Permisos" CssClass="btn btn-remove" OnClick="btnQuitarPermisos_Click" />
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

        // Función para mostrar el mensaje de éxito usando SweetAlert
        function mostrarMensaje(tipo) {
            if (tipo === 'asignado') {
                Swal.fire({
                    icon: 'success',
                    title: 'Permisos asignados correctamente',
                    text: 'Los permisos seleccionados se asignaron con éxito.',
                    confirmButtonColor: '#4CAF50'
                });
            } else if (tipo === 'quitado') {
                Swal.fire({
                    icon: 'success',
                    title: 'Permisos quitados correctamente',
                    text: 'Los permisos seleccionados se quitaron con éxito.',
                    confirmButtonColor: '#f44336'
                });
            }
        }

        function aplicarFondo() {
            document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('<%= ResolveUrl("~/Content/imagenes/Fondo.jpg") %>')";
            document.body.style.backgroundSize = "cover";
            document.body.style.backgroundPosition = "center";
            document.body.style.backgroundAttachment = "fixed";
        }

        document.addEventListener("DOMContentLoaded", function () {
            aplicarFondo();

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                aplicarFondo();
            });
        });
    </script>
</body>
</html>
