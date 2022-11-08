function request(type, endpoint, data = {}) {
  url = "/" + endpoint;

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

let client = {
  identifier: null,
  position: {
    latitude: null,
    longitude: null,
  },
};

function isLoggedIn() {
  return client.identifier !== null;
}

let clients = [];

function toggleLogin() {
  if (isLoggedIn()) {
    $("#content").show();
    $("#loginForm").hide();
  } else {
    $("#content").hide();
    $("#loginForm").show();
  }
}

function updateClient() {
  if (isLoggedIn()) {
    navigator.geolocation.getCurrentPosition(function(position) {
      client.position.latitude = position.coords.latitude;
      client.position.longitude = position.coords.longitude;

      request("POST", "clients", client).then((_) => {
        console.log(`Client: ${client.identifier} updated.`);

        $("#myPos").html(
          `Latitude: <b>${client.position.latitude}</b><br>Longitude: <b>${client.position.longitude}`
        );
      });
    });
  }
}

function getClients() {
  if (isLoggedIn()) {
    request("GET", "clients").then((response) => {
      clients = response;
      $("#data").html(JSON.stringify(clients, null, 2));
    });
  }
}

$(document).ready(function() {
  toggleLogin();

  setInterval(function() {
    updateClient();
    getClients();
  }, 2500);

  $("#loginForm").submit(function(e) {
    client.identifier = $("#clientIdentifier").val();

    updateClient();
    toggleLogin();
    e.preventDefault();
  });

  $('#logoutButton').click(function(e){
    client.identifier = null;
    toggleLogin();
  })
});
