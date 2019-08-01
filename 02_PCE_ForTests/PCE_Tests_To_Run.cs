using PCE_StarterProject;
using System;
using System.IO;
using NUnit.Framework;

/*
 * This file contains all the tests that will be run.
 * 
 * If you want to find out what a test does (or why it's failing), look in here
 * 
 * Former Post-Build Event:
mkdir "$(SolutionDir)01_PCE_Test_Runner\bin"
mkdir "$(SolutionDir)01_PCE_Test_Runner\bin\Debug"
copy "$(TargetPath)" "$(SolutionDir)01_PCE_Test_Runner\bin\Debug"
mkdir "$(SolutionDir)01_PCE_Test_Runner\bin\Release"
copy "$(TargetPath)" "$(SolutionDir)01_PCE_Test_Runner\bin\Release"
 
 * (Probably better to do this with xcopy instead of mkdir + copy
 * 
 */

namespace PCE_StarterProject
{


    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_SmartArray_Basic
    {
        protected SmartArray sa;
        const int SMART_ARRAY_SIZE = 5;
        [SetUp]
        virtual protected void SetUp()
        {
            sa = new SmartArray();
        }

        [Test]
        [Category("SmartArray Basics")]
        public void SetAtIndex_InBounds([Values(0, 1, 2, 3, 4)]int index)
        {
            sa.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Able to set element {0}!", index);
        }

        [Test]
        [Category("SmartArray Basics")]
        [ExpectedException("PCE_StarterProject.OverflowException")]
        public void SetAtIndex_OutOfBounds_Hi([Values(5, 6, 10)]int index)
        {
            sa.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Correctly unable to set element {0}!", index);
        }

        [Test]
        [Category("SmartArray Basics")]
        [ExpectedException("PCE_StarterProject.UnderflowException")]
        public void SetAtIndex_OutOfBounds_Lo([Values(-1, -2, -10)]int index)
        {
            Console.WriteLine("================= SetAtIndex =================");
            sa.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Correctly unable to set element {0}!", index);
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Print_All_Elts_Empty()
        {
            Console.WriteLine("CHECK THIS: SmartArray starts with all zeros");
            TestHelpers th = new TestHelpers();

            th.StartOutputCapturing();
            sa.PrintAllElements();
            String sResult = th.StopOutputCapturing();

            StringWriter sw = new StringWriter();
            for (int i = 0; i < SMART_ARRAY_SIZE; i++)
                sw.WriteLine(0);

            string sCorrect = sw.ToString();

            Console.WriteLine("Expected the output\n" + sCorrect + "\nActually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");

            bool theSame = TestHelpers.EqualsFuzzyString(sCorrect, sResult);
            Assert.That(theSame == true, "Expected the output\n" + sCorrect + "\nBut actually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Print_All_Elts_Set()
        {
            int[] nums = new int[] { 10, 20, 30, 40, 50 };
            for (int i = 0; i < nums.Length; i++)
            {
                sa.SetAtIndex(i, nums[i]);
            }

            TestHelpers th = new TestHelpers();

            th.StartOutputCapturing();
            sa.PrintAllElements();
            String sResult = th.StopOutputCapturing();

            StringWriter sw = new StringWriter();
            for (int i = 0; i < nums.Length; i++)
                sw.WriteLine(nums[i]);

            string sCorrect = sw.ToString();

            Console.WriteLine("Expected the output\n" + sCorrect + "\nActually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");

            bool theSame = TestHelpers.EqualsFuzzyString(sCorrect, sResult);
            Assert.That(theSame == true, "Expected the output\n" + sCorrect + "\nBut actually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");
        }

        [Test]
        [Category("SmartArray Basics")]
        public void GetAtIndex([Values(0, 1, 2, 3, 4)]int index)
        {
            int valueGotten;
            valueGotten = sa.GetAtIndex(index);
            Assert.That(valueGotten == 0,
                "TEST FAILED: UNEXPECTED VALUE FROM SLOT {0}: (EXPECTED 0, GOT {0})", index, valueGotten);

            Console.WriteLine("Test Passed: Able to get expected, UNINITIALIZED value from slot {0}!", index);
        }

        [Test]
        [Category("SmartArray Basics")]
        [ExpectedException("PCE_StarterProject.OverflowException")]
        public void GetAtIndex_OutOfBounds_Hi([Values(5, 6, 10)]int index)
        {
            int valueGotten = sa.GetAtIndex(index);
        }

        [Test]
        [Category("SmartArray Basics")]
        [ExpectedException("PCE_StarterProject.UnderflowException")]
        public void GetAtIndex_OutOfBounds_Lo([Values(-1, -2, -10)]int index)
        {
            int valueGotten = sa.GetAtIndex(index);

            Console.WriteLine("Test Passed: Correctly unable to get element {0}!", index);
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Set_Then_Get([Values(0, 1, 2, 3, 4)]int index)
        {
            int valueToAssign = (index + 1) * 10;
            sa.SetAtIndex(index, valueToAssign);

            int valueGotten = sa.GetAtIndex(index);

            Assert.That(valueGotten == valueToAssign,
                "TEST FAILED: UNEXPECTED VALUE FROM SLOT {0}: (EXPECTED 0, GOT {0})", index, valueGotten);

            Console.WriteLine("Test Passed: Able to get expected, UNINITIALIZED value from slot {0}!", index);
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Set_All_Then_Get_All()
        {
            int valueToAssign;

            for (int i = 0; i < sa.getSize(); i++)
            {
                valueToAssign = (i + 1) * 10;
                sa.SetAtIndex(i, valueToAssign);
            }

            for (int i = 0; i < sa.getSize(); i++)
            {
                valueToAssign = (i + 1) * 10;
                int valueGotten = sa.GetAtIndex(i);
                Assert.That(valueGotten == valueToAssign,
                    "TEST FAILED: UNEXPECTED VALUE FROM SLOT {0}: (EXPECTED 0, GOT {0})", i, valueGotten);
            }
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Find_Zero()
        {
            bool found = sa.Find(0);
            Assert.That(found == true,
                "TEST FAILED: UNABLE TO FIND VALUE 0!");
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Test_Get_Size()
        {
            Assert.That(sa.getSize() == SMART_ARRAY_SIZE,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                SMART_ARRAY_SIZE, sa.getSize());
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Find_Value_Present([Values(0, 1, 2, 3, 4)]int index, [Values(-10, 10, 200, 20)]int val)
        {
            bool found;
            sa.SetAtIndex(index, val);

            found = sa.Find(val);
            Assert.That(found == true, "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                val, index);
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Set_All_Then_Find()
        {
            int valueToAssign;
            for (int i = 0; i < sa.getSize(); i++)
            {
                valueToAssign = (i + 1) * 10;
                sa.SetAtIndex(i, valueToAssign);
            }

            for (int i = 0; i < sa.getSize(); i++)
            {
                valueToAssign = (i + 1) * 10;
                bool found = sa.Find(valueToAssign);
                Assert.That(found == true,
                    "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                    valueToAssign, i);
            }
        }

        [Test]
        [Category("SmartArray Basics")]
        public void Find_Value_Absent([Values(-10, 10, 200, 20)]int val)
        {
            bool found;

            found = sa.Find(val);
            Assert.That(found == false, "TEST FAILED: Found non-existent value {0} in the array",
                val);
        }
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_Stack_SmartArray_Basic : Test_SmartArray_Basic
    {
        // [SetUp]
        //override protected void SetUp()
        //{
        //    sa = new StackOfInts();
        //}
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_StackOfInts
    {
        StackOfInts soi;
        [SetUp]
        protected void SetUp()
        {
            soi = new StackOfInts();
        }

        [Test]
        [Category("StackOfInts")]
        public void Test_IsEmpty_After_Creation()
        {
            Assert.That(soi.isEmpty() == true, "TEST FAILED: NEWLY CREATED STACK IS NOT EMPTY!!");
        }

        [Test]
        [Category("StackOfInts")]
        public void Test_IsEmpty_After_Push()
        {
            soi.Push(10); // Test will fail if exception is thrown

            Assert.That(soi.isEmpty() == false, "TEST FAILED: STACK isEmpty AFTER PUSHING AN ELEMENT ONTO IT!!");
        }

        [Test]
        [Category("StackOfInts")]
        public void Test_IsEmpty_After_Push_Pop()
        {
            soi.Push(11);
            soi.Pop();

            Assert.That(soi.isEmpty() == true, "TEST FAILED: STACK isEmpty AFTER REMOVING LAST ELEMENT!");
        }

        [Test]
        [Category("StackOfInts")]
        public void Test_IsEmpty_After_Multiple_PushPops()
        {
            int valueGotten;

            soi.Push(11);

            soi.Pop(); // ignore the return value

            Assert.That(soi.isEmpty() == true, "TEST FAILED: STACK isEmpty AFTER REMOVING LAST ELEMENT!");


            // This would be a better test if it was randomized, and the net
            // number of elements tracked.
            soi.Push(3);
            soi.Push(2);
            soi.Push(1);

            valueGotten = soi.Pop();
            Assert.That(valueGotten == 1, "TEST FAILED: Should have popped 1 from stack (first time)!");

            valueGotten = soi.Pop();
            Assert.That(valueGotten == 2, "TEST FAILED: Should have popped 2 from stack!");

            soi.Push(1);
            soi.Push(1);

            valueGotten = soi.Pop();
            Assert.That(valueGotten == 1, "TEST FAILED: Should have popped 1 from stack (second time)!");

            // pushed 5 value on, popped 3 values off ==> stack should NOT be empty

            Assert.That(soi.isEmpty() == false, "TEST FAILED: STACK isEmpty DESPITE HAVING 3 ELEMENTS LEFT!!");
        }

        [Test]
        [Category("StackOfInts")]
        public void Test_PushPopPeek_Basic()
        {
            int valueFromStack = 1;
            int valueToPush = 1;

            // Basic test: add one, peek, remove one
            soi.Push(valueToPush);

            valueFromStack = soi.Peek();
            Assert.That(valueFromStack == valueToPush,
                "TEST FAILED: Peeked at stack: Expected to get the value {0} from stack, actually got {1}!",
                valueToPush, valueFromStack);


            valueFromStack = soi.Pop();
            Assert.That(valueFromStack == valueToPush,
                "TEST FAILED: Popped stack: Expected to get the value {0} from stack, actually got {1}!",
                valueToPush, valueFromStack);
        }
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_Queue_SmartArray_Basic : Test_SmartArray_Basic
    {
        //[SetUp]
        //override protected void SetUp()
        //{
        //    sa = new QueueOfInts();
        //}
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_QueueOfInts
    {
        QueueOfInts qoi;

        [SetUp]
        protected void SetUp()
        {
            qoi = new QueueOfInts();
        }

        [Test]
        [Category("QueueOfInts")]
        public void Test_IsEmpty()
        {
            Assert.That(qoi.isEmpty() == true, "TEST FAILED: NEWLY CREATED Queue IS NOT EMPTY!!");
        }

        [Test]
        [Category("QueueOfInts")]
        public void Test_IsEmpty_After_Enqueue()
        {
            qoi.Enqueue(10);

            Assert.That(qoi.isEmpty() == false, "TEST FAILED: Queue isEmpty AFTER EnqueueING AN ELEMENT ONTO IT!!");
        }

        [Test]
        [Category("QueueOfInts")]
        public void Test_IsEmpty_After_EnqueueDequeue()
        {
            int valueToEnqueue;
            int valueFromQueue;

            valueToEnqueue = 10;

            qoi.Enqueue(valueToEnqueue);

            valueFromQueue = qoi.Dequeue();

            Assert.That(qoi.isEmpty() == true, "TEST FAILED: STACK isEmpty AFTER REMOVING LAST ELEMENT!");
        }

        [Test]
        [Category("QueueOfInts")]
        public void Test_IsEmpty_After_Multiple_EnqueuesDequeues()
        {
            int valueFromQueue;
            int valueExpected;

            // This would be a better test if it was randomized, and the net
            // number of elements tracked.
            qoi.Enqueue(3);
            qoi.Enqueue(2);
            qoi.Enqueue(1);

            valueFromQueue = qoi.Dequeue();
            valueExpected = 3;
            Assert.That(valueFromQueue == valueExpected, "TEST FAILED: When dequeuing, expected to get " + valueExpected + ", but actually got {0}", valueFromQueue);

            valueFromQueue = qoi.Dequeue();
            valueExpected = 2;
            Assert.That(valueFromQueue == valueExpected, "TEST FAILED: When dequeuing, expected to get " + valueExpected + ", but actually got {0}", valueFromQueue);

            qoi.Enqueue(1);
            qoi.Enqueue(1);

            valueFromQueue = qoi.Dequeue();
            valueExpected = 1;
            Assert.That(valueFromQueue == valueExpected, "TEST FAILED: When dequeuing, expected to get " + valueExpected + ", but actually got {0}", valueFromQueue);

            // Enqueueed 5, Dequeueped 3, should NOT be empty

            Assert.That(qoi.isEmpty() == false, "TEST FAILED: Queue isEmpty DESPITE HAVING 3 ELEMENTS LEFT!!");
        }

        [Test]
        [Category("QueueOfInts")]
        public void Test_EnqueuePeekDequeue_Basic()
        {
            int valueFromQueue = -100;
            int valueToEnqueue;

            // Basic test: add one, peek, remove one
            valueToEnqueue = 1;

            qoi.Enqueue(valueToEnqueue);

            valueFromQueue = qoi.Peek();
            Assert.That(valueFromQueue == valueToEnqueue,
                "TEST FAILED: Peeked at quque: Expected to get the value {0}, actually got {1}!",
                valueToEnqueue, valueFromQueue);

            valueFromQueue = qoi.Peek();
            Assert.That(valueFromQueue == valueToEnqueue,
                "TEST FAILED: Dequeued queue: Expected to get the value {0}, actually got {1}!",
                valueToEnqueue, valueFromQueue);

        }

        //[Test]
        //[Category("QueueOfInts")]
        //public void Test_EnqueuePeekDequeue_CanOverflow()
        //{
        //    ErrorCode ec;

        //    // Can we overflow the Queue?
        //    for (int i = 0; i < qoi.getSize() + 2; i++)
        //    {
        //        ec = qoi.Enqueue(i);

        //        if (i < qoi.getSize())
        //            Assert.That(ec == ErrorCode.NoError, "TEST FAILED: Unable to Enqueue " + i + " onto Queue!");
        //        else
        //            Assert.That(ec == ErrorCode.Overflow, "TEST FAILED: Enqueueed more items onto Queue than Queue has capacity for!");
        //    }
        //    Console.WriteLine("Test Passed: Unable to Enqueue more items onto Queue than Queue has capacity for!!");
        //}

        //[Test]
        //[Category("QueueOfInts")]
        //public void Test_EnqueuePeekDequeue_Check_Upper_Boundaries()
        //{
        //    ErrorCode ec;
        //    int valueGotten = -100;

        //    // Can we overflow the Queue?
        //    for (int i = 0; i < qoi.getSize(); i++)
        //    {
        //        ec = qoi.Enqueue(i);
        //        Assert.That(ec == ErrorCode.NoError, "TEST FAILED: Unable to Enqueue " + i + " onto Queue!");
        //    }

        //    // check the results, again:
        //    for (int i = 0; i < qoi.getSize(); i++)
        //    {
        //        // Can we peek at it?
        //        ec = qoi.Peek(out valueGotten);
        //        Assert.That(ec == ErrorCode.NoError, "TEST FAILED: Unable to peek value from Queue!");
        //        Assert.That(valueGotten == i,
        //            "TEST FAILED: Peeked at quque: Expected to get the value {0}, actually got {1}!",
        //            i, valueGotten);

        //        // Can we dequeue it?
        //        ec = qoi.Dequeue(out valueGotten);
        //        Assert.That(ec == ErrorCode.NoError, "TEST FAILED: Unable to dequeue value from Queue!");
        //        Assert.That(valueGotten == i,
        //            "TEST FAILED: Dequeued front of quque: Expected to get the value {0}, actually got {1}!",
        //            i, valueGotten);
        //    }

        //    Assert.That(qoi.isEmpty() == true, "TEST FAILED: AFTER REMOVING ALL ITEMS, Queue CLAIMS TO BE NON-EMPTY!");

        //    ec = qoi.Peek(out valueGotten);
        //    Assert.That(ec == ErrorCode.Underflow, "TEST FAILED: Able to peek at an empty Queue!");
        //    Assert.That(valueGotten == Int32.MinValue, "TEST FAILED: Expected to see (Int32.MinValue: {0}) when Dequeueping an empty Queue, but actually got {1}!", Int32.MinValue, valueGotten);

        //    ec = qoi.Dequeue(out valueGotten);
        //    Assert.That(ec == ErrorCode.Underflow, "TEST FAILED: Able to Dequeue more items from Queue than are present!");
        //    Assert.That(valueGotten == Int32.MinValue, "TEST FAILED: Expected to see (Int32.MinValue: {0}) when Dequeueping an empty Queue, but actually got {1}!", Int32.MinValue, valueGotten);
        //}
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_SmartArrayResizable_Basic : Test_SmartArray_Basic
    {
        [SetUp]
        override protected void SetUp()
        {
            sa = new SmartArrayResizable();
        }
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_SmartArrayResizable
    {
        protected SmartArrayResizable sar;
        const int SMART_ARRAY_STARTING_SIZE = 5;

        [SetUp]
        virtual protected void SetUp()
        {
            sar = new SmartArrayResizable();
        }

        // The following were tested by the 'Test_SmartArrayResizable_Basic' class:
        //      SetAtIndex_InBounds([Values(0, 1, 2, 3, 4)]int index)
        //      SetAtIndex_OutOfBounds_Lo([Values(-1, -2, -10)]int index)

        [Test]
        [Category("SmartArrayResizable")]
        public void SetAtIndex_OutOfBounds_Hi([Values(SMART_ARRAY_STARTING_SIZE, SMART_ARRAY_STARTING_SIZE + 1, SMART_ARRAY_STARTING_SIZE + 20)]
            int index)
        {
            // Confirm that index is out of bounds, then 
            // resize, and confirm that index is now ok
            try
            {
                sar.SetAtIndex(index, 10);
                throw new Exception("TEST FAILED: When SetAtIndex is given an out-of-bounds index, it should throw an OverflowException (but it didn't!)");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Caught the expected OverflowException (this is a good thing :)  )");
            }

            sar.Resize(index + 1); // Note that this will catch 'off by one' errors, if you allocate
                                   // just a bit too little memory :)
            sar.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Correctly able to set element {0} after resizing!", index);
        }

        // The following were tested by the 'Test_SmartArrayResizable_Basic' class:
        //      Print_All_Elts_Empty
        //      Print_All_Elts_Set
        //      Set_All_Then_Get_All

        [Test]
        [Category("SmartArrayResizable")]
        public void Print_All_Elts_Resized_Then_Set()
        {
            int[] nums = new int[] { 10, 20, 30, 40, 50 };
            int[] extra = new int[] { 100, 90, 80, 70, 60 };

            // first, fill up the starting array
            for (int i = 0; i < nums.Length; i++)
            {
                sar.SetAtIndex(i, nums[i]);
            }

            Console.WriteLine("Finished initial fill!");

            // second, resize the array.  This should preserve the values we put into it, above
            sar.Resize(sar.getSize() + extra.Length);

            Console.WriteLine("Resized the array");


            // third, add some more values to the new space in the array
            for (   int i = sar.getSize() - extra.Length, j = 0; 
                    i < sar.getSize(); 
                    i++, j++)
            {
                Console.WriteLine("rgNums[" + i + "] = "+extra[j] +" ( which is extra[" + j + "] )");

                sar.SetAtIndex(i, extra[j]);
            }

            TestHelpers th = new TestHelpers();

            th.StartOutputCapturing();
            sar.PrintAllElements();
            String sResult = th.StopOutputCapturing();

            StringWriter sw = new StringWriter();
            for (int i = 0; i < nums.Length; i++)
                sw.WriteLine(nums[i]);
            for (int i = 0; i < extra.Length; i++)
                sw.WriteLine(extra[i]);

            string sCorrect = sw.ToString();

            Console.WriteLine("Expected the output\n" + sCorrect + "\nActually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");

            bool theSame = TestHelpers.EqualsFuzzyString(sCorrect, sResult);
            Assert.That(theSame == true, "Expected the output\n" + sCorrect + "\nBut actually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");
        }

        // The following were tested by the 'Test_SmartArrayResizable_Basic' class:
        //      GetAtIndex([Values(0, 1, 2, 3, 4)]int index)
        //      GetAtIndex_OutOfBounds_Lo([Values(-1, -2, -10)]int index)
        //      Set_Then_Get([Values(0, 1, 2, 3, 4)]int index)

        [Test]
        [Category("SmartArrayResizable")]
        public void GetAtIndex_OutOfBounds_Hi([Values(5, 6, 10)]int index)
        {
            int valueGotten;

            try
            {
                valueGotten = sar.GetAtIndex(index);
                throw new Exception("TEST FAILED: When GetAtIndex is given an out-of-bounds index, it should throw an OverflowException (but it didn't!)");
            }
            catch(OverflowException e)
            {
                Console.WriteLine("Caught the expected OverflowException (this is a good thing :)  )");
            }
            sar.Resize(index + 1); // Note that this will catch 'off by one' errors, if you allocate
            // just a bit too little memory :)

            sar.SetAtIndex(index, 10);

            valueGotten = sar.GetAtIndex(index);
            Assert.That(valueGotten == 10, "TEST FAILED: When GetAtIndex is given an out-of-bounds index, valueGotten should be {0}.  Instead, it is: {1}",
                10, valueGotten);

            Console.WriteLine("Test Passed: Correctly able to set element {0} after resizing!", index);
        }


        // The following were tested by the 'Test_SmartArrayResizable_Basic' class:
        //      Test_Get_Size()

        [Test]
        [Category("SmartArrayResizable")]
        public void Test_Get_Size_After_Resize()
        {
            Assert.That(sar.getSize() == SMART_ARRAY_STARTING_SIZE,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                SMART_ARRAY_STARTING_SIZE, sar.getSize());

            int newSize = sar.getSize() + 10;
            sar.Resize( newSize);

            Assert.That(sar.getSize() == newSize,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                newSize, sar.getSize());
        }

        // The following were tested by the 'Test_SmartArrayResizable_Basic' class:
        //      Find_Zero()
        //      Set_All_Then_Find()

        [Test]
        [Category("SmartArrayResizable")]
        public void Find_Value_Present_After_Resize([Values(SMART_ARRAY_STARTING_SIZE, SMART_ARRAY_STARTING_SIZE + 1, SMART_ARRAY_STARTING_SIZE + 20)]
            int index, [Values(-10, 10, 200, 20)]int val)
        {
            bool found;

            found = sar.Find(val);
            Assert.That(found == false, "TEST FAILED: Find value {0} in the array, even though the array is empty",
                val);

            try
            {
                sar.SetAtIndex(index, val);
                throw new Exception("TEST FAILED: When SetAtIndex is given an out-of-bounds index, it should throw an OverflowException (but it didn't!)");
            }
            catch ( OverflowException e)
            {
                Console.WriteLine("Caught the expected OverflowException (this is a good thing :)  )");
            }

            sar.Resize(index + 1);

            found = sar.Find(val);
            Assert.That(found == false, "TEST FAILED: Find value {0} in the array, even though the array is empty (after resize)",
                val);

            sar.SetAtIndex(index, val);

            found = sar.Find(val);
            Assert.That(found == true, "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                val, index);
        }

        [Test]
        [Category("SmartArrayResizable")]
        public void Find_Value_Absent_After_Resize([Values(-10, 10, 200, 20)]int val)
        {
            bool found;

            found = sar.Find(val);
            Assert.That(found == false, "TEST FAILED: Found non-existent value {0} in the array",
                val);

            sar.Resize(sar.getSize() + 10);

            found = sar.Find(val);
            Assert.That(found == false, "TEST FAILED: Find value {0} in the array, even though the array is empty (after resize)",
                val);
        }
    }

}