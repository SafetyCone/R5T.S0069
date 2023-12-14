using System;
using System.Threading.Tasks;


namespace R5T.S0069
{
    class Program
    {
        static async Task Main()
        {
            //XmlScripts.Instance.Text_ToXElement_ToText();
            //XmlScripts.Instance.Element_ToText();
            //XmlScripts.Instance.Construct_ComplexElement();
            //XmlScripts.Instance.OrderChildrenBySpecifiedNames();

            //Demonstrations.Instance.Create_XElement();
            //Demonstrations.Instance.Deserialize_XElement_AsIs();
            //Demonstrations.Instance.Serialize_XElement_AsIs();
            //Demonstrations.Instance.Remove_InsignificantWhitespace();
            //Demonstrations.Instance.Serialize_Unadorned();
            Demonstrations.Instance.Serialize_Indented_CustomImplementation();
            //Demonstrations.Instance.XElement_Equalities();

            //XPathDemonstrations.Instance.Test_XPathExpression();

            //Experiments.Instance.RoundTripMisformattedXmlText();
            //Experiments.Instance.FormatMisformattedXmlText();
            //Experiments.Instance.XmlWriterSettingsAreEqual();
            //Experiments.Instance.XmlWriterSettingsAreEqualByValue();
            //Experiments.Instance.XmlWriterSettings_ToString();
            //Experiments.Instance.XmlWriterSettings_DescribeTo();
            //await Experiments.Instance.CanAsynchronousSettingsBeUsedSynchronously();
            //Experiments.Instance.UseXPath();
            //Experiments.Instance.PrettyPrintXml();
        }
    }
}