<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="GUI.AccesoDenegado" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Acceso Denegado</title>
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
<body>
    <div class="error-container">
        <div class="error-code">403</div>
        <div class="error-title">Acceso Denegado</div>
        <p class="error-message">Lo sentimos, pero no posee los permisos necesarios para acceder a este contenido.</p>
        <a href="/MenuPrincipal.aspx" class="btn-home">Ir a la página principal</a>
    </div>
</body>
</html>
