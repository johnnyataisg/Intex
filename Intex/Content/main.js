function main() {
    $(".order-table-row").hover(function () {
        $(this).toggleClass("hovered");
    });

    $(".order-table-row").on("click", function () {
        $(this).toggleClass("highlighted");
        $(this).next().toggleClass("hidden");
    });

    $('#button').on("click", function () {
        var idList = new Array();
        $(".highlighted td:first-child").each(function () {
            idList.push($(this).text());
        });
        $.ajax({
            type: "GET",
            url: "/Admin/Delete",
            data: { values: idList },
            success: function (data) {
                alert(data.Result);
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            traditional: true
        });
        $(".highlighted").each(function () {
            $(this).remove();
        });
    });

    $(".table-row:odd").addClass("oddrow");
}
$(document).ready(main);