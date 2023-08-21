namespace Moss
{
    public interface ISystem : IUpdateable, IFixedUpdateAble, IAwakeAble, IStartAble, ILateUpdateAble, IOnDestroyAble
    {
    }

    public interface IUpdateable
    {
        void Update();
    }

    public interface IFixedUpdateAble
    {
        void FixedUpdate();
    }

    public interface IAwakeAble
    {
        void Awake();
    }

    public interface IStartAble
    {
        void Start();
    }

    public interface ILateUpdateAble
    {
        void LateUpdate();
    }

    public interface IOnDestroyAble
    {
        void OnDestroy();
    }
}