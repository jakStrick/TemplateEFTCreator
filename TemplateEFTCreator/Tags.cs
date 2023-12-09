namespace TemplateEFTCreator
{
    internal class Tags
    {
        public string MappingTags { get; set; }

        // tags.
        public void AddTags(string modelText, string pathText, string modelNumText, string tagType)
        {
            if (tagType == "openTags") //openingTags
            {
                MappingTags = "<?xml version=" + '\u0022' + "1.0" + '\u0022' + " encoding=" + '\u0022' + "utf-8" + '\u0022' + "?>\r\n";
                MappingTags += "<FingerprintTemplateMappings>\r\n";
                MappingTags += "  " + "<Version>1</Version>\r\n";
                MappingTags += "  " + "<Mappings>";
            }
            else if (tagType == "mappingTags")//mapping tags
            {
                MappingTags = "    " + "<Mapping>\r\n";
                MappingTags += "      " + "<Path>" + modelText + "/" + pathText + "</Path>\r\n";
                MappingTags += "      " + "<File>" + modelNumText + "_Model.xml" + "</File>\r\n";
                MappingTags += "    " + "</Mapping>";
            }
            else //closing tags
            {
                MappingTags = "  " + "</Mappings>\r\n";
                MappingTags += "</FingerprintTemplateMappings>";
            }
        }
    }
}