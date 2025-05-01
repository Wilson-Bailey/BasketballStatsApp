document.addEventListener("DOMContentLoaded", function () {
    // Flip card on click
    document.querySelectorAll(".flip-container").forEach(card => {
        card.addEventListener("click", function () {
            this.classList.toggle("clicked");
        });
    });

    const searchInput = document.getElementById('playerSearch');
    const playerCards = document.querySelectorAll('.player-card');

    if (searchInput) {
        searchInput.addEventListener('input', function () {
            const query = this.value.trim().toLowerCase();

            playerCards.forEach(card => {
                const searchableText = card.getAttribute('data-name').toLowerCase();
                if (searchableText.includes(query)) {
                    card.style.display = '';
                } else {
                    card.style.display = 'none';
                }
            });
        });
    }

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
