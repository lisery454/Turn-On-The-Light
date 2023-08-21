namespace Moss
{
    public interface ISetService
    {
        void SetService<TA, TB>(string id = null) where TB : TA where TA : IService;
    }
}