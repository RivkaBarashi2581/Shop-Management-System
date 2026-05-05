

namespace DO;
[Serializable]
public class DalIsExistException:Exception
{
    public  DalIsExistException(string massage):base(massage){ }

    //public DalIsExistException(string massage, Exception innerException) : base(massage) { }
}
[Serializable]
public class DalIsNotExistException : Exception
{
    public DalIsNotExistException(string massage) : base(massage) { }

    //public DalIsNotExistException(string massage,Exception innerException) : base(massage) { }
}