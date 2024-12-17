<%@ Page Title="Gestión de Familias" Language="C#" AutoEventWireup="true" CodeBehind="GestionFamilia.aspx.cs" Inherits="GUI.GestionFamilia" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Gestión de Permisos de Usuario</title>
    
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/GestionFamilia.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server" style="width: 100%; max-width: 1000px; margin: 0 auto;">
        <uc:NavBar ID="NavBar" runat="server" />

        <h1 class="title"><asp:Literal ID="litTituloPagina" runat="server" Text="Gestión de Permisos de Usuario"></asp:Literal></h1>

        <div class="containerFamilia">
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblNombreFamilia" runat="server" Text="Nombre de familia"></asp:Label>
                    <asp:TextBox ID="txtFamilia" runat="server" CssClass="form-control" MaxLength="30"/>
                    <asp:Button ID="btnAceptar" runat="server" Text="Alta" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClick="btnCancelar_Click" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>

                <div class="table-container">
                    <h3><asp:Label ID="lblListadoFamilias" runat="server" Text="Listado de familias"></asp:Label></h3>
                    <asp:GridView ID="gridFamilia" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Hijos" HeaderText="Hijos" Visible="False" />
                            <asp:BoundField DataField="Permiso" HeaderText="Permiso" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>      
        
        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
