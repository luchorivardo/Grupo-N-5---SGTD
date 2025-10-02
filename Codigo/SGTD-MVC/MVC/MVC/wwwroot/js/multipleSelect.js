class CustomMultipleSelect {
    constructor(container) {
        this.container = container;
        this.trigger = container.querySelector('.custom-multiple-select-trigger');
        this.optionsList = container.querySelector('.custom-multiple-select-options');
        this.hiddenInputsContainer = container.querySelector('.hidden-inputs');
        this.selectedValue = container.querySelector('.selected-value');
        this.multiple = container.classList.contains('multiple');
        this._init();
    }

    _init() {
        if (!this.trigger || !this.optionsList) return;

        // Inicializar los que ya vienen seleccionados
        this._initializePreselected();

        this.trigger.addEventListener('click', e => {
            e.preventDefault();
            e.stopPropagation();
            this._toggle();
        });

        this.optionsList.querySelectorAll('li').forEach(li => {
            if (!li.classList.contains('disabled')) {
                li.addEventListener('click', e => {
                    e.stopPropagation();
                    this._selectOption(li);
                });
            }
        });

        document.addEventListener('click', e => {
            if (!this.container.contains(e.target)) {
                this._close();
            }
        });
    }

    _initializePreselected() {
        const preselected = this.optionsList.querySelectorAll('li.selected');
        if (preselected.length > 0) {
            preselected.forEach(li => {
                const value = li.getAttribute('data-value');
                // Crear inputs ocultos para los seleccionados iniciales
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = this.container.dataset.name;
                input.value = value;
                input.dataset.value = value;
                this.hiddenInputsContainer.appendChild(input);
            });

            const selectedTexts = Array.from(preselected).map(li => li.textContent.trim());
            this.selectedValue.textContent = selectedTexts.join(', ');
            this.selectedValue.classList.remove('placeholder');
        }
    }

    _toggle() {
        this.container.classList.toggle('open');
    }

    _close() {
        this.container.classList.remove('open');
    }

    _selectOption(optionLi) {
        const value = optionLi.getAttribute('data-value');
        const text = optionLi.textContent.trim();

        if (this.multiple) {
            optionLi.classList.toggle('selected');

            if (optionLi.classList.contains('selected')) {
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = this.container.dataset.name;
                input.value = value;
                input.dataset.value = value;
                this.hiddenInputsContainer.appendChild(input);
            } else {
                const input = this.hiddenInputsContainer.querySelector(`input[data-value="${value}"]`);
                if (input) input.remove();
            }

            const selectedTexts = Array.from(this.optionsList.querySelectorAll('li.selected'))
                .map(li => li.textContent.trim());
            this.selectedValue.textContent = selectedTexts.length > 0
                ? selectedTexts.join(', ')
                : 'Seleccione uno o más rubros';
            this.selectedValue.classList.toggle('placeholder', selectedTexts.length === 0);

        } else {
            this.optionsList.querySelectorAll('li').forEach(li => li.classList.remove('selected'));
            optionLi.classList.add('selected');

            this.selectedValue.textContent = text;
            this.selectedValue.classList.remove('placeholder');

            this.hiddenInputsContainer.innerHTML = '';
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'RubroId';
            input.value = value;
            this.hiddenInputsContainer.appendChild(input);

            this._close();
        }
    }
}

document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.custom-multiple-select-container')
        .forEach(c => new CustomMultipleSelect(c));
});
