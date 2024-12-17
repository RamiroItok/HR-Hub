<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarEmail.ascx.cs" Inherits="GUI.Controls.ValidarEmail" %>

<div class="form-group">
    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label"></asp:Label>
    
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" onblur="validateEmailFormat(this)" MaxLength="60"></asp:TextBox>
    
    <asp:Label ID="lblEmailError" runat="server" CssClass="error-message" Style="color: red; display: none;"></asp:Label>
</div>

<script>
    function validateEmailFormat(input) {
        var emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
        var errorLabel = document.getElementById("<%= lblEmailError.ClientID %>");

        var errorMessage = "<%= lblEmailError.Text %>";

        if (!emailPattern.test(input.value)) {
            errorLabel.textContent = errorMessage;
            errorLabel.style.display = "block";
        } else {
            errorLabel.style.display = "none";
            errorLabel.textContent = "";
        }
    }
</script>
