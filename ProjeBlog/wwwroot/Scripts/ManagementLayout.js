﻿document.addEventListener("DOMContentLoaded", function () {
    const userInfo = document.getElementById("user-info");
    var userNames = document.getElementsByName("user-name-edited");
    var userImages = document.getElementsByName("leftPp");
    const url = `/Blog/UserAuth/GetCookieUserAuth`;
    fetch(url, {
        method: 'POST',

        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            console.log(data.appUserDetail.profilePicture);
            if (data && data.appUserDetail && data.appUserDetail.profilePicture) {
                // Eğer profil resmi varsa, tüm userImage'ler için src'yi güncelliyoruz
                for (var i = 0; i < userImages.length; i++) {
                    userImages[i].src = data.appUserDetail.profilePicture;
                    userInfo.innerHTML = data.userName + ' ' +data.role
                }
                for (var i = 0; i < userNames.length; i++) {
                    userNames[i].innerHTML = data.userName;
                }
                console.log("Profile picture updated for all elements.");
            } else {
                console.error("Profile picture not found in the response.");
            }
        })
        .catch(error => console.error('Error:', error));

    

});


