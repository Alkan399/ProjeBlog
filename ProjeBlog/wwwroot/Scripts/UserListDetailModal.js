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
        status: document.getElementById("filterStatus").value
    };

    fetch('/User/FilterUsers', {
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

function updateTable(users) {
    const tableBody = document.querySelector("#tableUsers tbody");
    tableBody.innerHTML = ""; // Mevcut tabloyu temizle

    // Eğer gelen içerik boşsa, kullanıcıya bilgi ver
    if (users == null) {
        const noDataRow = `
            <tr>
                <td colspan="7" style="text-align:center; color: red;">Veri bulunamadı.</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", noDataRow);
    }
    else {
        users.forEach(user => {
            const row = `
            <tr scope="row">
                <td>${(user.ID).toString()}</td>
                <th>${user.UserName}</th>
                <td>${user.Email}</td>
                <td>${new Date(user.CreatedDate).toLocaleDateString()}</td>
                <td>${new Date(user.UpdatedDate).toLocaleDateString()}</td>
                <th>${getRoleText(user.Role)}</th>
                <th>${getStatusText(user.Status)}</th>
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
