<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisDocumentos.aspx.cs" Inherits="GUI.MisDocumentos" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Mis documentos</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/MisDocumentos.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />
        <div class="containerMisDocumentos mt-5">
            <h2 class="text-center">Mis Documentos</h2>

            <div class="form-check mb-4 text-center">
                <asp:CheckBox ID="chkFirmado" runat="server" Text="Firmado" AutoPostBack="true" OnCheckedChanged="chkFirmado_CheckedChanged" CssClass="form-check-input" />
                <label class="form-check-label" for="chkFirmado"></label>
            </div>

            <asp:GridView ID="dataGridDocumentos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" GridLines="None" OnRowCommand="dataGridDocumentos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Documento.Id" HeaderText="Id" />

                    <asp:BoundField DataField="Documento.Nombre" HeaderText="Nombre" />

                    <asp:BoundField DataField="FechaEntrega" HeaderText="Fecha de entrega" DataFormatString="{0:dd/MM/yyyy}" />

                    <asp:BoundField DataField="FechaFirma" HeaderText="Fecha de firma" DataFormatString="{0:dd/MM/yyyy}" />

                    <asp:TemplateField HeaderText="Firmado">
                        <ItemTemplate>
                            <asp:Label ID="lblFirmado" runat="server" 
                                       Text='<%# Eval("Firmado").ToString() == "Firmado" ? "Sí" : "No" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnLeer" runat="server" CssClass="btn btn-info" Text="Leer documento" CommandName="Leer" CommandArgument='<%# Eval("Documento.Id") %>' />
                            <asp:Button ID="btnFirmar" runat="server" CssClass="btn btn-success" Text="Firmar" CommandName="Firmar" CommandArgument='<%# Eval("Documento.Id") %>' 
                                        Visible='<%# Eval("Firmado").ToString() == "NoFirmado" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblNoDocumentos" runat="server" Text="No hay documentos" CssClass="text-center text-muted mt-3" Visible="false"></asp:Label>
        </div>

        <div class="modal fade" id="leerDocumentoModal" tabindex="-1" role="dialog" aria-labelledby="leerDocumentoModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="leerDocumentoModalLabel">Leer Documento</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Literal ID="litDocumentoContenido" runat="server"></asp:Literal>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalLeerAntesDeFirmar" tabindex="-1" role="dialog" aria-labelledby="modalLeerAntesDeFirmarLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLeerAntesDeFirmarLabel">Advertencia</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Debe leer el documento antes de firmarlo.
           
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalFirmaExitosa" tabindex="-1" role="dialog" aria-labelledby="modalFirmaExitosaLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalFirmaExitosaLabel">Confirmación</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        El documento ha sido firmado exitosamente.
           
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
    </form>
</body>
</html>
