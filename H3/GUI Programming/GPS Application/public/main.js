let current = {
  identifier: "Me",
  color: "#0000ff",
  position: {
    latitude: 56.16501871755641,
    longitude: 9.537014096178655,
  },
};

let connections = [
  {
    identifier: "Me",
    color: "#0000ff",
    position: {
      latitude: 56.16501871755641,
      longitude: 9.537014096178655,
    },
  },
  {
    identifier: "Alanya Pizza",
    color: "Red",
    position: {
      latitude: 56.16594770658154,
      longitude: 9.535972537460449,
    },
  },
  {
    identifier: "Netto",
    color: "Yellow",
    position: {
      latitude: 56.171221610042195,
      longitude: 9.551184146300569,
    },
  },
];
let canvas = document.getElementById("canvasMap");
let ctx = canvas.getContext("2d");

function getDistance(position) {
  const R = 6371e3; // metres
  const φ1 = (current.position.latitude * Math.PI) / 180; // φ, λ in radians
  const φ2 = (position.latitude * Math.PI) / 180;
  const Δφ = ((position.latitude - current.position.latitude) * Math.PI) / 180;
  const Δλ =
    ((position.longitude - current.position.longitude) * Math.PI) / 180;

  const a =
    Math.sin(Δφ / 2) * Math.sin(Δφ / 2) +
    Math.cos(φ1) * Math.cos(φ2) * Math.sin(Δλ / 2) * Math.sin(Δλ / 2);
  const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

  return (R * c).toFixed(); // in metres
}

function convertPosition(range, position) {
  const x =
    canvas.width / 2 + (current.position.longitude - position.longitude);
  const y = canvas.height / 2 + (current.position.latitude - position.latitude);

  return { x: x, y: y };
}

function draw() {
  // Clear
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  ctx.save();

  // Draw grid
  const range = $("#range-slider").val();
  ctx.strokeStyle = "lightgrey";
  ctx.beginPath();

  for (let x = 0; x <= canvas.width; x += canvas.width / (range / 100)) {
    ctx.moveTo(x, 0);
    ctx.lineTo(x, canvas.height);
  }

  for (let y = 0; y <= canvas.height; y += canvas.height / (range / 100)) {
    ctx.moveTo(0, y);
    ctx.lineTo(canvas.width, y);
  }

  ctx.stroke();

  //const scale = $("#range").val();
  //ctx.scale(scale, scale);

  connections.forEach((client) => {
    ctx.save();
    //const x =
    //  canvas.width / 2 +
    //  (client.position.longitude - current.position.longitude) / 0.00009;
    //const y =
    //  canvas.height / 2 +
    //  (client.position.latitude - current.position.latitude) / 0.00009;

    position = convertPosition(range, client.position);
    console.log(position)

    ctx.translate(position.x, position.y);
    ctx.beginPath();
    ctx.fillStyle = client.color;
    ctx.arc(0, 0, 10, 0, 2 * Math.PI);
    ctx.closePath();
    ctx.fill();

    ctx.font = "bold 14px Arial";
    ctx.textAlign = "center";
    ctx.fillStyle = "black";
    ctx.fillText(client.identifier, 0, 25);

    ctx.font = "10px Arial";
    const distance = getDistance(client.position);

    if (distance > 0) {
      ctx.fillText(`${getDistance(client.position)}m`, 0, 35);
    }

    //ctx.fillText(client.position.latitude, 0, 25);
    //ctx.fillText(client.position.longitude, 0, 30);
    ctx.restore();
  });

  ctx.restore();
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

          $("#json-data").html(JSON.stringify(response, null, 2));
          connections = response;
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

  draw();

  //setInterval(function() {
  //  synchronize();
  //}, 2500);

  $("#colorPicker").change(() => {
    current.color = $("#colorPicker").val();
  });

  for (let i = 50; i <= $("#range-slider").prop("max"); i += 50) {
    $("#range-snaps").append(`<option value="${i}"></option>`);
  }

  $("#range-slider").on("input", () => {
    $("#rangeText").html(`Range: <b>${$("#range-slider").val()}</b>m`);
  });

  $("#range-slider").on("change", () => {
    draw();
  });

  $("#loginForm").submit(() => {
    current.identifier = $("#clientIdentifier").val();

    toggleLogin();
    e.preventDefault();
  });

  $("#logoutButton").click(() => {
    current.identifier = null;
    toggleLogin();
  });
});
