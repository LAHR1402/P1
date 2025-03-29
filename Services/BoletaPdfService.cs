using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class BoletaPdfService
{
    public byte[] GenerarBoleta(string nombre, string dni, decimal monto, string monedaOrigen, string monedaDestino, decimal tipoCambio)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Text("Boleta de Cambio").Bold().FontSize(20);

                page.Content().Column(col =>
                {
                    col.Item().Text($"Nombre: {nombre}");
                    col.Item().Text($"DNI: {dni}");
                    col.Item().Text($"Monto enviado: {monto} {monedaOrigen}");
                    col.Item().Text($"Tipo de cambio: {tipoCambio}");
                    col.Item().Text($"Monto recibido: {monto * tipoCambio} {monedaDestino}");
                });

                page.Footer().AlignRight().Text("Gracias por su preferencia").Italic();
            });
        });

        byte[] pdfBytes = document.GeneratePdf(); 
        return pdfBytes;
    }
}
