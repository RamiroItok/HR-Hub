<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentoReporte.aspx.cs" Inherits="GUI.DocumentoReporte" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="NavBar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HR Hub - Documento Reporte</title>

    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Style/NavBar.css" rel="stylesheet" />
    <link href="/Style/DocumentoReporte.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('Content/imagenes/Fondo1.jpg'); background-size: cover; background-position: center; background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: center; margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

        <uc:NavBar runat="server" ID="NavBarControl" />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="containerReporte mt-5">
                    <h2 class="text-center"><asp:Literal ID="litReporteTitulo" runat="server" Text="Reporte de Firmas de Documentos"></asp:Literal></h2>
                    <p class="text-center"><asp:Literal ID="litReporteDescripcion" runat="server" Text="Porcentaje de documentos firmados y no firmados por los empleados"></asp:Literal></p>

                    <asp:Button ID="btnGenerarReporte" runat="server" CssClass="btn btn-primary mb-4" OnClick="btnGenerarReporte_Click" />
                    <asp:Button ID="btnGenerarXML" runat="server" CssClass="btn btn-secondary mb-4 ml-2" OnClick="btnGenerarXML_Click" />


                    <div id="chartContainer"></div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGenerarReporte" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" CssClass="language-selector">
            <asp:ListItem Text="Español" Value="es"></asp:ListItem>
            <asp:ListItem Text="English" Value="en"></asp:ListItem>
        </asp:DropDownList>

        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    </form>

<script>
    function renderTortaGrafico(datos) {
        const container = document.getElementById('chartContainer');
        container.innerHTML = '';

        datos.forEach(function (item) {
            const chartContainer = document.createElement('div');
            chartContainer.classList.add('chart-container', 'mb-4');

            const canvas = document.createElement('canvas');
            chartContainer.appendChild(canvas);
            container.appendChild(chartContainer);

            new Chart(canvas, {
                type: 'pie',
                data: {
                    labels: ['Firmado (%)', 'No Firmado (%)'],
                    datasets: [{
                        data: [item.PorcentajeFirmado, item.PorcentajeNoFirmado],
                        backgroundColor: ['rgba(75, 192, 192, 0.6)', 'rgba(255, 99, 132, 0.6)'],
                        borderColor: ['rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)'],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { position: 'top' },
                        title: {
                            display: true,
                            text: item.NombreDocumento
                        },
                        datalabels: {
                            color: '#ffffff',
                            formatter: (value, context) => {
                                const total = context.dataset.data.reduce((acc, val) => acc + val, 0);
                                const percentage = (value / total * 100).toFixed(1) + '%';
                                return percentage;
                            },
                            font: {
                                weight: 'bold',
                                size: 14
                            },
                            anchor: 'center',
                            align: 'center'
                        }
                    }
                },
                plugins: [ChartDataLabels]
            });
        });
    }
</script>
</body>
</html>
