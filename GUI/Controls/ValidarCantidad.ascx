<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarCantidad.ascx.cs" Inherits="GUI.Controls.ValidarCantidad" %>

<script>
    function validarCantidadMaxima(textBox) {
        const maxStock = parseInt(textBox.getAttribute("data-stock"));
        const cantidadIngresada = parseInt(textBox.value);

        if (cantidadIngresada > maxStock) {
            Swal.fire({
                icon: 'warning',
                title: 'Cantidad excedida',
                text: `La cantidad ingresada es mayor al stock disponible (${maxStock}).`,
                confirmButtonText: 'OK',
                confirmButtonColor: '#d33'
            });

            textBox.value = maxStock;
        } else {
            Swal.fire({
                icon: 'success',
                title: '<span style="font-size: 1.5em;">Cantidad actualizada</span>',
                html: '<span style="font-size: 1.2em;">La cantidad del producto ha sido modificada.</span>',
                showConfirmButton: true,
                confirmButtonText: '<span style="font-size: 1.1em;">OK</span>'
            });
        }
    }
</script>
