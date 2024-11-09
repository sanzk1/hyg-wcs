namespace dataPointsModule.Managers.Base;

public abstract class ManagerAbstract<T>
{
    /// <summary>
    /// 设备连接
    /// </summary>
    /// <param name="t"></param>
   public abstract void Connect(T t);
    
    /// <summary>
    /// 设备断开连接
    /// </summary>
    /// <param name="t"></param>
    public abstract void Disconnect(T t);

    /// <summary>
    /// 设备重连
    /// </summary>
    /// <param name="t"></param>
    public abstract  void Reconnect(T t);

    /// <summary>
    /// 获取设备
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public abstract object GetDevice(T t);

    
}