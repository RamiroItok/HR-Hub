<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="GUI.Error" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Error 404 - Página no encontrada</title>
    <link rel="stylesheet" type="text/css" href="/Style/Error.css" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('Content/imagenes/Fondo.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; margin: 0;">
    <form id="form1" runat="server">
        <div class="error-container">
            <div class="image-container animate-fade-in">
                <img src="Content/imagenes/404.png" alt="404 Trabajando" />
            </div>
            <h1 class="animate-slide-in">Oops... ¡Página no encontrada!</h1>
            <p class="description animate-slide-in">
                Parece que la página que buscas no existe o está en construcción. <br />
                Estamos trabajando para que pronto esté disponible.
            </p>
            <a href="Home.aspx" class="btn animate-bounce">Volver al Inicio</a>
        </div>
    </form>
</body>
</html>