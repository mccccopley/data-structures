using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.structures
{
    public interface ISet<ElementType> where ElementType : IComparable<ElementType>
    {
        void AddElement(ElementType element);
        void RemoveElement(ElementType element);
        void Clear();
        bool HasElement(ElementType element);
        void GetSortedElements(IList<ElementType> orderedList);
    }
}
