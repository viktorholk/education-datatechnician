"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const sqlite3_1 = require("sqlite3");
const bcrypt_1 = __importDefault(require("bcrypt"));
class DbContext {
    static initialize(seedDatabase = false) {
        this.db.serialize(() => {
            this.db.run(`PRAGMA foreign_keys = 1`);
            this.db.run(`CREATE TABLE IF NOT EXISTS users(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        username TEXT UNIQUE,
        password TEXT
      )`);
            this.db.run(`CREATE TABLE IF NOT EXISTS tasks(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
      user_id INTEGER NOT NULL,
      title TEXT NOT NULL,
      completed INTEGER DEFAULT 0,
      FOREIGN KEY(user_id) REFERENCES users(id)
      )`);
            if (seedDatabase)
                this.seed();
            this.initialized = true;
        });
    }
    static seed() {
        this.db.serialize(() => {
            this.db.run(`INSERT INTO users (username, password) VALUES ("plaintext", "password");`);
            bcrypt_1.default.hash("password", 10, (error, hash) => {
                this.db.run(`INSERT INTO users (username, password) VALUES ("viktor", "${hash}");`);
                this.db.run(`INSERT INTO tasks (user_id,title, completed) VALUES (1, "Do admin stuff", 0);`);
                this.db.run(`INSERT INTO tasks (user_id,title, completed) VALUES (1, "Do nothing", 1);`);
                this.db.run(`INSERT INTO tasks (user_id,title, completed) VALUES (2, "Build a raft", 1);`);
                this.db.run(`INSERT INTO tasks (user_id,title, completed) VALUES (2, "Learn to build a raft", 0);`);
            });
        });
    }
}
exports.default = DbContext;
DbContext.db = new sqlite3_1.Database("db.sqlite");
DbContext.initialized = false;
