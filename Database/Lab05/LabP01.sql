use ITI

------> 1
select count(St_Id) as StudentCount 
from Student 
where st_age is not null


------> 2
select distinct Ins_Name 
from Instructor


------> 3
select St_Id as 'Student ID', isnull(concat_ws(' ', St_Fname, St_Lname),'Student Full Name') as 'Student Full Name', Dept_Name as 'Department Name' 
from Student inner join Department 
on Student.Dept_Id = Department.Dept_Id


------> 4
select Ins_Name as InstuctorName, Dept_Name as DepartmentName 
from Instructor left join Department 
on Instructor.Dept_Id = Department.Dept_Id


------> 5
select concat_ws(' ', St_Fname, St_Lname) as 'Student Full Name', Crs_Name as 'Course Name' 
from Student inner join Stud_Course 
on Student.St_Id = Stud_Course.St_Id inner join Course
on Course.Crs_Id = Stud_Course.Crs_Id and Grade is not null


------> 6
select Top_Name, count(Crs_Id) as CourcesCount
from Course inner join Topic 
on Course.Top_Id = Topic.Top_Id 
group by Top_Name


------> 7
select min(salary) as 'Min Salary', max(salary) as 'Max Salary'	
from Instructor


------> 8
select *  
from Instructor 
where Salary < (select avg(Salary) from Instructor)


------> 9
select Dept_Name as 'Department name' 
from Department inner join Instructor 
on Department.Dept_Id = Instructor.Dept_Id and Salary = (select min(Salary) from Instructor)


------> 10
select top(2) Salary
from Instructor
order by Salary desc


------> 11
select Ins_Name, coalesce(convert(varchar(50),salary), 'instructor bonus') as Salary
from Instructor


------> 12
select avg(salary) as AvgSalary 
from Instructor


------> 13
select st.St_Fname as StudentFirstName , super.*  
from student as st inner join student super
on st.St_super = super.St_Id


------> 14
select * 
from (select Salary, Dept_Id, ROW_NUMBER() over (partition by Dept_Id order by salary desc) as RN 
		from Instructor where salary is not null) as NewTable
where RN < 3


------> 15
select * 
from (select *, rank() over (partition by Dept_Id order by newid()) as RN 
		from Student) as NewTable 
where RN < 2