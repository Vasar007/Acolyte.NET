using Acolyte.PlatformSpecifc;

namespace Acolyte.Tests.Internal
{
    public static class TestDotNetPlatformHelper
    {
        public static bool IsNetFramework()
        {
            DotNetPlatform dotNetPlatform = GetPlatform();
            return dotNetPlatform is >= DotNetPlatform.NetFramework11 and <= DotNetPlatform.NetFramework;
        }

        public static bool IsNetStandard()
        {
            DotNetPlatform dotNetPlatform = GetPlatform();
            return dotNetPlatform is >= DotNetPlatform.NetStandard10 and <= DotNetPlatform.NetStandard;
        }

        public static bool IsNetCore()
        {
            DotNetPlatform dotNetPlatform = GetPlatform();
            return dotNetPlatform is >= DotNetPlatform.NetCore10 and <= DotNetPlatform.NetCore;
        }

        public static bool IsNetX()
        {
            DotNetPlatform dotNetPlatform = GetPlatform();
            return dotNetPlatform is >= DotNetPlatform.Net5 and <= DotNetPlatform.NetX;
        }

        public static DotNetPlatform GetPlatform()
        {
#if NETFRAMEWORK1_1
            return DotNetPlatform.NetFramework11;
#elif NETFRAMEWORK2_0
            return DotNetPlatform.NetFramework20;
#elif NETFRAMEWORK3_5
            return DotNetPlatform.NetFramework35;
#elif NETFRAMEWORK4_0
            return DotNetPlatform.NetFramework40;
#elif NETFRAMEWORK4_0_3
            return DotNetPlatform.NetFramework403;
#elif NETFRAMEWORK4_5
            return DotNetPlatform.NetFramework45;
#elif NETFRAMEWORK4_5_1
            return DotNetPlatform.NetFramework451;
#elif NETFRAMEWORK4_5_2
            return DotNetPlatform.NetFramework452;
#elif NETFRAMEWORK4_6
            return DotNetPlatform.NetFramework46;
#elif NETFRAMEWORK4_6_1
            return DotNetPlatform.NetFramework461;
#elif NETFRAMEWORK4_6_2
            return DotNetPlatform.NetFramework462;
#elif NETFRAMEWORK4_7
            return DotNetPlatform.NetFramework47;
#elif NETFRAMEWORK4_7_1
            return DotNetPlatform.NetFramework471;
#elif NETFRAMEWORK4_7_2
            return DotNetPlatform.NetFramework472;
#elif NETFRAMEWORK4_8
            return DotNetPlatform.NetFramework48;
#elif NETFRAMEWORK
            return DotNetPlatform.NetFramework;

#elif NETSTANDARD1_0
            return DotNetPlatform.NetStandard10;
#elif NETSTANDARD1_1
            return DotNetPlatform.NetStandard11;
#elif NETSTANDARD1_2
            return DotNetPlatform.NetStandard12;
#elif NETSTANDARD1_3
            return DotNetPlatform.NetStandard13;
#elif NETSTANDARD1_4
            return DotNetPlatform.NetStandard14;
#elif NETSTANDARD1_5
            return DotNetPlatform.NetStandard15;
#elif NETSTANDARD1_6
            return DotNetPlatform.NetStandard16;
#elif NETSTANDARD2_0
            return DotNetPlatform.NetStandard20;
#elif NETSTANDARD2_1
            return DotNetPlatform.NetStandard21;
#elif NETSTANDARD
            return DotNetPlatform.NetStandard;

#elif NETCORE1_0
            return DotNetPlatform.NetCore10;
#elif NETCORE1_1
            return DotNetPlatform.NetCore11;
#elif NETCORE2_0
            return DotNetPlatform.NetCore20;
#elif NETCORE2_1
            return DotNetPlatform.NetCore21;
#elif NETCORE2_2
            return DotNetPlatform.NetCore22;
#elif NETCORE3_0
            return DotNetPlatform.NetCore30;
#elif NETCORE3_1
            return DotNetPlatform.NetCore31;
#elif NETCORE
            return DotNetPlatform.NetCore;

#elif NET5
            return DotNetPlatform.Net5;
#elif NET6
            return DotNetPlatform.Net6;
#elif NETX
            return DotNetPlatform.NetX;

#else
            return DotNetPlatform.Unknown;
#endif
        }
    }
}
