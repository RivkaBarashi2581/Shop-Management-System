namespace DalApi;
using static DalApi.DalConfig;
using System.Reflection;
using DalFacade.DalApi;

public static class Factory
{
    public static IDal Get
    {
        get
        {
            string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
            string dal = s_dalPackages[dalType] ?? throw new DalConfigException($"Package for {dalType} is not found in packages list in dal-config.xml");
            //טוען את ה-assembly של ה-DAL הנבחר
            try { Assembly.LoadFrom($"{dal}.dll"); }
            //אם יש בעיה בטעינת ה-assembly, תופס את החריגה ומזריק חריגה מותאמת עם מידע נוסף
            catch (Exception ex) { throw new DalConfigException($"Failed to load {dal}.dll package", ex); }
            //מנסה למצוא את המחלקה המתאימה ב-assembly שהוטען, ואם לא מוצא, זורק חריגה מותאמת עם מידע נוסף
            Type type = Type.GetType($"Dal.{dal}, {dal}") ??
                throw new DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");
            //מנסה לגשת לפרופרטי ה-Instance של המחלקה שנמצאה, ואם לא מצליח, זורק חריגה מותאמת עם מידע נוסף
            return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ??
                throw new DalConfigException($"Class {dal} is not a singleton or wrong property name for Instance");
        }
    }
}
