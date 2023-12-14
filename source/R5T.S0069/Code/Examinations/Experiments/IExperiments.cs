using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Microsoft.Win32.SafeHandles;
using R5T.F0000;
using R5T.L0030.Extensions;
using R5T.T0141;


namespace R5T.S0069
{
    [ExperimentsMarker]
    public partial interface IExperiments : IExperimentsMarker
    {
        public void PrettyPrintXml()
        {
            var xmlText = Instances.XmlTexts.SummaryWithPara;

            Console.WriteLine(xmlText);

            //var xmlSerializer = Xmlwrli

            //var element = Instances.XElementOperator.Parse(xmlText);

            var element = XElement.Parse(
                xmlText.Value,
                //LoadOptions.None
                LoadOptions.PreserveWhitespace
                );

            Console.WriteLine(element);

            //var prettyXmlText = Instances.XElementOperator.To_Text(element);

            var stringBuilder = new StringBuilder();
            //var stringWriter = new StringWriter(stringBuilder);

            //element.Save(
            //    stringWriter,
            //    SaveOptions.None);

            //var prettyXmlText = stringBuilder.ToString();

            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                // Cannot set.
                //OutputMethod = XmlOutputMethod.Text,
            };

            using (var xmlWriter = XmlWriter.Create(
                stringBuilder,
                xmlWriterSettings))
            {
                element.WriteTo(xmlWriter);
            }

            var prettyXmlText = stringBuilder.ToString();

            Console.WriteLine(prettyXmlText);
        }

        /// <summary>
        /// <inheritdoc cref="CanAsynchronousSettingsBeUsedSynchronously" path="/summary"/>
        /// </summary>
        public void UseXPath()
        {
            var element = Instances.XElements.MemberWithSummaryWithPara;
            var xPath = Instances.XPaths.ParaInSummary;


            var paraElement = element.XPathSelectElement(xPath.Value);

            Console.WriteLine(paraElement);
        }

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
            var settings = Instances.XmlWriterSettingsSets.Standard;

            var xmlText = Instances.XmlOperator.WriteTo_Text(
                element,
                settings);

            Console.WriteLine(xmlText);

            settings = Instances.XmlWriterSettingsSets.Standard_Synchronous;

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
                Instances.XmlWriterSettingsSets.Standard
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
            var settings = Instances.XmlWriterSettingsSets.Default;

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

        /// <summary>
        /// What happens if you try to parse an XElement from text containing two top-level nodes?
        /// Result: System.Xml.XmlException: 'There are multiple root elements. Line 5, position 2.'
        /// </summary>
        public void XElementOfTwoNodes()
        {
            /// Inputs.
            var xmlText = Instances.XmlTexts.SummaryAndRemarks;


            /// Run.
            // System.Xml.XmlException: 'There are multiple root elements. Line 5, position 2.'
            var elements = XElement.Parse(xmlText.Value);

            Console.WriteLine(elements);
        }

        /// <summary>
        /// Given misformatted XML text, parse the text to an XElement, then format the XElement and get a string representation.
        /// </summary>
        public void FormatMisformattedXmlText()
        {
            // Inputs.
            var misformattedXmlText = Instances.XmlTexts.MemberWithSummaryAndRemarks_Misformatted2;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;


            /// Run.
            var element = Instances.XElementOperator.Parse(
                misformattedXmlText,
                // Ensure we preserve all the misformatted whitespace.
                LoadOptions.PreserveWhitespace);

            //var originalElement = Instances.XElementOperator.Clone(element);

            var lines = Instances.EnumerableOperator.From(Instances.XElementOperator.To_Text_AsIs(element));

            // Remove all whitespace-only child text nodes of the element.
            var whitespaceOnlyChildTextNodes = Instances.XElementOperator.Enumerate_ChildNodesOfType<XText>(element)
                .Where(Instances.XTextOperator.Is_WhitespaceOnly)
                .Now();

            foreach (var textNode in whitespaceOnlyChildTextNodes)
            {
                textNode.Remove();
            }

            lines = lines.Append("", Instances.XElementOperator.To_Text_AsIs(element));

            // Assume XML documentation comment is composed of multiple top-level elements, with no indentation.
            var childElements = Instances.XElementOperator.Get_ChildElements(element);
            foreach (var childElement in childElements)
            {
                childElement.AddBeforeSelf(new XText(Environment.NewLine));
            }

            lines = lines.Append("", Instances.XElementOperator.To_Text_AsIs(element));

            // Every newline in every text node becomes a newline+tab.
            var textNodes = Instances.XElementOperator.Get_DescendantNodesOfType<XText>(element);

            foreach (var textNode in textNodes)
            {
                var textNodeValue = textNode.Value;

                var newTextNodeValue = Instances.StringOperator.Replace(
                    textNodeValue,
                    Instances.Strings.NewLine_NonWindows + Instances.Strings.Tab,
                    Instances.Strings.NewLine_NonWindows);

                textNode.Value = newTextNodeValue;
            }

            lines = lines.Append("", Instances.XElementOperator.To_Text_AsIs(element));

            // Put the member elment end tag on its own line.
            Instances.XElementOperator.Add_BeforeElementEndTag(
                element,
                new XText(Environment.NewLine));

            lines = lines.Append("", Instances.XElementOperator.To_Text_AsIs(element));

            //// Remove all text nodes.
            //var textNodes = Instances.XElementOperator.Get_DescendantNodesOfType<XText>(element);

            //foreach (var textNode in textNodes)
            //{
            //    textNode.Remove();
            //}

            //lines = lines.Append("", Instances.XElementOperator.To_Text_NoModifications(element));

            //// Add new lines before each child element.
            //var childElements = Instances.XElementOperator.Get_ChildElements(element);

            //foreach (var childElement in childElements)
            //{
            //    childElement.AddBeforeSelf(new XText(Environment.NewLine));
            //}

            //lines = lines.Append("", Instances.XElementOperator.To_Text_NoModifications(element));

            //// Add a new line before the elements end tag.
            //Instances.XElementOperator.Add_BeforeElementEndTag(
            //    element,
            //    new XText(Environment.NewLine));

            //lines = lines.Append("", Instances.XElementOperator.To_Text_NoModifications(element));

            //// Only remove text nodes that are whitespace.
            //element = Instances.XElementOperator.Clone(originalElement);

            //textNodes = Instances.XElementOperator.Get_DescendantNodesOfType<XText>(element);

            //foreach (var textNode in textNodes)
            //{
            //    var isWhitespaceOnly = Instances.XTextOperator.Is_WhitespaceOnly(textNode);
            //    if(isWhitespaceOnly)
            //    {
            //        textNode.Remove();
            //    }
            //}

            //lines = lines.Append("", Instances.XElementOperator.To_Text_NoModifications(element));

            // Write out the results.
            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath,
                lines);

            Instances.NotepadPlusPlusOperator.Open(outputFilePath);
        }

        /// <summary>
        /// Given misformatted XML text, parse the text to an XElement, then get a string representation.
        /// Ensure the round-tripped XML texts are the same.
        /// </summary>
        public void RoundTripMisformattedXmlText()
        {
            /// Inputs.
            var misformattedXmlText = Instances.XmlTexts.MemberWithSummaryAndRemarks_Misformatted;


            /// Run.
            var xElement = Instances.XElementOperator.Parse(
                misformattedXmlText,
                // Ensure we preserve all the misformatted whitespace.
                LoadOptions.PreserveWhitespace);

            //// True, this is equal.
            //var roundTrippedMisformattedXmlText = xElement.ToString();

            // True, this is equal.
            var roundTrippedMisformattedXmlText = Instances.XElementOperator.To_Text(xElement);

            var areEqual = misformattedXmlText.Value == roundTrippedMisformattedXmlText;

            Console.WriteLine($"{areEqual}: Equal?\n\n{misformattedXmlText}\n\n=>\n\n{roundTrippedMisformattedXmlText}");
        }
    }
}
