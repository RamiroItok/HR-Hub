<%@ Page Title="Compra" Language="C#" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="GUI.Compra" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Compra</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/Compra.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <uc:NavBar ID="NavBar" runat="server" />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="containerCompra">
                    <div class="card-centered">
                        <h3 class="card-title">
                            <asp:Label ID="lblCompraResumenTitle" runat="server" Text="Resumen de la Compra"></asp:Label>
                        </h3>
                        <div class="text-center mt-4">
                            <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                        </div>
                        <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                <asp:BoundField DataField="Producto.PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                        <div class="text-right mt-3">
                            <h4>
                                <asp:Label ID="lblTotalCompraLabel" runat="server" Text="Total del Carrito:"></asp:Label>
                                <asp:Label ID="lblTotalCompra" runat="server" Text="$0.00" CssClass="font-weight-bold"></asp:Label>
                            </h4>
                        </div>
                    </div>

                    <div class="card-centered payment-details">
                        <h3 class="card-title payment-title">
                            <asp:Label ID="lblPaymentDetailsTitle" runat="server" Text="Detalles de Pago"></asp:Label>
                        </h3>
                        <div class="form-group">
                            <label for="txtNumeroTarjeta">
                                <asp:Label ID="lblCardNumberLabel" runat="server" Text="Número de Tarjeta"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control" placeholder="XXXX XXXX XXXX XXXX" MaxLength="16"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtNombreTitular">
                                <asp:Label ID="lblCardHolderLabel" runat="server" Text="Nombre del Titular"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtNombreTitular" runat="server" CssClass="form-control" placeholder="Nombre como aparece en la tarjeta"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtFechaVencimiento">
                                <asp:Label ID="lblExpiryDateLabel" runat="server" Text="Fecha de Vencimiento"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" placeholder="MM/AA" MaxLength="5"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtCVC">
                                <asp:Label ID="lblCVCLabel" runat="server" Text="CVC"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtCVC" runat="server" CssClass="form-control" placeholder="XXX" MaxLength="3"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnPagar" runat="server" Text="Pagar" CssClass="btn btn-success" OnClick="btnPagar_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="paymentModal" tabindex="-1" role="dialog" aria-labelledby="paymentModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="paymentModalLabel">Pago Realizado</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        ¡El pago se ha realizado correctamente!
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" data-dismiss="modal" onclick="redirectToProducts()">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function redirectToProducts() {
            window.location.href = '/Carrito.aspx';
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

    </script>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
