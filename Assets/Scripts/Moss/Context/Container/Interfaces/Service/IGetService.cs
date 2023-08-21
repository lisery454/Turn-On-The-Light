namespace Moss
{
    public interface IGetService
    {
        public T GetService<T>(string id = null) where T : IService;
    }
}