
$('#wrapper').on('click', '#deleteWishbtn', function () {
    var wishId = $('#WishId').val();
    $.ajax({
        type: "POST",
        url: "/Profile/DeleteWish",
        dataType: 'html',
        data: { 'id': wishId },
        success: function (result) {
            $("#partialWishlist").html(result);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("error");
        }
    });
});