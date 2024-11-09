namespace api.Common.DTO;

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
    
}