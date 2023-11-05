using System;
using System.Xml.XPath;

using R5T.T0141;


namespace R5T.S0069
{
    [DemonstrationsMarker]
    public partial interface IXPathDemonstrations : IDemonstrationsMarker
    {
        public void Test_XPathExpression()
        {
            /// Inputs.
            var element = Instances.XElements.ChildA;
            var xpathExpression = "./descendant::name";


            /// Run.
            var resultOrDefault = element.XPathSelectElement(xpathExpression);

            Console.WriteLine(resultOrDefault);
        }
    }
}
