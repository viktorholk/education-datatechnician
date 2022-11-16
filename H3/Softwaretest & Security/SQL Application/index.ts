import express, { Express, Request, Response, NextFunction } from "express";
import { join } from "path";
import bodyParser from "body-parser";
import jwt from "jsonwebtoken";
import bcrypt from "bcrypt";
import DbContext from "./dbContext";

declare global {
  namespace Express {
    interface Request {
      user: User;
    }
  }
}

interface User {
  id: number;
  username: string;
  password: string;
}

interface Task {
  userId: number;
  title: string;
  completed: number;
}

const app: Express = express();

const port = 3000;

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

const APP_SECRET: string = "thissupersecretkeyhehe";

DbContext.initialize();

function generateAccessToken(user: User) {
  return jwt.sign(user, APP_SECRET, { expiresIn: "1800s" });
}

function authenticateToken(
  request: Request,
  response: Response,
  next: NextFunction
) {
  const authHeader = request.headers["authorization"];
  const token = authHeader && authHeader.split(" ")[1];

  if (token === null) return response.sendStatus(401);

  jwt.verify(token as string, APP_SECRET, (error: any, user: any) => {
    if (error) return response.sendStatus(403);

    request.user = user;

    next();
  });
}

app.get("/", (request: Request, response: Response) => {
  response.sendFile(join(__dirname, "public", "index.html"));
});

app.get("/login", (request: Request, response: Response) => {
  response.sendFile(join(__dirname, "public", "login.html"));
});

app.post("/login", async (request: Request, response: Response) => {
  const data = request.body;

  const username: string = data["username"];
  const password: string = data["password"];

  if (!username || !password) return response.sendStatus(400);

  DbContext.db.get(
    `SELECT * FROM users WHERE username = "${username}"`,
    (err, user: User) => {
      if (err) response.sendStatus(500);

      if (user) {
        // Check if password is correct
        bcrypt.compare(password, user.password, function(error, result) {
          if (error) return response.sendStatus(500);

          if (result) {
            const token = generateAccessToken(user);
            response.json({
              token: token,
            });
          } else {
            response.sendStatus(404);
          }
        });
      } else {
        response.sendStatus(404);
      }
    }
  );
});


// This causes SQL injection
//app.post("/login", async (request: Request, response: Response) => {
//  const data = request.body;
//
//  const username: string = data["username"];
//  const password: string = data["password"];
//
//  if (!username || !password) return response.sendStatus(400);
//
//  DbContext.db.get(
//    `SELECT * FROM users WHERE username = "${username}" AND password = "${password}"`,
//    (err, user: User) => {
//      if (err) response.sendStatus(500);
//
//      if (user) {
//        const token = generateAccessToken(user);
//        response.json({
//          token: token,
//        });
//      } else {
//        response.sendStatus(404);
//      }
//    }
//  );
//});

// This prevents SQL injection
//app.post("/login", async (request: Request, response: Response) => {
//  const data = request.body;
//
//  const username: string = data["username"];
//  const password: string = data["password"];
//
//  if (!username || !password) return response.sendStatus(400);
//
//  DbContext.db.get(
//    `SELECT * FROM users WHERE username = ? AND password = ?`, [username, password],
//    (err, user: User) => {
//      if (err) response.sendStatus(500);
//
//      if (user) {
//        const token = generateAccessToken(user);
//        response.json({
//          token: token,
//        });
//      } else {
//        response.sendStatus(404);
//      }
//    }
//  );
//});

app.get("/signup", (request: Request, response: Response) => {
  response.sendFile(join(__dirname, "public", "signup.html"));
});

app.post("/signup", (request: Request, response: Response) => {
  const data = request.body;

  const username: string = data["username"];
  const password: string = data["password"];

  if (!username || !password) return response.sendStatus(400);

  // Hash password
  bcrypt.hash(password, 10, (error, hash) => {
    if (error) response.sendStatus(500);

    DbContext.db.run(
      `INSERT INTO users (username, password) VALUES (?,?)`,
      [username, hash],
      function(error) {
        if (error) return response.sendStatus(500);

        // Return authentication token
        const token = generateAccessToken({
          id: this.lastID,
          username: username,
          password: password,
        } as User);

        response.json({
          token: token,
        });
      }
    );
  });
});

app.get("/tasks", authenticateToken, (request: Request, response: Response) => {
  DbContext.db.all(
    `SELECT * FROM TASKS where user_id = ${request.user.id}`,
    (error: any, tasks: Task[]) => {
      if (error) response.sendStatus(500);
      response.json(tasks);
    }
  );
});

app.use("/public", express.static("public"));

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at https://localhost:${port}`);
});
