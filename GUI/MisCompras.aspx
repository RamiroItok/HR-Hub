<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="GUI.MisCompras" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Mis Compras</title>

    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/MisCompras.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <uc:NavBar ID="NavBar" runat="server" />

        <div class="containerCompras mt-4">
            <h2 class="text-center">Mis Compras</h2>
            <asp:GridView ID="gvCompras" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3"
                HeaderStyle-CssClass="table-header" RowStyle-CssClass="table-row"
                AlternatingRowStyle-CssClass="table-alt-row" OnRowCommand="gvCompras_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                    <asp:BoundField DataField="FechaPago" HeaderText="Fecha de Compra" SortExpression="FechaPago" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" DataFormatString="{0:C}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnVerResumen" runat="server" Text="Ver resumen de compra" CssClass="btn btn-primary btn-sm"
                                CommandName="VerResumen" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="resumenCompraModal" tabindex="-1" role="dialog" aria-labelledby="resumenCompraModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="resumenCompraModalLabel">Resumen de Compra</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p><strong>ID Compra:</strong>
                                    <asp:Label ID="lblIdCompra" runat="server" /></p>
                                <p><strong>Fecha de Compra:</strong>
                                    <asp:Label ID="lblFechaCompra" runat="server" /></p>
                                <p><strong>Total de la Compra:</strong>
                                    <asp:Label ID="lblTotalCompra" runat="server" /></p>

                                <asp:GridView ID="gvDetallesCompra" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3">
                                    <Columns>
                                        <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvCompras" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script>
        function showResumenCompraModal() {
            $('#resumenCompraModal').modal('show');
        }
    </script>
</body>
</html>
