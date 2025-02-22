using System;
using System.Collections.Generic;
using DataStructures.Hashing;
using NUnit.Framework;

namespace DataStructures.Tests.Hashing;

[TestFixture]
public class HashTableTests
{
    [Test]
    public void Add_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>();

        Assert.Throws<ArgumentNullException>(() => hashTable.Add(null, 1));
    }

    [Test]
    public void Add_ThrowsException_WhenKeyAlreadyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.Throws<ArgumentException>(() => hashTable.Add("a", 2));
    }

    [Test]
    public void Add_IncreasesCount_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.AreEqual(1, hashTable.Count);
    }

    [Test]
    public void Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        try
        {
            hashTable.Add("a", 2);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("ArgumentException");
        }
        Assert.AreEqual(1, hashTable.Count);
    }

    [Test]
    public void Add_ThrowsException_WhenValueIsNull()
    {
        var hashTable = new HashTable<string, string>();

        Assert.Throws<ArgumentNullException>(() => hashTable.Add("a", null));
    }

    [Test]
    public void Add_IncreasesCount_WhenValueDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.AreEqual(1, hashTable.Count);
    }

    [Test]
    public void Add_DoesNotIncreaseCount_WhenValueAlreadyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        try
        {
            hashTable.Add("b", 1);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("ArgumentException");
        }

        Assert.AreEqual(2, hashTable.Count);
    }

    [Test]
    public void Add_IncreasesCount_WhenValueIsNull()
    {
        var hashTable = new HashTable<string, string>();

        try
        {
            hashTable.Add("a", null);
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("ArgumentNullException");
        }
        Assert.AreEqual(0, hashTable.Count);
    }

    [Test]
    public void Add_IncreasesCount_WhenValueAlreadyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Add("b", 1);
        Assert.AreEqual(2, hashTable.Count);
    }

    [Test]
    public void Remove_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>();

        Assert.Throws<ArgumentNullException>(() => hashTable.Remove(null));
    }

    [Test]
    public void Remove_ReturnsFalse_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        Assert.IsFalse(hashTable.Remove("a"));
    }

    [Test]
    public void Remove_ReturnsTrue_WhenKeyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.IsTrue(hashTable.Remove("a"));
    }

    [Test]
    public void Remove_DecreasesCount_WhenKeyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Remove("a");

        Assert.AreEqual(0, hashTable.Count);
    }

    [Test]
    public void Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Remove("a");

        Assert.AreEqual(0, hashTable.Count);
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        Assert.IsFalse(hashTable.ContainsValue(1));
    }

    [Test]
    public void ContainsValue_ReturnsTrue_WhenValueExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.IsTrue(hashTable.ContainsValue(1));
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueIsNull()
    {
        var hashTable = new HashTable<string, string>();

        Assert.Throws<ArgumentNullException>(() => hashTable.ContainsValue(null));
    }

    [Test]
    public void ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        Assert.IsFalse(hashTable.ContainsKey("a"));
    }

    [Test]
    public void ContainsKey_ReturnsTrue_WhenKeyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.IsTrue(hashTable.ContainsKey("a"));
    }

    [Test]
    public void ContainsKey_ReturnsFalse_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>();

        Assert.Throws<ArgumentNullException>(() => hashTable.ContainsKey(null));
    }

    [Test]
    public void Clear_SetsCountToZero()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.AreEqual(0, hashTable.Count);
    }

    [Test]
    public void Clear_RemovesAllElements()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.IsFalse(hashTable.ContainsKey("a"));
    }

    [Test]
    public void Resize_IncreasesCapacity()
    {
        var hashTable = new HashTable<string, int>(4);

        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);

        /// Next Prime number after 4 is 5
        /// Capacity should be 5
        /// After resizing, the capacity should be 10
        Assert.AreEqual(10, hashTable.Capacity);
    }
    [Test]
    public void LoadFactor_ReturnsCorrectValue()
    {
        var hashTable = new HashTable<string, int>(4);

        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);
        Assert.AreEqual(0.75f, hashTable.LoadFactor);
    }

    [Test]
    public void Keys_ReturnsCorrectKeys()
    {
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var keys = new List<int> { 1,2,3 };

        CollectionAssert.AreEquivalent(keys, hashTable.Keys);
    }

    [Test]
    public void Values_ReturnsCorrectValues()
    {
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var values = new List<string> { "one", "two", "three" };

        CollectionAssert.AreEquivalent(values, hashTable?.Values);
    }

    [Test]
    public void Constructor_ThrowsException_WhenCapacityIsZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(0));
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 0));
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, -1));
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 2));
    }

    [Test]
    public void GetIndex_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable.GetIndex(null));
    }

    [Test]
    public void FindEntry_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable.FindEntry(null));
    }

    [Test]
    public void This_Get_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() =>
        {
            var value = hashTable[null];
            Console.WriteLine(value);
        });
    }

    [Test]
    public void This_Set_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable[null] = 1);
    }

    [Test]
    public void This_Get_ReturnsCorrectValue()
    {
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
        Assert.AreEqual(1, hashTable["one"]);
    }

    [Test]
    public void This_Set_UpdatesValue()
    {
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
        hashTable["one"] = 2;
        Assert.AreEqual(2, hashTable["one"]);
    }

    [Test]
    public void This_Set_KeyNotFoundException_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<KeyNotFoundException>(() => hashTable["one"] = 2);
    }

    [Test]
    public void This_Get_KeyNotFoundException_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<KeyNotFoundException>(() => {
            var value = hashTable["one"];
            Console.WriteLine(value);
             });
    }
}
