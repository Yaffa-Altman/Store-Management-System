namespace DO;
[Serializable]
public class DalIdDosentExistException : Exception
{
	public DalIdDosentExistException(String message):base(message)
	{
		
	}
}

[Serializable]
public class DalIdAlreadyExistsException : Exception
{
	public DalIdAlreadyExistsException(String message) : base(message)
    {

	}
}

[Serializable]
public class DalNotFoundException : Exception
{
    public DalNotFoundException(String message) : base(message)
    {

    }
}

