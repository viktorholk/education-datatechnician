import express from "express";
import bodyParser from "body-parser";

const app = express();
const port = 8080;

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(express.static("public"));

let clients = [
  {
    identifier: "Mercantec Erhvervsskole",
    color: "Red",
    position: {
      latitude: 56.46522397183195,
      longitude: 9.41383465599167,
    },
  },
  {
    identifier: "Føtex",
    color: "Blue",
    position: {
      latitude: 56.45297078703165,
      longitude: 9.406617115689468,
    },
  },
];
function getClientIndex(identifier) {
  for (let i = 0; i < clients.length; i++) {
    if (clients[i].identifier === identifier) {
      return i;
    }
  }
  return -1;
}

app.get("/clients", (req, res) => {
  res.json(clients);
});

app.post("/clients", (req, res) => {
  const data = req.body;

  if (data.identifier) {
    // Check if client already exists
    const clientIndex = getClientIndex(data.identifier);
    // Add timestamp
    data.updated_at = Date.now();

    // Create new
    if (clientIndex === -1) {
      clients.push(data);
      console.log(`Client: ${data.identifier} created`);
      return res.sendStatus(201);
    }

    // Update existing client
    clients[clientIndex] = data;
    console.log(`Client: ${clients[clientIndex].identifier} updated`);

    return res.sendStatus(200);
  }

  return res.sendStatus(400);
});

app.listen(port, () => {
  console.log(`GPS Application listening on port ${port}`);

  // Cleanup inactive clients
  setInterval(function() {
    let index = clients.length;

    while (index--) {
      const client = clients[index];
      if (client.updated_at) {
        const lastUpdate = Date.now() - client.updated_at;

        if (lastUpdate >= 5000) {
          console.log(`Client: ${client.identifier} removed`);
          clients.splice(index, 1);
        }
      }
    }
  }, 1000);
});
