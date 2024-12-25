document.addEventListener("DOMContentLoaded", function () {
    const url = `/Blog/Blog/FilterContents?page=${encodeURIComponent(page)}`;

    fetch(url, {
        method: 'POST',

        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(filters)
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            updateTable(data);
        })
        .catch(error => console.error('Error:', error));
});