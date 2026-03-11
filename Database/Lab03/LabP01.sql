use Company_SD

--> 1
select D.Dnum, D.Dname, D.MGRSSN, concat_ws(' ',E.Fname,E.Lname) as MangerName
from Departments D, Employee E
where D.MGRSSN = E.SSN

--> 2
select D.Dname,P.Pname
from Departments D, Project P
where D.Dnum = P.Dnum

--> 3
select D.*, concat_ws(' ',E.Fname,E.Lname) as FullName
from Departments D, Employee E
where D.Dnum = E.Dno

--> 4
select Pnumber, Pname, Plocation
from Project
where Plocation in ('cairo', 'alex')

--> 5
select *
from Project
where Pname like 'a%'

--> 6
select *
from Employee
where Dno = 30 and Salary between 1000 and 2000

--> 7 
select concat_ws(' ',E.Fname,E.Lname) as FullName
from Employee E, Project P, Works_for W
where E.Dno = 10 and W.Pno = P.Pnumber and W.ESSn = E.SSN and W.Hours >= 10 and P.Pname = 'AL Rabwah'

--> 8
select concat_ws(' ',E.Fname,E.Lname) as EmpName, concat_ws(' ',S.Fname,S.Lname) as ManagerName
from Employee E, Employee S
where S.SSN = E.Superssn and concat_ws(' ',S.Fname,S.Lname) = 'Kamel Mohamed'

--> 9
select concat_ws(' ',E.Fname,E.Lname) as FullName, P.Pname
from Employee E, Project P,Works_for W
where E.SSN = W.ESSn and P.Pnumber = W.Pno
order by P.Pname


--> 10
select P.Pname,P.Dnum,E.Lname,E.Address,E.Bdate
from Employee E,Departments D,Project P
where E.SSN = D.MGRSSN and P.Dnum = D.Dnum and P.City = 'cairo'

--> 11
select E.* 
from Employee E,Departments D
where E.SSN = D.MGRSSN

--> 12
select E.*,D.*
from Employee E left outer join Dependent D
on E.SSN = D.ESSN

--> 13
insert into Employee (Dno,SSN,Superssn,Salary) 
values(30,102672,112233,300)

--> 14
insert into Employee (Dno,SSN)
values(30,102660)

--> 15
update Employee
set Salary += 0.20 * Salary
where SSN = 102672