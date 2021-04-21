using System.Text.RegularExpressions;

namespace ALittle
{
	public class AProtobufRegexElement : ABnfRegexElement
	{
		public AProtobufRegexElement(ABnfFactory factory, ABnfFile file, int line, int col, int offset, string type, Regex regex)
            : base(factory, file, line, col, offset, type, regex)
        {
        }
	}
}