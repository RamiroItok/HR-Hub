<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCompras.aspx.cs" Inherits="GUI.ReporteCompras" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Reporte de Productos Más Comprados</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/Reporte.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

        <uc:NavBar runat="server" ID="NavBarControl" />

        <div class="containerReporte mt-5">
            <h2>Reporte de Productos Más Comprados</h2>
            <p>Este gráfico muestra los productos más comprados, agrupados por mes y cantidad comprada.</p>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="button-container">
                        <asp:Button ID="btnGenerarXML" runat="server" Text="Generar XML" CssClass="btn btn-secondary" OnClick="btnGenerarXML_Click" />
                        <div class="form-group">
                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnGenerarReporte" runat="server" Text="Generar Reporte" CssClass="btn btn-primary" OnClick="btnGenerarReporte_Click" />
                    </div>

                    <div id="chartContainer"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>

    <script>
        function renderCharts(data) {
            const container = document.getElementById('chartContainer');
            container.innerHTML = '';

            // Paleta de colores basada en el logo de HR Hub
            const colors = [
                'rgba(103, 165, 68, 0.8)',  // Verde principal
                'rgba(78, 126, 50, 0.8)',   // Verde oscuro
                'rgba(51, 51, 51, 0.8)',    // Gris oscuro
                'rgba(130, 185, 109, 0.8)', // Verde claro
                'rgba(200, 200, 200, 0.8)'  // Gris claro
            ];

            Object.keys(data).forEach((mes, index) => {
                const productos = data[mes];
                const labels = productos.map(p => p.Nombre);
                const values = productos.map(p => p.VecesComprado);

                const chartContainer = document.createElement('div');
                chartContainer.classList.add('chart-container');

                const title = document.createElement('h3');
                title.textContent = `Productos vendidos en ${mes}`;
                chartContainer.appendChild(title);

                const canvas = document.createElement('canvas');
                chartContainer.appendChild(canvas);
                container.appendChild(chartContainer);

                // Crear el gráfico de torta
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
                        animation: {
                            animateScale: true,
                            animateRotate: true
                        },
                        plugins: {
                            legend: { position: 'top' },
                            title: { display: true, text: `Productos Vendidos en ${mes}` },
                            datalabels: {
                                color: '#fff', // Color del texto
                                font: {
                                    weight: 'bold'
                                },
                                formatter: (value, context) => {
                                    return `${value}`; // Muestra la cantidad en cada sección
                                },
                                anchor: 'center',
                                align: 'center'
                            }
                        }
                    }
                });
            });
        }
    </script>
</body>
</html>