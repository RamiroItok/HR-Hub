<%@ Page Title="Productos" Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="GUI.Productos" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Productos</title>

    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Style/NavBar.css" rel="stylesheet" />
    <link href="Style/Productos.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('<%= ResolveUrl("~/Content/imagenes/Fondo2.jpg") %>'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid d-flex justify-content-center">
                    <div class="containerProductos">
                        <h1 class="titulo-productos">
                            <asp:Label ID="lblProductosTitle" runat="server" Text="Productos"></asp:Label>
                        </h1>

                        <div class="row filtros-busqueda d-flex justify-content-center">
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Filtrar por Nombre"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Filtrar por Descripción"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="DropDownEmpresa" CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="DropDownTipoProducto" CssClass="form-control" />
                            </div>
                            <div class="col-md-3 mt-3 d-flex justify-content-around">
                                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                            </div>
                        </div>

                        <div class="row justify-content-center" id="productContainer">
                            <asp:Repeater ID="ProductRepeater" runat="server" OnItemDataBound="ProductRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-md-3 d-flex align-items-stretch">
                                        <div class="card producto-card shadow-sm mt-4">
                                            <div class="img-container">
                                                <img src='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Imagen")) %>' class="card-img-top img-fluid" alt='<%# Eval("Nombre") %>' />
                                            </div>
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                                
                                                <!-- Usamos Label o Literal para asignar valores dinámicos desde el código-behind -->
                                                <p class="card-text">
                                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>: 
                                                    <asp:Label ID="lblDescripcionValue" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                                </p>
                                                <p class="card-text">
                                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label>: 
                                                    <asp:Label ID="lblEmpresaValue" runat="server" Text='<%# Eval("Empresa.Nombre") %>'></asp:Label>
                                                </p>
                                                <p class="card-text">
                                                    <asp:Label ID="lblTipoProducto" runat="server" Text="Tipo de Producto"></asp:Label>: 
                                                    <asp:Label ID="lblTipoProductoValue" runat="server" Text='<%# Eval("TipoProducto.Nombre") %>'></asp:Label>
                                                </p>
                                                <p class="card-text">
                                                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>: 
                                                    <asp:Label ID="lblCantidadValue" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                                </p>
                                                <p class="card-text">
                                                    <asp:Label ID="lblPrecio" runat="server" Text="Precio"></asp:Label>: 
                                                    <asp:Label ID="lblPrecioValue" runat="server" Text='<%# Eval("PrecioUnitario", "{0:C}") %>'></asp:Label>
                                                </p>

                                                <asp:Button runat="server" ID="btnAgregarCarrito" Text="Agregar al carrito" CssClass="btn btn-add-cart" 
                                                            CommandArgument='<%# Eval("Id") %>' CommandName="Agregar" OnClientClick="showCartModal();" 
                                                            OnCommand="AgregarAlCarrito_Command" Visible='<%# Convert.ToInt32(Eval("Cantidad")) > 0 %>' />

                                                <asp:Label ID="lblNoStock" runat="server" Text="No hay stock" CssClass="no-stock-label" Visible='<%# Convert.ToInt32(Eval("Cantidad")) <= 0 %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="footer">
            <p><asp:Literal ID="litTextoPiePagina" runat="server"></asp:Literal></p>
        </div>
    </form>

    <div class="modal fade" id="cartModal" tabindex="-1" role="dialog" aria-labelledby="cartModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text="Producto agregado"></asp:Label></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblModalMessage" runat="server" Text="¡El producto ha sido agregado al carrito exitosamente!"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-modal-close" data-dismiss="modal">
                        <asp:Label ID="lblModalCloseButton" runat="server" Text="Cerrar"></asp:Label>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        function showCartModal() {
            $('#cartModal').modal('show');
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
