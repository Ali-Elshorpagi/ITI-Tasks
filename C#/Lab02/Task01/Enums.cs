using System;
using System.Collections.Generic;
using System.Text;

namespace Task01
{
    public enum Gender : byte
    {
        M, F
    }

    [Flags]
    public enum SecurityLevel : byte
    {
        Guest = 1,
        Developer = 2,
        Secretary = 4,
        DBA = 8,
        FullPermissions = Guest | Developer | Secretary | DBA
    }
}
