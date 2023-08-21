namespace Moss
{
    public interface IGetCoState
    {
        public T GetCoState<T>(object obj, string id = null) where T : ICoState;
    }
}