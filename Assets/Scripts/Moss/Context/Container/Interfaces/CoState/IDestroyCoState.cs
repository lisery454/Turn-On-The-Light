namespace Moss
{
    public interface IDestroyCoState
    {
        public void DestroyCoState<T>(object obj, string id = null) where T : ICoState;
    }
}