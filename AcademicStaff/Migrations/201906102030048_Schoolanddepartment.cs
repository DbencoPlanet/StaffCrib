namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schoolanddepartment : DbMigration
    {
        public override void Up()
        {

            Sql("SET IDENTITY_INSERT Schools ON");



        

            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1, N'SCHOOL OF COMPUTING AND INFORMATION TECHNOLOGY', N'SCIT')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (2, N'SCHOOL OF PHYSICAL SCIENCES', N'SOPS')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (3, N'SCHOOL OF ENGINEERING AND ENGINEERING TECHNOLOGY', N'SEET')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1003, N'SCHOOL OF BIOLOGICAL SCIENCES', N'SOBS')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1004, N'SCHOOL OF ENVIRONMENTAL SCIENCES', N'SOES')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1006, N'SCHOOL OF AGRICULTURE AND AGRICULTURAL TECHNOLOGY', N'SAAT')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1007, N'SCHOOL OF HEALTH TECHNOLOGY', N'SOHT')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1008, N'SCHOOL OF MANAGEMENT TECHNOLOGY', N'SMAT')");
            Sql("INSERT INTO Schools (Id, Name, ShortCode) VALUES (1009, N'SCHOOL OF BASIC MEDICAL SCIENCES', N'SBMS')");

            Sql("SET IDENTITY_INSERT Schools OFF");

            Sql("SET IDENTITY_INSERT Departments ON");

            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId) VALUES (1, N'COMPUTER SCIENCE', N'CSC', 1)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId) VALUES (2, N'INFORMATION TECHNOLOGY', N'IFT', 1)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (3, N'GEOLOGY', N'GLY', 2)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (4, N'MATHEMATICS', N'MTH', 2)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1005, N'MICROBIOLOGY', N'MCB', 1003)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1006, N'CYBER SECURITY', N'CYB', 1)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1007, N'BIOCHEMISTRY', N'BCH', 1003)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1008, N'AGRICULTURAL ECONOMICS', N'AEC', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1009, N'AGRICULTURAL EXTENSION', N'AEX', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1010, N'ANIMAL SCIENCE AND TECHNOLOGY', N'AST', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1011, N'CROP SCIENCE AND TECHNOLOGY', N'CST', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1012, N'FISHERIES AQUACULTURE TECHNOLOGY', N'FAT', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1013, N'FORESTRY AND WILD LIFE TECHNOLOGY', N'FWT', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1014, N'SOIL SCIENCE AND TECHNOLOGY', N'SST', 1006)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1015, N'ANATOMY', N'ANM', 1009)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1016, N'PHYSIOLOGY', N'PSL', 1009)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1017, N'BIOLOGY', N'BIO', 1003)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1018, N'BIOTECHNOLOGY', N'BTC', 1003)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1019, N'SOFTWARE ENGINEERING', N'SEN', 1)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1020, N'AGRICULTURAL AND BIO-RESOURCES ENGINEERING', N'ABE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1021, N'CHEMICAL ENGINEERING', N'CHE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1022, N'CIVIL ENGINEERING', N'CIE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1023, N'ELECTRICAL AND ELECTRONICS ENGINEERING', N'EEE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1024, N'FOOD SCIENCE TECHNOLOGY', N'FST', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1025, N'MATERIAL AND METALLURGICAL AND ENGINEERING', N'MME', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1026, N'MECHANICAL ENGINEERING', N'MEE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1027, N'MECHATRONICS ENGINEERING', N'MCE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1028, N'PETROLEUM ENGINEERING', N'PET', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1029, N'POLYMER AND TEXTILE ENGINEERING', N'PTE', 3)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1030, N'ARCHITECTURE', N'ARC', 1004)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1031, N'BUILDING TECHNOLOGY', N'BDT', 1004)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1032, N'ENVIRONMENTAL TECHNOLOGY', N'EVT', 1004)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1033, N'QUANTITY SURVEYING', N'QST', 1004)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1034, N'SURVEYING AND GEOINFORMATICS', N'SVG', 1004)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1035, N'URBAN AND REGIONAL PLANNING', N'URP', 1004)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1036, N'BIOMEDICAL TECHNOLOGY', N'BMT', 1007)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1037, N'DENTAL TECHNOLOGY', N'DNT', 1007)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1038, N'OPTOMETRY', N'OPT', 1007)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1039, N'PROSTHETICS AND OTHORTICS', N'POT', 1007)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1040, N'PUBLIC HEALTH TECHNOLOGY', N'PHT', 1007)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1041, N'ENVIRONMENTAL HEALTH SCIENCE', N'EHS', 1007)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1042, N'FINANCIAL MANAGEMENT TECHNOLOGY', N'FMT', 1008)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1043, N'MARITIME MANAGEMENT TECHNOLOGY', N'MMT', 1008)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1044, N'PROJECT MANAGEMENT TECHNOLOGY', N'PMT', 1008)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1045, N'TRANSPORT MANAGEMENT TECHNOLOGY', N'TMT', 1008)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1046, N'MANAGEMENT TECHNOLOGY', N'MAT', 1008)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1047, N'CHEMISTRY', N'ICH', 2)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1048, N'SCIENCE LABORATORY TECHNOLOGY', N'SLT', 2)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1049, N'STATISTICS', N'STA', 2)");
            Sql("INSERT INTO Departments (Id, Name, ShortCode, SchoolId)  VALUES (1050, N'PHYSICS', N'IPH', 2)");
            Sql("SET IDENTITY_INSERT Departments  OFF");
        }
   
        public override void Down()
        {
        }
    }
}
