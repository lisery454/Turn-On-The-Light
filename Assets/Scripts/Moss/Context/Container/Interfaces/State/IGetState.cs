namespace Moss
{
    public interface IGetState
    {
        public T GetState<T>(string id = null) where T : IState;
    }
}