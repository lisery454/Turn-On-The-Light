using System;
using System.Collections.Generic;
using System.Reflection;

namespace Moss
{
    public class Injector
    {
        private readonly Container _container;
        private readonly Dictionary<Type, string> _typeMethodsDic;
        private readonly Dictionary<Type, InjectFlag> _typeFlagsDic;


        public Injector(Container container)
        {
            _container = container;
            _typeMethodsDic = new Dictionary<Type, string>
            {
                [typeof(ISystem)] = "GetSystem",
                [typeof(IService)] = "GetService",
                [typeof(IState)] = "GetState",
            };
            _typeFlagsDic = new Dictionary<Type, InjectFlag>
            {
                [typeof(ISystem)] = InjectFlag.System,
                [typeof(IService)] = InjectFlag.Service,
                [typeof(IState)] = InjectFlag.State,
            };
        }

        private bool _IsFlag(Type type, InjectFlag injectFlag)
        {
            return (injectFlag & _typeFlagsDic[type]) != 0;
        }

        public void InjectPrivateState(object obj)
        {
            obj.GetType().GetFieldInfosForEach(info =>
            {
                var newInjectAttribute = info.GetCustomAttribute<NewInjectAttribute>();
                if (newInjectAttribute == null) return;

                if (info.FieldType.IsAssignableTo(typeof(IState)))
                {
                    var methodInfo = _container.GetType().GetMethod("GetPrivateState");
                    var makeGenericMethod = methodInfo!.MakeGenericMethod(info.FieldType);
                    var bindingObj = makeGenericMethod.Invoke(_container, new[] { obj, newInjectAttribute.id });
                    info.SetValue(obj, bindingObj);
                }
            });

            obj.GetType().GetPropertyInfosForEach(info =>
            {
                var newInjectAttribute = info.GetCustomAttribute<NewInjectAttribute>();
                if (newInjectAttribute == null) return;

                if (info.PropertyType.IsAssignableTo(typeof(IState)))
                {
                    var methodInfo = _container.GetType().GetMethod("GetPrivateState");
                    var makeGenericMethod = methodInfo!.MakeGenericMethod(info.PropertyType);
                    var bindingObj = makeGenericMethod.Invoke(_container, new[] { obj, newInjectAttribute.id });
                    info.SetValue(obj, bindingObj);
                }
            });

            obj.GetType().GetMethodInfosForEach(info =>
            {
                var newInjectAttribute = info.GetCustomAttribute<NewInjectAttribute>();
                if (newInjectAttribute == null) return;
                var parameterList = new List<object>();

                info.GetParameters().Foreach(parameterInfo =>
                {
                    var id = parameterInfo.GetCustomAttribute<WithIdAttribute>()?.id;

                    if (parameterInfo.ParameterType.IsAssignableTo(typeof(IState)))
                    {
                        var methodInfo = _container.GetType().GetMethod("GetPrivateState");
                        var makeGenericMethod = methodInfo!.MakeGenericMethod(parameterInfo.ParameterType);
                        var bindingObj = makeGenericMethod.Invoke(_container, new[] { obj, id });
                        parameterList.Add(bindingObj);
                    }
                });
                info.Invoke(obj, parameterList.ToArray());
            });
        }


        public void Inject(object obj,
            InjectFlag injectFlag = InjectFlag.Service | InjectFlag.State | InjectFlag.System)
        {
            if (obj == null) return;
            _FieldInject(obj, injectFlag);
            _PropertyInject(obj, injectFlag);
            _MethodInject(obj, injectFlag);
        }

        private void _FieldInject(object obj,
            InjectFlag injectFlag = InjectFlag.Service | InjectFlag.State | InjectFlag.System)
        {
            obj.GetType().GetFieldInfosForEach(info =>
            {
                var injectAttribute = info.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute == null) return;
                _typeMethodsDic.Foreach((type, s) =>
                {
                    if (_IsFlag(type, injectFlag) && info.FieldType.IsAssignableTo(type))
                    {
                        info.SetValue(obj, _GetBindingObj(s, info.FieldType, injectAttribute.id));
                    }
                });
            });
        }

        private void _PropertyInject(object obj,
            InjectFlag injectFlag = InjectFlag.Service | InjectFlag.State | InjectFlag.System)
        {
            obj.GetType().GetPropertyInfosForEach(info =>
            {
                var injectAttribute = info.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute == null) return;
                _typeMethodsDic.Foreach((type, s) =>
                {
                    if (_IsFlag(type, injectFlag) && info.PropertyType.IsAssignableTo(type))
                    {
                        info.SetValue(obj, _GetBindingObj(s, info.PropertyType, injectAttribute.id));
                    }
                });
            });
        }

        private void _MethodInject(object obj,
            InjectFlag injectFlag = InjectFlag.Service | InjectFlag.State | InjectFlag.System)
        {
            obj.GetType().GetMethodInfosForEach(info =>
            {
                var injectAttribute = info.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute == null) return;
                var parameterList = new List<object>();

                info.GetParameters().Foreach(parameterInfo =>
                {
                    var id = parameterInfo.GetCustomAttribute<WithIdAttribute>()?.id;
                    _typeMethodsDic.Foreach((type, s) =>
                    {
                        if (_IsFlag(type, injectFlag) && parameterInfo.ParameterType.IsAssignableTo(type))
                        {
                            parameterList.Add(_GetBindingObj(s, parameterInfo.ParameterType, id));
                        }
                    });
                });
                info.Invoke(obj, parameterList.ToArray());
            });
        }

        private object _GetBindingObj(string methodName, Type type, string id = null)
        {
            var methodInfo = _container.GetType().GetMethod(methodName);
            var makeGenericMethod = methodInfo!.MakeGenericMethod(type);
            var bindingObj = makeGenericMethod.Invoke(_container, new object[] { id });
            return bindingObj;
        }
    }
}