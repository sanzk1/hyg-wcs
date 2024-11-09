namespace infrastructure.Exceptions;

public class DeviceException: ApplicationException
{
    public int code { set; get; }
    public string message { set; get; }
    public DeviceException(string message)
    {
        this.code = 500;
        this.message = message;
    }
    public DeviceException(int code, string message)
    {
        this.code = code;
        this.message = message;
    }

    
}