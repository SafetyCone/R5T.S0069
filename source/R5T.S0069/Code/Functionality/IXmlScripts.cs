using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using R5T.L0030.Extensions;
using R5T.T0132;


namespace R5T.S0069
{
    [FunctionalityMarker]
    public partial interface IXmlScripts : IFunctionalityMarker
    {
        public void OrderChildrenBySpecifiedNames()
        {
            /// Inputs.
            var element = Instances.XElements_Constructed.Complex_OrderedChildren;
            // Alphabetically reverse order, leaving some of the child names out.
            var orderedNames = new[]
            {
                Instances.ElementNames.ChildC,
                Instances.ElementNames.ChildA,
            };


            /// Run.
            element.OrderChildren_ByNames(orderedNames);

            var text = Instances.XElementOperator.To_Text(element);

            Console.WriteLine(text);
        }

        /// <summary>
        /// Constructs a complex element.
        /// </summary>
        public void Construct_ComplexElement()
        {
            var element = Instances.XElementOperator.New(
                Instances.ElementNames.Simple,
                Instances.XElementOperator.New(
                    Instances.ElementNames.ChildA
                ),
                Instances.XElementOperator.New(
                    Instances.ElementNames.ChildB
                ),
                Instances.XElementOperator.New(
                    Instances.ElementNames.ChildC
                ),
                Instances.XElementOperator.New(
                    Instances.ElementNames.ChildD
                )
            );

            var text = Instances.XElementOperator.To_Text(element);

            Console.WriteLine(text);
        }

        public void Element_ToText()
        {
            /// Inputs.
            var element = Instances.XElements_Constructed.Simple;

            var text = Instances.XElementOperator.To_Text(element);

            Console.WriteLine(text);
        }

        /// <summary>
        /// Round-trip some simple XML text using functionality.
        /// </summary>
        public void Text_ToXElement_ToText_Functionality()
        {
            /// Inputs.
            var text =
                Instances.XmlTexts.SingleElement
                ;


            /// Run.
            var xElement = XElement.Parse(text.Value, LoadOptions.PreserveWhitespace);

            var output = Instances.XElementOperator.To_Text(xElement);

            Console.WriteLine(output);
        }

        /// <summary>
        /// Round-trip some simple XML text.
        /// </summary>
        public void Text_ToXElement_ToText()
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
