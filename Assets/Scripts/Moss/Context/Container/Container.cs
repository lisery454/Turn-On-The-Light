namespace Moss
{
    public partial class Container : IGetAble, ISetAble
    {
        public readonly Injector injector;

        public Container()
        {
            injector = new Injector(this);
            _InitSystemPart();
            _InitServicePart();
            _InitStatePart();
            _InitCoStatePart();
        }
    }
}