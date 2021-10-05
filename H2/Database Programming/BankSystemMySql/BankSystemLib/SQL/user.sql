DROP USER IF EXISTS 'bank_system'@'localhost';

CREATE USER 'bank_system'@'localhost'
  IDENTIFIED BY 'supersecret';
  
GRANT ALL
    ON bank_system.* TO 'bank_system'@'localhost';
  

