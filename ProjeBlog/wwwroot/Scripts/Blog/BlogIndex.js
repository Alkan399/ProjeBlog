document.addEventListener("DOMContentLoaded", function () {
    const categoryTags = document.getElementsByName("categoryTag");

    for (let i = 0; i < categoryTags.length; i++) {
        categoryTags[i].addEventListener("click", function () {

            for (let j = 0; j < categoryTags.length; j++) {
                categoryTags[j].classList.remove("active");
            }

            categoryTags[i].classList.add("active");

            document.getElementById("sendButton").click();
        });
    }
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault(); // Sayfanın yeniden yüklenmesini önler

    // Input alanlarından değerleri al
    var filterCategory = null;

    

    var CategoryTagList = document.querySelectorAll('[name="categoryTag"]');
    console.log(CategoryTagList);

    for (var i = 0; i < CategoryTagList.length; i++) {
        console.log(CategoryTagList[i]);
        if (CategoryTagList[i].className == "active" && CategoryTagList[i].innerText != "Hepsi") {
            filterCategory = CategoryTagList[i].dataset.categoryid;
        }
        else if (CategoryTagList[i].innerText == "Hepsi")
        {
            filterCategory = null;
        }
        
    }

    const filterTitle = document.getElementById("titleInput").value;
    const filterEntry = document.getElementById("entryInput").value;
    const page = 1; // İlk sayfadan başlatmak için sabit bir değer veriyoruz

    // URL oluştur ve yönlendir
    const baseUrl = "/Blog/Blog/Index"; // Hedef controller/action
    const queryString = `?filterTitle=${encodeURIComponent(filterTitle)}&filterEntry=${encodeURIComponent(filterEntry)}&filterCategory=${encodeURIComponent(filterCategory)}&page=${page}`;
    window.location.href = baseUrl + queryString;
});