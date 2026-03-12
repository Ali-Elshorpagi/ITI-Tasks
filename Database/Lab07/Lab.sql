-- 1. Create a scalar function that takes date and returns Month name of that date.
create function getMonthName(@d date) returns varchar(20)
begin
	return format(@d,'MMMM')
end

select dbo.getMonthName('2024-07-15') as month_name



-- 2. Create a multi-statements table-valued function that takes 2 integers and returns the values between them.
create function getRange(@first int, @last int) returns @t table (num int)
begin
	declare @cnt int = @first
	while @cnt <= @last
		begin
			insert into @t values (@cnt)
			set @cnt += 1
		end
	return
end

select * from dbo.getRange(7, 16)



-- 3. Create inline function that takes Student No and returns Department Name with Student full name.
create function getStudentFullNameDepartmentName(@stdNo int) returns table
return (
			select d.Dept_Name, concat(isnull(s.St_Fname,''),isnull(s.St_Lname,''), ' ') as FullName
			from Student s inner join Department d on s.Dept_Id = d.Dept_Id
			where s.St_Id = @stdNo
		)

select * from dbo.getStudentFullNameDepartmentName(4)



-- 4. Create a scalar function that takes Student ID and returns a message to user 
----- a. If first name and Last name are null then display 'First name & last name are null'
----- b. If First name is null then display 'first name is null'
----- c. If Last name is null then display 'last name is null'
----- d. Else display 'First name & last name are not null'
create function studentNamesStatus(@stdNo int) returns varchar(50)
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

    return @result
end

select dbo.studentNamesStatus(33) as NameStatus



-- 5. Create inline function that takes integer which represents manager ID and displays department name, Manager Name and hiring date 
create function getManagerInfo(@mgrId int) returns table
return (
            select  d.Dept_Name, i.Ins_Name as MangerName, d.Manager_hiredate
            from Instructor i inner join Department d on i.Ins_Id = d.Dept_Manager
            where i.Ins_Id = @mgrId
        )

select * from dbo.getManagerInfo(2)



-- 6. Create multi-statements table-valued function that takes a string
----- If string='first name' returns student first name
----- If string='last name' returns student last name 
----- If string='full name' returns Full Name from student table 
----- Note: Use “ISNULL” function
create function getStudentNameQueryBased (@input varchar(20)) returns @t table (result varchar(50))
as
begin
    if @input = 'first name'
        insert into @t
        select isnull(St_Fname,'') from Student

    else if @input = 'last name'
        insert into @t
        select isnull(St_Lname,'') from Student

    else if @input = 'full name'
        insert into @t
        select concat(isnull(St_Fname,''),isnull(St_Lname,''), ' ') from Student
    return
end

select * from dbo.getStudentNameQueryBased('first name')
select * from dbo.getStudentNameQueryBased('last name')
select * from dbo.getStudentNameQueryBased('full name')



-- 7. Write a query that returns the Student No and Student first name without the last char
select St_Id, substring(St_Fname, 1, len(St_Fname) - 1)
from Student



-- 8. Wirte query to delete all grades for the students Located in SD Department 
delete sc
from Stud_Course sc, Student s, Department d
where sc.St_Id = s.St_Id and s.Dept_Id = d.Dept_Id and d.Dept_Name = 'SD'



----------------------------- BONUS -----------------------------
-- [1].
create table emp
(
	emp_id int,
	emp_name varchar(50),
	emp_level hierarchyid
) 

insert into emp values(1, 'Ahmed', '/1/')
insert into emp values(2, 'Sara', '/1/1/')
insert into emp values(3, 'Ali', '/1/2/')
insert into emp values(3, 'Ali', '/1/1/1/')
select * from emp



-- [2].
create table tmpStudent
(
    id int,
    fname varchar(20),
    lname varchar(20),
)

declare @start_Id int = 3000

while @start_Id < 6001
begin 
    insert into tmpStudent
    values (@start_Id, 'Jane', 'Smith')
    set @start_Id += 1
end