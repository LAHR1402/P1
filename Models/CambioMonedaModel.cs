namespace CasaDeCambio.Models
{
    public class CambioMonedaModel
    {
        public decimal Cantidad { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Resultado { get; set; }
    }
}
