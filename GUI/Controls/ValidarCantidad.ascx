<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarCantidad.ascx.cs" Inherits="GUI.Controls.ValidarCantidad" %>

<asp:Literal ID="litCantidadExcedidaTitulo" runat="server" Visible="false" />
<asp:Literal ID="litCantidadExcedidaTexto" runat="server" Visible="false" />
<asp:Literal ID="litCantidadActualizadaTitulo" runat="server" Visible="false" />
<asp:Literal ID="litCantidadActualizadaTexto" runat="server" Visible="false" />
<asp:Literal ID="litConfirmButtonText" runat="server" Visible="false" />

<script>
    function validarCantidadMaxima(textBox) {
        const maxStock = parseInt(textBox.getAttribute("data-stock"));
        const cantidadIngresada = parseInt(textBox.value);

        if (cantidadIngresada > maxStock) {
            Swal.fire({
                icon: 'warning',
                title: '<%= litCantidadExcedidaTitulo.Text %>',
                text: `<%= litCantidadExcedidaTexto.Text %> (${maxStock}).`,
                confirmButtonText: '<%= litConfirmButtonText.Text %>',
                confirmButtonColor: '#d33'
            });

            textBox.value = maxStock;
        } else {
            Swal.fire({
                icon: 'success',
                title: '<%= litCantidadActualizadaTitulo.Text %>',
                html: '<%= litCantidadActualizadaTexto.Text %>',
                showConfirmButton: true,
                confirmButtonText: '<%= litConfirmButtonText.Text %>'
            });
        }
    }
</script>
