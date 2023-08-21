using System;

namespace Moss
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class WithIdAttribute : Attribute
    {
        public string id = null;
    }
}