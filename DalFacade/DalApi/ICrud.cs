using DO;
namespace DalFacade.DalApi
{
    /// הוספת המילה pucic לפני interface והוספת T כדי שיהיה גנרי
    public interface ICrud<T>
    {
        /// <summary>
        /// פונקציה ליצירת אוביקט
        /// </summary>
        /// <param name="item">מקבלת פרטי המוצר</param>
        /// <returns>מחזירה את קוד המוצר שנוצר</returns>
        int Create(T item);

        /// <summary>
        ///     פונקציה שמחזירה מוצר לפי המזהה שקבלה
        /// </summary>
        /// <param name="id">מזהה המוצר הרצוי</param>
        /// <returns>מחזירה את המוצר המבוקש</returns>
        T? Read(int id);
        T? Read(Func<T, bool> filter);
        /// <summary>
        /// פונקציה שמחזירה רשימה של כל המוצרים
        /// </summary>
        /// <returns>רשימת המוצרים</returns>
        //IEnumerable<T?> ReadAll();
        List<T?> ReadAll(Func<T, bool>? filter = null);
        /// <summary>
        ///פונקציה שמעדכנת נתוני מוצר 
        /// </summary>
        /// <param name="item">מקבלת פרטי מוצר</param>
        /// 
        void Update(T item);
        /// <summary>
        /// מוחקת מוצר לפי המזהה שקבלה
        /// </summary>
        /// <param name="id">מקבלת את מזהה המוצר הרצוי למחיקה</param>
        void Delete(int id);
    }
}
