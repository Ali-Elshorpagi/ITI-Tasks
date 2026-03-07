INSERT INTO Departments (Name, Capacity) VALUES
('Computer Science', 120),
('Business', 150),
('Engineering', 200),
('Mathematics', 80),
('Arts', 60);

GO

INSERT INTO Courses (Name, Duration) VALUES
('Database Systems', 4),
('Data Structures', 5),
('Marketing Principles', 3),
('Thermodynamics', 4),
('Linear Algebra', 4),
('Operating Systems', 5),
('Financial Accounting', 3),
('Machine Learning', 6),
('Digital Design', 4),
('Statistics', 4);

GO

INSERT INTO Students (Name, Age, Email, DepartmentId) VALUES
('Alice Johnson', 20, 'alice.johnson@example.com', 1),
('Bob Smith', 22, 'bob.smith@example.com', 1),
('Carol White', 21, 'carol.white@example.com', 2),
('David Brown', 23, 'david.brown@example.com', 3),
('Emma Davis', 19, 'emma.davis@example.com', 4),
('Frank Moore', 24, 'frank.moore@example.com', 2),
('Grace Taylor', 20, 'grace.taylor@example.com', 1),	
('Henry Wilson', 22, 'henry.wilson@example.com', 3),
('Ivy Martinez', 21, 'ivy.martinez@example.com', 5),
('Jack Anderson', 23, 'jack.anderson@example.com', 4);

GO

INSERT INTO StudentCourses (StudentId, CourseId, Degree) VALUES
(1, 1, 85),
(1, 2, 90),
(1, 8, 88),

(2, 1, 78),
(2, 6, 82),

(3, 3, 91),
(3, 7, 87),

(4, 4, 75),
(4, 9, 80),

(5, 5, 89),
(5, 10, 92),

(6, 3, 73),
(6, 7, 77),

(7, 2, 84),
(7, 6, 86),
(7, 8, 90),

(8, 4, 79),
(8, 9, 83),

(9, 10, 88),

(10, 5, 81),
(10, 10, 85);

GO

INSERT INTO CourseDepartment (CoursesId, DepartmentsId) VALUES
-- Computer Science (1): Database Systems, Data Structures, Operating Systems, Machine Learning
(1, 1),
(2, 1),
(6, 1),
(8, 1),
-- Business (2): Marketing Principles, Financial Accounting
(3, 2),
(7, 2),
-- Engineering (3): Thermodynamics, Digital Design
(4, 3),
(9, 3),
-- Mathematics (4): Linear Algebra, Statistics
(5, 4),
(10, 4),
-- Arts (5): Statistics
(10, 5);