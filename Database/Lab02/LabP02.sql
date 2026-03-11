use Company_SD

-- 1
select * 
from Employee 

-- 2
select Fname,Lname,salary,DNO 
from Employee

-- 3
select Pname,Plocation,Dnum 
from Project

-- 4
select Fname+ ' '+Lname as FullName, salary * 0.1 * 12 as 'ANNUAL_COMM' 
from Employee

-- 5
select SSN, Fname+ ' '+ Lname as FullName 
from Employee
where salary > 1000;

-- 6
select SSN, Fname+ ' '+ Lname as FullName 
from Employee
where salary*12 > 10000;

-- 7 
select Fname, Lname, salary 
from Employee
where Sex = 'f';

-- 8
select Dnum, Dname 
from Departments
where MGRSSN = 968574;

-- 9
select Pnumber, Pname, Plocation
from Project
where Dnum = 10;