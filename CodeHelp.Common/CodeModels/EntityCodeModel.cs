using System.Text;
using CodeHelp.Common.CodeModels.CodeBirthType;

namespace CodeHelp.Common.CodeModels
{
    public class EntityCodeModel : BaseCodeModel<Entity>
    {
        public override string GetCodeBirthText(CodeModel codeModel)
        {
            var textBuilder = new StringBuilder();
            textBuilder.Append("public class ");
            textBuilder.Append(codeModel.TableName);
            textBuilder.Append(": Aggregate\n{");
            foreach (var codeModelColumn in codeModel.Columns)
            {
                textBuilder.Append("public string ");
                textBuilder.Append(codeModelColumn);
                textBuilder.Append(" { get; private set; }");
            }
            textBuilder.Append("}");

            return textBuilder.ToString();
        }
    }
}