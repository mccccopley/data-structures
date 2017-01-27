using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.structures
{
    public class HashTable<KeyType, ValueType> : IHashTable<KeyType, ValueType> where KeyType : IComparable<KeyType>
    {
        private struct HashChainItem
        {
            public KeyType key;
            public ValueType value;
            public int nextIndex;
            public int listIndex;

            public HashChainItem(KeyType key, ValueType value)
            {
                this.key = key;
                this.value = value;
                nextIndex = -1;
                listIndex = -1;
            }
        }

        private const int InitialTableSize = 8;
        private const int InitialKeyCapacity = 16;
        private const int HashChainIndexTerminal = -1;

        private static readonly HashChainItem NullHashChainItem = new HashChainItem(default(KeyType), default(ValueType));

        private int containedKeyCount = 0;
        private int availableHashChainIndex = -1;
        private HashChainItem[] arrayOfHashChainItems = null;
        private int[] tableOfHashChainHeadIndices = null;

        public HashTable()
        {
            tableOfHashChainHeadIndices = new int[InitialTableSize];
            arrayOfHashChainItems = new HashChainItem[InitialKeyCapacity];
            InitializeHashChainItems();
            InitializeTable();
        }

        public void Add(KeyType key, ValueType value)
        {
            int tableIndexForKey = CalculateTableIndexForKey(key);
            int existingHashChainItemIndex = FindHashChainItemIndexForKey(tableIndexForKey, key);

            if (existingHashChainItemIndex != HashChainIndexTerminal)
            {
                arrayOfHashChainItems[existingHashChainItemIndex].value = value;
            }
            else
            {
                if (availableHashChainIndex == HashChainIndexTerminal)
                {
                    DoubleTheCapacity();
                }

                int nextAvailableHashChainIndex = arrayOfHashChainItems[availableHashChainIndex].nextIndex;
                int existingHashChainHeadIndex = tableOfHashChainHeadIndices[tableIndexForKey];

                arrayOfHashChainItems[availableHashChainIndex].nextIndex = existingHashChainHeadIndex;
                arrayOfHashChainItems[availableHashChainIndex].listIndex = tableIndexForKey;
                arrayOfHashChainItems[availableHashChainIndex].key = key;
                arrayOfHashChainItems[availableHashChainIndex].value = value;
                tableOfHashChainHeadIndices[tableIndexForKey] = availableHashChainIndex;

                availableHashChainIndex = nextAvailableHashChainIndex;
                containedKeyCount++;
            }
        }

        public void Remove(KeyType key)
        {
            int tableIndexForKey = CalculateTableIndexForKey(key);
            int hashChainHeadIndex = tableOfHashChainHeadIndices[tableIndexForKey];
            int hashChainItemIndex = hashChainHeadIndex;
            int hashChainPreviousItemIndex = HashChainIndexTerminal;

            while (hashChainItemIndex != HashChainIndexTerminal)
            {
                if (arrayOfHashChainItems[hashChainItemIndex].key.CompareTo(key) == 0)
                {
                    RemoveHashChainItem(tableIndexForKey, hashChainItemIndex, hashChainPreviousItemIndex);
                    return;
                }

                hashChainItemIndex = arrayOfHashChainItems[hashChainItemIndex].nextIndex;
            }
        }

        public bool Get(KeyType key, out ValueType value)
        {
            int tableIndexForKey = CalculateTableIndexForKey(key);
            int existingHashChainItemIndex = FindHashChainItemIndexForKey(tableIndexForKey, key);

            if (existingHashChainItemIndex != HashChainIndexTerminal)
            {
                value = arrayOfHashChainItems[existingHashChainItemIndex].value;
                return true;
            }
            else
            {
                value = NullHashChainItem.value;
                return false;
            }
        }

        public void Clear()
        {
            InitializeHashChainItems();
            InitializeTable();
        }

        private void InitializeHashChainItems()
        {
            int hashChainItemCount = arrayOfHashChainItems.Length;

            for (int hashChainItemIndex = 0; hashChainItemIndex < hashChainItemCount; ++hashChainItemIndex)
            {
                arrayOfHashChainItems[hashChainItemIndex].nextIndex = hashChainItemIndex + 1;
            }

            arrayOfHashChainItems[hashChainItemCount - 1].nextIndex = HashChainIndexTerminal;
            availableHashChainIndex = 0;
            containedKeyCount = 0;
        }

        private void InitializeTable()
        {
            int tableEntryCount = tableOfHashChainHeadIndices.Length;

            for (int tableIndex = 0; tableIndex < tableEntryCount; ++tableIndex)
            {
                tableOfHashChainHeadIndices[tableIndex] = HashChainIndexTerminal;
            }
        }

        private int CalculateTableIndexForKey(KeyType key)
        {
            int hashCode = key.GetHashCode();
            int tableCount = tableOfHashChainHeadIndices.Length;
            int tableIndex = (hashCode & 0x7fffffff) % tableCount;
            return tableIndex;
        }

        private int FindHashChainItemIndexForKey(int tableIndex, KeyType key)
        {
            int hashChainHeadIndex = tableOfHashChainHeadIndices[tableIndex];
            int hashChainItemIndex = hashChainHeadIndex;

            while (hashChainItemIndex != HashChainIndexTerminal)
            {
                HashChainItem hashChainItem = arrayOfHashChainItems[hashChainItemIndex];
                
                if (hashChainItem.key.CompareTo(key) == 0)
                {
                    return hashChainItemIndex;
                }

                hashChainItemIndex = hashChainItem.nextIndex;
            }

            return HashChainIndexTerminal;
        }

        private void RemoveHashChainItem(int tableEntryIndex, int hashChainItemIndex, int hashChainItemPreviousIndex)
        {
            if (hashChainItemPreviousIndex == HashChainIndexTerminal)
            {
                tableOfHashChainHeadIndices[tableEntryIndex] = arrayOfHashChainItems[hashChainItemIndex].nextIndex;
            }
            else
            {
                arrayOfHashChainItems[hashChainItemPreviousIndex].nextIndex = arrayOfHashChainItems[hashChainItemIndex].nextIndex;
            }

            arrayOfHashChainItems[hashChainItemIndex].nextIndex = availableHashChainIndex;
            arrayOfHashChainItems[hashChainItemIndex].listIndex = HashChainIndexTerminal;
            arrayOfHashChainItems[hashChainItemIndex].key = default(KeyType);
            arrayOfHashChainItems[hashChainItemIndex].value = default(ValueType);
            availableHashChainIndex = hashChainItemIndex;

            containedKeyCount--;
        }

        private void DoubleTheCapacity()
        {
            int keyCapacity = arrayOfHashChainItems.Length;
            int doubledKeyCapacity = keyCapacity * 2;
            HashChainItem[] doubledArrayOfHashChainItems = new HashChainItem[doubledKeyCapacity];

            for (int hashChainItemIndex = 0; hashChainItemIndex < keyCapacity; ++hashChainItemIndex)
            {
                doubledArrayOfHashChainItems[hashChainItemIndex] = arrayOfHashChainItems[hashChainItemIndex];
            }

            for (int hashChainItemIndex = keyCapacity; hashChainItemIndex < doubledKeyCapacity; ++hashChainItemIndex)
            {
                doubledArrayOfHashChainItems[hashChainItemIndex].nextIndex = hashChainItemIndex + 1;
            }

            doubledArrayOfHashChainItems[doubledKeyCapacity - 1].nextIndex = HashChainIndexTerminal;

            if (availableHashChainIndex == HashChainIndexTerminal)
            {
                availableHashChainIndex = keyCapacity;
            }

            arrayOfHashChainItems = doubledArrayOfHashChainItems;
        }
    }

    public class IntPairHashTable : HashTable<int, int> {}
}
