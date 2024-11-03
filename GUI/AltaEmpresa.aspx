<%@ Page Title="AltaEmpresa" Language="C#" AutoEventWireup="true" CodeBehind="AltaEmpresa.aspx.cs" Inherits="GUI.AltaEmpresa" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Empresas Clientes</title>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="~/Style/AltaEmpresa.css" rel="stylesheet" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0;">
    <form id="form1" runat="server" style="display: flex; justify-content: center; align-items: center; min-height: 100vh;">
        <uc:NavBar runat="server" ID="NavBarControl" />
        
        <div class="containerEmpresas">
            <div class="content-box">
                <h2 class="text-center mb-4">Registro de Empresas</h2>

                <div class="form-group">
                    <label for="txtNombreEmpresa">Nombre de la Empresa:</label>
                    <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control" placeholder="Ingrese el nombre"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="fileLogo">Logo de la Empresa:</label>
                    <asp:FileUpload ID="fileLogo" runat="server" CssClass="form-control-file" />
                </div>

                <div class="form-group">
                    <label for="txtURLEmpresa">URL de la Empresa:</label>
                    <asp:TextBox ID="txtURLEmpresa" runat="server" CssClass="form-control" placeholder="https://www.ejemplo.com"></asp:TextBox>
                </div>

                <div class="text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Registrar" CssClass="btn btn-primary mr-2" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" OnClick="btnCancel_Click" />
                </div>

                <div class="text-center mt-4">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </form>

    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
