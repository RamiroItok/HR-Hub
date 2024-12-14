<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarRegistroUsuarioDatos.ascx.cs" Inherits="GUI.Controls.ValidarRegistroUsuarioDatos" %>

<div class="form-group">
    <asp:Label ID="lblDireccion" runat="server" AssociatedControlID="txtDireccion" />
    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
    <span id="errorDireccion" class="error-message" style="color:red; display:none;"></span>
</div>

<div class="form-group">
    <asp:Label ID="lblNumeroDireccion" runat="server" AssociatedControlID="txtNumeroDireccion" />
    <asp:TextBox ID="txtNumeroDireccion" runat="server" CssClass="form-control" />
    <span id="errorNumeroDireccion" class="error-message" style="color:red; display:none;"></span>
</div>

<div class="form-group">
    <asp:Label ID="lblDepartamento" runat="server" AssociatedControlID="txtDepartamento" />
    <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" />
    <span id="errorDepartamento" class="error-message" style="color:red; display:none;"></span>
</div>

<div class="form-group">
    <asp:Label ID="lblCodigoPostal" runat="server" AssociatedControlID="txtCodigoPostal" />
    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" />
    <span id="errorCodigoPostal" class="error-message" style="color:red; display:none;"></span>
</div>

<div class="form-group">
    <asp:Label ID="lblCiudad" runat="server" AssociatedControlID="txtCiudad" />
    <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
    <span id="errorCiudad" class="error-message" style="color:red; display:none;"></span>
</div>

<div class="form-group">
    <asp:Label ID="lblProvincia" runat="server" AssociatedControlID="txtProvincia" />
    <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control"  OnKeyPress="return validarTexto(event);" />
    <span id="errorProvincia" class="error-message" style="color:red; display:none;"></span>
</div>

<div class="form-group">
    <asp:Label ID="lblPais" runat="server" AssociatedControlID="txtPais" />
    <asp:TextBox ID="txtPais" runat="server" CssClass="form-control"  OnKeyPress="return validarTexto(event);" />
    <span id="errorPais" class="error-message" style="color:red; display:none;"></span>
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
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorDireccion", "<%= ErrorDireccion %>");
        };
        document.getElementById("<%= txtNumeroDireccion.ClientID %>").onblur = function () {
            validateInput(this, /^[0-9]+$/, "errorNumeroDireccion", "<%= ErrorNumeroDireccion %>");
        };
        document.getElementById("<%= txtDepartamento.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-z0-9]+$/, "errorDepartamento", "<%= ErrorDepartamento %>");
        };
        document.getElementById("<%= txtCodigoPostal.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-z0-9]+$/, "errorCodigoPostal", "<%= ErrorCodigoPostal %>");
        };
        document.getElementById("<%= txtCiudad.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorCiudad", "<%= ErrorCiudad %>");
        };
        document.getElementById("<%= txtProvincia.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorProvincia", "<%= ErrorProvincia %>");
        };
        document.getElementById("<%= txtPais.ClientID %>").onblur = function () {
            validateInput(this, /^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$/, "errorPais", "<%= ErrorPais %>");
        };
    });

    function validarTexto(e) {
        var charCode = e.charCode || e.keyCode;
        if ((charCode >= 65 && charCode <= 90) ||
            (charCode >= 97 && charCode <= 122) ||
            charCode === 32 ||
            charCode === 8) {
            return true;
        }
        return false;
    }
</script>
