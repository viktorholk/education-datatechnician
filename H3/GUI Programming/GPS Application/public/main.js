function request(type, endpoint, data = {}) {
  url = "http://localhost:3000/" + endpoint;

  return new Promise((resolve, reject) => {
    $.ajax({
      type: type,
      url: url,
      contentType: "application/json",
      headers: { "Content-Type": "application/json" },
      data: JSON.stringify(data),
      success: (response) => resolve(response),
      error: (response) => reject(response),
    });
  });
}

let client = null;

function toggleLogin() {
  if (client == null) {
    $("#content").hide();
    $("#loginForm").show();
  } else {
    $("#content").show();
    $("#loginForm").hide();
  }
}

function getGeoLocationPosition() {
  navigator.geolocation.getCurrentPosition(updateGeoLocationPosition);
}

function updateGeoLocationPosition(position) { 
  $('#coords').html(`Latitude: ${position.coords.latitude}<br/>Longitude: ${position.coords.longitude}`) 
}

$(document).ready(function() {
  toggleLogin();
  getGeoLocationPosition();

  navigator.geolocation.getCurrentPosition(function(data) {
    console.log(data);
  });

  $("#loginForm").submit(function(e) {
    const clientIdentifier = $("#clientIdentifier").val();

    request("POST", "clients", {
      identifier: clientIdentifier,
    }).then((response) => {
      client = response;
      toggleLogin();
    });

    e.preventDefault();
  });
});
