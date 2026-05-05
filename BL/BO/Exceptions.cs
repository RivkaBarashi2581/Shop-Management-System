namespace BO;

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException) { }
}

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
        : base(message, innerException) { }
}

[Serializable]
public class BlInvalidInputException : Exception
{
    public BlInvalidInputException(string? message) : base(message) { }
    public BlInvalidInputException(string message, Exception innerException)
        : base(message, innerException) { }
}

// זה קוד של המורה

//[Serializable]
//public class BlDoesNotExistException : Exception
//{
//    public BlDoesNotExistException(string? message) : base(message) { }
//    public BlDoesNotExistException(string message, Exception innerException)
//                : base(message, innerException) { }
//}
