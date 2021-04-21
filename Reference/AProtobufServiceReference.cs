
namespace ALittle
{
    public class AProtobufServiceReference : AProtobufReferenceTemplate<AProtobufServiceElement>
    {
        public AProtobufServiceReference(ABnfElement element) : base(element) { }

		public override ABnfGuessError CheckError()
		{
            if (m_element.GetServiceName() == null)
                return new ABnfGuessError(m_element, "No service name defined ");

            if (m_element.GetServiceBody() == null)
                return new ABnfGuessError(m_element, "The content of the service is not defined ");
            return null;
		}
	}
}
