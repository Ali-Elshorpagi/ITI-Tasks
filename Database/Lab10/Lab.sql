-- [1].
use Company_SD;

declare empCursor cursor
for select SSN, Salary from Employee

declare @id int, @salary int

open empCursor
fetch empCursor into @id, @salary
while @@fetch_status = 0
begin
    if @salary < 3000
        update Employee
        set Salary = Salary * 1.1
        where SSN = @id
    else
        update Employee
        set Salary = Salary * 1.2
        where SSN = @id

    fetch empCursor into @id, @salary
end

close empCursor
deallocate empCursor


select SSN, Salary
from Employee
order by Salary desc



-- [2].
use ITI;

declare deptCursor cursor
for select d.Dept_Name, i.Ins_Name
from Department d inner join Instructor i on d.Dept_Manager = i.Ins_Id

declare @deptName varchar(50), @managerName varchar(50)

open deptCursor
fetch deptCursor into @deptName, @managerName

while @@fetch_status = 0
begin
    print 'Department: ' + @deptName + ' | Manager: ' + @managerName
    fetch deptCursor into @deptName, @managerName
end

close deptCursor
deallocate deptCursor

select d.Dept_Name, i.Ins_Name
from Department d inner join Instructor i on d.Dept_Manager = i.Ins_Id



-- [3].
declare studentCursor cursor
for select St_Fname from Student

declare @name varchar(50)
declare @allNames varchar(max) = ''

open studentCursor
fetch studentCursor into @name

while @@fetch_status = 0
begin
    set @allNames = @allNames + @name + ', '
    fetch studentCursor into @name
end

close studentCursor
deallocate studentCursor

select @allNames as AllStudentsFirstName
select st_fname from student



-- [4].
create sequence EmpTestSeq
    start with 1
    increment by 1
    minValue 1
    maxValue 10
    no cycle

create table TempTest
(
	id int,
	TName varchar(50) 
)

insert into TempTest values(next value for EmpTestSeq , 'Aphrodite');
select * from TempTest



-- [5].
use master;

create database AdventureWorksSnap
on
(
    name = AdventureWorks2012_Data,
    filename = 'E:\ITI\Database\Day 10\adventureworks_snapshot.ss'
)
as snapshot of AdventureWorks2012;

use AdventureWorksSnap
select * from Person.Address
/*
use AdventureWorks2012
select name, physical_name
from sys.database_files
*/



-- [6].
----- 1.
use ITI;

alter proc getMonthNameProc @d date
as
begin
	select format(@d,'MMMM')
end

getMonthNameProc '2025-03-15'



----- 2.
alter proc getRangeProc @first int, @last int
as
begin
	declare @cnt int = @first
	while @cnt <= @last
		begin
			print @cnt
			set @cnt += 1
		end
end

getRangeProc 7, 16



----- 3.
create proc getStudentFullNameDepartmentNameProc @stdNo int
as
begin
	select d.Dept_Name, concat(isnull(s.St_Fname,''),isnull(s.St_Lname,''), ' ') as FullName
	from Student s inner join Department d on s.Dept_Id = d.Dept_Id
	where s.St_Id = @stdNo
end

getStudentFullNameDepartmentNameProc 4



----- 4.
alter proc studentNamesStatusProc @stdNo int
as
begin
    declare @fname varchar(20), @lname varchar(20), @result varchar(100)

    select 
        @fname = St_Fname,
        @lname = St_Lname
    from Student
    where St_Id = @stdNo

    if @fname is null and @lname is null
       set @result = 'first name & last name are null'
    else if @fname is null
        set @result = 'first name is null'
    else if @lname is null
        set @result = 'last name is null'
    else
        set @result = 'first name & last name are not null'

    select @result
end

studentNamesStatusProc 33



----- 5.
create proc getManagerInfoProc @mgrId int
as
begin
    select  d.Dept_Name, i.Ins_Name as MangerName, d.Manager_hiredate
    from Instructor i inner join Department d on i.Ins_Id = d.Dept_Manager
    where i.Ins_Id = @mgrId
end

getManagerInfoProc 2



----- 6.
create proc getStudentNameQueryBasedProc @input varchar(20)
as
begin
    if @input = 'first name'
        select isnull(St_Fname,'') from Student
    else if @input = 'last name'
        select isnull(St_Lname,'') from Student
    else if @input = 'full name'
        select concat(isnull(St_Fname,''),isnull(St_Lname,''), ' ') from Student
    return
end

getStudentNameQueryBasedProc 'first name'
getStudentNameQueryBasedProc 'last name'
getStudentNameQueryBasedProc 'full name'