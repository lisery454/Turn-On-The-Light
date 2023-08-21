using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moss
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 返回<paramref name="self"/>类型在所有汇编集中的子类型的集合
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isExported"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetSubTypesInAssemblies(this Type self, bool isExported = false)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.StartsWith("Assembly"))
                .SelectMany(assembly => !isExported ? assembly.GetTypes() : assembly.GetExportedTypes())
                .Where(type => type.IsSubclassOf(self));
        }

        /// <summary>
        /// 返回<paramref name="self"/>类型在所有汇编集中的并且有<typeparamref name="TClassAttribute"/>特性的子类型的集合
        /// </summary>
        /// <param name="self">父类型</param>
        /// <param name="isExported"></param>
        /// <typeparam name="TClassAttribute">类特性</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetSubTypesWithClassAttributeInAssemblies<TClassAttribute>(this Type self,
            bool isExported = false)
            where TClassAttribute : Attribute
        {
            return self.GetSubTypesInAssemblies(isExported)
                .Where(type => type.GetCustomAttribute<TClassAttribute>() != null);
        }

        /// <summary>
        /// 返回实现了<paramref name="self"/>接口类型的在所有汇编集中的子类型的集合
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isExported"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAssignableTypesInAssemblies(this Type self, bool isExported = false)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.StartsWith("Assembly"))
                .SelectMany(assembly => !isExported ? assembly.GetTypes() : assembly.GetExportedTypes())
                .Where(self.IsAssignableFrom)
                .Where(type => type != self);
        }

        /// <summary>
        /// 返回实现了<paramref name="self"/>接口类型的在所有汇编集中的并且有<typeparamref name="TClassAttribute"/>特性的子类型的集合
        /// </summary>
        /// <param name="self">父类型</param>
        /// <param name="isExported"></param>
        /// <typeparam name="TClassAttribute">类特性</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAssignableTypesWithClassAttributeInAssemblies<TClassAttribute>(
            this Type self,
            bool isExported = false)
            where TClassAttribute : Attribute
        {
            return self.GetAssignableTypesInAssemblies(isExported)
                .Where(type => type.GetCustomAttribute<TClassAttribute>() != null);
        }


        public static void GetFieldInfosForEach(this Type self, Action<FieldInfo> action,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
        {
            var fields = self.GetFields(bindingFlags);
            foreach (var field in fields)
            {
                action?.Invoke(field);
            }
        }

        public static void GetPropertyInfosForEach(this Type self, Action<PropertyInfo> action,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
        {
            var properties = self.GetProperties(bindingFlags);
            foreach (var property in properties)
            {
                action?.Invoke(property);
            }
        }

        public static void GetMethodInfosForEach(this Type self, Action<MethodInfo> action,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
        {
            var methodInfos = self.GetMethods(bindingFlags);
            foreach (var methodInfo in methodInfos)
            {
                action?.Invoke(methodInfo);
            }
        }

        public static bool IsAssignableTo(this Type self, Type other)
        {
            return other.IsAssignableFrom(self);
        }
    }
}