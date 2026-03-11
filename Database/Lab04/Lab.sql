use Company_SD

---> 1
select de.Dependent_name, de.Sex
from Dependent de inner join Employee emp
on emp.SSN = de.ESSN and emp.Sex = 'F' and de.Sex = 'F'
union all
select de.Dependent_name, de.Sex
from Dependent de inner join Employee emp
on emp.SSN = de.ESSN and emp.Sex = 'M' and de.Sex = 'M'


---> 2
select pr.Pname, sum(wf.Hours) as HourPerWeek
from Project pr left join Works_for wf
on pr.Pnumber = wf.Pno
group by pr.Pname


---> 3
select dept.* 
from Employee emp inner join Departments dept
on dept.Dnum = emp.Dno
where emp.SSN = (select min(SSN) from Employee)


---> 4
select dept.Dname, max(emp.Salary) as MaxSalary, min(emp.Salary) as MinSalary, avg(Salary) as AvgSalary
from Departments dept left join Employee emp
on dept.Dnum = emp.Dno
group by dept.Dname


---> 5.1
select concat_ws(' ', emp.Fname, emp.Lname) as FullName
from Employee emp inner join Departments dept
on emp.SSN = dept.MGRSSN and not exists (select * from Dependent de where dept.MGRSSN = de.ESSN)


---> 5.2
select concat_ws(' ', emp.Fname, emp.Lname) as FullName
from Employee emp inner join Departments dept
on emp.SSN = dept.MGRSSN and emp.SSN not in (select de.ESSN from Dependent de)


---> 6
select dept.Dnum, dept.Dname, count(emp.SSN), avg(emp.Salary) as AvgSalary
from Departments dept left join Employee emp
on dept.Dnum = emp.Dno
group by Dname, Dnum
having avg(emp.Salary) < (select avg(e.Salary) from Employee e)


---> 7.1
select concat_ws(' ', emp.Fname, emp.Lname) as FullName, pr.Pname, pr.Dnum 
from Employee emp inner join Works_for wf
on emp.SSN = wf.ESSn inner join Project pr
on wf.Pno = pr.Pnumber
order by pr.Dnum, emp.Fname, emp.Lname


---> 7.2
select concat_ws(' ', emp.Fname, emp.Lname) as FullName, pr.Pname, pr.Dnum 
from Employee emp , Project pr , Works_for wf
where emp.SSN = wf.ESSN and pr.Pnumber = wf.Pno
order by pr.Dnum , emp.Lname , emp.Fname


---> 8.1
select top 2 emp.Salary 
from Employee emp
order by emp.Salary desc 


---> 8.2
select  emp.Salary 
from Employee emp
where emp.Salary is not null and (select count(e.Salary) from Employee e where e.Salary > emp.Salary) < 2
order by emp.Salary desc 


---> 9
select distinct concat_ws(' ', emp.Fname, emp.Lname) as FullName 
from Employee emp inner join Dependent de
on emp.SSN = de.ESSN  
where de.Dependent_name like '%' + concat_ws(' ', emp.Fname, emp.Lname) + '%'


---> 10
select emp.SSN, concat_ws(' ', emp.Fname, emp.Lname) as FullName
from Employee emp 
where exists (select * from Dependent de where emp.SSN = de.ESSN)


---> 11
insert into Departments values('DEPT IT', 100, 112233, '1-11-2006')


---> 12
-------> a
update Departments
set MGRSSN = 968574, [MGRStart Date] = getdate()
where Dnum =100

-------> b
update Departments 
set MGRSSN = 102672, [MGRStart Date] = getdate()
where Dnum = 20

update Employee
set Dno = 20
where SSN = 102672

-------> c
update Employee
set Superssn = 102672
where SSN = 102660


---> 13
delete from Dependent where ESSN = 223344

update Departments
set MGRSSN = 102672
where MGRSSN = 223344

update Employee
set Superssn = 102672
where Superssn = 223344

update Works_for
set ESSn = 102672
where ESSn = 223344

delete from Employee where SSN = 223344


---> 14
update Employee 
set Salary += Salary * 0.3
from Employee emp inner join Works_for wf 
on emp.SSN= wf.ESSn inner join Project pr 
on pr.Pnumber = wf.Pno and pr.Pname='Al Rabwah'


