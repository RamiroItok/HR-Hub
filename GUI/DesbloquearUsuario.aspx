<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesbloquearUsuario.aspx.cs" Inherits="GUI.DesbloquearUsuario" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/DesbloquearUsuario.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="containerDesbloqueo mt-5">
            <h2 class="text-center">
                <asp:Label ID="lblTituloDesbloqueo" runat="server"></asp:Label></h2>

            <div class="table-responsive">
                <asp:GridView ID="gvUsuariosBloqueados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" EmptyDataText="No hay usuarios bloqueados.">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" disabled /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="false" OnCheckedChanged="chkSeleccionar_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="button-group text-center">
                <asp:Button ID="btnDesbloquear" runat="server" CssClass="btn btn-primary btn-action" Text="Desbloquear" OnClick="btnDesbloquear_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="alert-message" Visible="false"></asp:Label>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

    </form>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const checkboxes = document.querySelectorAll("#<%= gvUsuariosBloqueados.ClientID %> input[type='checkbox']");
            checkboxes.forEach((checkbox) => {
                checkbox.addEventListener("change", () => {
                    if (checkbox.checked) {
                        checkboxes.forEach((cb) => {
                            if (cb !== checkbox) cb.checked = false;
                        });
                    }
                });
            });

            document.getElementById("<%= btnDesbloquear.ClientID %>").addEventListener("click", function (event) {
                event.preventDefault();
                const selectedCheckbox = Array.from(checkboxes).find(cb => cb.checked);

                if (selectedCheckbox) {
                    Swal.fire({
                        title: tituloConfirmacion,
                        text: mensajeConfirmacion,
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#67a544",
                        cancelButtonColor: "#d33",
                        confirmButtonText: botonConfirmar,
                        cancelButtonText: botonCancelar
                    }).then((result) => {
                        if (result.isConfirmed) {
                            __doPostBack("<%= btnDesbloquear.ClientID %>", "");
                    }
                });
            } else {
                Swal.fire("Error", mensajeSeleccionUsuarioError, "error");
            }
        });
        });
</script>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.all.min.js"></script>
</body>
</html>
