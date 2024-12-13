
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("filterButton").click();
});


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
    console.log(contents);

    // Eğer gelen içerik boşsa, kullanıcıya bilgi ver
    if (contents == null) {
        const noDataRow = `
            <tr>
                <td colspan="7" style="text-align:center; color: red;">Veri bulunamadı.</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", noDataRow);
    }
    else {
        // İçerik varsa, tabloyu doldur
        contents.forEach(content => {
            const row = `
                <tr scope="row">
                    <td>${(content.ID).toString()}</td>
                    <th>${content.AppUser.UserName}</th>
                    <td>
                        <a href="${content.CoverImagePath}">
                            <img src="${content.CoverImagePath}" style="width:60px">
                        </a>
                    </td>
                    <td>${content.Title}</td>
                    <td>${new Date(content.CreatedDate).toLocaleDateString()}</td>
                    <td>${new Date(content.UpdatedDate).toLocaleDateString()}</td>
                    <th>${getStatusText(content.Status)}</th>
                    <td>
                        <a href="/Management/Content/Update/${(content.ID).toString()}">Edit</a> |
                        <a href="/Management/Content/Details/${(content.ID).toString()}";" class="details-btn">Görüntüle</a> | 
                        <a href="/Management/Content/DeleteManagement/${(content.ID).toString()}">Delete</a>
                    </td>
                </tr>
            `;
            tableBody.insertAdjacentHTML("beforeend", row); // Yeni satırları tabloya ekle
        });
    }
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