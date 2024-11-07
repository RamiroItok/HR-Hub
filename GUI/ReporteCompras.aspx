<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCompras.aspx.cs" Inherits="GUI.ReporteCompras" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - <asp:Literal ID="litPageTitle" runat="server" Text="Reporte de Productos Mas Comprados"></asp:Literal></title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/Reporte.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo2.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="containerReporte mt-5">
            <h2><asp:Literal ID="litTitle" runat="server"></asp:Literal></h2>
            <p><asp:Literal ID="litDescription" runat="server"></asp:Literal></p>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="button-container">
                        <asp:Button ID="btnGenerarXML" runat="server" CssClass="btn btn-secondary" OnClick="btnGenerarXML_Click" />
                        <div class="form-group">
                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnGenerarReporte" runat="server" CssClass="btn btn-primary" OnClick="btnGenerarReporte_Click" />
                    </div>

                    <div id="chartContainer"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function mostrarNotificacionXMLGenerado(title, text) {
            Swal.fire({
                icon: 'success',
                title: title,
                text: text,
                showConfirmButton: true,
                confirmButtonText: 'OK'
            });
        }

        function mostrarNotificacionSinDatos(title, text) {
            Swal.fire({
                icon: 'warning',
                title: title,
                text: text,
                showConfirmButton: true,
                confirmButtonText: 'OK'
            });
        }

        function renderCharts(data, title) {
            const container = document.getElementById('chartContainer');
            container.innerHTML = '';

            const colors = [
                'rgba(103, 165, 68, 0.8)',
                'rgba(78, 126, 50, 0.8)',
                'rgba(51, 51, 51, 0.8)',
                'rgba(130, 185, 109, 0.8)',
                'rgba(200, 200, 200, 0.8)'
            ];

            Object.keys(data).forEach((mes, index) => {
                const productos = data[mes];
                const labels = productos.map(p => p.Nombre);
                const values = productos.map(p => p.VecesComprado);

                const chartContainer = document.createElement('div');
                chartContainer.classList.add('chart-container');

                const chartTitle = document.createElement('h3');
                chartTitle.textContent = `${title} ${mes}`;
                chartContainer.appendChild(chartTitle);

                const canvas = document.createElement('canvas');
                chartContainer.appendChild(canvas);
                container.appendChild(chartContainer);

                new Chart(canvas.getContext('2d'), {
                    type: 'pie',
                    data: {
                        labels: labels,
                        datasets: [{
                            data: values,
                            backgroundColor: colors.slice(0, labels.length),
                            borderColor: 'rgba(255, 255, 255, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { position: 'top' }
                        }
                    }
                });
            });
        }
    </script>
        
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
