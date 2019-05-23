﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MagicalLifeAPITest")]
[assembly: InternalsVisibleTo("MagicalLifeClientStandard")]
[assembly: InternalsVisibleTo("MagicalLifeDedicatedServerCore")]
[assembly: InternalsVisibleTo("MLGUIWindows")]
[assembly: InternalsVisibleTo("MagicalLifeModdingAPI")]
[assembly: InternalsVisibleTo("MagicalLifeServerStandard")]
[assembly: InternalsVisibleTo("MagicalLifeSettingsStandard")]
[assembly: InternalsVisibleTo("MonoGUI")]

namespace MagicalLifeAPI.Security
{
    /// <summary>
    /// This class determines who can access classes and objects marked with "internal".
    /// </summary>
    internal class FriendAssemblies
    {
    }
}