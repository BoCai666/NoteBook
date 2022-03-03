using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public struct MyKeyValuePair<TK, TV>
    {
        public TK key;
        public TV value;
        public MyKeyValuePair(TK key, TV value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class MyDictionary<K, V> : IEnumerable<MyKeyValuePair<K, V>>
    {
        public struct Entry
        {
            public K key;
            public V value;
            public int hashCode;
            public int next;
        }
        private int freeCount;
        private int count;
        public int freeList;
        public int[] buckets;
        public Entry[] entries;

        public int Count => count - freeCount;

        public MyDictionary() : this(0)
        {
        }

        public MyDictionary(int capacity)
        {
            Initialize(capacity);
        }

        public V this[K key]
        {
            get
            {
                var index = FindEntry(key);
                if (index == -1) throw new Exception("Key not exist");
                return entries[index].value;
            }
            set
            {
                Insert(key, value, isAdd: false);
            }
        }
        
        public void Add(K key, V value)
        {
            Insert(key, value, isAdd: true);
        }

        public bool Remove(K key)
        {
            var ret = BucketCollision(key);
            var bucketIndex = ret.bucketIndex;
            var hashCode = ret.hashCode;
            var lastEntryIndex = -1;
            for (int entryIndex = buckets[bucketIndex]; entryIndex >= 0; entryIndex = entries[entryIndex].next)
            {
                var entry = entries[entryIndex];
                if (entry.hashCode == hashCode && CompareKey(entry.key, key))
                {
                    if (lastEntryIndex < 0) buckets[bucketIndex] = entries[entryIndex].next;
                    else entries[lastEntryIndex].next = entries[entryIndex].next;
                    entries[entryIndex].hashCode = -1;
                    entries[entryIndex].key = default(K);
                    entries[entryIndex].value = default(V);
                    entries[entryIndex].next = freeList;
                    freeList = entryIndex;
                    freeCount++;
                    return true;
                }
                lastEntryIndex = entryIndex;
            }
            return false;
        }

        public bool ContainsKey(K key)
        {
            return FindEntry(key) != -1;
        }

        void Initialize(int capacity)
        {
            if (capacity < 0) throw new Exception("Capacity can not less than zero");
            var minPrime = GetPrime(capacity);
            buckets = new int[minPrime];
            for (int i = 0; i < minPrime; i++)
            {
                buckets[i] = -1;
            }
            entries = new Entry[minPrime];
            freeList = -1;
        }

        void Insert(K key, V value, bool isAdd)
        {
            if (buckets == null) Initialize(0);
            var ret = BucketCollision(key);
            var bucketIndex = ret.bucketIndex;
            var hashCode = ret.hashCode;
            for (int entryIndex = buckets[bucketIndex]; entryIndex >= 0; entryIndex = entries[entryIndex].next)
            {
                var entry = entries[entryIndex];
                if (entry.hashCode == hashCode && CompareKey(entry.key, key))
                {
                    if (isAdd) throw new Exception("The key has existed in dictionary");
                    entries[entryIndex].value = value;
                    return;
                }
            }
            int insertIndex;
            if (freeCount > 0)
            {
                insertIndex = freeList;
                freeList = entries[insertIndex].next;
                freeCount--;
            }
            else
            {
                if (count == entries.Length)
                {
                    Resize();
                    bucketIndex = hashCode % buckets.Length;
                }
                insertIndex = count++;
            }
            entries[insertIndex].hashCode = hashCode;
            entries[insertIndex].key = key;
            entries[insertIndex].value = value;
            entries[insertIndex].next = buckets[bucketIndex];
            buckets[bucketIndex] = insertIndex;
        }

        void Resize()
        {
            Resize(ExpandPrime(count), buildNewHashCodes: false);
        }

        void Resize(int newSize, bool buildNewHashCodes)
        {
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newSize; i++)
            {
                newBuckets[i] = -1;
            }
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(entries, 0, newEntries, 0, count);
            if (buildNewHashCodes)
            {
                for (int i = 0; i < count; i++)
                {
                    var entry = newEntries[i];
                    if (entry.hashCode != -1)
                    {
                        newEntries[i].hashCode = BucketCollision(entry.key).hashCode;
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {
                var entry = newEntries[i];
                if (entry.hashCode >= 0)
                {
                    var bucketIndex = entry.hashCode % newSize;
                    newEntries[i].next = newBuckets[bucketIndex];
                    newBuckets[bucketIndex] = i;
                }
            }
            entries = newEntries;
            buckets = newBuckets;
        }

        int FindEntry(K key)
        {
            var ret = BucketCollision(key);
            var bucketIndex = ret.bucketIndex;
            var hashCode = ret.hashCode;
            for (int entryIndex = buckets[bucketIndex]; entryIndex >= 0; entryIndex = entries[entryIndex].next)
            {
                var entry = entries[entryIndex];
                if (entry.hashCode == hashCode && CompareKey(entry.key, key)) return entryIndex;
            }
            return -1;
        }

        (int hashCode, int bucketIndex) BucketCollision(K key)
        {
            var hashCode = key.GetHashCode() & int.MaxValue;
            var bucketIndex = hashCode % buckets.Length;
            return (hashCode, bucketIndex);
        }

        bool CompareKey(K k1, K k2)
        {
            return k1.Equals(k2);
        }

        struct MyDictEnumerator : IEnumerator<MyKeyValuePair<K, V>>
        {
            readonly MyDictionary<K, V> dictionary;
            int index;
            public MyKeyValuePair<K, V> Current => current;

            object IEnumerator.Current => throw new NotImplementedException();

            MyKeyValuePair<K, V> current;

            public MyDictEnumerator(MyDictionary<K, V> dictionary)
            {
                this.dictionary = dictionary;
                index = 0;
                current = new MyKeyValuePair<K, V>(default(K), default(V));
            }

            public bool MoveNext()
            {
                while (index < dictionary.count)
                {
                    var entry = dictionary.entries[index];
                    if (entry.hashCode >= 0)
                    {
                        index++;
                        current.key = entry.key;
                        current.value = entry.value;
                        return true;
                    }
                }
                index = dictionary.count;
                current.key = default(K);
                current.value = default(V);
                return false;
            }

            public void Reset()
            {
                index = 0;
                current.key = default(K);
                current.value = default(V);
            }

            public void Dispose()
            {
            }
        }

        public IEnumerator<MyKeyValuePair<K, V>> GetEnumerator()
        {
            return new MyDictEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        int ExpandPrime(int size)
        {
            var min = 2 * size;
            return GetPrime(min);
        }

        int GetPrime(int min)
        {
            for (int i = 0; i < primes.Length; i++)
            {
                var num = primes[i];
                if (num >= min) return num;
            }
            throw new Exception("Prime Can not be enough");
        }

        readonly int[] primes =
        {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71,
        };

    }

}
