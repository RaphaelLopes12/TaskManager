document.addEventListener("DOMContentLoaded", function () {
    var dateInputs = document.querySelectorAll('input[type="date"]');

    dateInputs.forEach(dateInput => {
        // Formata corretamente ao carregar a página
        formatDateInput(dateInput);

        dateInput.addEventListener("change", function () {
            formatDateInput(dateInput);
        });
    });

    function formatDateInput(input) {
        let value = input.value;

        if (value) {
            let parts = value.split("-");
            if (parts.length === 3) {
                let formattedDate = `${parts[2]}/${parts[1]}/${parts[0]}`; // Converte YYYY-MM-DD para DD/MM/YYYY
                input.setAttribute("data-formatted-date", formattedDate);
            }
        }
    }
});
