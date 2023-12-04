using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEFTCreator
{
    internal class Tags
    {

        // tags.
        public string AddTags(string modelText, string pathText, string modelNumText, string tagType)
        {
            string tags;

            if (tagType == "openTags") //openingTags
            {

                tags = "<?xml version=" + '\u0022' + "1.0" + '\u0022' + " encoding=" + '\u0022' + "utf-8" + '\u0022' + "?>\r\n";
                tags += "<FingerprintTemplateMappings>\r\n";
                tags += "  " + "<Version>1</Version>\r\n";
                tags += "  " + "<Mappings>";

            }
            else if (tagType == "mappingTags")//mapping tags
            {

                tags = "    " + "<Mapping>\r\n";
                tags += "      " + "<Path>" + modelText + "/" + pathText + "</Path>\r\n";
                tags += "      " + "<File>" + modelNumText + "_Model.xml" + "</File>\r\n";
                tags += "    " + "</Mapping>";

            }
            else //closing tags
            {

                tags = "  " + "</Mappings>\r\n";
                tags += "</FingerprintTemplateMappings>";

            }

            return tags;
        }
    }
}
