using System;

namespace Acolyte.PlatformSpecifc
{
    [Flags]
    public enum DotNetPlatform : ulong
    {
        Unknown = 0,

        NetFramework11 = 1,
        NetFramework20 = NetFramework11 << 1,
        NetFramework35 = NetFramework20 << 1,
        NetFramework40 = NetFramework35 << 1,
        NetFramework403 = NetFramework40 << 1,
        NetFramework45 = NetFramework403 << 1,
        NetFramework451 = NetFramework45 << 1,
        NetFramework452 = NetFramework451 << 1,
        NetFramework46 = NetFramework452 << 1,
        NetFramework461 = NetFramework46 << 1,
        NetFramework462 = NetFramework461 << 1,
        NetFramework47 = NetFramework462 << 1,
        NetFramework471 = NetFramework47 << 1,
        NetFramework472 = NetFramework471 << 1,
        NetFramework48 = NetFramework472 << 1,
        NetFramework = NetFramework11 |
                          NetFramework20 |
                          NetFramework35 |
                          NetFramework40 | NetFramework403 |
                          NetFramework45 | NetFramework451 | NetFramework452 |
                          NetFramework46 | NetFramework461 | NetFramework462 |
                          NetFramework47 | NetFramework471 | NetFramework472 |
                          NetFramework48,

        NetStandard10 = NetFramework48 << 1,
        NetStandard11 = NetStandard10 << 1,
        NetStandard12 = NetStandard11 << 1,
        NetStandard13 = NetStandard12 << 1,
        NetStandard14 = NetStandard13 << 1,
        NetStandard15 = NetStandard14 << 1,
        NetStandard16 = NetStandard15 << 1,
        NetStandard20 = NetStandard16 << 1,
        NetStandard21 = NetStandard20 << 1,
        NetStandard = NetStandard10 | NetStandard11 | NetStandard12 | NetStandard13 | NetStandard14 | NetStandard15 | NetStandard16 |
                          NetStandard20 | NetStandard21,

        NetCore10 = NetStandard21 << 1,
        NetCore11 = NetCore10 << 1,
        NetCore20 = NetCore11 << 1,
        NetCore21 = NetCore20 << 1,
        NetCore22 = NetCore21 << 1,
        NetCore30 = NetCore22 << 1,
        NetCore31 = NetCore30 << 1,
        NetCore = NetCore10 | NetCore11 |
                          NetCore20 | NetCore21 | NetCore22 |
                          NetCore30 | NetCore31,

        Net5 = NetCore31 << 1,
        Net6 = Net5 << 1,
        NetX = Net5 |
                          Net6,
    }
}
