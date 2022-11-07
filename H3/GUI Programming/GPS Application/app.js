import express from "express";
import bodyParser from "body-parser";

const app = express();
const port = 3000;

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json())
app.use(express.static("public"));

let clients = [];

app.get("/clients", (req, res) => {
  res.json({ test: true });
});

app.post("/clients", (req, res) => {
  const identifier = req.body.identifier

  if (identifier) {
    const client = {
      identifier: identifier,
    }

    res.json(client)
  }
});

app.listen(port, () => {
  console.log(`GPS Application listening on port ${port}`);
});
