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
<body>
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="containerProductos">
                    <h1 class="titulo-productos">Productos</h1>
                
                    <div class="row filtros-busqueda">
                        <div class="col-md-3">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Filtrar por Nombre"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Filtrar por Descripción"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="DropDownEmpresa" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="DropDownTipoProducto" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 mt-3">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                
                    <div class="row" id="productContainer">
                        <asp:Repeater ID="ProductRepeater" runat="server">
                            <ItemTemplate>
                                <div class="col-md-3">
                                    <br />
                                    <div class="card producto-card shadow-sm" runat="server" ID="productoCard">
                                        <div class="img-container">
                                            <img src='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Imagen")) %>' class="card-img-top img-fluid" alt='<%# Eval("Nombre") %>' />
                                        </div>
                                        <div class="card-body">
                                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                            <p class="card-text">Descripcion: <%# Eval("Descripcion") %></p>
                                            <p class="card-text">Empresa: <%# Eval("Empresa.Nombre") %></p>
                                            <p class="card-text">Tipo de Producto: <%# Eval("TipoProducto.Nombre") %></p>
                                            <p class="card-text">Cantidad: <%# Eval("Cantidad") %></p>
                                            <p class="card-text">Precio: $<%# Eval("PrecioUnitario") %></p>
                                            <asp:Button runat="server" Text="Agregar al carrito" CssClass="btn btn-add-cart" CommandArgument='<%# Eval("Id") %>' CommandName="Agregar" OnClientClick="showCartModal();" OnCommand="AgregarAlCarrito_Command" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="footer">
            <p>&copy; 2024 HR Hub. Todos los derechos reservados.</p>
        </div>
    </form>

    <div class="modal fade" id="cartModal" tabindex="-1" role="dialog" aria-labelledby="cartModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="cartModalLabel">Producto agregado</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    ¡El producto ha sido agregado al carrito exitosamente!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-modal-close" data-dismiss="modal">Cerrar</button>
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
    </script>
</body>
</html>