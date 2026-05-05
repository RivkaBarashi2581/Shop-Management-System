using System.Xml.Linq;

namespace DalXml
{
    internal static class Config
    {
        //יצירת קובץ xml בשם data-config.xml בתיקיית xml שבתיקיית הבינארי של הפרויקט,
        //עם הערכים הראשוניים של מזהה המוצר ומזהה המכירה
        private static readonly string path =  Path.Combine(AppContext.BaseDirectory, "xml", "data-config.xml");
        const string PRODUCTID = "productId";
        const string SALEID = "saleId";
        //טעינת הקובץ xml לתוך משתנה סטטי מסוג XElement
        static XElement dataConfigXml = LoadConfig();

        private static XElement LoadConfig()
        {
            //בדיקה אם הקובץ קיים, אם לא קיים זריקת שגיאה עם הודעה מתאימה
            if (!File.Exists(path))
                throw new FileNotFoundException($"data-config.xml file was not found at '{path}'", path);

            return XElement.Load(path);
        }

        private static int ProductId = int.Parse(dataConfigXml.Element(PRODUCTID)!.Value);

        public static int GetProductId
        {
            //הגדלת מזהה המוצר ב-1,
            //עדכון הערך בקובץ ה-xml ושמירת הקובץ, והחזרת הערך החדש של מזהה המוצר

            get
            {
                ProductId++;
                dataConfigXml.Element(PRODUCTID)!.SetValue(ProductId.ToString());
                dataConfigXml.Save(path);
                return ProductId;
            }
        }
        
        private static int SaleId = int.Parse(dataConfigXml.Element(SALEID)!.Value);

        public static int GetSaleId
        {
            //הגדלת מזהה המכירה ב-1,
            get
            {
                SaleId++;
                //עדכון הערך בקובץ ה-xml ושמירת הקובץ, והחזרת הערך החדש של מזהה המכירה
                dataConfigXml.Element(SALEID)!.SetValue(SaleId.ToString());
                dataConfigXml.Save(path);
                return SaleId;
            }
        }
    }
}
