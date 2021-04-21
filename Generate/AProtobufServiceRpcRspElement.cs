namespace ALittle
{
	public class AProtobufServiceRpcRspElement : ABnfNodeElement
	{
		public AProtobufServiceRpcRspElement(ABnfFactory factory, ABnfFile file, int line, int col, int offset, string type)
            : base(factory, file, line, col, offset, type)
        {
        }

        private bool m_flag_CustomType = false;
        private AProtobufCustomTypeElement m_cache_CustomType = null;
        public AProtobufCustomTypeElement GetCustomType()
        {
            if (m_flag_CustomType) return m_cache_CustomType;
            m_flag_CustomType = true;
            foreach (var child in m_childs)
            {
                if (child is AProtobufCustomTypeElement)
                {
                    m_cache_CustomType = child as AProtobufCustomTypeElement;
                    break;
                }
            }
            return m_cache_CustomType;
        }

	}
}