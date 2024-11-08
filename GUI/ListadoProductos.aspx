<%@ Page Title="ListadoProductos" Language="C#" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="GUI.ListadoProductos" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><asp:Literal ID="litPageTitle" runat="server" Text="HR Hub - Listado de Productos"></asp:Literal></title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/ListadoProductos.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />

        <uc:NavBar runat="server" ID="NavBarControl" />

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <div class="container mt-5">
            <h2 class="text-center mb-4">
                <asp:Literal ID="litTitle" runat="server" Text="Listado de Productos"></asp:Literal>
            </h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

            <div class="row">
                <asp:Repeater ID="rptProductos" runat="server" OnItemDataBound="rptProductos_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100 shadow">
                                <img src='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Imagen")) %>' class="card-img-top" alt='<%# Eval("Nombre") %>' />
                                <div class="card-body text-center">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text mb-1">
                                        <strong id="cantidadLabel">Cantidad:</strong> <%# Eval("Cantidad") %>
                                    </p>
                                    <p class="card-text">
                                        <strong><span id="precioUnitarioLabel">Precio Unitario:</span></strong> $<%# Eval("PrecioUnitario", "{0:N2}") %>
                                    </p>
                                </div>

                                <div class="card-footer text-center">
                                    <button type="button" class="btn btn-success mr-2"
                                        onclick="openModal('<%# Eval("Id") %>', 
                                                               '<%# Eval("Nombre") %>', 
                                                               '<%# Eval("Descripcion") %>', 
                                                               '<%# Eval("Cantidad") %>', 
                                                               '<%# Eval("PrecioUnitario") %>', 
                                                               '<%# Eval("IdEmpresa") %>', 
                                                               '<%# Eval("IdTipoProducto") %>')">
                                        <asp:Literal ID="litModificar" runat="server" Text="Modificar"></asp:Literal>
                                    </button>
                                    <asp:Button ID="btnEliminar"
                                        runat="server"
                                        Text=""
                                        CssClass="btn btn-danger"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnCommand="btnEliminar_Command"
                                        OnClientClick="return confirm('¿Está seguro de que desea eliminar este producto?');" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="modal fade" id="modalModificarProducto" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLabel">
                            <asp:Literal ID="litModalTitle" runat="server" Text="Modificar Producto"></asp:Literal>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="IdProducto" runat="server" />
                        <div class="form-group">
                            <label for="txtNombreProducto"><asp:Literal ID="litNombreLabel" runat="server" Text="Nombre"></asp:Literal></label>
                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" placeholder="Nombre del Producto" />
                        </div>
                        <div class="form-group">
                            <asp:Literal ID="litEmpresaLabel" runat="server" Text="Empresa:"></asp:Literal>
                            <asp:DropDownList runat="server" ID="DropDownEmpresa" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="fileImagenProducto"><asp:Literal ID="litImagenLabel" runat="server" Text="Imagen"></asp:Literal></label>
                            <asp:FileUpload ID="fileImagenProducto" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtDescripcion"><asp:Literal ID="litDescripcionLabel" runat="server" Text="Descripcion"></asp:Literal></label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Descripcion del Producto" />
                        </div>
                        <div class="form-group">
                            <asp:Literal ID="litTipoProductoLabel" runat="server" Text="Tipo de Producto:"></asp:Literal>
                            <asp:DropDownList runat="server" ID="DropDownTipoProducto" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtCantidad"><asp:Literal ID="litCantidadLabelModal" runat="server" Text="Cantidad"></asp:Literal></label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" placeholder="Cantidad" />
                        </div>
                        <div class="form-group">
                            <label for="txtPrecioUnitario"><asp:Literal ID="litPrecioUnitarioLabelModal" runat="server" Text="Precio Unitario"></asp:Literal></label>
                            <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" placeholder="Precio unitario" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" OnClick="btnGuardarCambios_Click" CssClass="btn btn-primary" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">
                            <asp:Literal ID="litCloseButton" runat="server" Text="Cerrar"></asp:Literal>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function openModal(id, nombre, descripcion, cantidad, precio, empresaId, tipoProductoId) {
            $('#<%= IdProducto.ClientID %>').val(id);
            $('#txtNombreProducto').val(nombre);
            $('#txtDescripcion').val(descripcion);
            $('#txtCantidad').val(cantidad);
            $('#txtPrecioUnitario').val(precio);

            $('#<%= DropDownEmpresa.ClientID %>').val(empresaId);
            $('#<%= DropDownTipoProducto.ClientID %>').val(tipoProductoId);

            $('#modalModificarProducto').modal('show');
        }
    </script>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
