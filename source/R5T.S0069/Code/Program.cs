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

            Demonstrations.Instance.XElement_Equalities();

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