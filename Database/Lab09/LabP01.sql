--------------------------------- PART 01 ---------------------------------

-- [1]. Create a stored procedure without parameters to show the number of students per department name
use ITI;
create procedure sp_StudentCountPerDept
as
begin
    select d.dept_name, count(s.st_id) as student_count
    from department d left join student s on d.dept_id = s.dept_id
    group by d.dept_name
end

sp_StudentCountPerDept



-- [2]. 
use Company_SD;
alter procedure sp_CheckNoEmployeesInP1
as
begin
    declare @emp_count int

    select @emp_count = count(*)
    from Works_for
    where Pno = 100

    if @emp_count >= 3
        print 'the number of employees in the project p1 is 3 or more'
    else
    begin
        print 'the following employees work for the project p1'

        select e.Fname, e.Lname
        from Employee e inner join Works_for w on e.SSN = w.ESSn
        where w.Pno = 100
    end
end

sp_CheckNoEmployeesInP1



-- [3]. 
alter procedure sp_ReplaceEmployee(@oldEmpNo int, @newEmpNo int, @projectNo int)
as 
begin -- try and catch
    begin try
        update Works_for
        set ESSn = @newEmpNo
        where ESSn = @oldEmpNo and Pno = @projectNo
      end try
    begin catch
        select error_message() as 'Error Message';
    end catch
end

select * from Works_for where Pno = 600
sp_ReplaceEmployee 521634, 968574, 600
select * from Works_for where Pno = 600



-- [4]. 
----- a.
alter table Project
add budget money

update Project
set budget = 100000

----- b.
create table ProjectAudit
(
    Pno int,
    userName varchar(50),
    modifiedDate datetime,
    budgetOld money,
    budgetNew money
)

----- c.
alter trigger trg_ProjectBudgetAudit on Project
after update
as
begin
    if update(budget)
    begin
        insert into ProjectAudit
        select d.Pnumber, user_name(), getdate(), d.budget, i.budget
        from deleted d inner join inserted i on d.Pnumber = i.Pnumber
    end
end

select Pnumber, budget from project where Pnumber = 200

update Project
set budget = 200000
where Pnumber = 200

select * from ProjectAudit
where Pno = 200



-- [5]. 
use ITI;
alter trigger trg_NoInsertIntoDepartment on Department
instead of insert
as
begin
    print 'you can’t insert a new record in department table'
end

insert into Department (Dept_Id, Dept_Name) values (30, 'ai')



-- [6]. 
use Company_SD;
create trigger trg_NoInsertIntoEmployeeInMarch on Employee
after insert
as
begin
    if (month(getdate()) = 3)
        begin
            print 'insertion into employee table is not allowed in march'
            rollback
        end
end



-- [7]. 
use ITI;
----- a.
create table StudentAudit
(
    ServerUserName varchar(50),
    AuditDate datetime,
    Note varchar(200)
)

-----  b.
alter trigger trg_StudentAuditAfterInsert on Student
after insert
as
begin
    insert into StudentAudit
    select suser_name(), getdate(), suser_name() + ' insert new row with key=' + cast(St_Id as varchar) + ' in table student'
    from inserted
end

insert into Student (St_Id, St_Fname) values (101, 'nour')
select * from StudentAudit



-- [8].
create trigger trg_StudentAuditInsteadOfDelete on Student
instead of delete
as 
begin
    insert into StudentAudit
    select suser_name(), getdate(), 'try to delete row with key=' + cast(st_id as varchar)
    from deleted
end


delete from Student where St_Id = 101

select * from StudentAudit


