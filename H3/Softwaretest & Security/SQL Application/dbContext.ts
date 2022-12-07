import { Database } from "sqlite3";
import bcrypt from "bcrypt";

export default abstract class DbContext {
  public static db: Database = new Database("db.sqlite");
  private static initialized: boolean = false;

  public static initialize(seedDatabase: boolean = false): void {
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

      if (seedDatabase) this.seed();

      this.initialized = true;
    });
  }

  public static seed(): void {
    this.db.serialize(() => {
      this.db.run(
        `INSERT INTO users (username, password) VALUES ("plaintext", "password");`
      );

      bcrypt.hash("password", 10, (error, hash) => {
        this.db.run(
          `INSERT INTO users (username, password) VALUES ("viktor", "${hash}");`
        );
        this.db.run(
          `INSERT INTO tasks (user_id,title, completed) VALUES (1, "Do admin stuff", 0);`
        );
        this.db.run(
          `INSERT INTO tasks (user_id,title, completed) VALUES (1, "Do nothing", 1);`
        );
        this.db.run(
          `INSERT INTO tasks (user_id,title, completed) VALUES (2, "Build a raft", 1);`
        );
        this.db.run(
          `INSERT INTO tasks (user_id,title, completed) VALUES (2, "Learn to build a raft", 0);`
        );
      });
    });
  }
}
