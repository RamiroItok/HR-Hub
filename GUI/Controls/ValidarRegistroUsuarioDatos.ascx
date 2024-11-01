<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarRegistroUsuarioDatos.ascx.cs" Inherits="GUI.Controls.ValidarRegistroUsuarioDatos" %>


<div class="form-group">
    <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" AssociatedControlID="txtDireccion" />
    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Placeholder="Ingrese su direccion"/>
    <span id="errorDireccion" class="error-message" style="color:red; display:none;">Ingrese solo caracteres en el campo Dirección.</span>
</div>

<div class="form-group">
    <asp:Label ID="lblNumeroDireccion" runat="server" Text="Número de Dirección:" AssociatedControlID="txtNumeroDireccion" />
    <asp:TextBox ID="txtNumeroDireccion" runat="server" CssClass="form-control" Placeholder="Ingrese su numero de direccion"/>
    <span id="errorNumeroDireccion" class="error-message" style="color:red; display:none;">Ingrese solo números en el campo Número de Dirección.</span>
</div>

<div class="form-group">
    <asp:Label ID="lblDepartamento" runat="server" Text="Departamento:" AssociatedControlID="txtDepartamento" />
    <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" Placeholder="Ingrese su departamento"/>
    <span id="errorDepartamento" class="error-message" style="color:red; display:none;">Ingrese solo caracteres en el campo Departamento.</span>
</div>

<div class="form-group">
    <asp:Label ID="lblCodigoPostal" runat="server" Text="Código Postal:" AssociatedControlID="txtCodigoPostal" />
    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" Placeholder="Ingrese su codigo postal"/>
    <span id="errorCodigoPostal" class="error-message" style="color:red; display:none;">Ingrese solo caracteres o números en el campo Código Postal.</span>
</div>

<div class="form-group">
    <asp:Label ID="lblCiudad" runat="server" Text="Ciudad:" AssociatedControlID="txtCiudad" />
    <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" Placeholder="Ingrese una ciudad"/>
    <span id="errorCiudad" class="error-message" style="color:red; display:none;">Ingrese solo caracteres en el campo Ciudad.</span>
</div>

<div class="form-group">
    <asp:Label ID="lblProvincia" runat="server" Text="Provincia:" AssociatedControlID="txtProvincia" />
    <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" Placeholder="Ingrese una provincia"/>
    <span id="errorProvincia" class="error-message" style="color:red; display:none;">Ingrese solo caracteres en el campo Provincia.</span>
</div>

<div class="form-group">
    <asp:Label ID="lblPais" runat="server" Text="País:" AssociatedControlID="txtPais" />
    <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" Placeholder="Ingrese un pais"/>
    <span id="errorPais" class="error-message" style="color:red; display:none;">Ingrese solo caracteres en el campo País.</span>
</div>

<script>
    document.addEventListener("DOMContentLoaded", function () {
        function validateInput(input, regex, errorElementId, errorMessage) {
            const errorElement = document.getElementById(errorElementId);
            if (!regex.test(input.value)) {
                errorElement.style.display = "inline";
                errorElement.textContent = errorMessage;
            } else {
                errorElement.style.display = "none";
            }
        }

        document.getElementById("<%= txtDireccion.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorDireccion", "Ingrese solo caracteres en el campo Dirección.");
        };
        document.getElementById("<%= txtCiudad.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorCiudad", "Ingrese solo caracteres en el campo Ciudad.");
        };
        document.getElementById("<%= txtProvincia.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorProvincia", "Ingrese solo caracteres en el campo Provincia.");
        };
        document.getElementById("<%= txtPais.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorPais", "Ingrese solo caracteres en el campo País.");
        };

        document.getElementById("<%= txtNumeroDireccion.ClientID %>").onblur = function () {
            validateInput(this, /^[0-9]+$/, "errorNumeroDireccion", "Ingrese solo números en el campo Número de Dirección.");
        };

        document.getElementById("<%= txtDepartamento.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-z0-9]+$/, "errorDepartamento", "Ingrese solo caracteres o números en el campo Código Postal.");
        };
        document.getElementById("<%= txtCodigoPostal.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-z0-9]+$/, "errorCodigoPostal", "Ingrese solo caracteres o números en el campo Código Postal.");
        };
    });
</script>
