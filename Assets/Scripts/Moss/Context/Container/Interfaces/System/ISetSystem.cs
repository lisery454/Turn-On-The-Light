namespace Moss
{
    public interface ISetSystem
    {
        void SetSystem<TA, TB>(string id = null) where TB : TA where TA : ISystem;
    }
}