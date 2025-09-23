class CustomSelect {
    constructor(container) {
        this.container = container;
        this.trigger = container.querySelector('.custom-select-trigger');
        this.optionsList = container.querySelector('.custom-select-options');
        this.hiddenInput = container.querySelector('input[type="hidden"]');
        this.selectedValue = container.querySelector('.selected-value');
        this._init();
    }

    _init() {
        if (!this.trigger || !this.optionsList) return;

        // 1) Toggle open/close al hacer click en el trigger
        this.trigger.addEventListener('click', e => {
            e.preventDefault();
            e.stopPropagation();
            this._toggle();
        });

        // 2) Delegación: escucha clicks en el UL y detecta si fue un <li>
        this.optionsList.addEventListener('click', e => {
            const li = e.target.closest('li');
            if (li && this.optionsList.contains(li)) {
                e.stopPropagation();
                this._selectOption(li);
            }
        });

        // 3) Cerrar al clicar fuera
        document.addEventListener('click', e => {
            if (!this.container.contains(e.target)) {
                this._close();
            }
        });
    }

    _toggle() {
        if (this.container.classList.contains('open')) {
            this._close();
        } else {
            this._open();
        }
    }

    _open() {
        // cerrar otros abiertos
        document
            .querySelectorAll('.custom-select-container.open')
            .forEach(c => c !== this.container && c.classList.remove('open'));
        this.container.classList.add('open');
    }

    _close() {
        this.container.classList.remove('open');
    }

    _selectOption(optionLi) {
        // marcar seleccionado
        this.optionsList
            .querySelectorAll('li')
            .forEach(li => li.classList.remove('selected'));
        optionLi.classList.add('selected');

        // actualizar trigger
        const value = optionLi.getAttribute('data-value');
        const text = optionLi.textContent.trim();

        if (this.selectedValue && this.hiddenInput.name !== 'opcion') {
            this.selectedValue.textContent = text;
            this.selectedValue.classList.remove('placeholder'); // quita el gris
        }

        // actualizar hidden input
        if (this.hiddenInput) {
            this.hiddenInput.value = value;
        }

        this._close();

        // disparar evento externo
        this.container.dispatchEvent(new CustomEvent('custom-select:change', {
            detail: { value, text }
        }));
    }
}

// auto-init
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.custom-select-container')
        .forEach(c => new CustomSelect(c));
});
