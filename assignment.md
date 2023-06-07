# Assignments
1. USe The Azure Storage Explore and create a Blob Container of name 'datafiles'
2. Create an Azure Function for BlobTrigger, this will read allfile uploaded in Blob, if the file extension if '.txt', then will check if the file has the first line as  'Candidate Profile', if yes then it will read contents of such files and show on console



# Date: 07-June-2023

1. Create Database in Azure SQL with Following Tabel
Create Table Products(
  ProductRowId int Identity Primary Key,
  ProductId varchar(20) Unique,
  ProductName varchar(400) Not Null,
  Manufacturere varchar(400) Not Null,
  Price int Not Null 
)

2. Create a Azure Function with Http Trigger for Product Search based on following Requirements
	- Search All Products by Manufacturers, a single parameter as Manufacturer
	- Serach a Product for All Manufactureres , a single parameter as ProductName
	- Serach with logical AND and OR Conditions for Manufacturer and ProductName
		- Parameters to function will eb as follows
			- string manufacturer, string cond, string productname
			- For condition=="AND"
				- That Matching ProductName for the Manifacturer
			- For condition == "OR"
				- All Products for Manufacturer
					- All Products for Toshiba	
				- Products from All Manufacturers matching with ProductName
					- Laptops from All Manufactureres 	
	



