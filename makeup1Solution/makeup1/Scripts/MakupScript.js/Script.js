$(function () {
    console.log("Ble");

    $("#searchBox").on("keyup", function () {
        Search($(this).val());
    });
});

function Search(query) {

    if (query.length == 0) {
        $("#searchResults").html("");
        return;
    }

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
    });
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
        caption: $("#imageCaption").val()
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