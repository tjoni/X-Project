
$("#submitWish").click(function () {
    var url = $('#urlString').val();
    messages = document.getElementById("error-message");
    var form = document.getElementById("urlString");

    messages.innerHTML = ""
    messages.style.color = "red";
        $.ajax({
            type: "POST",
            url: "/Profile/AddWishList",
            dataType: 'html',
            data: { 'url': url },
            success: function (result) {
                
                $("#partialWishlist").html(result);
                $("#urlString").trigger("reset");
                $("#urlString").val("")
            },
            error: function (xhr, ajaxOptions, thrownError) {
                messages.innerHTML = "Bad url adress!";
       }
        });
    });
