
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

CREATE TABLE IF NOT EXISTS flag_types(
    id      INT NOT NULL AUTO_INCREMENT,
    name    VARCHAR(32) NOT NULL UNIQUE,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS flags(
    id          INT NOT NULL AUTO_INCREMENT,
    flag_type_id INT NOT NULL,
    text        TEXT NOT NULL,
    created_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    FOREIGN KEY (flag_type_id) REFERENCES flag_types(id)
);

DELIMITER //
CREATE TRIGGER IF NOT EXISTS flags_deposit
    AFTER INSERT
        ON transactions FOR EACH ROW
        BEGIN
            IF NEW.amount > 25000
            THEN
                INSERT INTO flags (flag_type_id, text) VALUES (1, CONCAT((
                    SELECT users.login FROM users WHERE users.id = (SELECT accounts.user_id FROM accounts WHERE accounts.id = NEW.account_id)), 
                    " just deposited $",
                    NEW.amount));
            END IF;
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

    INSERT INTO flag_types (name) VALUES ('unusual_account_activity');
    INSERT INTO flag_types (name) VALUES ('unusual_amount');

    INSERT INTO flags (flag_type_id, text) VALUES (1, "sample has logged in from Bosnia");
    INSERT INTO flags (flag_type_id, text) VALUES (1, "sample has logged in from Romania");
    INSERT INTO flags (flag_type_id, text) VALUES (1, "sample has logged in from Denmark");

    INSERT INTO users (login, password, admin) VALUES ('admin', 'admin', true);
    INSERT INTO users (login, password) VALUES ('sample', 'password');
    INSERT INTO users (login, password) VALUES ('sample2', 'password');
    INSERT INTO users (login, password) VALUES ('sample3', 'password');

    INSERT INTO accounts (user_id, account_number, account_type) VALUES (2, '1234 56789009', 1);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 1, NOW(), 500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 2, NOW(), 250);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 1, NOW(), 1500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (1, 2, NOW(), 750);

    INSERT INTO accounts (user_id, account_number, account_type) VALUES (2, '9943 55532129', 2);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (2, 1, NOW(), 1500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (2, 1, NOW(), 3500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (2, 1, NOW(), 2500);

    INSERT INTO accounts (user_id, account_number, account_type) VALUES (3, '1234 12341239', 1);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (3, 1, NOW(), 500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (3, 2, NOW(), 250);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (3, 1, NOW(), 1500);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (3, 1, NOW(), 250000);

    INSERT INTO accounts (user_id, account_number, account_type) VALUES (4, '5555 12345679', 1);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (4, 1, NOW(), 1200);
    INSERT INTO transactions (account_id, transaction_type, date, amount) VALUES (4, 1, NOW(), 250);

    SELECT * FROM account_types;
    SELECT * FROM transaction_types;
    SELECT * FROM flag_types;
    SELECT * FROM flags;
    SELECT * FROM users;
    SELECT * FROM accounts;
    SELECT * FROm transactions;
END //
DELIMITER ; 

CREATE VIEW IF NOT EXISTS user_accounts AS
    SELECT accounts.id, users.login, accounts.account_number, account_types.name FROM accounts
    INNER JOIN users ON accounts.user_id = users.id
    INNER JOIN account_types ON accounts.account_type = account_types.id;

CREATE VIEW IF NOT EXISTS user_transactions AS
    SELECT transactions.id, accounts.account_number, transaction_types.name as 'type', transactions.date, transactions.amount FROM transactions
    INNER JOIN accounts ON transactions.account_id = accounts.id
    INNER JOIN transaction_types ON transactions.transaction_type = transaction_types.id;

CREATE VIEW IF NOT EXISTS account_transactions_count AS
    SELECT users.login, 
        accounts.account_number, 
        (SELECT COUNT(transactions.id) FROM transactions WHERE transactions.account_id = accounts.id) as 'count' 
        FROM accounts

    INNER JOIN users ON users.id = accounts.user_id;

CREATE VIEW IF NOT EXISTS system_flags AS 
    SELECT flags.id, flag_types.name as 'type', flags.text, flags.created_at FROM flags
    INNER JOIN flag_types ON flags.flag_type_id = flag_types.id;
