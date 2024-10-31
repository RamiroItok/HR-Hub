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
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavBar ID="NavBar" runat="server" />

            <div class="containerCompra">
                <div class="card-centered">
                    <h3 class="card-title">Resumen de la Compra</h3>
                    <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                        <Columns>
                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                            <asp:BoundField DataField="Producto.PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                        </Columns>
                    </asp:GridView>
                    <div class="text-right mt-3">
                        <h4>Total del Carrito: <asp:Label ID="lblTotalCompra" runat="server" Text="$0.00" CssClass="font-weight-bold"></asp:Label></h4>
                    </div>
                </div>

            <div class="card-centered payment-details">
                <h3 class="card-title payment-title">Detalles de Pago</h3>
                <div class="form-group">
                    <label for="txtNumeroTarjeta">Número de Tarjeta</label>
                    <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control" placeholder="XXXX XXXX XXXX XXXX" MaxLength="16"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtNombreTitular">Nombre del Titular</label>
                    <asp:TextBox ID="txtNombreTitular" runat="server" CssClass="form-control" placeholder="Nombre como aparece en la tarjeta"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtFechaVencimiento">Fecha de Vencimiento</label>
                    <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" placeholder="MM/AA" MaxLength="5"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtCVC">CVC</label>
                    <asp:TextBox ID="txtCVC" runat="server" CssClass="form-control" placeholder="XXX" MaxLength="3"></asp:TextBox>
                </div>
                <asp:Button ID="btnPagar" runat="server" Text="Pagar" CssClass="btn btn-success" OnClick="btnPagar_Click" />
            </div>
        </div>

    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
