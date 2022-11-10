let current = {
  identifier: "Test Client",
  position: {
    latitude: null,
    longitude: null,
  },
};

let connections = [];
let canvas = document.getElementById("canvasMap");
let ctx = canvas.getContext("2d");

function draw() {
  // Clear
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  ctx.save()


  const scale = $("#range").val();
  ctx.scale(scale, scale);

  ctx.beginPath();

  connections.forEach((client) => {
  ctx.save();
    const x =
      (canvas.width / 2 +
        (client.position.longitude - current.position.longitude) / 0.00009) /
      scale;
    const y =
      (canvas.height / 2 +
        (client.position.latitude - current.position.latitude) / 0.00009) /
      scale;

    ctx.translate(x, y)
    ctx.moveTo(0,0)
    ctx.arc(0,0, 5, 0, 2 * Math.PI);

    ctx.font = "10px Arial";
    ctx.textAlign = "center";
    ctx.fillText(client.identifier, 0, 15);

    ctx.font = "6px Arial";

    ctx.fillText(client.position.latitude, 0,  20);
    ctx.fillText(client.position.longitude, 0, 25);
    ctx.restore();
  });

  ctx.stroke();
  ctx.fill();

  ctx.restore()
}

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

function valid() {
  return current.identifier !== null;
}

function synchronize() {
  if (valid()) {
    navigator.geolocation.getCurrentPosition((position) => {
      current.position.latitude = position.coords.latitude;
      current.position.longitude = position.coords.longitude;

      // Update current client
      // Fetch connections after
      request("POST", "clients", current).then((_) => {
        request("GET", "clients").then((response) => {
          $("#myPos").html(
            `Latitude: <b>${current.position.latitude}</b><br>Longitude: <b>${current.position.longitude}`
          );

          $("#data").html(JSON.stringify(response, null, 2));
          // Filter current client
          //let index = response.findIndex(
          //  (client) => client.identifier === current.identifier
          //);
          //if (index > -1) {
          //  response.splice(index, 1);
          //}
          connections = response;

          // Draw canvas
          draw();
        });
      });
    });
  }
}

function toggleLogin() {
  if (valid()) {
    $("#content").show();
    $("#loginForm").hide();
  } else {
    $("#content").hide();
    $("#loginForm").show();
  }
}

$(document).ready(function() {
  toggleLogin();

  setInterval(function() {
    synchronize();
  }, 2500);

  $("#range").on("change", () => {
    draw();
  });

  $("#loginForm").submit(function(e) {
    current.identifier = $("#clientIdentifier").val();

    toggleLogin();
    e.preventDefault();
  });

  $("#logoutButton").click(function(e) {
    current.identifier = null;
    toggleLogin();
  });
});
