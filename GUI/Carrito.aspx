<%@ Page Title="Carrito" Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="GUI.Carrito" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

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
    <form id="form1" runat="server">
        <uc:NavBar ID="NavBar" runat="server" />

        <div class="containerCarrito mt-4">
            <h2>Carrito de Compras</h2>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="form-control"
                                                     OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true" />
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
                
                        <div class="text-right mt-3">
                            <h4>Total del Carrito: <asp:Label ID="lblTotalCarrito" runat="server" Text="0.00" CssClass="font-weight-bold"></asp:Label></h4>
                        </div>
                            <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" CssClass="btn btn-finalizar-compra mt-3" OnClick="btnFinalizarCompra_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

<script>
    function mostrarNotificacionModificacion() {
        Swal.fire({
            icon: 'success',
            title: '<span style="font-size: 1.5em;">Cantidad actualizada</span>',
            html: '<span style="font-size: 1.2em;">La cantidad del producto ha sido modificada.</span>',
            showConfirmButton: true,
            confirmButtonText: '<span style="font-size: 1.1em;">OK</span>'
        });
    }

    function mostrarNotificacionEliminacion() {
        Swal.fire({
            icon: 'warning',
            title: '<span style="font-size: 1.5em;">Producto eliminado</span>',
            html: '<span style="font-size: 1.2em;">El producto ha sido eliminado del carrito.</span>',
            showConfirmButton: true,
            confirmButtonText: '<span style="font-size: 1.1em;">OK</span>'
        });
    }
</script>

</body>
</html>