<%@ Page Title="GestionPermisosUsuario" Language="C#" AutoEventWireup="true" CodeBehind="GestionPermisosUsuario.aspx.cs" Inherits="GUI.GestionPermisosUsuario" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><asp:Literal ID="litPageTitle" runat="server"></asp:Literal></title>
    
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
            <h2 class="title"><asp:Literal ID="litTitle" runat="server"></asp:Literal></h2>
            
            <div class="form-group">
                <label for="drpUsuarios" class="label"><asp:Literal ID="litSelectUserLabel" runat="server"></asp:Literal></label>
                <asp:DropDownList ID="drpUsuarios" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpUsuarios_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <asp:UpdatePanel ID="UpdatePanelNoAsignados" runat="server">
                <ContentTemplate>
                    <h4 class="subtitle"><asp:Literal ID="litUnassignedPermissionsTitle" runat="server"></asp:Literal></h4>
                    <asp:Label ID="lblNoPermisosNoAsignados" runat="server" CssClass="text-muted" Visible="false"></asp:Label>
                    <asp:GridView ID="gvPermisosNoAsignados" runat="server" CssClass="table table-permissions" AutoGenerateColumns="False" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate><input type="checkbox" id="checkAllNoAsignados" onclick="toggleCheckboxes(this, 'gvPermisosNoAsignados')" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id"  HeaderText="Id" />
                            <asp:BoundField DataField="Nombre"  HeaderText="Nombre" />
                        </Columns>
                    </asp:GridView>
                    
                    <div class="button-container">
                        <asp:Button ID="btnAsignarPermisos" runat="server" CssClass="btn btn-assign" OnClick="btnAsignarPermisos_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanelAsignados" runat="server">
                <ContentTemplate>
                    <h4 class="subtitle"><asp:Literal ID="litAssignedPermissionsTitle" runat="server"></asp:Literal></h4>
                    <asp:Label ID="lblNoPermisosAsignados" runat="server" CssClass="text-muted" Visible="false"></asp:Label>
                    <asp:GridView ID="gvPermisosAsignados" runat="server" CssClass="table table-permissions" AutoGenerateColumns="False" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate><input type="checkbox" id="checkAllAsignados" onclick="toggleCheckboxes(this, 'gvPermisosAsignados')" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id"  HeaderText="Id" />
                            <asp:BoundField DataField="Nombre"  HeaderText="Nombre" />
                        </Columns>
                    </asp:GridView>

                    <div class="button-container">
                        <asp:Button ID="btnQuitarPermisos" runat="server" CssClass="btn btn-remove" OnClick="btnQuitarPermisos_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
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

        function mostrarMensaje(tipo) {
            let titulo, texto;
            if (tipo === 'asignado') {
                titulo = mensajes.PermisosAsignadosTitulo;
                texto = mensajes.PermisosAsignadosTexto;
            } else if (tipo === 'quitado') {
                titulo = mensajes.PermisosQuitadosTitulo;
                texto = mensajes.PermisosQuitadosTexto;
            }
            Swal.fire({ icon: 'success', title: titulo, text: texto, confirmButtonColor: '#4CAF50' });
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
            prm.add_endRequest(function () { aplicarFondo(); });
        });
    </script>
</body>
</html>