CREATE SEQUENCE "Categories_0" START WITH 100000 INCREMENT BY 1
/
CREATE SEQUENCE "Employees_0" START WITH 100000 INCREMENT BY 1
/
CREATE SEQUENCE "Suppliers_0" START WITH 100000 INCREMENT BY 1
/
CREATE SEQUENCE "Orders_0" START WITH 100000 INCREMENT BY 1
/
CREATE SEQUENCE "Products_0" START WITH 100000 INCREMENT BY 1
/

CREATE TABLE "Shippers" (
	"ShipperID" NUMBER(11,0) PRIMARY KEY NOT NULL ,
	"CompanyName" varchar2 (40) NOT NULL ,
	"Phone" varchar2 (24) NULL
)
/

CREATE TABLE  "Categories" 
   (	"CategoryID" NUMBER(11,0), 
	"CategoryName" VARCHAR2(15) NOT NULL ENABLE, 
	"Description" VARCHAR2(1000), 
	"Picture" RAW(2000),
        CONSTRAINT "PK_Categories" PRIMARY KEY ("CategoryID") ENABLE
   )
/

CREATE INDEX  "CategoryName" ON  "Categories" ("CategoryName")
/

CREATE OR REPLACE TRIGGER  "Categories" 
BEFORE INSERT ON "Categories" FOR EACH ROW 
DECLARE last_Sequence NUMBER; 
	last_InsertID NUMBER; 
BEGIN 
	IF (:NEW."CategoryID" IS NULL) THEN 
		SELECT "Categories_0".NEXTVAL INTO :NEW."CategoryID" FROM DUAL; 
	ELSE 
		SELECT Last_Number-1 INTO last_Sequence FROM User_Sequences WHERE UPPER(Sequence_Name) = UPPER('Categories_0'); 
		SELECT :NEW."CategoryID" INTO last_InsertID FROM DUAL; 
		WHILE (last_InsertID > last_Sequence) LOOP 
			SELECT "Categories_0".NEXTVAL INTO last_Sequence FROM DUAL; 
		END LOOP; 
	END IF; 
END;
/
ALTER TRIGGER  "Categories" ENABLE
/

CREATE TABLE  "Customers" 
   (	"CustomerID" VARCHAR2(5) NOT NULL ENABLE, 
	"CompanyName" VARCHAR2(40) NOT NULL ENABLE, 
	"ContactName" VARCHAR2(30), 
	"ContactTitle" VARCHAR2(30), 
	"Address" VARCHAR2(60), 
	"City" VARCHAR2(32), 
	"Region" VARCHAR2(15), 
	"PostalCode" VARCHAR2(10), 
	"Country" VARCHAR2(15), 
	"Phone" VARCHAR2(24), 
	"Fax" VARCHAR2(24), 
	 CONSTRAINT "PK_Customers" PRIMARY KEY ("CustomerID") ENABLE
   )
/

CREATE INDEX  "City" ON  "Customers" ("City")
/

CREATE INDEX  "CompanyName" ON  "Customers" ("CompanyName")
/

CREATE INDEX  "PostalCode" ON  "Customers" ("PostalCode")
/

CREATE INDEX  "Region" ON  "Customers" ("Region")
/

CREATE TABLE  "Employees" 
   (	"EmployeeID" NUMBER(11,0), 
	"LastName" VARCHAR2(20) NOT NULL ENABLE, 
	"FirstName" VARCHAR2(10) NOT NULL ENABLE, 
	"Title" VARCHAR2(30), 
	"TitleOfCourtesy" VARCHAR2(25), 
	"BirthDate" DATE, 
	"HireDate" DATE, 
	"Address" VARCHAR2(60), 
	"City" VARCHAR2(15), 
	"Region" VARCHAR2(15), 
	"PostalCode" VARCHAR2(10), 
	"Country" VARCHAR2(15), 
	"HomePhone" VARCHAR2(24), 
	"Extension" VARCHAR2(4), 
	"Photo" BLOB, 
	"Notes" CLOB, 
    "ReportsTo" NUMBER(11,0) NULL REFERENCES "Employees" ("EmployeeID"),
	"PhotoPath" VARCHAR2(255), 
	 CONSTRAINT "PK_Employees" PRIMARY KEY ("EmployeeID") ENABLE
   )
/

CREATE INDEX  "LastName" ON  "Employees" ("LastName")
/

CREATE INDEX  "PostalCode00000" ON  "Employees" ("PostalCode")
/

CREATE OR REPLACE TRIGGER  "Employees" BEFORE INSERT ON "Employees" FOR EACH ROW DECLARE last_Sequence NUMBER; last_InsertID NUMBER; BEGIN IF (:NEW."EmployeeID" IS NULL) THEN SELECT "Employees_0".NEXTVAL INTO :NEW."EmployeeID" FROM DUAL; ELSE SELECT Last_Number-1 INTO last_Sequence FROM User_Sequences WHERE UPPER(Sequence_Name) = UPPER('Employees_0'); SELECT :NEW."EmployeeID" INTO last_InsertID FROM DUAL; WHILE (last_InsertID > last_Sequence) LOOP SELECT "Employees_0".NEXTVAL INTO last_Sequence FROM DUAL; END LOOP; END IF; END;
/
ALTER TRIGGER  "Employees" ENABLE
/

CREATE TABLE  "Orders" 
   (	"OrderID" NUMBER(11,0), 
	"CustomerID" VARCHAR2(5), 
	"EmployeeID" NUMBER(11,0), 
	"OrderDate" TIMESTAMP(9), 
	"RequiredDate" TIMESTAMP(3),
	"ShippedDate" DATE, 
    "ShipVia" NUMBER(11,0) REFERENCES "Shippers"("ShipperID"),
    "Freight" NUMBER(19,8) DEFAULT 0, 
	"ShipName" VARCHAR2(40), 
	"ShipAddress" VARCHAR2(60), 
	"ShipCity" VARCHAR2(32), 
	"ShipRegion" VARCHAR2(15), 
	"ShipPostalCode" VARCHAR2(10), 
	"ShipCountry" VARCHAR2(15), 
	 CONSTRAINT "PK_Orders" PRIMARY KEY ("OrderID") ENABLE, 
	 FOREIGN KEY ("CustomerID")
	  REFERENCES  "Customers" ("CustomerID") ENABLE
   )
/

CREATE INDEX  "CustomerID" ON  "Orders" ("CustomerID")
/

CREATE INDEX  "EmployeeID" ON  "Orders" ("EmployeeID")
/

CREATE INDEX  "OrderDate" ON  "Orders" ("OrderDate")
/

CREATE INDEX  "ShipPostalCode" ON  "Orders" ("ShipPostalCode")
/

CREATE INDEX  "ShippedDate" ON  "Orders" ("ShippedDate")
/

CREATE OR REPLACE TRIGGER  "Orders" BEFORE INSERT ON "Orders" FOR EACH ROW DECLARE last_Sequence NUMBER; last_InsertID NUMBER; BEGIN IF (:NEW."OrderID" IS NULL) THEN SELECT "Orders_0".NEXTVAL INTO :NEW."OrderID" FROM DUAL; ELSE SELECT Last_Number-1 INTO last_Sequence FROM User_Sequences WHERE UPPER(Sequence_Name) = UPPER('Orders_0'); SELECT :NEW."OrderID" INTO last_InsertID FROM DUAL; WHILE (last_InsertID > last_Sequence) LOOP SELECT "Orders_0".NEXTVAL INTO last_Sequence FROM DUAL; END LOOP; END IF; END;
/
ALTER TRIGGER  "Orders" ENABLE
/


CREATE TABLE  "PreviousEmployees" 
   (	"EmployeeID" NUMBER(11,0) NOT NULL ENABLE, 
	"LastName" VARCHAR2(20) NOT NULL ENABLE, 
	"FirstName" VARCHAR2(10) NOT NULL ENABLE, 
	"Title" VARCHAR2(30), 
	"TitleOfCourtesy" VARCHAR2(25), 
	"BirthDate" DATE, 
	"HireDate" DATE, 
	"Address" VARCHAR2(60), 
	"City" VARCHAR2(32), 
	"Region" VARCHAR2(15), 
	"PostalCode" VARCHAR2(10), 
	"Country" VARCHAR2(32), 
	"HomePhone" VARCHAR2(24), 
	"Extension" VARCHAR2(4), 
	"Photo" BLOB, 
	"Notes" CLOB, 
    "PhotoPath" VARCHAR2(255), 
	 CONSTRAINT "PK_PreviousEmployees" PRIMARY KEY ("EmployeeID") ENABLE
   )
/


CREATE TABLE  "Suppliers" 
   (	"SupplierID" NUMBER(11,0), 
	"CompanyName" VARCHAR2(40) NOT NULL ENABLE, 
	"ContactName" VARCHAR2(30), 
	"ContactTitle" VARCHAR2(30), 
	"Address" VARCHAR2(60), 
	"City" VARCHAR2(15), 
	"Region" VARCHAR2(15), 
	"PostalCode" VARCHAR2(10), 
	"Country" VARCHAR2(15), 
	"Phone" VARCHAR2(24), 
	"Fax" VARCHAR2(24), 
	"HomePage" CLOB, 
	 CONSTRAINT "PK_Suppliers" PRIMARY KEY ("SupplierID") ENABLE
   )
/

CREATE INDEX  "CompanyName00000" ON  "Suppliers" ("CompanyName")
/

CREATE INDEX  "PostalCode00001" ON  "Suppliers" ("PostalCode")
/

CREATE OR REPLACE TRIGGER  "Suppliers" BEFORE INSERT ON "Suppliers" FOR EACH ROW DECLARE last_Sequence NUMBER; last_InsertID NUMBER; BEGIN IF (:NEW."SupplierID" IS NULL) THEN SELECT "Suppliers_0".NEXTVAL INTO :NEW."SupplierID" FROM DUAL; ELSE SELECT Last_Number-1 INTO last_Sequence FROM User_Sequences WHERE UPPER(Sequence_Name) = UPPER('Suppliers_0'); SELECT :NEW."SupplierID" INTO last_InsertID FROM DUAL; WHILE (last_InsertID > last_Sequence) LOOP SELECT "Suppliers_0".NEXTVAL INTO last_Sequence FROM DUAL; END LOOP; END IF; END;
/
ALTER TRIGGER  "Suppliers" ENABLE
/

CREATE TABLE  "Products" 
   (	"ProductID" NUMBER(11,0), 
	"ProductName" VARCHAR2(40) NOT NULL ENABLE, 
	"SupplierID" NUMBER(11,0), 
	"CategoryID" NUMBER(11,0), 
	"QuantityPerUnit" VARCHAR2(20), 
	"UnitPrice" NUMBER DEFAULT 0, 
	"UnitsInStock" NUMBER(5,0) DEFAULT 0, 
	"UnitsOnOrder" NUMBER(5,0) DEFAULT 0, 
	"ReorderLevel" NUMBER(5,0) DEFAULT 0, 
	"Discontinued" NUMBER(3,0) DEFAULT 0 NOT NULL ENABLE, 
	"DiscontinuedDate" DATE, 
	 CONSTRAINT "PK_Products" PRIMARY KEY ("ProductID") ENABLE, 
	 FOREIGN KEY ("CategoryID")
	  REFERENCES  "Categories" ("CategoryID") ENABLE, 
	 FOREIGN KEY ("SupplierID")
	  REFERENCES  "Suppliers" ("SupplierID") ENABLE
   )
/

CREATE INDEX  "CategoriesProducts" ON  "Products" ("CategoryID")
/

CREATE INDEX  "ProductName" ON  "Products" ("ProductName")
/

CREATE INDEX  "SupplierID" ON  "Products" ("SupplierID")
/

CREATE OR REPLACE TRIGGER  "Products" BEFORE INSERT ON "Products" FOR EACH ROW 
DECLARE last_Sequence NUMBER; last_InsertID NUMBER; 
BEGIN
    IF (:NEW."ProductID" IS NULL) THEN 
        SELECT "Products_0".NEXTVAL INTO :NEW."ProductID" FROM DUAL; 
    ELSE 
        SELECT Last_Number-1 INTO last_Sequence FROM User_Sequences WHERE UPPER(Sequence_Name) = UPPER('Products_0'); 
        SELECT :NEW."ProductID" INTO last_InsertID FROM DUAL; 
        WHILE (last_InsertID > last_Sequence) LOOP 
           SELECT "Products_0".NEXTVAL INTO last_Sequence FROM DUAL; 
        END LOOP; 
    END IF; 
END;
/
ALTER TRIGGER  "Products" ENABLE
/

CREATE TABLE  "Regions" 
   (	"RegionID" NUMBER(11,0) NOT NULL ENABLE, 
	"RegionDescription" VARCHAR2(50) NOT NULL ENABLE, 
	 CONSTRAINT "PK_Region" PRIMARY KEY ("RegionID") ENABLE
   )
/

CREATE TABLE  "Territories" 
   (	"TerritoryID" NUMBER(11,0) NOT NULL ENABLE, 
	"TerritoryDescription" VARCHAR2(50) NOT NULL ENABLE, 
	"RegionID" NUMBER(11,0) NOT NULL ENABLE, 
	 CONSTRAINT "PK_Territories" PRIMARY KEY ("TerritoryID") ENABLE, 
	 FOREIGN KEY ("RegionID")
	  REFERENCES  "Regions" ("RegionID") ENABLE
   )
/

CREATE TABLE  "OrderDetails" 
   (	"OrderID" NUMBER(11,0) NOT NULL ENABLE, 
	"ProductID" NUMBER(11,0) NOT NULL ENABLE, 
	"UnitPrice" NUMBER DEFAULT 0 NOT NULL ENABLE, 
	"Quantity" NUMBER(5,0) DEFAULT 1 NOT NULL ENABLE, 
	"Discount" NUMBER(19,4) NOT NULL ENABLE, 
	 CONSTRAINT "PK_Order_Details" PRIMARY KEY ("OrderID", "ProductID") ENABLE, 
	 FOREIGN KEY ("OrderID")
	  REFERENCES  "Orders" ("OrderID") ENABLE, 
	 FOREIGN KEY ("ProductID")
	  REFERENCES  "Products" ("ProductID") ENABLE
   )
/

CREATE INDEX  "OrderID" ON  "OrderDetails" ("OrderID")
/

CREATE INDEX  "ProductID" ON  "OrderDetails" ("ProductID")
/

CREATE TABLE  "EmployeesTerritories" 
   (	"EmployeeID" NUMBER(11,0) NOT NULL ENABLE, 
	"TerritoryID" NUMBER(11,0) NOT NULL ENABLE, 
	 CONSTRAINT "PK_EmployeeTerritories" PRIMARY KEY ("EmployeeID", "TerritoryID") ENABLE, 
	 FOREIGN KEY ("EmployeeID")
	  REFERENCES  "Employees" ("EmployeeID") ENABLE, 
	 FOREIGN KEY ("TerritoryID")
	  REFERENCES  "Territories" ("TerritoryID") ENABLE
   )
/

CREATE TABLE  "InternationalOrders" 
   (	"OrderID" NUMBER(11,0) NOT NULL ENABLE, 
	"CustomsDescription" VARCHAR2(100) NOT NULL ENABLE, 
	"ExciseTax" NUMBER NOT NULL ENABLE, 
	 CONSTRAINT "PK_InternationalOrders" PRIMARY KEY ("OrderID") ENABLE, 
	 FOREIGN KEY ("OrderID")
	  REFERENCES  "Orders" ("OrderID") ENABLE
   )
/

CREATE TABLE  "OracleDataTypes" 
(	"ID" NUMBER(11,0) PRIMARY KEY NOT NULL ENABLE, 
	"GuidColumn" RAW(16) NULL
)
/

EXIT
/