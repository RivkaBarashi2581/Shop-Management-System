


namespace Tools
{
    public class LogManager
    {
        // נתיב לתיקיית הלוגים
        static string LogDirPath = "Log";


        // נתיב לקובץ הלוג הראשי (לא בשימוש ישירות, רק לדוגמה)
        static string LogFilePath = $"{LogDirPath}\\LogFile.txt";

        public static string getDirPartYear()
        {
            return LogDirPath + "/" + DateTime.Now.Year.ToString();
        }

        public static string getDirPartMonth()
        {
            return getDirPartYear() + "/" + DateTime.Now.Month.ToString();

        }
        public static string getFilePath()
        {
            return getDirPartMonth() + "/" + DateTime.Now.Day.ToString() + ".txt";
        }

        public static void WriteToLog(string message, string nameProject, string nameFanction)
        {
            /// יצירת התיקיות והקובץ אם הם לא קיימים
            string logFilePath = getFilePath();
            string LogDirPath=Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(LogDirPath))
            {
                Directory.CreateDirectory(LogDirPath);
            }

            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}\t{nameProject}.{nameFanction}:\t{message}");
            }


        }


        public static void DeleteInLog()
        {
            if (!Directory.Exists(LogDirPath))
            {
                Console.WriteLine("Log Directory not exist ");
                return;
            }
            // שמירת הזמנים הנוכחים במשתנים
            DateTime NowDate= DateTime.Now;
            int thisYear= NowDate.Year;
            int thisMonth= NowDate.Month;

                //מעבר על כל תיקיות הלוג 
                foreach (string yearDirectory in Directory.GetDirectories(LogDirPath))
                {
                // שמירת השנה במשתנה סטרינג
                    string yearName = Path.GetFileName(yearDirectory);
                // אם מצליח להשתנה לINT
                    if (int.TryParse(yearName, out int year))
                    {
                    // מעבר על החודשים
                        foreach (string monthDirectory in Directory.GetDirectories(yearDirectory))
                        {
                        // שמירת החודש במשתנה
                            string monthName = Path.GetFileName(monthDirectory);
                        // אם מצליח להמיר את החודש לINT
                            if (int.TryParse(monthName, out int month))
                            {
                                // שליחה לפונקציה שבודקת האם הוא בחודשיים האחרונים
                                if (IsInLastTwoMonths(thisYear, thisMonth, year, month))
                                {
                                // מדפיס שלשמור את חודש זה
                                    Console.WriteLine($"Keeping directory: {monthDirectory}");
                                // עוצר את האיטרציה הנוכחית
                                    continue;
                                }
                                // אם הוא לא מהחודשיים האחרונים אז תמחוק את תיקיה זאת
                                try
                                {
                                    Directory.Delete(monthDirectory, true);
                                    Console.WriteLine($"Deleted old log directory: {monthDirectory}");
                                }
                            // זריקת חריגה
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Failed to delete directory {monthDirectory}: {ex.Message}");
                                }
                            }
                        }


                    }
                }

                 static bool IsInLastTwoMonths(int thisYear, int thisMonth, int year, int month)
                {
                // בדיקה חשבונים האם הוא בחודשיים האחרונים
                    int monthDifference = (thisYear - year) * 12 + (thisMonth - month);
                // אםם הכמות חודשים קטנה מ2 וגם גדולה מאפס תחזיר TRUE
                    return monthDifference <= 2 && monthDifference >= 0;
                }
            

        }

    }
}
