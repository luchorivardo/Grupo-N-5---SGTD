document.addEventListener('DOMContentLoaded', function () {
    const switchInput = document.getElementById('estadoSwitch');
    const switchLabel = document.getElementById('estadoLabel');
    const hiddenEstado = document.getElementById('EstadoId');

    // Inicializar valores
    switchLabel.textContent = switchInput.checked ? "Activo" : "Inactivo";
    hiddenEstado.value = switchInput.checked ? 1 : 2;

    // Actualizar dinámicamente
    switchInput.addEventListener('change', () => {
        if (switchInput.checked) {
            switchLabel.textContent = "Activo";
            hiddenEstado.value = 1;
        } else {
            switchLabel.textContent = "Inactivo";
            hiddenEstado.value = 2;
        }
    });
});