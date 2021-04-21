
namespace ALittle
{
    public class AProtobufServiceRpcReference : AProtobufReferenceTemplate<AProtobufServiceRpcElement>
    {
        public AProtobufServiceRpcReference(ABnfElement element) : base(element) { }

        public override int GetDesiredIndentation(int offset, ABnfElement select)
        {
			ABnfElement parent = m_element.GetParent();
            if (parent is AProtobufServiceBodyElement)
                return parent.GetReference().GetDesiredIndentation(offset, null);
            return 0;
        }

        public override int GetFormateIndentation(int offset, ABnfElement select)
        {
			ABnfElement parent = m_element.GetParent();
            if (parent is AProtobufServiceBodyElement)
                return parent.GetReference().GetFormateIndentation(offset, null);
            return 0;
        }

		public override ABnfGuessError CheckError()
		{
            if (m_element.GetServiceRpcName() == null)
                return new ABnfGuessError(m_element, "Rpc service name is not defined ");

            var childs = m_element.GetChilds();
            if (childs.Count == 0 || childs[childs.Count - 1].GetElementText() != ";")
                return new ABnfGuessError(m_element, "Rpc expression does not end with a line terminator (;)");
            return null;
		}
	}
}
