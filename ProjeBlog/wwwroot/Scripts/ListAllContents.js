
document.getElementById("filterButton").addEventListener("click", function () {
    const filters = {
        userName: document.getElementById("filterUserName").value,
        title: document.getElementById("filterTitle").value,
        entry: document.getElementById("filterEntry").value,
        status: document.getElementById("filterStatus").value
    };

    fetch('/Management/Content/FilterContents', {
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

function updateTable(contents) {
    const tableBody = document.querySelector("#tableContents tbody");
    tableBody.innerHTML = ""; // Mevcut tabloyu temizle

    (contents.$values).forEach(content => {
        const row = `
            <tr scope="row">
                <td>${content.id}</td>
                <th>${content.appUser.userName}</th>
                <td>
                    <a href="${content.coverImagePath}">
                        <img src="${content.coverImagePath}" style="width:60px">
                    </a>
                </td>
                <td>${content.title}</td>
                <td>${new Date(content.createdDate).toLocaleDateString()}</td>
                <td>${new Date(content.updatedDate).toLocaleDateString()}</td>
                <th>${getStatusText(content.status)}</th>
                <td>
                    <a href="/Management/User/Update/${content.id}">Edit</a> |
                    <a href="javascript:void(0);" class="details-btn">Görüntüle</a> | 
                    <a href="/Management/User/Delete/${content.id}">Delete</a>
                </td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", row); // Yeni satırları tabloya ekle
    });
}
function getStatusText(status) {
    switch (status) {
        case 1:
            return 'Aktif';
        case 2:
            return 'Aktif';
        case 3:
            return 'Silinmiş';
        default:
            return status; // Eğer bilinmeyen bir status varsa, olduğu gibi göster
    }
}