using UnityEngine;

namespace Moss
{
    public static class InjectRules
    {
        public static T GetCoState<T>(this MonoBehaviour self, string id = null) where T : ICoState
        {
            return Game.Instance.Context.Container.GetCoState<T>(self, id);
        }

        public static void DestroyCoState<T>(this MonoBehaviour self, string id = null) where T : ICoState
        {
            Game.Instance.Context.Container.DestroyCoState<T>(self, id);
        }

        public static T GetCoState<T>(this ICommand self, object obj, string id = null) where T : ICoState
        {
            return Game.Instance.Context.Container.GetCoState<T>(obj, id);
        }

        public static T GetCoState<T>(this IService self, object obj, string id = null) where T : ICoState
        {
            return Game.Instance.Context.Container.GetCoState<T>(obj, id);
        }


        public static T GetState<T>(this IService self, string id = null) where T : IState
        {
            return Game.Instance.Context.Container.GetState<T>(id);
        }

        public static T GetState<T>(this MonoBehaviour self, string id = null) where T : IState
        {
            return Game.Instance.Context.Container.GetState<T>(id);
        }

        public static T GetState<T>(this ICommand self, string id = null) where T : IState
        {
            return Game.Instance.Context.Container.GetState<T>(id);
        }

        public static T GetService<T>(this ICommand self, string id = null) where T : IService
        {
            return Game.Instance.Context.Container.GetService<T>(id);
        }
    }
}