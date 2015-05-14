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
    console.log("Searching for hashtag: " + hashtag);
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

        /*var resultElement = $("<a />", {
            text: currentPhoto.Caption,
            href: "/Home/Index/" + currentPhoto.ID
        });*/
        var resultElement = $("<div />");
        var resultCaption = $("<p>" + currentPhoto.Caption + "</p>");
        var resultImage = $("<img />", {
            src: currentPhoto.PhotoUrl
        });

        resultElement.append(resultCaption);
        resultElement.append(resultImage);

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

    /*
    $.ajax('/Search/SearchForUser?query=' + query, {
        type: 'Get',
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            $("#errorMessage").text("Error occured, please contact support");
        },
        success: function (data, textStatus, jqXHR) {
            $("#searchResults").html("");
            $(data).each(function() {
                console.log($(this)[0].username);
                $("<a/>", {
                    text: $(this)[0].username,
                    href: "/Photo/FriendsProfile/" + $(this)[0].username
                }).appendTo("#searchResults");
            })
        }
    });*/
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

function UploadImage() {
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