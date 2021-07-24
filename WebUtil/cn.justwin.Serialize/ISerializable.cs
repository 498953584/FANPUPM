namespace cn.justwin.Serialize
{
    using System;

    /// <summary>
    /// 序列号接口
    /// </summary>
    public interface ISerializable
    {
        T Deserialize<T>(string str) where T: class;
        string Serialize<T>(T t);
    }
}

