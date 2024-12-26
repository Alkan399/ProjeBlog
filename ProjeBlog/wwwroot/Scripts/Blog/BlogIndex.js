document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault(); // Sayfanın yeniden yüklenmesini önler

    // Input alanlarından değerleri al
    const filterTitle = document.getElementById("titleInput").value;
    const filterEntry = document.getElementById("entryInput").value;
    const page = 1; // İlk sayfadan başlatmak için sabit bir değer veriyoruz

    // URL oluştur ve yönlendir
    const baseUrl = "/Blog/Blog/Index"; // Hedef controller/action
    const queryString = `?filterTitle=${encodeURIComponent(filterTitle)}&filterEntry=${encodeURIComponent(filterEntry)}&page=${page}`;
    window.location.href = baseUrl + queryString;
});