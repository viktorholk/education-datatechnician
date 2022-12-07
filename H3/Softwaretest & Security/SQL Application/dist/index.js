"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const path_1 = require("path");
const body_parser_1 = __importDefault(require("body-parser"));
const jsonwebtoken_1 = __importDefault(require("jsonwebtoken"));
const bcrypt_1 = __importDefault(require("bcrypt"));
const dbContext_1 = __importDefault(require("./dbContext"));
const app = (0, express_1.default)();
const port = 3000;
app.use(body_parser_1.default.urlencoded({ extended: false }));
app.use(body_parser_1.default.json());
const APP_SECRET = "thissupersecretkeyhehe";
dbContext_1.default.initialize();
function generateAccessToken(user) {
    return jsonwebtoken_1.default.sign(user, APP_SECRET, { expiresIn: "1800s" });
}
function authenticateToken(request, response, next) {
    const authHeader = request.headers["authorization"];
    const token = authHeader && authHeader.split(" ")[1];
    if (token === null)
        return response.sendStatus(401);
    jsonwebtoken_1.default.verify(token, APP_SECRET, (error, user) => {
        if (error)
            return response.sendStatus(403);
        request.user = user;
        next();
    });
}
app.get("/", (request, response) => {
    response.sendFile((0, path_1.join)(__dirname, "public", "index.html"));
});
app.get("/login", (request, response) => {
    response.sendFile((0, path_1.join)(__dirname, "public", "login.html"));
});
app.post("/login", (request, response) => __awaiter(void 0, void 0, void 0, function* () {
    const data = request.body;
    const username = data["username"];
    const password = data["password"];
    if (!username || !password)
        return response.sendStatus(400);
    dbContext_1.default.db.get(`SELECT * FROM users WHERE username = "${username}"`, (err, user) => {
        if (err)
            response.sendStatus(500);
        if (user) {
            // Check if password is correct
            bcrypt_1.default.compare(password, user.password, function (error, result) {
                if (error)
                    return response.sendStatus(500);
                if (result) {
                    const token = generateAccessToken(user);
                    response.json({
                        token: token,
                    });
                }
                else {
                    response.sendStatus(404);
                }
            });
        }
        else {
            response.sendStatus(404);
        }
    });
}));
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
app.get("/signup", (request, response) => {
    response.sendFile((0, path_1.join)(__dirname, "public", "signup.html"));
});
app.post("/signup", (request, response) => {
    const data = request.body;
    const username = data["username"];
    const password = data["password"];
    if (!username || !password)
        return response.sendStatus(400);
    // Hash password
    bcrypt_1.default.hash(password, 10, (error, hash) => {
        if (error)
            response.sendStatus(500);
        dbContext_1.default.db.run(`INSERT INTO users (username, password) VALUES (?,?)`, [username, hash], function (error) {
            if (error)
                return response.sendStatus(500);
            // Return authentication token
            const token = generateAccessToken({
                id: this.lastID,
                username: username,
                password: password,
            });
            response.json({
                token: token,
            });
        });
    });
});
app.get("/tasks", authenticateToken, (request, response) => {
    dbContext_1.default.db.all(`SELECT * FROM TASKS where user_id = ${request.user.id}`, (error, tasks) => {
        if (error)
            response.sendStatus(500);
        response.json(tasks);
    });
});
app.use("/public", express_1.default.static("public"));
app.listen(port, () => {
    console.log(`⚡️[server]: Server is running at https://localhost:${port}`);
});
