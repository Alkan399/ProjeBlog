
import { getStatusText, checkDateTime } from './CommonLibrary/CommonRepo.js';
let currentPage = 1;
let pageToGo = 1;
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("filterButton").click();
});



document.getElementById("filterButton").addEventListener("click", function () {
    const filters = {
        firstName: document.getElementById("filterFirstName").value || null,
        lastName: document.getElementById("filterLastName").value || null,
        email: document.getElementById("filterEmail").value || null,
        status: document.getElementById("filterStatus").value || null,
        itemsPerPage: document.getElementById("dropdownPage").value || null,
        dateOfBirth: document.getElementById("filterDateOfBirth").value.toString() || null
    };

    const page = parseInt(pageToGo);
    console.log(filters);

    fetch(`/Management/Basvuru/FilterBasvurus?page=${encodeURIComponent(page)}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(filters)  // JSON.stringify ile gönderim yapıyoruz
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            updateTable(data);
        })
        .catch(error => console.error(error));
    currentPage = pageToGo;
});

function updateTable(basvurus) {
    pageToGo = 1;
    const tableBody = document.querySelector("#tableBasvurus tbody");
    const tableFooter = document.querySelector("#tableBasvurus tfoot tr td");
    tableBody.innerHTML = ""; // Mevcut tabloyu temizle
    console.log(pageToGo);
    console.log(basvurus.Contents);

    tableFooter.innerHTML = "";

    const back = `
                        <a href="" class="page" data-page="${currentPage - 1}">
                            <
                        </a>
                    
                    `;
    const forward = `
                        <a href="" class="page" data-page="${currentPage + 1}">
                            >
                        </a>
                    
                    `;
    if (currentPage != 1) {
        tableFooter.insertAdjacentHTML("beforeend", back);
    }

    for (let i = 1; i < basvurus.PageCount + 1; i++) {
        const rowFooter = `
                    
                        <a href="" class="page" data-page="${i}">
                            ${i}
                        </a>
                    
                    `;
        tableFooter.insertAdjacentHTML("beforeend", rowFooter);

    }

    if (currentPage != basvurus.PageCount) {
        tableFooter.insertAdjacentHTML("beforeend", forward);
    }


    const pageLinks = document.getElementsByClassName("page");
    Array.from(pageLinks).forEach(link => {
        if (currentPage != link.dataset.page) {
            link.style.textDecoration = "none";
        }
        else {
            link.style.textDecoration = "underline";
        }

        link.addEventListener("click", function (event) {
            event.preventDefault(); // Varsayılan link davranışını engelle
            console.log(this.dataset.page);
            pageToGo = parseInt(this.dataset.page); // Data-page değerini al
            document.getElementById("filterButton").click(); // Filtre butonunu tetikle
        });
    });


    // Eğer gelen içerik boşsa, kullanıcıya bilgi ver
    if (basvurus.Contents.length == 0) {
        const noDataRow = `
            <tr>
                <td colspan="7" style="text-align:center; color: red;">Veri bulunamadı.</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", noDataRow);
    }
    else {
        // İçerik varsa, tabloyu doldur
        basvurus.Contents.forEach(content => {
            const row = `
                <tr scope="row" style="text-align:center;">
                    <td>${(content.ID).toString()}</td>
                    <td style=""><b>${content.FirstName}</b></td>
                    <td style=""><b>${content.LastName}</b></td>
                    <td>${content.Email}</td>
                    <td>${new Date(content.DateOfBirth).toLocaleDateString()}</td>
                    <td>${new Date(content.CreatedDate).toLocaleDateString()}</td>
                    <td>${checkDateTime(new Date(content.UpdatedDate).toLocaleDateString())}</td>
                    <td>${getStatusText(content.Status)}</td>
                    <td>
                        <a href="/Management/Basvuru/Update/${(content.ID).toString()}">Edit</a> |
                        <a href="/Management/Basvuru/DetailsManagement/${(content.ID).toString()}" class="details-btn">Görüntüle</a> | 
                        <a href="/Management/basvuru/Delete/${(content.ID).toString()}">Delete</a>
                    </td>
                </tr>
            `;
            tableBody.insertAdjacentHTML("beforeend", row); // Yeni satırları tabloya ekle
        });

    }
}




