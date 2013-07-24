/*****************************************************************************
 * Functions
 ****************************************************************************/

/*
 * Simple echo functions
 */
 
CREATE OR REPLACE FUNCTION "EchoIntegerFunction" (inArg INTEGER)
	RETURN INTEGER IS
BEGIN
	RETURN inArg;
END;
/

CREATE OR REPLACE FUNCTION "EchoStringFunction" (inArg nvarchar2)
	RETURN nvarchar2 IS
BEGIN
	RETURN inArg;
END;
/

CREATE OR REPLACE FUNCTION "AddIntegers" (argA INTEGER, argB INTEGER) RETURN INTEGER IS
	result INTEGER;
BEGIN
	result := argA + argB;
	RETURN result;
END;
/

CREATE OR REPLACE FUNCTION "IntegerFunctionNoArgs"
	RETURN INTEGER IS
BEGIN
	RETURN 1;
END;
/
 
 /*
 * Simple functions with output arguments
 */
 
CREATE OR REPLACE FUNCTION "AddIntegers_OutputParam" (argA INTEGER, argB INTEGER, result out INTEGER)
	RETURN INTEGER IS
BEGIN
	result := argA + argB;
	RETURN result;
END;
/

CREATE OR REPLACE FUNCTION "ConcatStrings_OutputParam" (argA nvarchar2, argB nvarchar2, result out nvarchar2)
	RETURN nvarchar2 IS
BEGIN
	result := CONCAT(argA, argB);
	RETURN result;
END;
/
 
 /*****************************************************************************
 * Stored procedures
 ****************************************************************************/

CREATE OR REPLACE PROCEDURE "NullProc" AS
BEGIN
	NULL;
END;
/

CREATE OR REPLACE PROCEDURE "DoConcatStrings" (argA nvarchar2, argB nvarchar2, result out nvarchar2) AS
BEGIN
	result := CONCAT(argA, argB);
END;
/

CREATE OR REPLACE PROCEDURE "DoAddIntegers" (argA INTEGER, argB INTEGER, result out INTEGER) AS
BEGIN
	result := argA + argB;
END;
/

/*
	TEST package declaration
*/
CREATE OR REPLACE PACKAGE TEST AS

	TYPE T_CURSOR IS REF CURSOR;

	PROCEDURE "GetCategories" (categoryCursor out T_CURSOR);
	
	PROCEDURE "GetCategories2" (categoryCursor out T_CURSOR);

	PROCEDURE "GetCategoriesAndProducts" (someInputParam  int, categoryCursor out T_CURSOR,
					productCursor out T_CURSOR);

END TEST;
/

/*
	TEST package definition
*/
CREATE OR REPLACE PACKAGE BODY TEST AS

	PROCEDURE "GetCategories" (categoryCursor out T_CURSOR) IS
	BEGIN
		OPEN categoryCursor FOR SELECT * FROM "Categories";
	END "GetCategories";
	
	PROCEDURE "GetCategories2" (categoryCursor out T_CURSOR) IS
	BEGIN
		OPEN categoryCursor FOR SELECT * FROM "Categories";
	END "GetCategories2";

	PROCEDURE "GetCategoriesAndProducts" (someInputParam  int, categoryCursor out T_CURSOR,
					productCursor out T_CURSOR) IS
	BEGIN
		OPEN categoryCursor FOR SELECT * FROM "Categories" where MOD("CategoryID", someInputParam) = 0;
		OPEN productCursor FOR SELECT * FROM "Products" where MOD("ProductID", someInputParam) = 0;
	END "GetCategoriesAndProducts";

END TEST;
/

EXIT
/