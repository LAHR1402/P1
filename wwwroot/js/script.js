document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("form-conversion").addEventListener("submit", function (event) {
        event.preventDefault();

        let cantidad = parseFloat(document.getElementById("cantidad").value);
        let monedaOrigen = document.getElementById("monedaOrigen").value;
        let monedaDestino = document.getElementById("monedaDestino").value;

        let tipoCambio = 0.634; // Aquí podrías obtenerlo dinámicamente en el futuro
        let resultado = cantidad * tipoCambio;

        document.getElementById("resultado").innerText = resultado.toFixed(2);
    });
});
