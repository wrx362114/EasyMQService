namespace ES.Framework.Core
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public interface IService : ISingleton
    {
        string Name { get; }
        bool Start();
        bool Stop();

    }
}
