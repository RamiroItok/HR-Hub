<%@ Page Title="ListadoEmpresas" Language="C#" AutoEventWireup="true" CodeBehind="ListadoEmpresas.aspx.cs" Inherits="GUI.ListadoEmpresas" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Listado de Empresas</title>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Style/NavBar.css" rel="stylesheet" />
    <link href="Style/ListadoEmpresas.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="container mt-5">
            <h2 class="text-center mb-4">Listado de Empresas</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

            <div class="row">
                <asp:Repeater ID="rptEmpresas" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100 shadow">
                                <img src='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Logo")) %>' class="card-img-top" alt='<%# Eval("Nombre") %>' />
                                <div class="card-body text-center">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text">
                                        <a href='<%# EnsureUrl(Eval("URLEmpresa").ToString()) %>' target="_blank"><%# Eval("URLEmpresa") %></a>
                                    </p>
                                </div>
                                <div class="card-footer text-center">
                                    <button type="button" class="btn btn-success mr-2" onclick="openModal('<%# Eval("Id") %>', '<%# Eval("Nombre") %>', '<%# Eval("URLEmpresa") %>')">Modificar</button>
                                    <asp:Button ID="btnEliminar" 
                                                runat="server" 
                                                Text="Eliminar" 
                                                CssClass="btn btn-danger" 
                                                CommandName="Eliminar" 
                                                CommandArgument='<%# Eval("Id") %>' 
                                                OnCommand="btnEliminar_Command" 
                                                OnClientClick="return confirm('¿Está seguro de que desea eliminar esta empresa?');" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="modal fade" id="modalModificarEmpresa" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="modalLabel">Modificar Empresa</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <asp:HiddenField ID="hfEmpresaId" runat="server" />
                <div class="form-group">
                    <label for="txtNombreEmpresa">Nombre</label>
                    <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control" placeholder="Nombre de la Empresa" />
                </div>
                <div class="form-group">
                    <label for="txtURLEmpresa">URL</label>
                    <asp:TextBox ID="txtURLEmpresa" runat="server" CssClass="form-control" placeholder="URL de la Empresa" />
                </div>
                <div class="form-group">
                    <label for="fileLogoEmpresa">Logo</label>
                    <asp:FileUpload ID="fileLogoEmpresa" runat="server" CssClass="form-control" />
                </div>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" OnClick="btnGuardarCambios_Click" CssClass="btn btn-primary" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
              </div>
            </div>
          </div>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    
    <script>
        function openModal(id, nombre, url) {
            $('#<%= hfEmpresaId.ClientID %>').val(id);
            $('#txtNombreEmpresa').val(nombre);
            $('#txtURLEmpresa').val(url);
            $('#modalModificarEmpresa').modal('show');
        }
    </script>
</body>
</html>