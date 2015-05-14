$(function () {
    console.log("Ble");

    $("#searchBox").on("keyup", function () {
        Search($(this).val());
    });
});

function SearchForUser(query) {
    $.post("/Search/SearchForUser?query=" + query)
        .success(ProcessUserResults);
}

function SearchForHashtag(hashtag) {
  //  console.log("Searching for hashtag: " + hashtag);
    $.post("/Search/SearchForHashtag?hashtag=" + hashtag.slice(1, hashtag.length))
        .success(ProcessHashtagResults);
}

function ProcessUserResults(listOfUsers) {
    $("#searchResults").html("");

    for(var i = 0; i < listOfUsers.length; ++i) {
        var currentUser = listOfUsers[i];

        var resultElement = $("<a />", {
            text: currentUser.username,
            href: "/Photo/FriendsProfile/" + currentUser.username
        });

        $("#searchResults").append(resultElement);
    }
}

function ProcessHashtagResults(listOfPhotos) {

    $("#searchResults").html("");

    for (var i = 0; i < listOfPhotos.length; ++i) {
        var currentPhoto = listOfPhotos[i];

        var resultElement = $("<div/>");
        var resultCaption = $("<p>" + currentPhoto.Caption + "</p>");
        var resultImage = $("<img />", {
            src: currentPhoto.PhotoUrl
        });
        
        resultElement.append(resultImage);
        resultElement.append(resultCaption);

        $("#searchResults").append(resultElement);
    }
}

function Search(query) {

    if (query.length == 0) {
        $("#searchResults").html("");
        return;
    }

    if (query[0] === "#") {
        SearchForHashtag(query);
    }
    else {
        SearchForUser(query);
    }

}

function FollowUser() {
    $.ajax('/Photo/FollowUser?username=' + $("#followUsername").text(), {
        type: 'Get',
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            $("#errorMessage").text("Error occured, please contact support");
        },
        success: function (data, textStatus, jqXHR) {
            if (data) {
                $(".followText").fadeOut(function () {
                    $(".unFollowText").fadeIn();
                })
            }
        }
    });
}

function UnFollowUser() {
    $.ajax('/Photo/UnFollowUser?username=' + $("#followUsername").text(), {
        type: 'Get',
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            $("#errorMessage").text("Error occured, please contact support");
        },
        success: function (data, textStatus, jqXHR) {
            if (data) {
                $(".unFollowText").fadeOut(function () {
                    $(".followText").fadeIn();
                })
            }
        }
    });
}

$('#uploadImageBtn').on('click', function UploadImage() {
    var data = {
        imageUrl: $("#imageUrl").val(),
        hash: $("#imageHash").val(),
        caption: $("#imageCaption").val(),
        categorie: $("#imageCategorie").val()
    };
    $.ajax('/Photo/Upload', {
        type: 'Post',
        dataType: 'json',
        data: data,
        error: function (data) {
            $("#errorMessage").text("Error occured, please contact support");
        },
        success: function (data) { 
            window.location.href = '/Photo/MyProfile';
        }
    });
})
