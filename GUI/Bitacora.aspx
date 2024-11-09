<%@ Page Title="Bitacora" Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="GUI.Bitacora" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Bitácora - HR Hub</title>

    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="~/Style/Bitacora.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('/Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0; display: flex; justify-content: center; align-items: center;">
    <form id="form1" runat="server" style="width: 100%; max-width: 1200px; margin: 0 auto;">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <div class="bitacora-page">
            <h1 class="titulo-bitacora"><asp:Literal ID="litTituloBitacora" runat="server" /></h1>
            <div class="filter-container">
                <div class="form-group">
                    <asp:Label ID="lblSearch" runat="server" Text="Texto de Búsqueda:" AssociatedControlID="txtSearch" />
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Ingrese una descripción"/>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" AssociatedControlID="drpUsuarios" />
                    <asp:DropDownList ID="drpUsuarios" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            
                <div class="form-group">
                    <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de Usuario" AssociatedControlID="drpTipoUsuario" />
                    <asp:DropDownList ID="drpTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            
                <div class="form-group">
                    <asp:Label ID="lblCriticidad" runat="server" Text="Criticidad" AssociatedControlID="drpCriticidad" />
                    <asp:DropDownList ID="drpCriticidad" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            
                <div class="form-group">
                    <asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:" AssociatedControlID="txtFechaDesde" />
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control datepicker" />
                </div>
                
                <div class="form-group">
                    <asp:Label ID="lblFechaHasta" runat="server" Text="Fecha Hasta:" AssociatedControlID="txtFechaHasta" />
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control datepicker" />
                </div>
            
                <div class="button-group">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar a Excel" CssClass="btn btn-success" OnClick="btnExportarExcel_Click" />
                    <asp:Button ID="btnGenerarXML" runat="server" Text="Generar XML" CssClass="btn btn-info" OnClick="btnGenerarXML_Click" />
                    <asp:Button ID="btnBajaBitacora" runat="server" Text="Baja bitacora" CssClass="btn btn-primary" OnClick="btnBajaBitacora_Click" />
                </div>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="message-label" Visible="false" />

            <asp:GridView ID="gvBitacora" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" 
                          AllowPaging="True" PageSize="15" OnPageIndexChanging="gvBitacora_PageIndexChanging" PagerStyle-CssClass="gridview-pagination"
                          PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" />
                </Columns>
            </asp:GridView>
        </div>
    </form>

    <script>
        var fpFechaDesde, fpFechaHasta;

        document.addEventListener("DOMContentLoaded", function () {
            fpFechaDesde = flatpickr("#<%= txtFechaDesde.ClientID %>", {
                enableTime: true,
                dateFormat: "Y-m-d H:i",
            });

            fpFechaHasta = flatpickr("#<%= txtFechaHasta.ClientID %>", {
                enableTime: true,
                dateFormat: "Y-m-d H:i",
            });
        });
    </script>

    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
