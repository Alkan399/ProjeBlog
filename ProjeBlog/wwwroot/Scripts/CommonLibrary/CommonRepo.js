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
export default getStatusText;