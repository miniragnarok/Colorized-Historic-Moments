using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ColorizedHistoricMoments
{
    class Program
    {
        private static readonly string currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\");
        private static readonly string colorizedHistoricMomentsXlp = "ColorizedHistoricMoments.xlp";
        private static readonly string finishedImagesFolder = Path.Combine(currentDirectory, "Textures");
        private static readonly string colorizedHistoricMomentsModInfo = "ColorizedHistoricMoments.modinfo";
        private static readonly string textureTemplateFileName = "TextureTemplate.tex";

        static void Main()
        {
            TransformXlp();
            TransformModInfo();
            CreateTextureFiles();
        }

        private static void TransformXlp()
        {
            var xlpFilepath = Path.Combine(currentDirectory, "XLPs", colorizedHistoricMomentsXlp);

            var xlp = XElement.Load(xlpFilepath);
            var entries = xlp.Descendants("m_Entries").SingleOrDefault();
            entries.Elements().Remove();

            var imageNames = GetFinishedImageNames();
            foreach(var image in imageNames)
            {
                var newElement = new XElement("Element");

                newElement.Add(new XElement(
                    "m_EntryID",
                    new XAttribute("text", image)
                ));

                newElement.Add(new XElement(
                    "m_ObjectName",
                    new XAttribute("text", image)
                ));

                entries.Add(newElement);
            }

            xlp.Save(xlpFilepath);
        }

        private static List<string> GetFinishedImageNames()
        {
            var imageNames = new List<string>();
            foreach(var file in Directory.EnumerateFiles(finishedImagesFolder, "*.dds"))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    imageNames.Add(fileName);
                }
            }

            return imageNames;
        }

        private static void TransformModInfo()
        {
            var xlpFilepath = Path.Combine(currentDirectory, colorizedHistoricMomentsModInfo);

            var xlp = XElement.Load(xlpFilepath);
            var entries = xlp.Descendants("Files").SingleOrDefault();
            entries.Elements()
                .Where(m => !m.Value.EndsWith(".dep")
                    && !m.Value.EndsWith(".blp")
                ).Remove();

            var pathDelimiter = '/';
            var fileDirectories = new List<string>
            {
                "ArtDefs",
                "SQL",
                "Textures",
                "XLPs"
            };

            foreach (var directoryName in fileDirectories)
            {
                foreach(var file in Directory.EnumerateFiles(Path.Combine(currentDirectory, directoryName)))
                {
                    var fileElement = new XElement("File");
                    fileElement.Add(directoryName + pathDelimiter + Path.GetFileName(file));
                    entries.Add(fileElement);
                }
            }

            xlp.Save(xlpFilepath);
        }

        private static void CreateTextureFiles()
        {
            foreach (var file in Directory.EnumerateFiles(finishedImagesFolder, "*.dds"))
            {
                var textureFileName = Path.GetFileNameWithoutExtension(file) + ".tex";
                if(!File.Exists(Path.Combine(finishedImagesFolder, textureFileName)))
                {
                    var templateFilePath = Path.Combine(currentDirectory, textureTemplateFileName);
                    var template = XElement.Load(templateFilePath);

                    var sourceFilePathTextAttribute = template.Descendants("m_SourceFilePath")
                        .SingleOrDefault()
                        .Attribute("text");
                    sourceFilePathTextAttribute.Value = sourceFilePathTextAttribute.Value + Path.GetFileName(file);

                    var dataFilesRelativePathTextAttribute = template.Descendants("m_DataFiles")
                        .SingleOrDefault()
                        .Descendants("m_RelativePath")
                        .SingleOrDefault()
                        .Attribute("text");
                    dataFilesRelativePathTextAttribute.Value = Path.GetFileName(file);

                    var nameTextAttribute = template.Descendants("m_Name")
                        .SingleOrDefault()
                        .Attribute("text");
                    nameTextAttribute.Value = Path.GetFileNameWithoutExtension(file);

                    template.Save(Path.Combine(finishedImagesFolder, textureFileName));
                }
            }
        }
    }
}
