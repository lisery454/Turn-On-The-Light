namespace Moss
{
    public interface IGetSystem
    {
        public T GetSystem<T>(string id = null) where T : ISystem;
    }
}