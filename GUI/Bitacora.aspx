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
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="bitacora-page">
            <div class="container">
            <h1 class="titulo-bitacora">Bitácora</h1>
            <div class="filter-container">
                <div class="form-group">
                    <asp:Label ID="lblSearch" runat="server" Text="Texto de Búsqueda:" AssociatedControlID="txtSearch" />
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
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
                    <label for="txtFechaDesde">Fecha Desde:</label>
                    <div class="input-container">
                        <input type="text" ID="txtFechaDesde" runat="server" CssClass="form-control" />
                        
                        <button type="button" class="calendar-button" onclick="openFlatpickr('txtFechaDesde')">
                            <i class="fas fa-calendar-alt"></i>
                        </button>
                    </div>
                </div>
                
                <div class="form-group">
                    <label for="txtFechaHasta">Fecha Hasta:</label>
                    <div class="input-container">
                        <input type="text" ID="txtFechaHasta" runat="server" CssClass="form-control" />
                        <button type="button" class="calendar-button" onclick="openFlatpickr('txtFechaHasta')">
                            <i class="fas fa-calendar-alt"></i>
                        </button>
                    </div>
                </div>
            
                <div class="button-group">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
                </div>
            </div>
                <asp:Panel runat="server" CssClass="form-group">
                    <asp:Label runat="server" ID="lblMensaje" CssClass="message-label" Visible="false"></asp:Label>                
            </asp:Panel>

            <asp:GridView ID="gvBitacora" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" 
                          AllowPaging="True" PageSize="15" OnPageIndexChanging="gvBitacora_PageIndexChanging" PagerStyle-CssClass="gridview-pagination"
                          PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="Center" >
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

        function openFlatpickr(fieldId) {
            if (fieldId === 'txtFechaDesde') {
                fpFechaDesde.open();
            } else if (fieldId === 'txtFechaHasta') {
                fpFechaHasta.open();
            }
        }
    </script>

    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>