using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.structures
{
    public interface IHashTable<KeyType, ValueType> where KeyType : IComparable<KeyType>
    {
        void Add(KeyType key, ValueType value);
        void Remove(KeyType key);
        bool Get(KeyType key, out ValueType value);
        void Clear();
    }
}
