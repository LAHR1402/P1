using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using CasaDeCambio.Models;
using Microsoft.AspNetCore.Mvc;


namespace CasaDeCambio.Controllers
{
    public class CambioMonedaController : Controller
    {
        public IActionResult Index()
        {
            return View(new CambioMonedaModels()); // Evita que Model sea null
        }

        [HttpPost]
        public IActionResult Convertir(CambioMonedaModels model)
        {
            if (model == null || model.Cantidad <= 0)
            {
                ModelState.AddModelError("", "Ingrese una cantidad válida.");
                return View("Index", model);
            }

                    double tasaCambio = ObtenerTasaCambio(model.MonedaOrigen, model.MonedaDestino);
                        model.Resultado = model.Cantidad * tasaCambio;

                        return View("Index", model);
        }

        private decimal ObtenerTasaCambio(string monedaOrigen, string monedaDestino)
        {
            // Simulación de tasas de cambio
            if (monedaOrigen == "BRL" && monedaDestino == "PEN") return 0.74m;
            if (monedaOrigen == "PEN" && monedaDestino == "BRL") return 1.35m;
            if (monedaOrigen == "USD" && monedaDestino == "PEN") return 3.85m;
            if (monedaOrigen == "PEN" && monedaDestino == "USD") return 0.26m;
            if (monedaOrigen == "USD" && monedaDestino == "BRL") return 5.00m;
            if (monedaOrigen == "BRL" && monedaDestino == "USD") return 0.20m;

            return 1; // Si es la misma moneda
        }

        [HttpPost]
        public IActionResult GenerarBoleta(CambioMonedaModels model)
        {
            if (model == null || string.IsNullOrEmpty(model.Nombre) || string.IsNullOrEmpty(model.Apellido) || string.IsNullOrEmpty(model.Correo))
            {
                ModelState.AddModelError("", "Complete todos los datos.");
                return View("Index", model);
            }

            // Generar PDF
            byte[] pdfBytes = CrearBoletaPDF(model);

            return File(pdfBytes, "application/pdf", "Boleta.pdf");
        }

        private byte[] CrearBoletaPDF(CambioMonedaModels model)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, ms);

                document.Open();
                document.Add(new Paragraph("Boleta de Cambio de Moneda"));
                document.Add(new Paragraph($"Nombre: {model.Nombre} {model.Apellido}"));
                document.Add(new Paragraph($"Correo: {model.Correo}"));
                document.Add(new Paragraph($"Monto Cambiado: {model.Resultado} {model.MonedaDestino}"));
                document.Add(new Paragraph($"Fecha: {DateTime.Now}"));

                document.Close();
                return ms.ToArray();
            }
        }
    }
}
