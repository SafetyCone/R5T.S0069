using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using R5T.L0030.Extensions;
using R5T.T0141;


namespace R5T.S0069
{
    [ExperimentsMarker]
    public partial interface IExperiments : IExperimentsMarker
    {
        /// <summary>
        /// The XML writer settings type as an "Async" property that can be either true or false.
        /// I wonder, can a settings instance set to be asynchronous be used synchronously (or vice-versa)?
        /// Result: async used synchronously, yes, sync used asynchronously, NO!
        /// </summary>
        /// <remarks>
        /// Conclusion: the standard settings (which are asynchronous) can be used synchronously with no issue.
        /// </remarks>
        public async Task CanAsynchronousSettingsBeUsedSynchronously()
        {
            var element = Instances.XElements.Simple;

            // Standard settings are asynchronous.
            var settings = Instances.XmlWriterSettingSets.Standard;

            var xmlText = Instances.XmlOperator.WriteTo_Text(
                element,
                settings);

            Console.WriteLine(xmlText);

            settings = Instances.XmlWriterSettingSets.Standard_Synchronous;

            // Will error: System.InvalidOperationException: 'Set XmlWriterSettings.Async to true if you want to use Async Methods.'
            xmlText = await Instances.XmlOperator.WriteTo_Text_Asynchronous(
                element,
                settings);

            Console.WriteLine(xmlText);
        }

        /// <summary>
        /// The regular to-string method for the XML writer settings type is useless (it's the the default object to-string method, which prints the full type name of the object).
        /// Create my own describe-to method.
        /// Result: great!
        /// </summary>
        public void XmlWriterSettings_DescribeTo()
        {
            var settings =
                //Instances.XmlWriterSettingSets.Default
                Instances.XmlWriterSettingSets.Standard
                ;

            Instances.XmlWriterSettingsOperator.DescibeTo_Synchronous(
                settings,
                Console.Out);
        }

        /// <summary>
        /// What is returned when the XML writer settings to-string method is called?
        /// Result: System.Xml.XmlWriterSettings (the regular object to-string result).
        /// </summary>
        public void XmlWriterSettings_ToString()
        {
            var settings = Instances.XmlWriterSettingSets.Default;

            var output = settings.ToString();

            Console.WriteLine(output);
        }

        /// <summary>
        /// Uses a custom value-equality method to check the value equality of two XML writer settings.
        /// Result: works!
        /// </summary>
        public void XmlWriterSettingsAreEqualByValue()
        {
            var settings1 = new XmlWriterSettings().Set_Standard();
            var settings2 = new XmlWriterSettings().Set_Standard();

            var areEqual = Instances.XmlWriterSettingsOperator.Equals(
                settings1,
                settings2);

            Console.WriteLine($"{areEqual}: are equal");
        }

        /// <summary>
        /// The XML writer settings type (<see cref="XmlWriterSettings"/>) is a reference type.
        /// It does not seem to implement an equality operator, so by default reference type instances are different by reference.
        /// Does the class somehow have value-based equality?
        /// Result: no, no value equality!
        /// </summary>
        public void XmlWriterSettingsAreEqual()
        {
            var settings1 = new XmlWriterSettings().Set_Standard();
            var settings2 = new XmlWriterSettings().Set_Standard();

            var areEqual = settings1 == settings2;

            Console.WriteLine($"{areEqual}: are equal");
        }

        /// <summary>
        /// Round-trip some simple XML text.
        /// </summary>
        public void ExploreStringConversion()
        {
            /// Inputs.
            var text =
                Instances.XmlTexts.SingleElement
                ;


            /// Run.
            var xElement = XElement.Parse(text.Value, LoadOptions.PreserveWhitespace);

            //var output = xElement.ToString();

            var xmlWriterSettings = new XmlWriterSettings
            {
                Async = false,
                Indent = true,
                OmitXmlDeclaration = true,
            };

            var stringBuilder = new StringBuilder();

            //using var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
            //{
            //    Indent = true,

            //});

            //using var xmlWriter = XmlWriter.Create(stringBuilder);

            //// Need to call close if outside of a dispose scope.
            //xmlWriter.Close();

            //using (var xmlWriter = XmlWriter.Create(stringBuilder))
            using (var xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
            {
                xElement.WriteTo(xmlWriter);
            }

            var output = stringBuilder.ToString();

            Console.WriteLine($"{text}: input,\n{output}: output");
        }
    }
}
