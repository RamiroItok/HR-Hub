<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavBar.ascx.cs" Inherits="GUI.Controls.NavBar" %>

<header class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <asp:LinkButton ID="btnInicio" class="navbar-brand" runat="server" OnClick="btnInicio_Click">HR Hub</asp:LinkButton>
        </div>
        <div class="navbar-collapse collapse">
            <ul class="nav navbar-nav">
                <li class="dropdown" visible="false" id="registroLink" runat="server">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Registro <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a runat="server" href="~/ListarUsuarios">Lista de usuarios</a></li>
                        <li><a runat="server" href="~/Registro">Registrar usuario</a></li>
                    </ul>
                </li>
                <li class="dropdown" id="seguridadLink" runat="server">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Seguridad <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a id="gestionFamiliaLink" visible="false" runat="server" href="~/GestionFamilia">Gestión de Familia</a></li>
                        <li><a id="gestionFamiliaPatenteLink" visible="false" runat="server" href="~/GestionFamiliaPatente">Gestión de Familia Patente</a></li>
                        <li><a id="backUpLink" visible="false" runat="server" href="~/Backup">Backup</a></li>
                        <li><a id="restoreLink" visible="false" runat="server" href="~/Restore">Restore</a></li>
                        <li><a runat="server" id="falloIntegridadSeguridadLink" visible="false" href="~/FalloIntegridad">Fallo de integridad</a></li>
                    </ul>
                </li>
                <li><a runat="server" id="falloIntegridadLink" visible="false" href="~/FalloIntegridad">Fallo de integridad</a></li>
                <li><a runat="server" id="bitacoraLink" visible="false" href="~/Bitacora">Bitacora</a></li>
                <li><a runat="server" id="contactoLink" href="~/Contact">Contacto</a></li>
            </ul>
            
            <ul class="nav navbar-nav navbar-right">
                <li class="dropdown" id="miCuentaLink" runat="server">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Mi cuenta <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a runat="server" href="~/MisDatos">Mis datos</a></li>
                        <li><a runat="server" href="~/CambiarContraseña">Cambiar contraseña</a></li>
                    </ul>
                </li>
                <li id="loginLink" runat="server"><a href="Login.aspx">Login</a></li>
                <li id="logoutLink" runat="server"><asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Cerrar Sesión</asp:LinkButton></li>
            </ul>
        </div>
    </div>
</header>