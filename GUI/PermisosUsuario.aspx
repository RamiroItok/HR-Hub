<%@ Page Title="PermisosUsuario" Language="C#" AutoEventWireup="true" CodeBehind="PermisosUsuario.aspx.cs" Inherits="GUI.PermisosUsuario" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Listado de Usuarios - Permisos</title>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/PermisosUsuario.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="containerListado">
            <div class="table-container animate__animated animate__fadeIn">
                <h2>Listado de Usuarios - Permisos</h2>
        
                <div class="search-container">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar usuario..." />
                    <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="button cancel" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
        
                <asp:Label ID="lblMensaje" runat="server" CssClass="validation-message" Text=""></asp:Label>
                    <ContentTemplate>
                        <div style="overflow-x: auto; flex: 1;">
                            <asp:GridView ID="dataGridUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" GridLines="None" DataKeyNames="Id" OnRowCommand="dataGridUsuarios_RowCommand">
                                <Columns>                                    
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" />
                                    <asp:BoundField DataField="Puesto" HeaderText="Puesto" />
                                    <asp:BoundField DataField="Area" HeaderText="Área" />
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnVerMas" runat="server" CssClass="btn btn-info" Text="Ver más" CommandName="VerMas" CommandArgument='<%# Eval("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:HiddenField ID="hiddenSelectedId" runat="server" />
                    </ContentTemplate>
            </div>
        </div>

        <div class="modal fade" id="verMasModal" tabindex="-1" role="dialog" aria-labelledby="verMasModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="verMasModalLabel">Detalles del Usuario</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p><strong>ID:</strong> <span id="modalId"></span></p>
                        <p><strong>Nombre:</strong> <span id="modalNombre"></span></p>
                        <p><strong>Apellido:</strong> <span id="modalApellido"></span></p>
                        <p><strong>Email:</strong> <span id="modalEmail"></span></p>
                        <p><strong>Familia:</strong> <span id="modalFamilia"></span></p>
                        <p><strong>Permisos:</strong></p>
                        <ul id="modalPermisos"></ul>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
    </form>
</body>
</html>
