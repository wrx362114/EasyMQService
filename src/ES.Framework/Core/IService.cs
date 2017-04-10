namespace ES.Framework.Core
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public interface IService : ISingleton
    {
        bool Start();
        bool Stop();

    }
}
