namespace domain.Dto;
public class DataPointDto
{
    public bool state { set; get; } = false;
    public object value { set; get; } = string.Empty;
    public string msg { set; get; } = string.Empty;

    public DataPointDto()
    {
        
    }

    public DataPointDto(bool state, object value, string msg)
    {
        this.state = state;
        this.value = value;
        this.msg = msg;
    }

    public static DataPointDto ok(object value)
    {
        return new DataPointDto(true, value, string.Empty);
    }
    public static DataPointDto failed(string msg)
    {
        return new DataPointDto(false, string.Empty, msg);
    }
    
    
}