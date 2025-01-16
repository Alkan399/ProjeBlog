export function getStatusText(status) {
    switch (status) {
        case 1:
            return '<b style="color:green">Aktif</b>';
        case 2:
            return '<b style="color:green">Aktif</b>';
        case 3:
            return '<b style="color:red">Silinmiş</b>';
        default:
            return status; // Eğer bilinmeyen bir status varsa, olduğu gibi göster
    }
}

export function checkDateTime(date) {
    console.log(date);
    if (date.toString() == "1.01.0001 00:00:00" || date.toString() == "1.01.0001" || date.toString() == "01.01.1") {
        return "Tarih yok!";
    }
    else {
        return date;
    }
}

export function formatPhoneNumber(input) {

    let value = input.value.replace(/\D/g, ''); // Sadece sayıları al
    let formattedValue = '';

    // Formatlama işlemi
    if (value.length > 0) formattedValue += '(' + value.substring(0, 3);
    if (value.length > 3) formattedValue += ') ' + value.substring(3, 6);
    if (value.length > 6) formattedValue += '-' + value.substring(6, 10);

    input.value = formattedValue;
    
}

export function updateElementForRightBar(element, contents, count) {


    for (var i = 0; i < count; i++) {

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


}

export default getStatusText;