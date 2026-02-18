show databases;
create database CitizenDB;
use CitizenDB;
CREATE TABLE States (
    StateId INT PRIMARY KEY AUTO_INCREMENT,
    StateName VARCHAR(50) NOT NULL,
    IsActive BIT DEFAULT 1
);
CREATE TABLE Districts (
    DistrictId INT PRIMARY KEY AUTO_INCREMENT,
    DistrictName VARCHAR(50) NOT NULL,
    StateId INT,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (StateId) REFERENCES States(StateId)
);
CREATE TABLE Languages (
    LanguageId INT PRIMARY KEY AUTO_INCREMENT,
    LanguageName VARCHAR(50) NOT NULL,
    IsActive BIT DEFAULT 1
);
CREATE TABLE Occupations (
    OccupationId INT PRIMARY KEY AUTO_INCREMENT,
    OccupationName VARCHAR(50) NOT NULL,
    RequiresLicense BIT DEFAULT 0,
    IsActive BIT DEFAULT 1
);
show tables;
CREATE TABLE Persons (
    PersonId INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Address TEXT,
    Gender VARCHAR(10),
    DOB DATE,
    StateId INT,
    DistrictId INT,
    OccupationId INT,
    MaritalStatus VARCHAR(20),
    SpouseName VARCHAR(100) NULL,
    HasLicense BOOLEAN DEFAULT 0,
    LicenseNumber VARCHAR(50) NULL,
    ProfilePhoto VARCHAR(200),   -- stores file path
    IsActive BOOLEAN DEFAULT TRUE,

    FOREIGN KEY (StateId) REFERENCES States(StateId),
    FOREIGN KEY (DistrictId) REFERENCES Districts(DistrictId),
    FOREIGN KEY (OccupationId) REFERENCES Occupations(OccupationId)
);
show tables;
CREATE TABLE PersonLanguages (
    PersonId INT,
    LanguageId INT,

    PRIMARY KEY (PersonId, LanguageId),

    FOREIGN KEY (PersonId) REFERENCES Persons(PersonId),
    FOREIGN KEY (LanguageId) REFERENCES Languages(LanguageId)
);

INSERT INTO States (StateName) VALUES
('Karnataka'),
('Maharashtra'),
('Tamil Nadu');

INSERT INTO Districts (DistrictName, StateId) VALUES
('Bangalore Urban', 1),
('Mysore', 1),
('Pune', 2);

INSERT INTO Languages (LanguageName) VALUES
('Kannada'),
('English'),
('Hindi');

INSERT INTO Occupations (OccupationName, RequiresLicense) VALUES
('Doctor', 1),
('Engineer', 0),
('Teacher', 1);

select * from states;
select * from Districts;
select * from Languages;
select * from Occupations;

INSERT INTO Persons 
(Name, Address, Gender, DOB, StateId, DistrictId, OccupationId, 
 MaritalStatus, SpouseName, HasLicense, LicenseNumber, ProfilePhoto)
VALUES
('Akshata', 'Bangalore', 'Female', '2003-05-12',
 1, 1, 2,
 'Single', NULL,
 0, NULL,
 '/Uploads/Profile/akshata.jpg');
 
 INSERT INTO Persons 
(Name, Address, Gender, DOB, StateId, DistrictId, OccupationId, 
 MaritalStatus, SpouseName, HasLicense, LicenseNumber, ProfilePhoto)
VALUES
('Rahul', 'Mysore', 'Male', '1995-08-20',
 1, 2, 1,
 'Married', 'Priya',
 1, 'DL12345',
 '/Uploads/Profile/rahul.jpg');
 
 INSERT INTO Persons 
(Name, Address, Gender, DOB, StateId, DistrictId, OccupationId, 
 MaritalStatus, SpouseName, HasLicense, LicenseNumber, ProfilePhoto)
VALUES
('Sneha', 'Pune', 'Female', '1998-03-10',
 2, 3, 3,
 'Married', 'Arjun',
 0, NULL,
 '/Uploads/Profile/sneha.jpg');
 
select * from Persons;

INSERT INTO PersonLanguages (PersonId, LanguageId)
VALUES
(1, 1),
(1, 2);

INSERT INTO PersonLanguages (PersonId, LanguageId)
VALUES
(2, 2),
(2, 3);

INSERT INTO PersonLanguages (PersonId, LanguageId)
VALUES
(3, 3);

select * from PersonLanguages;

SELECT p.Name,
       s.StateName,
       d.DistrictName,
       o.OccupationName
FROM Persons p
JOIN States s ON p.StateId = s.StateId
JOIN Districts d ON p.DistrictId = d.DistrictId
JOIN Occupations o ON p.OccupationId = o.OccupationId;

SELECT p.Name, l.LanguageName
FROM Persons p
JOIN PersonLanguages pl ON p.PersonId = pl.PersonId
JOIN Languages l ON pl.LanguageId = l.LanguageId;


select * from Persons;























