<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="GUI.AccesoDenegado" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        /* Fondo con un gradiente sutil */
        body {
            background: linear-gradient(135deg, #f4f7f6 0%, #dfe9e8 100%);
            font-family: 'Arial', sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            color: #333;
        }

        /* Contenedor principal del mensaje de error */
        .error-container {
            text-align: center;
            max-width: 500px;
            padding: 40px;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
            transition: transform 0.3s ease;
        }

        /* Efecto de sombreado y escala en hover */
        .error-container:hover {
            transform: scale(1.02);
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
        }

        /* Número de error */
        .error-code {
            font-size: 100px;
            font-weight: 700;
            color: #ff6b6b;
            margin: 0;
        }

        /* Mensaje de acceso denegado */
        .error-title {
            font-size: 24px;
            font-weight: 600;
            color: #333;
            margin-top: 10px;
        }

        /* Texto informativo */
        .error-message {
            font-size: 16px;
            color: #666;
            margin-top: 10px;
            line-height: 1.5;
        }

        /* Botón para volver a la página principal */
        .btn-home {
            display: inline-block;
            margin-top: 25px;
            background-color: #6AB547;
            color: #ffffff;
            padding: 12px 28px;
            border-radius: 30px;
            font-weight: bold;
            text-decoration: none;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
        }

        .btn-home:hover {
            background-color: #4a8c38;
            box-shadow: 0 5px 15px rgba(74, 140, 56, 0.4);
        }

        /* Estilos de animación */
        .error-container {
            animation: slideIn 0.5s ease;
        }

        #ddlLanguage {
            position: fixed;
            bottom: 15px; /* Separación desde la parte inferior */
            right: 15px; /* Separación desde el borde derecho */
            z-index: 1000; /* Asegura que esté por encima de otros elementos */
            padding: 5px;
            font-size: 14px;
            background-color: #fff; /* Fondo blanco para mejor visibilidad */
            border: 1px solid #ccc;
            border-radius: 4px;
            cursor: pointer;
        }

        #ddlLanguage option {
            color: #333;
        }

        @keyframes slideIn {
            from {
                transform: translateY(-30px);
                opacity: 0;
            }
            to {
                transform: translateY(0);
                opacity: 1;
            }
        }
    </style>
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <div class="error-container">
            <div class="error-code" runat="server" id="errorCode"></div>
            <div class="error-title" runat="server" id="errorTitle"></div>
            <p class="error-message" runat="server" id="errorMessage"></p>
            <a href="/MenuPrincipal.aspx" class="btn-home" runat="server" id="btnHome"></a>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>
</body>
</html>