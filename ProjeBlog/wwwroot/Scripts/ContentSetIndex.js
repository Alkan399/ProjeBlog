
import { getStatusText, checkDateTime } from './CommonLibrary/CommonRepo.js';
let currentPage = 1;
let pageToGo = 1;
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("filterButton").click();
});



document.getElementById("filterButton").addEventListener("click", function () {
    const filters = {
        id: document.getElementById("filterId").value || null,
        name: document.getElementById("filterName").value || null,
        description: document.getElementById("filterDescription").value || null,
        status: document.getElementById("filterStatus").value || null,
        itemsPerPage: document.getElementById("dropdownPage").value || null

    };
    const page = parseInt(pageToGo);
    const url = `/Management/ContentSet/FilterContents?page=${encodeURIComponent(page)}`;

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
    currentPage = pageToGo;
});

function updateTable(contents) {
    pageToGo = 1;
    const tableBody = document.querySelector("#tableContents tbody");
    const tableFooter = document.querySelector("#tableContents tfoot tr td");
    tableBody.innerHTML = ""; // Mevcut tabloyu temizle
    console.log(pageToGo);
    console.log(contents.Contents);

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

    for (let i = 1; i < contents.PageCount + 1; i++) {
        const rowFooter = `
                    
                        <a href="" class="page" data-page="${i}">
                            ${i}
                        </a>
                    
                    `;
        tableFooter.insertAdjacentHTML("beforeend", rowFooter);

    }

    if (currentPage != contents.PageCount) {
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
    if (contents.Contents.length == 0) {
        const noDataRow = `
            <tr>
                <td colspan="9" style="text-align:center; color: red;">Veri bulunamadı.</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", noDataRow);
    }
    else {
        // İçerik varsa, tabloyu doldur
        contents.Contents.forEach(content => {
            const row = `
                <tr scope="row" style="text-align:center;">
                    <td>${(content.ID).toString()}</td>
                    <td style=""><b>${content.Name}</b></td>
                    <td>${content.Description}</td>
                    <td>${content.ContentSetContent}</td>
                    <td>${new Date(content.CreatedDate).toLocaleDateString()}</td>
                    <td>${checkDateTime(new Date(content.UpdatedDate).toLocaleDateString())}</td>
                    <td>${getStatusText(content.Status)}</td>
                    <td>
                        <a href="/Management/Content/Update/${(content.ID).toString()}">Edit</a> |
                        <a href="/Management/ContentSetContent/Index/${(content.ID).toString()}" class="details-btn">İçerikleri Görüntüle</a> | 
                        <a href="/Management/Content/DeleteManagement/${(content.ID).toString()}">Delete</a>
                    </td>
                </tr>
            `;
            tableBody.insertAdjacentHTML("beforeend", row); // Yeni satırları tabloya ekle
        });

    }
}




