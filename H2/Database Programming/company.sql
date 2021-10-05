

CREATE TABLE employees (
	emp_no INT NOT NULL AUTO_INCREMENT,
    birth_date DATE,
    first_name VARCHAR(14),
    last_name VARCHAR(16),
    sex ENUM("M", "F"),
    hire_date DATE,
    PRIMARY KEY (emp_no)
)


CREATE TABLE departments (
	dept_no CHAR(4) NOT NULL,
    dept_name VARCHAR(40),
    PRIMARY KEY (dept_no)
)

CREATE TABLE dept_manager(
	dept_no CHAR(4) NOT NULL,
    emp_no INT NOT NULL,
    from_date DATE,
    to_date DATE,
    FOREIGN KEY (dept_no) REFERENCES departments(dept_no),
    FOREIGN KEY (emp_no) REFERENCES employees(emp_no)
)

CREATE TABLE dept_emp(
    emp_no INT NOT NULL,
    dept_no CHAR(4) NOT NULL,
    from_date DATE,
    to_date DATE,
    FOREIGN KEY (emp_no) REFERENCES employees(emp_no),
    FOREIGN KEY (dept_no) REFERENCES departments(dept_no)
)

CREATE TABLE saleries(
    emp_no INT NOT NULL,
    salary INT NOT NULL,
    from_date DATE NOT NULL,
    to_date DATE NOT NULL,
    FOREIGN KEY (emp_no) REFERENCES employees(emp_no)
)

CREATE TABLE titles(
    emp_no INT NOT NULL,
    title VARCHAR(50),
    from_date DATE,
    to_date DATE,
	FOREIGN KEY (emp_no) REFERENCES employees(emp_no)
)