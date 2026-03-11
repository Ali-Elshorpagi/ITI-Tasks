use AdventureWorks2012

------> 1
select SalesOrderID, ShipDate
from Sales.SalesOrderHeader
where OrderDate between '7/28/2002' and '7/29/2014'


------> 2
select ProductID, Name 
from Production.Product 
where StandardCost < 110


------> 3
select ProductID, Name 
from Production.Product 
where Weight is null


------> 4
select * 
from Production.Product
where Color in ('Silver' , 'Black' , 'Red Color')


------> 5
select * 
from Production.Product 
where Name like 'B%'


------> 6
------------> 1st
UPDATE Production.ProductDescription
SET Description = 'Chromoly steel_High of defects'
WHERE ProductDescriptionID = 3

------------> 2nd
select *
from Production.ProductDescription
where Description like '%[_]%'


------> 7
select sum(TotalDue) as SumTotalDue, OrderDate
from Sales.SalesOrderHeader
where OrderDate between '7/1/2001' and '7/31/2014'
group by OrderDate


------> 8
select distinct HireDate
from HumanResources.Employee 


------> 9
select avg(distinct ListPrice) as AvgDistPrices
from Production.Product


------> 10
select concat('The ',Name,' is only!  ' , ListPrice)
from Production.Product
where ListPrice between 100 and 120
order by ListPrice asc


------> 11
------------> a
select rowguid, Name, SalesPersonID, Demographics into store_Archive
from Sales.Store

------------> test 	--701 rows	
select rowguid ,Name, SalesPersonID, Demographics 
from store_Archive 

------------> b
select rowguid, Name, SalesPersonID, Demographics into store_Archive_2
from Sales.Store
where 1 > 2


------> 12
select format(getdate(),'MM/dd/yy') as 'Today Date'
union 
select format(getdate(),'dddd')
union 
select format(getdate(),'MM')
union 
select format(getdate(),'yy')
union 
select format(getdate(),'dddd-MM-yyyy') 