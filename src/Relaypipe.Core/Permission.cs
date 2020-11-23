using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Core
{
    public enum Permission
    {
        READ,
        READWRITE,
        EXECUTE,
        READEXECUTE,
        READWRITEEXECUTE
    }

    public static class PermissionExtensions
    {
        public static string ToPermissionString(this Permission p)
        {
            switch (p)
            {
                case Permission.READ:
                    return "r";
                case Permission.READWRITE:
                    return "rw";
                case Permission.EXECUTE:
                    return "x";
                case Permission.READEXECUTE:
                    return "rx";
                case Permission.READWRITEEXECUTE:
                    return "rwx";
            }

            return null;
        }
    }
}
