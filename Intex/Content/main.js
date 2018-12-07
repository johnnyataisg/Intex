function main() {
    $(".order-table-row").hover(function () {
        $(this).toggleClass("hovered");
    });

    $(".sample-row").hover(function () {
        $(this).toggleClass("hovered");
    });

    $(".customer-table-row").hover(function () {
        $(this).toggleClass("hovered");
    });

    $(".order-table-row").on("click", function () {
        $(this).toggleClass("highlighted");
        $(this).next().toggleClass("hidden");
    });

    $(".sample-row").on("click", function () {
        $(this).toggleClass("highlighted");
        $(this).next().toggleClass("hidden");
    });

    $(".customer-table-row").on("click", function () {
        var information = new Array();
        $(this).find($(".hidden")).each(function () {
            information.push($(this).text());
        });
        $(".label1 span").html(information[0]);
        $(".label2 span").html(information[1]);
        $(".modal").addClass("show");
    });

    $(".close").on("click", function () {
        $(".modal").removeClass("show");
    });
}
$(document).ready(main);