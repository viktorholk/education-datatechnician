
CREATE TABLE IF NOT EXISTS users(
    id          INT NOT NULL AUTO_INCREMENT,
    login       VARCHAR(32) NOT NULL UNIQUE,
    password    VARCHAR(64) NOT NULL,
    admin       BOOLEAN DEFAULT False,
    PRIMARY KEY (id)
);


CREATE TABLE IF NOT EXISTS account_types(
    id          INT NOT NULL AUTO_INCREMENT,
    name        VARCHAR(32) UNIQUE,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS accounts(
    id              INT NOT NULL AUTO_INCREMENT, 
    user_id         INT NOT NULL,
    account_number  VARCHAR(15) NOT NULL UNIQUE,
    account_type    INT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (account_type) REFERENCES account_types(id)
);

CREATE TABLE IF NOT EXISTS transaction_types(
    id      INT NOT NULL AUTO_INCREMENT, 
    name    VARCHAR(32) UNIQUE,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS transactions(
    id                  INT NOT NULL AUTO_INCREMENT,
    account_id          INT NOT NULL, 
    transaction_type    INT NOT NULL,
    date                DATE NOT NULL,
    amount              DOUBLE NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (account_id) REFERENCES accounts(id),
    FOREIGN KEY (transaction_type) REFERENCES transactions(id)
);

CREATE TABLE IF NOT EXISTS log_types(
    id      INT NOT NULL AUTO_INCREMENT,
    name    VARCHAR(32) NOT NULL UNIQUE,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS logs(
    id          INT NOT NULL AUTO_INCREMENT,
    log_type_id INT NOT NULL,
    text        TEXT NOT NULL,
    created_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    FOREIGN KEY (log_type_id) REFERENCES log_types(id)
);

DELIMITER //
CREATE TRIGGER IF NOT EXISTS create_account
    AFTER INSERT
        ON accounts FOR EACH ROW
        BEGIN
            INSERT INTO logs (log_type_id, text) VALUES (1, CONCAT('Created new account ', NEW.account_number));
        END //
DELIMITER ; 

DELIMITER //
CREATE PROCEDURE IF NOT EXISTS create_defaults()
BEGIN
    INSERT INTO account_types (name) SELECT ('Checking') WHERE NOT EXISTS (SELECT * FROM account_types WHERE name = 'Checking');
    INSERT INTO account_types (name) SELECT ('Savings') WHERE NOT EXISTS (SELECT * FROM account_types WHERE name = 'Savings');

    INSERT INTO transaction_types (name) VALUES ('Deposit');
    INSERT INTO transaction_types (name) VALUES ('Withdraw');
    INSERT INTO transaction_types (name) VALUES ('Send');
    INSERT INTO transaction_types (name) VALUES ('Receive');
    INSERT INTO transaction_types (name) VALUES ('Interest');

    INSERT INTO log_types (name) VALUES ('INFO');
    INSERT INTO log_types (name) VALUES ('ERROR');

    INSERT INTO users (login, password, admin) VALUES ('admin', 'admin', true);
    INSERT INTO users (login, password) VALUES ('sample', 'password');
    INSERT INTO users (login, password) VALUES ('sample2', 'password');
    INSERT INTO users (login, password) VALUES ('sample3', 'password');

    INSERT INTO accounts (user_id, account_number, account_type) VALUES (1, '1234 5678900', 1);
    INSERT INTO accounts (user_id, account_number, account_type) VALUES (1, '9943 5553212', 2);

    INSERT INTO accounts (user_id, account_number, account_type) VALUES (2, '1234 1234123', 1);
    INSERT INTO accounts (user_id, account_number, account_type) VALUES (3, '5555 1234567', 1);

    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 1, NOW(), 500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 2, NOW(), 250);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 1, NOW(), 1500);

    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (2, 1, NOW(), 1200);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (2, 1, NOW(), 250);

    SELECT * FROM account_types;
    SELECT * FROM transaction_types;
    SELECT * FROM log_types;
    SELECT * FROM users;
    SELECT * FROM accounts;
    SELECT * FROm transactions;
END //
DELIMITER ; 
