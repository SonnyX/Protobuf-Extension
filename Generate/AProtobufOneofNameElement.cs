namespace ALittle
{
	public class AProtobufOneofNameElement : ABnfNodeElement
	{
		public AProtobufOneofNameElement(ABnfFactory factory, ABnfFile file, int line, int col, int offset, string type)
            : base(factory, file, line, col, offset, type)
        {
        }

        private bool m_flag_Id = false;
        private AProtobufIdElement m_cache_Id = null;
        public AProtobufIdElement GetId()
        {
            if (m_flag_Id) return m_cache_Id;
            m_flag_Id = true;
            foreach (var child in m_childs)
            {
                if (child is AProtobufIdElement)
                {
                    m_cache_Id = child as AProtobufIdElement;
                    break;
                }
            }
            return m_cache_Id;
        }

	}
}