namespace BO;

//לא קיים
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}
//חריגה מהמלאי
[Serializable]
public class BlOutOfStockException : Exception
{
    public BlOutOfStockException(string? message) : base(message) { }
    public BlOutOfStockException(string message, Exception innerException)
                : base(message, innerException) { }
}

//אובייקט כבר קיים
[Serializable]
public class BlIdAlreadyExistsException : Exception
{
    public BlIdAlreadyExistsException(string message) : base(message) { }
    public BlIdAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}
//לא נמצא
[Serializable]
public class BlNotFoundException : Exception
{
    public BlNotFoundException(string message) : base(message) { }
    public BlNotFoundException(string message, Exception innerException)
               : base(message, innerException) { }
}

