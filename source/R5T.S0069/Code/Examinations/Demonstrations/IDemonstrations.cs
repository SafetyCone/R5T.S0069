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
