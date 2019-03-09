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

        static void Main()
        {
            TransformXlp();
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
    }
}
