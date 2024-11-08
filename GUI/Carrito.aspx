<%@ Page Title="Carrito" Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="GUI.Carrito" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>
<%@ Register Src="~/Controls/ValidarCantidad.ascx" TagPrefix="ucc" TagName="ValidarCantidad" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Carrito</title>

    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/Carrito.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; max-width: 900px; margin: 0 auto;">
        <uc:NavBar ID="NavBar" runat="server" />

        <div class="containerCarrito mt-4">
            <h2><asp:Label ID="lblCarritoTitulo" runat="server" Text="Carrito de Compras"></asp:Label></h2>
            <div class="text-center mt-4">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Panel ID="pnlCarritoVacio" runat="server" Visible="false" CssClass="empty-cart-message">
                        <h4><asp:Label ID="lblCarritoVacioMessage" runat="server" Text="¡No hay productos en el carrito!"></asp:Label></h4>
                    </asp:Panel>

                    <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                        DataKeyNames="IdProducto" OnRowCommand="gvCarrito_RowCommand" OnRowDataBound="gvCarrito_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Imagen">
                                <ItemTemplate>
                                    <asp:Image ID="imgProducto" runat="server" Width="50px" Height="50px"
                                        ImageUrl='<%# ConvertirImagenABase64((byte[])Eval("Producto.Imagen")) %>'
                                        AlternateText="Imagen del Producto" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                            <asp:BoundField DataField="Producto.PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />

                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate>
                                    <ucc:ValidarCantidad ID="ValidarCantidadControl" runat="server" />

                                    <asp:TextBox ID="txtCantidad" runat="server"
                                        Text='<%# Eval("Cantidad") %>'
                                        CssClass="form-control"
                                        OnTextChanged="txtCantidad_TextChanged"
                                        AutoPostBack="true"
                                        onblur="validarCantidadMaxima(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subtotal">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubtotal" runat="server" Text="" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-danger">
                                        <i class="fas fa-trash-alt"></i> Eliminar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="cart-footer">
                        <asp:Button ID="btnLimpiarCarrito" runat="server" Text="Limpiar Carrito" CssClass="btn btn-warning" OnClick="btnLimpiarCarrito_Click" />

                        <div class="total-container">
                            <h4><asp:Label ID="lblTotalCarritoLabel" runat="server" Text="Total del Carrito:"></asp:Label>
                                <asp:Label ID="lblTotalCarrito" runat="server" Text="0.00" CssClass="font-weight-bold"></asp:Label>
                            </h4>
                            <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" CssClass="btn btn-finalizar-compra mt-3" OnClick="btnFinalizarCompra_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script>
        function mostrarNotificacionEliminacion() {
            Swal.fire({
                icon: 'warning',
                title: '<span style="font-size: 1.5em;">Producto eliminado</span>',
                html: '<span style="font-size: 1.2em;">El producto ha sido eliminado del carrito.</span>',
                showConfirmButton: true,
                confirmButtonText: '<span style="font-size: 1.1em;">OK</span>'
            });
        }

        function mostrarNotificacionCarritoVacio() {
            Swal.fire({
                icon: 'success',
                title: 'Carrito vaciado',
                text: 'Todos los productos han sido eliminados del carrito.',
                showConfirmButton: true,
                confirmButtonText: 'OK',
                confirmButtonColor: '#4CAF50'
            });
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

</body>
</html>
