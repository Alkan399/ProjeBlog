import { getStatusText, checkDateTime } from './CommonLibrary/CommonRepo.js';
let currentPage = 1;
let pageToGo = 1;
// Modal'ı açma ve kapama işlemleri
var modal = document.getElementById("myModal");
var closeModal = document.getElementsByClassName("close")[0];

// Detaylar linkine tıklanıldığında modal açılacak
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("filterButton").click();
    attachDetailsEventListeners();
});

// Modal'ı kapat
if (closeModal) {
    closeModal.onclick = function () {
        modal.style.display = "none";
    }
}

// Modal dışına tıklayınca modal'ı kapat
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

document.getElementById("filterButton").addEventListener("click", function () {

    const filters = {
        userName: document.getElementById("filterUserName").value,
        email: document.getElementById("filterEmail").value,
        role: document.getElementById("filterRole").value,
        status: document.getElementById("filterStatus").value,
        itemsPerPage: document.getElementById("dropdownPage").value
    };
    const page = parseInt(pageToGo);
    const url = `/Management/User/FilterUsers?page=${encodeURIComponent(page)}`;

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

function updateTable(users) {
    pageToGo = 1;
    const tableBody = document.querySelector("#tableContents tbody");
    const tableFooter = document.querySelector("#tableContents tfoot tr td");
    tableBody.innerHTML = ""; // Mevcut tabloyu temizle
    console.log(pageToGo);
    console.log(users.Contents);

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

    for (let i = 1; i < users.PageCount + 1; i++) {
        const rowFooter = `
                    
                        <a href="" class="page" data-page="${i}">
                            ${i}
                        </a>
                    
                    `;
        tableFooter.insertAdjacentHTML("beforeend", rowFooter);

    }

    if (currentPage != users.PageCount) {
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
    console.log(users);
    // Eğer gelen içerik boşsa, kullanıcıya bilgi ver
    if (users.Contents.length == 0) {
        const noDataRow = `
            <tr>
                <td colspan="7" style="text-align:center; color: red;">Veri bulunamadı.</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", noDataRow);
    }
    else {
        users.Contents.forEach(user => {
            const row = `
            <tr scope="row" style="text-align:center;">
                <td>${(user.ID).toString()}</td>
                <td><b>${user.UserName}</b></td>
                <td>${user.Email}</td>
                <td>${new Date(user.CreatedDate).toLocaleDateString()}</td>
                <td>${new Date(user.UpdatedDate).toLocaleDateString()}</td>
                <td>${getRoleText(user.Role)}</td>
                <td><b>${getStatusText(user.Status)}</b></td>
                <td>
                    <a href="/Management/User/Update/${(user.ID).toString()}">Edit</a> |
                    <a href="javascript:void(0);" class="details-btn"
                    data-id="${(user.ID).toString()}"
                    data-username="${user.UserName}"
                    data-email="${user.Email}"
                    data-created="${user.CreatedDate}"
                    data-updated="${user.UpdatedDate}"
                    data-role="${user.Role}"
                    data-status="${user.Status}"
                    data-firstname="${user.AppUserDetail.FirstName || ''}"
                    data-lastname="${user.AppUserDetail.LastName || ''}"
                    data-dateofbirth="${user.AppUserDetail.DateOfBirth || ''}">
                    Details
                   </a> |
                   <a href="/Management/User/Delete/${(user.ID).toString()}">Delete</a>
                </td>
            </tr>
        `;
            tableBody.insertAdjacentHTML("beforeend", row); // Yeni satırları tabloya ekle
        });
       
    }
    attachDetailsEventListeners();
}


function getRoleText(role) {
    switch (role) {
        case 0:
            return 'Admin';
        case 1:
            return 'Kullanıcı';
        case 2:
            return 'Visitor';
        default:
            return role; // Eğer bilinmeyen bir role varsa, olduğu gibi göster
    }
}
//function getStatusText(status) {
//    switch (status) {
//        case 1:
//            return 'Aktif';
//        case 2:
//            return 'Aktif';
//        case 3:
//            return 'Silinmiş';
//        default:
//            return status; // Eğer bilinmeyen bir status varsa, olduğu gibi göster
//    }
//}

function attachDetailsEventListeners() {
    const detailsBtns = document.querySelectorAll(".details-btn");
    detailsBtns.forEach(function (button) {
        button.addEventListener("click", function () {
            var id = this.getAttribute("data-id");
            var username = this.getAttribute("data-username");
            var email = this.getAttribute("data-email");
            var createdDate = this.getAttribute("data-created");
            var updatedDate = this.getAttribute("data-updated");
            var role = this.getAttribute("data-role");
            var status = this.getAttribute("data-status");
            var firstName = this.getAttribute("data-firstname");
            var lastName = this.getAttribute("data-lastname");
            var dateOfBirth = this.getAttribute("data-dateofbirth");

            // Modal içeriğini güncelle
            if (document.getElementById("modalId")) {
                document.getElementById("modalId").textContent = id;
            }
            if (document.getElementById("modalUserName")) {
                document.getElementById("modalUserName").textContent = username;
            }
            if (document.getElementById("modalEmail")) {
                document.getElementById("modalEmail").textContent = email;
            }
            if (document.getElementById("modalCreatedDate")) {
                document.getElementById("modalCreatedDate").textContent = createdDate;
            }
            if (document.getElementById("modalUpdatedDate")) {
                document.getElementById("modalUpdatedDate").textContent = updatedDate;
            }
            if (document.getElementById("modalRole")) {
                document.getElementById("modalRole").textContent = role;
            }
            if (document.getElementById("modalStatus")) {
                document.getElementById("modalStatus").textContent = status;
            }
            if (document.getElementById("modalFirstName")) {
                document.getElementById("modalFirstName").textContent = firstName;
            }
            if (document.getElementById("modalLastName")) {
                document.getElementById("modalLastName").textContent = lastName;
            }
            if (document.getElementById("modalDateOfBirth")) {
                document.getElementById("modalDateOfBirth").textContent = dateOfBirth;
            }

            // Modal'ı göster
            modal.style.display = "block";
        });
    });
}
