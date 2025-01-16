import { getStatusText, checkDateTime, updateElementForRightBar } from '/Scripts/CommonLibrary/CommonRepo.js';

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
    const pageWidth = Math.max(
        document.body.scrollWidth,
        document.documentElement.scrollWidth
    );

    const pageHeight = Math.max(
        document.body.scrollHeight,
        document.documentElement.scrollHeight
    );

    console.log(`Sayfa genişliği: ${pageWidth}px, Sayfa yüksekliği: ${pageHeight}px`);

    const element = document.querySelector('#BlogRightColumnContentSets'); // Elementi seçin
    const ElementHeight = element.offsetHeight;
    console.log(element, ElementHeight);

    var count = pageHeight / ElementHeight;
    console.log(count, Math.round(count));

    ////////////////////////////////////////////

    fetch('/api/Blog/ApiControllers/UtilityMethods/GetContentSetElements', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ location: parseInt(3) }) // 3 --> (LocationEnum)BlogIndexRightBar
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            element.innerHTML = "";
            updateElementForRightBar(element, data, Math.floor(count));

        })
        .catch(error => console.error('Hata:', error));
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