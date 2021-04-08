
$(document).ready(function () {
    $("#content").html($('#content-usage').html());

    $(document).on('click', '#usage-button', function (e) {
        $("#content").html($('#content-usage').html());
        changeColor("#usage-button");
      });
    $(document).on('click', '#download-button', function (e) {
        $("#content").html($('#content-download').html());
        changeColor("#download-button");
      });
    $(document).on('click', '#contact-button', function (e) {
        $("#content").html($('#content-contact').html());
        changeColor("#contact-button");
      });
      function changeColor (button) { 
          let buttons = ['#usage-button', '#download-button', '#contact-button'];
          buttons.forEach(i => {
            if (i == button){
                $(button).addClass("selected");
            } else {
                $(i).removeClass("selected");
            }
          });
       }
});