namespace Moss
{
    public interface ISetState
    {
        void SetState<TA, TB>(string id = null) where TB : TA where TA : IState;
    }
}