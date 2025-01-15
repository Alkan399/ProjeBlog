document.addEventListener("DOMContentLoaded", function () {
    //const url = `/Blog/Blog/FilterContents?page=${encodeURIComponent(page)}`;

    //fetch(url, {
    //    method: 'POST',

    //    headers: {
    //        'Content-Type': 'application/json'
    //    },
    //    body: JSON.stringify(filters)
    //})
    //    .then(response => response.json())
    //    .then(data => {
    //        console.log(data);
    //        updateTable(data);
    //    })
    //    .catch(error => console.error('Error:', error));

    const pageWidth = Math.max(
        document.body.scrollWidth,
        document.documentElement.scrollWidth
    );

    const pageHeight = Math.max(
        document.body.scrollHeight,
        document.documentElement.scrollHeight
    );

    console.log(`Sayfa genişliği: ${pageWidth}px, Sayfa yüksekliği: ${pageHeight}px`);

    const element = document.querySelector('#RightBarMain'); // Elementi seçin
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
        body: JSON.stringify({ location: parseInt(1) })
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            updateElement(data);

        })
        .catch(error => console.error('Hata:', error));
    
    function updateElement(contents) {




        // İçerik varsa, tabloyu doldur
        contents.forEach(contentSet => {
            const SetName = `
                    <h1 class="search_taital">${contentSet.ContentSet.Name}</h1>
                    `
            element.insertAdjacentHTML("beforeend", SetName); // Yeni satırları tabloya ekle
            contentSet.ContentSet.ContentSetContents.forEach(csc => {
                const Content = `
                            <div class="recent_box">
                                <div class="recent_left">
                                    <div class="image_6"><img style="width:100px" src="${csc.Content.CoverImagePath}"></div>
                                </div>
                                <div class="recent_right">
                                    <a href="/Blog/Blog/ContentPage?idx=${csc.Content.ID}"><h3 class="consectetur_text">${csc.Content.Title}</h3></a>
                                    <p class="dolor_text">${csc.Content.Entry}</p>
                                </div>
                            </div>
                            
            `;
                element.insertAdjacentHTML("beforeend", Content); // Yeni satırları tabloya ekle

            })

        });
    }

});

