namespace CasaDeCambio.Models
{
    public class CambioMonedaModel
    {
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal Monto { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal MontoConvertido => Monto * TipoCambio;
    }
}
