﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="GUI.MisCompras" %>
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
            <h2 class="text-center">
                <asp:Literal ID="lblTituloMisCompras" runat="server" Text="Mis Compras"></asp:Literal>
            </h2>

            <div class="text-center mt-4">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                </div>

            <asp:GridView ID="gvCompras" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3"
                HeaderStyle-CssClass="table-header" RowStyle-CssClass="table-row"
                AlternatingRowStyle-CssClass="table-alt-row" OnRowCommand="gvCompras_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" SortExpression="Id" />
                    <asp:BoundField DataField="FechaPago" SortExpression="FechaPago" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Total" SortExpression="Total" DataFormatString="{0:C}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnVerResumen" runat="server" Text='<%# btnResumenText %>' CssClass="btn btn-primary btn-sm"
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
                                <h5 class="modal-title" id="resumenCompraModalLabel">
                                    <asp:Literal ID="lblModalTituloResumenCompra" runat="server"></asp:Literal>
                                </h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p><strong><asp:Literal ID="lblLabelIDCompra" runat="server"></asp:Literal></strong>
                                    <asp:Label ID="lblIdCompra" runat="server" /></p>
                                <p><strong><asp:Literal ID="lblLabelFechaCompra" runat="server"></asp:Literal></strong>
                                    <asp:Label ID="lblFechaCompra" runat="server" /></p>
                                <p><strong><asp:Literal ID="lblLabelTotalCompra" runat="server"></asp:Literal></strong>
                                    <asp:Label ID="lblTotalCompra" runat="server" /></p>

                                <asp:GridView ID="gvDetallesCompra" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3">
                                    <Columns>
                                        <asp:BoundField DataField="NombreProducto" />
                                        <asp:BoundField DataField="Cantidad" />
                                        <asp:BoundField DataField="PrecioUnitario" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="Subtotal" DataFormatString="{0:C}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                    <asp:Literal ID="lblButtonCerrar" runat="server"></asp:Literal>
                                </button>
                                <asp:Button ID="btnDescargarPdf" runat="server" Text="Descargar" CssClass="btn btn-primary" OnClientClick="descargarPdf(); return false;" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvCompras" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script>
        function showResumenCompraModal() {
            $('#resumenCompraModal').modal('show');
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
            prm.add_endRequest(function () {
                aplicarFondo();
            });
        });

        function descargarPdf() {
            var idCompra = document.getElementById('<%= lblIdCompra.ClientID %>').innerText;
            window.open('MisCompras.aspx?DownloadPdf=true&idCompra=' + idCompra, '_blank');
        }
    </script>
</body>
</html>
