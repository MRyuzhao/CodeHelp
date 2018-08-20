using System.Collections.Generic;

namespace CodeHelp.Common.CodeModels
{
    public class CodeModel
    {
        public IList<string> Columns { get; set; }
        public string TableName { get; set; }

        public CodeModel()
        {
            Columns = new List<string>();
        }
    }
}