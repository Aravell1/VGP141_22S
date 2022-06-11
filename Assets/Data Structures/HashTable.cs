using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Key = System.String;

namespace VGP141.DataStructures
{
    public class HashTable<Value> where Value : class
    {
        private class Node
        {
            public Key Key { get; set; }
            public Value Value { get; set; }

            public Node()
            {
                Key = null;
                Value = null;
            }

            public Node(Key key, Value value)
            {
                Key = key;
                Value = value;
            }
        }

        public uint Size { get; private set; }
        public uint TotalItemCount { get; private set; }

        private Node[] table;

        /// <summary>
        /// Constructs a HashTable using the Double Hash collision resolution method.
        /// </summary>
        /// <param name="size">The predicted number of elements.</param>
        public HashTable(uint size = 0)
        {
            if (size > 0)
            {
                size *= 2;
                Size = GetNextPrimeNum(size);
                Debug.Log($"Creating HashTable with a doubled predicted size of {size} and final size of {Size}");
            }

            table = new Node[Size];
            for (int i = 0; i < Size; i++)
            {
                table[i] = new Node();
            }
        }

        public bool Insert(Key key, Value value)
        {
            // calculate the index
            uint index = Hash(key);

            // return if our hash table is full
            if (TotalItemCount == Size)
            {
                return false;
            }

            // calculate the step offset
            uint stepOffset = DoubleHash(key);

            while (table[index].Value != null)
            {
                // If the key is already in the table, replace value with passed in value
                if (table[index].Key == key)
                {
                    table[index].Value = value;
                    return true;
                }

                // if not the same key, we need to advance our index using our stepOffset
                index += stepOffset;
                index %= Size;
            }

            // insert new data
            table[index].Key = key;
            table[index].Value = value;

            ++TotalItemCount;

            return true;
        }

        public void Remove(Key key)
        {
            // calculate the index
            uint index = Hash(key);
            // calculate the step offset
            uint stepOffset = DoubleHash(key);
            // save original index to stop an infinite loop
            uint originalIndex = index;

            while (table[index].Value != null)
            {
                // If the key is found, remove value
                if (table[index].Key == key)
                {
                    table[index].Key = null;
                    table[index].Value = null;

                    --TotalItemCount;
                    return;
                }

                // if not the same key, we need to advance our index using our stepOffset
                index += stepOffset;
                index %= Size;

                // if the table does not contain the key
                if (originalIndex == index)
                {
                    return;
                }
            }
        }

        public Value Find(Key key)
        {
            // calculate the index
            uint index = Hash(key);
            // calculate the step offset
            uint stepOffset = DoubleHash(key);
            // save original index to stop an infinite loop
            uint originalIndex = index;

            while (table[index].Value != null)
            {
                // If the key is found, return value
                if (table[index].Key == key)
                {
                    return table[index].Value;
                }

                // if not the same key, we need to advance our index using our stepOffset
                index += stepOffset;
                index %= Size;

                // if the table does not contain the key
                if (originalIndex == index)
                {
                    return null;
                }
            }

            return null;
        }

        public void Clear()
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = null;
            }
            table = null;

            Size = 0;
            TotalItemCount = 0;
        }

        private uint Hash(Key key)
        {
            uint hash = 0;

            for (int i = 0; i < key.Length; i++)
            {
                hash = hash * 256 + key[i];
            }

            return hash % Size;
        }

        private uint DoubleHash(Key key)
        {
            const uint hashConst = 3;
            uint hash = 0;

            for (int i = 0; i < key.Length; i++)
            {
                hash = hash * 256 + key[i];
            }

            return hashConst - hash % hashConst;
        }

        bool IsNumPrime(uint value)
        {
            for (uint i = 2; (i * i) <= value; i++)
            {
                if ((value % i) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        uint GetNextPrimeNum(uint value)
        {
            uint i = value + 1;

            for (; ; i++)
            {
                if (IsNumPrime(i))
                {
                    break;
                }
            }

            return i;
        }
    }
}