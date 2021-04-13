
$(document).ready(function () {
    let buttons = ['#usage-button', '#download-button', '#contact-button'];
    // Preload content with usage content
    $("#content").html($('#content-usage').html());

    navigationContentHandler('#usage-button', '#content-usage');
    navigationContentHandler('#download-button', '#content-download');
    navigationContentHandler('#contact-button', '#content-contact');

    function navigationContentHandler (button, toContent){
        $(document).on('click', button, function (e) {
            // Change the content
            $("#content").html($(toContent).html());
            // Change color of selected button
            // and revert the color of the other buttons
            buttons.forEach(i => {
                if (i == button){
                    $(button).addClass("selected");
                } else {
                    $(i).removeClass("selected");
                }
            });
        });
    }
}); 