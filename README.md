# Project WebApi + Angular

מסמך זה מתאר את הפרויקט, איך להפעיל אותו מקומית, ואיך להשתמש בו.

## מה יש בפרויקט

הפרויקט בנוי משני חלקים:

- צד שרת: ASP.NET Core Web API (.NET 8)
- צד לקוח: Angular 19

מבנה עיקרי:

- `ProjectAPI/` - שרת API, הרשאות JWT, עבודה מול SQL Server, Swagger, לוגים (Serilog)
- `Client/` - אפליקציית Angular (מסכים למשתמש ולמנהל)
- `ProjectAPI.sln` - Solution שמכיל את חלק השרת

## פיצ'רים מרכזיים

- התחברות והרשמה (`Auth`)
- ניהול מתנות, תורמים, משתמשים וסל קניות
- בחירת זוכה להגרלה
- דוחות (למשל סך הכנסות)
- הרשאות לפי תפקיד (למשל `Manager`)

## דרישות מקדימות

יש להתקין:

1. .NET SDK 8
2. Node.js LTS (מומלץ 18+ או 20+)
3. Angular CLI (אופציונלי, אפשר גם דרך npm scripts)
4. SQL Server מקומי

## הגדרות חשובות לפני הרצה

קובץ: `ProjectAPI/appsettings.json`

- `ConnectionStrings:DefaultConnection` צריך להפנות ל-SQL Server תקין
- `JwtSettings` צריך להכיל `SecretKey`, `Issuer`, `Audience`
- `Smtp` נדרש לשליחת מייל לזוכה

דוגמה:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;DataBase=LotteryDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"
}
```

## הרצת השרת (API)

מתיקיית השורש:

```bash
cd ProjectAPI
dotnet restore
dotnet ef database update
dotnet run
```

ברירת מחדל בפרופיל פיתוח:

- HTTP: `http://localhost:5022`
- HTTPS: `https://localhost:7184`
- Swagger: `https://localhost:7184/swagger` או `http://localhost:5022/swagger`

הערה:
אם `dotnet ef` לא מזוהה, אפשר להתקין כלי EF:

```bash
dotnet tool install --global dotnet-ef
```

## הרצת הלקוח (Angular)

בטרמינל נפרד:

```bash
cd Client
npm install
npm start
```

האפליקציה תעלה בכתובת:

- `http://localhost:4200`

נתיב ברירת מחדל הוא מסך התחברות:

- `http://localhost:4200/login`

## סדר הרצה מומלץ

1. להפעיל קודם את ה-API (`dotnet run`)
2. לוודא ש-Swagger נפתח
3. להפעיל את Angular (`npm start`)
4. לפתוח דפדפן על `http://localhost:4200/login`

## איך משתמשים במערכת

### משתמש חדש

1. לעבור ל-`/register`
2. למלא פרטים ולהירשם
3. להתחבר דרך `/login`

### לאחר התחברות

- מתקבל JWT מהשרת
- הטוקן נשלח אוטומטית בבקשות דרך Interceptor (`Authorization: Bearer <token>`)
- אפשר לצפות במתנות, לנהל סל, ולהשתמש בפעולות מותאמות הרשאה

### פעולות מנהל

פעולות מסוימות דורשות תפקיד `Manager`, לדוגמה:

- יצירת מתנה
- יצירת/עדכון מבצע מכירות
- הגרלת זוכה
- צפייה בדוח הכנסות

## API מרכזי (דוגמאות)

בסיס API:

- `http://localhost:5022/api`

נקודות קצה שימושיות:

- `POST /api/Auth/login`
- `POST /api/Auth/register`
- `GET /api/GiftsControllers/GetAllGifts`
- `POST /api/Basket/CreateBasket`
- `POST /api/Lottery/draw-winner` (Manager)
- `GET /api/Report/total-income` (Manager)

## לוגים וניטור

השרת משתמש ב-Serilog:

- פלט לקונסול
- קבצי לוג בתיקייה `ProjectAPI/Logs/`

## תקלות נפוצות

1. שגיאת חיבור למסד נתונים
- לבדוק `DefaultConnection` ושהשירות של SQL Server רץ.

2. שגיאת 401/403
- לבדוק שהתחברת וקיבלת טוקן תקין.
- לבדוק אם endpoint דורש `Manager`.

3. שגיאת CORS
- בפרויקט מוגדרת מדיניות פיתוח פתוחה (`AllowAnyOrigin`).
- לוודא שה-API רץ ב-`5022/7184` והלקוח ב-`4200`.

4. שגיאת HTTPS בתעודה מקומית
- אפשר לבדוק קודם דרך HTTP (`http://localhost:5022`) או לאשר תעודת פיתוח מקומית.

## בדיקות ובניה

צד לקוח:

```bash
cd Client
npm test
npm run build
```

צד שרת:

```bash
cd ProjectAPI
dotnet build
```

## אבטחה והערות חשובות

- לא לשמור סיסמאות אמיתיות ו-Secrets בתוך `appsettings.json` בסביבת Production.
- מומלץ להשתמש ב-Secret Manager או משתני סביבה.
- אם הפרויקט עולה ל-Git ציבורי, חשוב להחליף סודות (JWT/SMTP) שכבר נחשפו.

---
t
