var page = document.getElementsByName("currentPage")[0].dataset.currentpage;
document.addEventListener("DOMContentLoaded", function () {
    const categoryTags = document.getElementsByName("categoryTag");
    const previousPageButton = document.getElementsByName("previousPageButton");
    const nextPageButton = document.getElementsByName("nextPageButton");
    console.log(nextPageButton);

    if (previousPageButton[0] != null) {
        previousPageButton[0].addEventListener("click", function () {
            page--;
            document.getElementById("sendButton").click();
        });
    }
    if (nextPageButton[0] != null) {
        nextPageButton[0].addEventListener("click", function () {
            page++;
            console.log(page);
            document.getElementById("sendButton").click();
        });
    }
    
    for (let i = 0; i < categoryTags.length; i++) {
        categoryTags[i].addEventListener("click", function () {

            for (let j = 0; j < categoryTags.length; j++) {
                categoryTags[j].classList.remove("active");
            }
            page = 1;
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
    
    const itemsPerPage = 5;
    const baseUrl = "/Blog/Blog/Index";
    const queryString = `?filterTitle=${encodeURIComponent(filterTitle)}&filterEntry=${encodeURIComponent(filterEntry)}&filterCategory=${encodeURIComponent(filterCategory)}&page=${page}&itemsPerPage=${itemsPerPage}`;
    window.location.href = baseUrl + queryString;
});