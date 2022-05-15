using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.UnitTests
{
    public class CollectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            var nums = new Collection<int>();

            Assert.That(nums.Count == 0, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[]");
        }
        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var nums = new Collection<int>(5);

            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[5]");
        }
        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            var nums = new Collection<int>(5, 4);

            Assert.That(nums.Count == 2, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[5, 4]");
        }
        [Test]
        public void Test_Collection_Add()
        {
            var nums = new Collection<int>();

            nums.Add(9);

            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[9]");
        }
        [Test]
        public void Test_Collection_AddRange()
        {
            var items = new int[] { 6, 7, 8 };
            var nums = new Collection<int>();

            nums.AddRange(items);

            Assert.That(nums.Count == 3, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[6, 7, 8]");
        }
        [Test]
        public void Test_Collection_AddWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;

            var newNums = System.Linq.Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");
            // Act
            var item0 = names[0];
            var item1 = names[1];
            // Assert
            Assert.That(item0, Is.EqualTo("Peter"));
            Assert.That(item1, Is.EqualTo("Maria"));
        }
        [Test]
        public void TestCollectionGetByInvalidIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");

            // Assert
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria]"));
        }
        [Test]
        public void Test_Collection_SetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");
            // Act
            var item0 = names[0];
            // Assert
            Assert.That(item0, Is.EqualTo("Peter"));
        }
        [Test]
        public void Test_Collection_InsertAtStart()
        {
            // Arrange         
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums.InsertAt(0, 0);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[0, 1, 2, 3, 4, 5]")); ;

        }
        [Test]
        public void Test_Collection_InsertAtEnd()
        {   // Arrange         
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums.InsertAt(5, 6);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4, 5, 6]")); ;
        }
        [Test]
        public void Test_Collection_ExchangeMiddle()
        {   // Arrange         
            var nums = new Collection<int>(1, 2, 3, 4, 5);
            var item0 = nums[0];
            var item1 = nums[1];

            // Act
            nums.Exchange(item1, item0);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 3, 2, 4, 5]")); ;
        }
        [Test]
        public void Test_Collection_RemoveAtEnd()
        {   // Arrange         
            var nums = new Collection<int>(1, 2, 3, 4, 5);
            
            // Act
            nums.RemoveAt(4);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4]"));
        }
        [Test]
        public void Test_Collection_Clear()
        {   // Arrange         
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums.Clear();

            // Assert
            Assert.That(nums.Count == 0);
        }
        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            // Arrange
            const int itemsCount = 1000000;
            var nums = new Collection<int>();

            // Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            // Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }
        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter");

            // Assert
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

    }
}