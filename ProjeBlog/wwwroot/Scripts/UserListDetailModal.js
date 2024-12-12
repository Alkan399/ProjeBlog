// Modal'ı açma ve kapama işlemleri
var modal = document.getElementById("myModal");
var closeModal = document.getElementsByClassName("close")[0];

// Detaylar linkine tıklanıldığında modal açılacak
var detailsBtns = document.querySelectorAll(".details-btn");
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
            // Tabloyu güncellemek için gelen veriyi işleyin
            console.log(data);
            updateTable(data);
            // TODO: Gelen data ile tabloyu yeniden render edin.
        })
        .catch(error => console.error('Error:', error));
});

function updateTable(users) {
    const tableBody = document.querySelector("#example2 tbody");
    tableBody.innerHTML = ""; // Mevcut tabloyu temizle

    users.forEach(user => {
        const row = `
            <tr scope="row">
                <td>${user.id}</td>
                <th>${user.userName}</th>
                <td>${user.email}</td>
                <td>${new Date(user.createdDate).toLocaleDateString()}</td>
                <td>${new Date(user.updatedDate).toLocaleDateString()}</td>
                <th>${getRoleText(user.role)}</th>
                <th>${getStatusText(user.status)}</th>
                <td>
                    <a href="/User/Update/${user.id}">Edit</a> |
                    <a href="javascript:void(0);" class="details-btn"
                       data-id="${user.id}"
                       data-username="${user.userName}"
                       data-email="${user.email}"
                       data-created="${user.createdDate}"
                       data-updated="${user.updatedDate}"
                       data-role="${user.role}"
                       data-status="${user.status}">
                    Details
                   </a> |
                   <a href="/User/Delete/${user.id}">Delete</a>
                </td>
            </tr>
        `;
        tableBody.insertAdjacentHTML("beforeend", row); // Yeni satırları tabloya ekle
    });
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
            return 'Inserted';
        case 2:
            return 'Updated';
        case 3:
            return 'Deleted';
        default:
            return status; // Eğer bilinmeyen bir status varsa, olduğu gibi göster
    }
}

