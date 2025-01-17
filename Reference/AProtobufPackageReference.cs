﻿
namespace ALittle
{
    public class AProtobufPackageReference : AProtobufReferenceTemplate<AProtobufPackageElement>
    {
        public AProtobufPackageReference(ABnfElement element) : base(element) { }

        public override ABnfGuessError CheckError()
        {
            var childs = m_element.GetChilds();
            if (childs.Count == 0 || childs[childs.Count - 1].GetElementText() != ";")
                return new ABnfGuessError(m_element, "The package statement must end with a line terminator (;)");

            return null;
        }
    }
}
