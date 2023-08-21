using System;

namespace Moss
{
    [Flags]
    public enum InjectFlag
    {
        Service = 1,
        State = 2,
        System = 4,
    }
}