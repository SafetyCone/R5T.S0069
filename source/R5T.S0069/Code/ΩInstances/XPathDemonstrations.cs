using System;


namespace R5T.S0069
{
    public class XPathDemonstrations : IXPathDemonstrations
    {
        #region Infrastructure

        public static IXPathDemonstrations Instance { get; } = new XPathDemonstrations();


        private XPathDemonstrations()
        {
        }

        #endregion
    }
}
