using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ColorizedHistoricMoments
{
    class Program
    {
        private static readonly string modName = "ColorizedHistoricMoments";
        private static readonly string currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\");
        private static readonly string finishedImagesFolder = Path.Combine(currentDirectory, "Textures");
        private static readonly string colorizedHistoricMomentsModInfo = "ColorizedHistoricMoments.modinfo";
        private static readonly string imageFileNamePrepend = "CHM_";
        private static readonly string imageFileExtension = ".dds";
        private static readonly string civ6ModFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Path.Combine(@"my games\Sid Meier's Civilization VI\Mods", modName));
        private static List<string> fileDirectories = new List<string>
            {
                "SQL",
                "Textures"
            };

    static void Main()
        {
            RenameImageFiles();
            TransformModInfo();
            PublishFinishedMod();
        }

        private static void TransformModInfo()
        {
            var xlpFilepath = Path.Combine(currentDirectory, colorizedHistoricMomentsModInfo);

            var xlp = XElement.Load(xlpFilepath);

            var chmTextures = xlp.Descendants("InGameActions")
                .SingleOrDefault()
                .Descendants("ImportFiles")
                .Where(m => m.Attribute("id").Value == "CHM_Textures")
                .SingleOrDefault();
            chmTextures.Descendants("File").Remove();

            var entries = xlp.Descendants("Files").SingleOrDefault();
            entries.Elements().Remove();

            var pathDelimiter = '/';

            foreach (var directoryName in fileDirectories)
            {
                foreach(var file in Directory.EnumerateFiles(Path.Combine(currentDirectory, directoryName)))
                {
                    var fileElement = new XElement("File");
                    fileElement.Add(directoryName + pathDelimiter + Path.GetFileName(file));
                    chmTextures.Add(fileElement);
                    entries.Add(fileElement);
                }
            }

            xlp.Save(xlpFilepath);
        }

        private static void RenameImageFiles()
        {
            foreach (var file in Directory.EnumerateFiles(finishedImagesFolder, "*" + imageFileExtension))
            {
                var currentFileName = Path.GetFileNameWithoutExtension(file);
                if (!currentFileName.StartsWith(imageFileNamePrepend))
                {
                    var newFileName = currentFileName;
                    newFileName = newFileName.Trim().Replace(' ', '_');

                    var regex = new Regex("[^a-zA-Z0-9_]");
                    newFileName = regex.Replace(newFileName, string.Empty);

                    newFileName = imageFileNamePrepend + newFileName;

                    var finalLocation = Path.Combine(finishedImagesFolder, newFileName + imageFileExtension);
                    if (File.Exists(finalLocation))
                    {
                        File.Delete(finalLocation);
                    }

                    File.Move(Path.Combine(finishedImagesFolder, currentFileName + imageFileExtension), Path.Combine(finishedImagesFolder, newFileName + imageFileExtension));
                }
            }
        }

        private static void PublishFinishedMod()
        {
            var modInfoSource = Path.Combine(currentDirectory, colorizedHistoricMomentsModInfo);
            var modInfoDestination = Path.Combine(civ6ModFolderPath, colorizedHistoricMomentsModInfo);

            File.Copy(modInfoSource, modInfoDestination);

            foreach (var directoryName in fileDirectories)
            {
                var source = Path.Combine(currentDirectory, directoryName);
                var destination = Path.Combine(civ6ModFolderPath, directoryName);
                CopyDirectory.Copy(source, destination);
            }
        }
    }
}
