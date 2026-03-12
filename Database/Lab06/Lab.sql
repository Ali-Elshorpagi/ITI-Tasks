-- [1].
----- rule & user datatype & default
create rule loc_rule as @loc in ('NY', 'DS', 'KW');

sp_addtype loc,'nchar(2)', 'NOT NULL';

sp_bindrule loc_rule, 'loc';

create default loc_default as 'NY';

sp_bindefault loc_default, 'loc';

----- table
create table Department
(
	DeptNo int primary key,
	DeptName varchar(20),
	Location loc
);

insert into Department values 
(1, 'Research', 'NY'),
(2, 'Accounting', 'DS'),
(3, 'Markiting', 'KW');

insert into Department (DeptNo, DeptName) values (4, 'HR'); -- loc will be NY

select * from Department;





-- [2].
----- rule on salary
create rule salary_rule as @salary_var < 6000;

----- table
create table Employee
(
    EmpNo     int,     
    EmpFname  varchar(20) not null,
    EmpLname  varchar(20) not null,
    DeptNo    int,             
    Salary    int,
    constraint c1 primary key(EmpNo),
    constraint c2 unique(Salary),
    constraint c3 foreign key (DeptNo)
        references Department(DeptNo)
);

sp_bindrule salary_rule, 'Employee.Salary';

----- insert
insert into Employee values
(25348,'Mathew','Smith',3,2500),
(10102,'Ann','Jones',3,3000),
(18316,'John','Barrimore',1,2400),
(29346,'James','James',2,2800),
(9031,'Lisa','Bertoni',2,4000),
(2581,'Elisa','Hansel',2,3600),
(28559,'Sybl','Moser',1,2900);





-- project & works_on table using wizard
----- insert into project
insert into Project values
(1,'Apollo',120000),
(2,'Gemini',95000),
(3,'Mercury',185600);

----- insert into works_on
insert into Works_On values
(10102,1,'Analyst','2006-10-01'),
(10102,3,'Manager','2012-01-01'),
(25348,2,'Clerk','2007-02-15'),
(18316,2,NULL,'2007-06-01'),
(29346,2,NULL,'2006-12-15'),
(2581,3,'Analyst','2007-10-15'),
(9031,1,'Manager','2007-04-15'),
(28559,1,NULL,'2007-08-01'),
(28559,2,'Clerk','2012-02-01'),
(9031,3,'Clerk','2006-11-15'),
(29346,1,'Clerk','2007-01-04');





-- Testing
----- 1.
insert into Works_On (EmpNo, ProjectNo, Enter_Date)
values (11111,1,GETDATE()); -- error: employee does not exist

----- 2.
update Works_On
set EmpNo = 11111 -- error: employee does not exist
where EmpNo = 10102; 

----- 3.
update Employee
set EmpNo = 22222
where EmpNo = 10102; -- error: referenced by works_on

----- 4.
delete from Employee where EmpNo = 10102; -- error: has referenced by works_on





-- Table modification
alter table Employee add TelephoneNumber varchar(20);
alter table Employee drop column TelephoneNumber;
select * from Employee;

--------------------------- PART 02 ------------------------------------
-- [2].
create schema Company;

alter schema Company transfer dbo.Department;

create schema [Human Resource];

alter schema [Human Resource] transfer dbo.Employee;




-- [3].
select constraint_name, constraint_type, table_name
from INFORMATION_SCHEMA.TABLE_CONSTRAINTS
where table_name = 'Employee';



-- [4].
create synonym Emp for [Human Resource].Employee;
select * from Employee; -- error: not in dbo schema 
select * from [Human Resource].Employee; -- acual table location
select * from Emp; -- synonym points to Employee
select * from [Human Resource].Emp; -- error: synonyms don't belong to schemas



-- [5].
update p
set p.Budget = p.Budget * 1.1
from Company.Project p , Works_on w
where p.ProjectNo = w.ProjectNo and w.EmpNo = 10102 and w.Job = 'Manager';



-- [6].
update d
set d.DeptName = 'Sales'
from Company.Department d, [Human Resource].Employee e
where d.DeptNo = e.DeptNo and e.EmpFname = 'James';



-- [7].
update w
set w.Enter_Date = '2007-12-12'
from Works_On w , [Human Resource].Employee e ,Company.Department d
where  w.EmpNo = e.EmpNo and e.DeptNo = d.DeptNo and w.ProjectNo = 1 and d.DeptName = 'Sales';



-- [8].
delete Works_on
from Works_on w , [Human Resource].Employee e , Company.Department d
where w.EmpNo = e.EmpNo and e.DeptNo = d.DeptNo and d.Location = 'KW';










