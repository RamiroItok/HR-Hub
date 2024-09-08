<%@ Page Title="Bitácora" Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="GUI.Bitacora" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Bitácora - HR Hub</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/SiteMaster.css" rel="stylesheet" />
    <link href="~/Style/Bitacora.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="container">
            <h2>Bitácora</h2>
            <!-- Filtros de búsqueda -->
            <div class="filter-container">
                <asp:Label ID="lblSearch" runat="server" Text="Texto de Búsqueda:" AssociatedControlID="txtSearch" />
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />

                <asp:Label ID="lblUsuario" runat="server" Text="Usuario" AssociatedControlID="drpUsuarios" />
                <asp:DropDownList ID="drpUsuarios" runat="server" CssClass="form-control"></asp:DropDownList>

                <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de Usuario" AssociatedControlID="drpTipoUsuario" />
                <asp:DropDownList ID="drpTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList>

                <asp:Label ID="lblCriticidad" runat="server" Text="Criticidad" AssociatedControlID="drpCriticidad" />
                <asp:DropDownList ID="drpCriticidad" runat="server" CssClass="form-control"></asp:DropDownList>
                <br />

                <asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:" AssociatedControlID="txtFechaDesde" />
                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />

                <asp:Label ID="lblFechaHasta" runat="server" Text="Fecha Hasta:" AssociatedControlID="txtFechaHasta" />
                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />

                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />

                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
            </div>

            <!-- GridView para mostrar los resultados -->
            <asp:GridView ID="gvBitacora" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="TipoUsuario" HeaderText="TipoUsuario" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" />
                </Columns>
            </asp:GridView>
        </div>
    </form>

    <!-- Scripts -->
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
