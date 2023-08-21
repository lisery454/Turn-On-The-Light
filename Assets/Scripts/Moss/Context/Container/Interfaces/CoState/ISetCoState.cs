namespace Moss
{
    public interface ISetCoState
    {
        void SetCoState<TA, TB>(string id = null) where TB : TA where TA : ICoState;
    }
}