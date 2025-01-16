import { getStatusText, checkDateTime, updateElementForRightBar } from '/Scripts/CommonLibrary/CommonRepo.js';

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
            element.innerHTML = "";
            updateElementForRightBar(element, data, Math.round(count));

        })
        .catch(error => console.error('Hata:', error));
    

});

