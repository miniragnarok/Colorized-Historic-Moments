using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ColorizedHistoricMoments
{
    class Program
    {
        private static readonly string modName = "LocateTextures";
        private static readonly string currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\");
        private static readonly string locateTexturesModInfo = "LocateTextures.modinfo";
        private static readonly string locateTexturesSql = "locateTextures.sql";
        private static readonly string civ6ModFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Path.Combine(@"my games\Sid Meier's Civilization VI\Mods", modName));

        static void Main()
        {
            PublishFinishedMod();
        }

        private static void PublishFinishedMod()
        {
            var modInfoSource = Path.Combine(currentDirectory, locateTexturesModInfo);
            var modInfoDestination = Path.Combine(civ6ModFolderPath, locateTexturesModInfo);

            File.Copy(modInfoSource, modInfoDestination);

            var sqlSource = Path.Combine(currentDirectory, locateTexturesSql);
            var sqlDestination = Path.Combine(civ6ModFolderPath, locateTexturesSql);

            File.Copy(sqlSource, sqlDestination);
        }
    }
}
