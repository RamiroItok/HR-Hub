<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavBar.ascx.cs" Inherits="GUI.Controls.NavBar" %>

<header class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <asp:LinkButton ID="btnInicio" class="navbar-brand" runat="server" OnClick="btnInicio_Click">
                <asp:Literal ID="litHRHub" runat="server">HR Hub</asp:Literal>
            </asp:LinkButton>
        </div>
        <div class="navbar-collapse collapse">
            <ul class="nav navbar-nav">
                <li class="dropdown" id="registroLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <asp:Literal ID="litRegistro" runat="server">Registro</asp:Literal><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a id="listarUsuariosLink" runat="server" href="~/ListadoUsuarios"><asp:Literal ID="litListarUsuarios" runat="server">Lista de usuarios</asp:Literal></a></li>
                        <li><a id="registroUsuarioLink" runat="server" href="~/Registro"><asp:Literal ID="litRegistrarUsuario" runat="server">Registrar usuario</asp:Literal></a></li>
                    </ul>
                </li>
                
                <li class="dropdown" id="seguridadLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <asp:Literal ID="litSeguridad" runat="server">Seguridad</asp:Literal><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a id="gestionFamiliaLink" runat="server" visible="false" href="~/GestionFamilia"><asp:Literal ID="litGestionFamilia" runat="server">Gestión de Familia</asp:Literal></a></li>
                        <li><a id="gestionFamiliaPatenteLink" runat="server" visible="false" href="~/GestionFamiliaPatente"><asp:Literal ID="litGestionFamiliaPatente" runat="server">Gestión de Familia Patente</asp:Literal></a></li>
                        <li><a id="gestionPermisosUsuarioLink" runat="server" visible="false" href="~/GestionPermisosUsuario"><asp:Literal ID="litGestionPermisosUsuario" runat="server">Gestión de Permisos de Usuario</asp:Literal></a></li>
                        <li><a id="permisosUsuarioLink" runat="server" visible="false" href="~/PermisosUsuario"><asp:Literal ID="litPermisosUsuario" runat="server">Permisos Usuarios</asp:Literal></a></li>
                        <li><a id="backUpLink" runat="server" visible="false" href="~/Backup"><asp:Literal ID="litBackup" runat="server">Backup</asp:Literal></a></li>
                        <li><a id="restoreLink" runat="server" visible="false" href="~/Restore"><asp:Literal ID="litRestore" runat="server">Restore</asp:Literal></a></li>
                        <li><a id="desbloquearUsuariosLink" runat="server" visible="false" href="~/DesbloquearUsuario"><asp:Literal ID="litDesbloquearUsuarios" runat="server">Usuarios Bloqueados</asp:Literal></a></li>
                        <li><a id="falloIntegridadSeguridadLink" runat="server" visible="false" href="~/FalloIntegridad"><asp:Literal ID="litFalloIntegridad" runat="server">Fallo de integridad</asp:Literal></a></li>
                    </ul>
                </li>

                <li class="dropdown" id="documentosLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <asp:Literal ID="litDocumentos" runat="server">Documentos</asp:Literal><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a id="cargaDocumentosLink" runat="server" visible="false" href="~/CargaDocumento"><asp:Literal ID="litCargaDocumentos" runat="server">Carga de documentos</asp:Literal></a></li>
                    </ul>
                </li>
                
                <li class="dropdown" id="configuracionEmpresaLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <asp:Literal ID="litConfigEmpresa" runat="server">Configuración Empresas</asp:Literal><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a runat="server" href="~/AltaEmpresa"><asp:Literal ID="litAltaEmpresa" runat="server">Alta de Empresa</asp:Literal></a></li>
                        <li><a runat="server" href="~/ListadoEmpresas"><asp:Literal ID="litListadoEmpresas" runat="server">Listado de Empresas</asp:Literal></a></li>
                    </ul>
                </li>
                
                <li class="dropdown" id="configuracionProductosLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <asp:Literal ID="litConfigProductos" runat="server">Configuración Productos</asp:Literal><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a runat="server" href="~/AltaProducto"><asp:Literal ID="litAltaProducto" runat="server">Alta de Producto</asp:Literal></a></li>
                        <li><a runat="server" href="~/ListadoProductos"><asp:Literal ID="litListadoProductos" runat="server">Listado de Productos</asp:Literal></a></li>
                    </ul>
                </li>
                
                <li class="dropdown" id="reportesLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <asp:Literal ID="litReportes" runat="server">Reportes</asp:Literal><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a id="reporteComprasLink" runat="server" href="~/ReporteCompras"><asp:Literal ID="litReporteCompras" runat="server">Reporte de compras</asp:Literal></a></li>
                    </ul>
                </li>

                <li><a runat="server" id="falloIntegridadLink" visible="false" href="~/FalloIntegridad"><asp:Literal ID="litFalloIntegridad1" runat="server">Fallo de integridad</asp:Literal></a></li>
                <li><a runat="server" id="bitacoraLink" visible="false" href="~/Bitacora"><asp:Literal ID="litBitacora" runat="server">Bitácora</asp:Literal></a></li>
                <li><a runat="server" id="productosLink" visible="false" href="~/Productos"><asp:Literal ID="litProductos" runat="server">Productos</asp:Literal></a></li>
                <li><a runat="server" id="carritoLink" visible="false" href="~/Carrito"><asp:Literal ID="litCarrito" runat="server">Carrito</asp:Literal></a></li>
                <li><a runat="server" id="contactoLink" href="~/Contact"><asp:Literal ID="litContacto" runat="server">Contacto</asp:Literal></a></li>
            </ul>

            <ul class="nav navbar-nav navbar-right">                
                <li class="dropdown" id="miCuentaLink" runat="server" visible="false">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><asp:Literal ID="litMiCuenta" runat="server">Mi cuenta</asp:Literal><b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a runat="server" id="misComprasLink" visible="false" href="~/MisCompras"><asp:Literal ID="litMisCompras" runat="server">Mis compras</asp:Literal></a></li>
                        <li><a runat="server" id="misDocumentosLink" visible="false" href="~/MisDocumentos"><asp:Literal ID="litMisDocumentos" runat="server">Mis documentos</asp:Literal></a></li>
                        <li><a runat="server" href="~/CambiarContraseña"><asp:Literal ID="litCambiarContraseña" runat="server">Cambiar contraseña</asp:Literal></a></li>
                    </ul>
                </li>

                <li id="loginLink" runat="server"><a href="Login.aspx"><asp:Literal ID="litLogin" runat="server">Login</asp:Literal></a></li>
                <li id="logoutLink" runat="server">
                    <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click"><asp:Literal ID="litLogout" runat="server">Cerrar Sesión</asp:Literal></asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>
</header>