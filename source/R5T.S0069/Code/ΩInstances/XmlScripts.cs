using System;


namespace R5T.S0069
{
    public class XmlScripts : IXmlScripts
    {
        #region Infrastructure

        public static IXmlScripts Instance { get; } = new XmlScripts();


        private XmlScripts()
        {
        }

        #endregion
    }
}
