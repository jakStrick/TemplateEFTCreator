using System.Xml.Linq;

namespace TemplateEFTCreator
{
    public class Model
    {
        public XElement ModelList { get; set; }

        public void SetModelList()
        {
            FileManager fileManager = new FileManager();

            string fileName = fileManager.SetFileName();

            ModelList = fileManager.ReadFromFile(fileName);
        }
    }
}