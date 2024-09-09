<%@ Page Title="Bitacora" Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="GUI.Bitacora" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Bitácora - HR Hub</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/SiteMaster.css" rel="stylesheet" />
    <link href="~/Style/Bitacora.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="container">
            <h2>Bitacora</h2>
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
                        <input type="datetime-local" ID="txtFechaDesde" runat="server" CssClass="form-control" />
                        <i class="fas fa-calendar-alt calendar-icon"></i>
                    </div>
                </div>
                
                <div class="form-group">
                    <label for="txtFechaHasta">Fecha Hasta:</label>
                    <div class="input-container">
                        <input type="datetime-local" ID="txtFechaHasta" runat="server" CssClass="form-control" />
                        <i class="fas fa-calendar-alt calendar-icon"></i>
                    </div>
                </div>
            
                <div class="button-group">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
                </div>
            </div>

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
    </form>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
