use ITI

--1.Create a view that displays student full name, course name if the student has a grade more than 50. 
create view stdGrades
as
select student.st_fname + ' ' + student.st_lname as full_name,Course.Crs_Name, Stud_Course.Grade 
from Student ,Stud_Course,Course
where Student.St_Id = Stud_Course.St_Id and Course.Crs_Id = Stud_Course.Crs_Id and Stud_Course.Grade > 50

select * from stdGrades

--2.Create an Encrypted view that displays manager names and the topics they teach.
create view managerTopics
with encryption
as
select distinct Instructor.Ins_Name, Topic.Top_Name from Instructor,Department,Ins_Course,Course,Topic
where Department.Dept_Manager = Instructor.Ins_Id 
and Instructor.Ins_Id = Ins_Course.Ins_Id 
and Course.Crs_Id = Ins_Course.Crs_Id
and Topic.Top_Id = Course.Top_Id

drop view managerTopics
sp_helptext 'managerTopics'
sp_helptext 'stdGrades'
select * from managerTopics

--3.Create a view that will display Instructor Name, Department Name for the ‘SD’ or ‘Java’ 
--Department “use Schema binding” and describe what is the meaning of Schema Binding

create view instructorDepartment
with schemabinding
as
select i.Ins_Name,d.Dept_Name from dbo.Instructor i inner join dbo.Department d
on d.Dept_Id = i.Dept_Id and d.Dept_Name in ('SD','Java')

select * from instructorDepartment

--4. Create a view “V1” that displays student data for student who lives in Alex or Cairo. 
--Note: Prevent the users to run the following query 
--Update V1 set st_address=’tanta’
--Where st_address=’alex’;

create view v1
as
select * from Student
where Student.St_Address in ('Alex','Cairo')
with check option

select * from v1
Update V1 set st_address='tanta'
Where st_address='alex';

--5.Create index on column (Hiredate) that allow u to cluster the data in table Department. 
--What will happen?
sp_helpindex 'department'

alter table department 
drop constraint PK_Department

alter table department 
add constraint PK_Department primary key nonclustered (dept_id)

create clustered index ix_dept_hiredate 
on department(manager_hiredate)

--6.Create index that allow u to enter unique ages in student table. What will happen?
create unique index ix_student_age 
on student(st_age)  --- fails

--7.Create temporary table [Session based] on Company DB to save employee name and his today task.

create table #todaytasks
(
    emp_name varchar(50),
    today_task varchar(250)
)

insert into #todaytasks values
('Ahmed','Do stuff'),('Ahmed','Do some other stuff')

select * from #todaytasks

--8.Create a view that will display the project name and the number of employees work on it.
--“Use Company DB”
use Company_SD
create view Emp_Count
as
select  Project.Pname,count(Works_for.ESSn) as Emp_Count from Project,Works_for
where Project.Pnumber = Works_for.Pno
group by Project.Pname

select * from Emp_Count


--9.Using Merge statement between the following two tables [User ID, Transaction Amount]

create table lastt(lid int,Transaction_Amount int)

create table Dailyt(did int,Transaction_Amount int)

Merge into lastt as T   
using dailyt as S       
on T.lid = S.did

when Matched then        
	update 
		set T.Transaction_Amount = S.Transaction_Amount
when not matched then       
	insert
    values(S.did,S.Transaction_Amount)                 
	
output $action;




------------Part Two--------------
use [SD32-Company]

--1.Create view named   “v_clerk” that will display employee#,project#, 
--date of hiring of all the jobs of the type 'Clerk'.
create view v_clerk
as
select EmpNo,ProjectNo,Enter_Date from Works_on
where Job = 'Clerk'

select * from v_clerk

--2. Create view named  “v_without_budget” that will display all the projects data without budget
create view v_without_budget
as
select p.ProjectNo,p.ProjectName from Company.Project p

drop view v_without_budget
select * from v_without_budget

--3.Create view named  “v_count “ that will display the project name and the # of jobs in it
create view v_count 
as
select p.ProjectName,count(w.Job) as Jobs_Count from Works_on w inner join Company.Project p
on p.ProjectNo = w.ProjectNo
group by p.ProjectName

select * from v_count


--4. Create view named ” v_project_p2” that will display the emp# s for the project# ‘p2’
--use the previously created view  “v_clerk”

create view v_project_p2
as
select * from v_clerk
where v_clerk.ProjectNo = 2

select * from v_project_p2
drop view v_project_p2

--5.modifey the view named  “v_without_budget”  to display all DATA in project p1 and p2
alter view v_without_budget
as
select * from Company.Project p
where p.ProjectNo in (1,2)

select * from v_without_budget

--6.Delete the views  “v_ clerk” and “v_count”
drop view v_clerk
drop view v_count

--7.Create view that will display the emp# and emp lastname who works on dept# is ‘d2’
create view Emp_Data
as 
select e.EmpNo,e.[Emp Lname] from [Human Resources].Employee e
where e.DeptNo = 2

select * from Emp_Data

--8.Display the employee  lastname that contains letter “J”
--Use the previous view created in Q#7
select * from Emp_Data
where Emp_Data.[Emp Lname] like '%J%'

--9.Create view named “v_dept” that will display the department# and department name
create view v_dept 
as
select d.DeptNo,d.DeptName from Company.Department d

select * from v_dept

--10.using the previous view try enter new department data where dept# is ’d4’ and dept name is ‘Development’
insert into v_dept (v_dept.DeptName)
values ('Development')

select * from v_dept

--11.Create view name “v_2006_check” that will display employee#, 
--the project #where he works and the date of joining the project 
--which must be from the first of January and the last of December 2006.
--this view will be used to insert data so make sure that the coming new data must match the condition

create view v_2006_check
as
select w.EmpNo,w.ProjectNo,w.Enter_Date,d.Location 
from Works_on w,[Human Resources].Employee e,Company.Department d
where d.DeptNo = e.DeptNo and e.EmpNo = w.EmpNo and w.Enter_Date between '2006-01-01' and '2006-12-31'
with check option

select * from v_2006_check

insert into v_2006_check (EmpNo, ProjectNo, Enter_Date)
values (28559, 3, '2006-05-15');

insert into v_2006_check (EmpNo, ProjectNo, Enter_Date)
values (9031, 3, '2007-05-15');