Project Description :
The Person Management System is a web-based application developed using ASP.NET Web Forms and MySQL.
It allows users to manage person records including personal details, occupation, state, district, and known languages.

Technologies Used :
ASP.NET Web Forms
C#
MySQL
ADO.NET
Visual Studio 2026

Database Name
CitizenDB

Tables Used :
States
Districts
Languages
Occupations
Persons
PersonLanguages (Many-to-Many Relationship)

Features :
Add new person
Edit person details
Delete person
View all persons
Dropdown binding (State, District, Occupation)
Multiple language selection
Server-side validation

Validation Rules :
Name is required
Fields cannot be empty
Maximum length validation
Proper data type validation

How to Run the Project
Clone the repository
Open the solution file in Visual Studio
Import CitizenDB.sql into MySQL
Update connection string in web.config

Repository Structure
Person-Management-System
│
├── PersonManagementSystem/  (ASP.NET Project Folder)
├── CitizenDB.sql
├── Documentation.pdf
└── README.md
Run the project
