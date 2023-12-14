using System;
using System.Linq;
using System.Xml.Linq;

using R5T.T0141;


namespace R5T.S0069
{
    [DemonstrationsMarker]
    public partial interface IDemonstrations : IDemonstrationsMarker
    {
        /// <summary>
        /// Serialize a complex XML element, in an indented format, using a custom indentation method.
        /// </summary>
        public void Serialize_Indented_CustomImplementation()
        {
            /// Inputs.
            var element =
                //Instances.XElements._Parsed.Complex_OrderedChildren
                Instances.XElements._Constructed.Complex_OrderedChildren
                ;
            var outputXmlFilePath = Instances.FilePaths.OutputXmlFilePath;


            /// Run.
            var indentedElement = Instances.XElementOperator._Implementations.Indent(element);

            Instances.XElementOperator.To_File_AsIs_Synchronous(
                outputXmlFilePath.Value,
                indentedElement);

            Instances.NotepadPlusPlusOperator.Open(
                outputXmlFilePath);
        }

        /// <summary>
        /// It can be useful to serialize XML without any insignificant formatting whitespace (like indentation).
        /// </summary>
        public void Serialize_Unadorned()
        {
            /// Inputs.
            var element =
                Instances.XElements._Parsed.Complex_OrderedChildren
                ;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            var outputXmlFilePath = Instances.FilePaths.OutputXmlFilePath;


            /// Run.
            var originalText = Instances.XElementOperator.To_Text_AsIs(element);

            var text = Instances.XElementOperator.To_Text_Unadorned(element);

            Instances.NotepadPlusPlusOperator.WriteTextAndOpen(
                outputFilePath.Value,
                $"{text}\n\n{originalText}");

            Instances.XElementOperator.To_File_Unadorned_Synchronous(
                outputXmlFilePath.Value,
                element);

            Instances.NotepadPlusPlusOperator.Open(
                outputXmlFilePath);
        }

        public void Remove_InsignificantWhitespace()
        {
            /// Inputs.
            var element =
                Instances.XElements.WithInsignificantWhitespace
                ;


            /// Run.
            var trimmedElement = Instances.XElementOperator.Clone(element);
            
            Instances.XElementOperator.Remove_InsignificantWhitespace_Modify(trimmedElement);
                
            var text = Instances.XElementOperator.To_Text_AsIs(trimmedElement);

            var originalText = Instances.XElementOperator.To_Text_AsIs(element);

            Console.WriteLine(originalText);
            Console.WriteLine();
            Console.WriteLine(text);
        }

        /// <summary>
        /// The XML serialization process can inject extra formatting into the serialized result
        /// (for example, by adding an XML declaration, or indentation).
        /// <para>
        /// Note: the XML *de*serialization process can also add or remove extra formatting into the XElement result,
        /// formatting that generally only becomes apparent after serialization the XElement.
        /// This means you might look for the issue in the *de*serialization process, and drive yourself crazy not finding it because the problem is caused
        /// by XText elements inserted during the serialization process.
        /// </para>
        /// </summary>
        public void Serialize_XElement_AsIs()
        {
            /// Inputs.
            var element =
                //Instances.XElements.MemberWithSummaryWithPara
                //Instances.XElements.Misformatted
                Instances.XElements.WithInsignificantWhitespace
                ;
            var filePath = Instances.FilePaths.OutputXmlFilePath;


            /// Run.
            Instances.XElementOperator.To_File_AsIs_Synchronous(
                filePath,
                element);

            Instances.NotepadPlusPlusOperator.Open(
                filePath);
        }

        /// <summary>
        /// The XML deserlization process can insert extra formatting into the deserialized result (in the form of whitespace XText elements for whitespace and indentation).
        /// <para>
        /// Note: the XML *serlialization* process (either to text, or to a file) can also add or remove extra formatting into the result.
        /// So be careful looking for formatting problems in the *de*serialization process that are not actually present, but instead are from the *serialization* process.
        /// </para>
        /// </summary>
        public void Deserialize_XElement_AsIs()
        {
            /// Inputs.
            var xmlText =
                //Instances.XmlTexts.MemberWithSummaryAndRemarks
                Instances.XmlTexts.WithInsignificantWhitespace
                ;


            /// Run.
            var element = Instances.XElementOperator.Parse_AsIs(xmlText);

            var text = Instances.XElementOperator.To_Text_AsIs(element);

            Console.WriteLine(xmlText);
            Console.WriteLine();
            Console.WriteLine(text);
            Console.WriteLine();

            var areEqual = xmlText.Value == text;

            Console.WriteLine($"{areEqual}: Are equal?");
        }

        /// <summary>
        /// How do you do the simplest operation, creating an XElement with a given name?
        /// </summary>
        public void Create_XElement()
        {
            /// Inputs.
            var elementName = Instances.ElementNames.Simple_Uppercase;


            /// Run.
            var element = Instances.XElementOperator.Create_Element(elementName);

            Console.WriteLine(element);
        }

        /// <summary>
        /// How do deep-copy (clone) an XElement?
        /// <para>
        /// Result: Use the <see cref="XElement(XElement)"/> constructor.
        /// </para>
        /// </summary>
        public void Clone_XElement()
        {

        }

        public void XElement_Equalities()
        {
            var element = Instances.XElements.Complex_OrderedChildren;

            var elementReference = element;

            var elementReferenceIsReferenceEqual = Instances.XElementOperator.Equals_ByReference(element, elementReference);
            // True: Element reference is equal by reference.
            Console.WriteLine($"{elementReferenceIsReferenceEqual}: Element reference is equal by reference.");

            var elementReferenceIsDeepEqual = Instances.XElementOperator.Equals_ByValue_Deep(element, elementReference);
            // True: Element reference is equal by deep.
            Console.WriteLine($"{elementReferenceIsDeepEqual}: Element reference is equal by deep.");

            var elementClone = Instances.XElementOperator.Clone(element);

            var elementCloneIsReferenceEqual = Instances.XElementOperator.Equals_ByReference(element, elementClone);
            // False: Element clone is equal by reference.
            Console.WriteLine($"{elementCloneIsReferenceEqual}: Element clone is equal by reference.");

            var elementCloneIsDeepEqual = Instances.XElementOperator.Equals_ByValue_Deep(element, elementClone);
            // True: Element clone is equal by deep.
            Console.WriteLine($"{elementCloneIsDeepEqual}: Element clone is equal by deep.");

            // Are children of the clone equal by reference?
            var firstChild = element.Elements().First();
            var cloneFirstChild = elementClone.Elements().First();

            var childrenOfCloneAreReferenceEqual = Instances.XElementOperator.Equals_ByReference(firstChild, cloneFirstChild);
            Console.WriteLine($"{childrenOfCloneAreReferenceEqual}: Clone children are equal by reference.");
        }
    }
}
