/********************************************************************************************
  ⚠️ יש להריץ סקריפט זה **רק לאחר** הרצת הסקריפטים שמזינים את קטגוריות ההכנסות וההוצאות.

  הסקריפט מוסיף עבור כל חודש בשנת 2024 בין 5 ל-6 הוצאות ובין 3 ל-4 הכנסות עבור משתמש מסוים.

  📌 יש לעדכן את מזהה המשתמש בשורה המתאימה לפני ההרצה (userId)
********************************************************************************************/

DECLARE @UserId INT = [x]; -- 👈 יש לשנות ל-ID של המשתמש הרלוונטי

-- הגדרות טווח
DECLARE @Month INT = 1;
DECLARE @MonthId INT;
DECLARE @Year INT = 2024;

-- יצירת טבלת קטגוריות זמנית (בהנחה שהקטגוריות הוזנו קודם)
DECLARE @ExpenseCategories TABLE (Id INT);
INSERT INTO @ExpenseCategories
SELECT expenseCategoryId FROM ExpenseCategories;

DECLARE @IncomeCategories TABLE (Id INT);
INSERT INTO @IncomeCategories
SELECT incomeCategoryId FROM IncomeCategories;

-- עבור כל חודש ב-2024
WHILE @Month <= 12
BEGIN
    -- שליפת monthId המתאים
    SELECT @MonthId = monthId
    FROM Months
    WHERE [month] = @Month AND [year] = @Year AND userId = @UserId;

    IF @MonthId IS NULL
    BEGIN
        PRINT CONCAT('❌ Month not found for ', @Month, '/', @Year);
        SET @Month = @Month + 1;
        CONTINUE;
    END

    DECLARE @i INT = 0;
    DECLARE @RandomCount INT = FLOOR(RAND() * 2) + 5; -- 5 עד 6 הוצאות

    WHILE @i < @RandomCount
    BEGIN
        INSERT INTO Expenses (userId, amount, expenseCategoryId, expenseName, date, monthId, description)
        SELECT TOP 1
            @UserId,
            ROUND(50 + RAND() * 450, 0), -- סכום בין 50 ל-500
            ec.Id,
            ec.Name,
            DATEFROMPARTS(@Year, @Month, FLOOR(RAND() * 28) + 1),
            @MonthId,
            CONCAT('Auto-generated expense ', @i + 1)
        FROM ExpenseCategories ec
        JOIN @ExpenseCategories t ON t.Id = ec.expenseCategoryId
        ORDER BY NEWID();

        SET @i = @i + 1;
    END

    SET @i = 0;
    SET @RandomCount = FLOOR(RAND() * 2) + 3; -- 3 עד 4 הכנסות

    WHILE @i < @RandomCount
    BEGIN
        INSERT INTO Incomes (userId, amount, incomeCategoryId, incomeName, date, monthId, description)
        SELECT TOP 1
            @UserId,
            ROUND(1000 + RAND() * 4000, 0), -- סכום בין 1000 ל-5000
            ic.Id,
            ic.Name,
            DATEFROMPARTS(@Year, @Month, FLOOR(RAND() * 28) + 1),
            @MonthId,
            CONCAT('Auto-generated income ', @i + 1)
        FROM IncomeCategories ic
        JOIN @IncomeCategories t ON t.Id = ic.incomeCategoryId
        ORDER BY NEWID();

        SET @i = @i + 1;
    END

    PRINT CONCAT('✅ Added data for month ', @Month);
    SET @Month = @Month + 1;
END


