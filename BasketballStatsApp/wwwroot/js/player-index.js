document.addEventListener("DOMContentLoaded", function () {
    // Flip card on click
    document.querySelectorAll(".flip-container").forEach(card => {
        card.addEventListener("click", function () {
            this.classList.toggle("clicked");
        });
    });

    // AJAX Delete Player
    document.querySelectorAll(".ajax-delete").forEach(button => {
        button.addEventListener("click", async function (event) {
            event.stopPropagation();
            event.preventDefault();

            const playerId = this.dataset.id;
            const confirmed = confirm("Are you sure you want to delete this player?");
            if (!confirmed) return;

            const token = document.querySelector('#antiForgeryForm input[name="__RequestVerificationToken"]')?.value;

            const response = await fetch(`/Player/DeleteConfirmedAjax/${playerId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                }
            });

            if (response.ok) {
                this.closest(".col-md-3").remove();
            } else {
                const errText = await response.text();
                console.error("Delete failed:", response.status, errText);
                alert("Failed to delete player.");
            }
        });
    });
});
