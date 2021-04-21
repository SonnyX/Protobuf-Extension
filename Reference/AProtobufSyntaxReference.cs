
namespace ALittle
{
    public class AProtobufSyntaxReference : AProtobufReferenceTemplate<AProtobufSyntaxElement>
    {
        public AProtobufSyntaxReference(ABnfElement element) : base(element) { }

        public override ABnfGuessError CheckError()
        {
            // 检查最后的分号
            if (m_element.GetStringList().Count < 2)
                return new ABnfGuessError(m_element, "The syntax statement must end with a line terminator (;)");

            var child = m_element.GetText();
            if (child == null) return null;

            var value = child.GetElementString();
            if (value == "proto2" || value == "proto3")
                return null;

            return new ABnfGuessError(child, "Only proto2 or proto3 can be filled here ");
        }
    }
}

