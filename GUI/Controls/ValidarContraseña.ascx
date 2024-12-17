<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarContraseña.ascx.cs" Inherits="GUI.Controls.ValidarContraseña" %>

<div class="password-container form-group">
    <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" AssociatedControlID="txtPassword" CssClass="form-label"></asp:Label>
    
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="password-input" Placeholder="Ingrese su contraseña" MaxLength="50"></asp:TextBox>
    
    <asp:CustomValidator ID="cvPassword" runat="server" ControlToValidate="txtPassword" 
        ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula, un número y un carácter especial." 
        OnServerValidate="ValidatePassword" Display="Dynamic" CssClass="error-message" EnableClientScript="false"></asp:CustomValidator>
</div>