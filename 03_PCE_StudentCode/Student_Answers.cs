#define TESTING
using System;

/*
 * STUDENTS: Your answers (your code) goes into this file!!!!
 * 
 * NOTE: In addition to your answers, this file also contains a 'main' method, 
 *      in case you want to run a normal console application.
 * 
 * If you want to / have to put something into 'Main' for these PCEs, then put that 
 * into the Program.Main method that is located below, 
 * then select this project as startup object 
 * (Right-click on the project, then select 'Set As Startup Object'), then run it 
 * just like any other normal, console app: use the menu item Debug->Start Debugging, or 
 * Debug->Start Without Debugging
 * 
 */

namespace PCE_StarterProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Use this for any code that you want to
			// put into main, and run as a stand-alone console applicatiion
			// (WITHOUT any tests).
			// Remember to right-click on the 03_PCE_StudentCode project
			// (in the Solution Explorer - View->Solution Explorer if you don't
			// see the panel on the right), and the select 'Set As Startup Project'
		}
	}

	public class StackOfInts : SmartArray // this must inherit from the SmartArray class
    // ORIG: public class StackOfInts // this must inherit from the SmartArray class
	{
		/// <summary>
		/// topOfStack will be the index of the NEXT space that will be used
		/// So it therefore starts out at 0, meaning that 0 is UNoccupied.
		/// </summary>
		protected int topOfStack = 0;
		public StackOfInts():base()
		{
		}

        //O(1) because it is accessing one element in the array.
        public bool isEmpty()
        {
             if (GetAtIndex(topOfStack) <= 0)
                    return true;
              else
                    return false;
        }
        //O(1) because it accesses one element in the array, to set a value. 
        public void Push(int item)
        {
            if (topOfStack == getSize())
                throw new OverflowException("Wrong");

            topOfStack++;
            SetAtIndex(topOfStack, item);
             
           
        }
        //O(1) because it just acesses one element in the array
		public int Peek()
		{
            if (isEmpty())
                throw new UnderflowException("Wrong");
            else
                return GetAtIndex(topOfStack);
        }
       
        
        //O(1) because it just acesses one element in the array
        public int Pop()
        {
            if (isEmpty())
                throw new UnderflowException("Wrong");
            else
            {
                topOfStack--;

               return GetAtIndex(topOfStack+1);
                
            }
          
        }

	}

	// You will need to add in your ErrorCode enum, too

    public class UnderflowException : Exception
    {
        public UnderflowException(string s) : base(s) { }
    }
    public class OverflowException : Exception
    {
        public OverflowException(string s) : base(s) { }
    }

   public class SmartArray
    {
       protected int[] rgNums = new int [5];

        public SmartArray()
        {
            rgNums = new int[5];
        }
        //O(1) just accesses one array element to change it
        public void SetAtIndex(int idx, int val)
        {
            Console.WriteLine("Called with idx={0} (and val={1}  .Length={2}", idx, val, rgNums.Length);
            if (idx < 0)
                throw new UnderflowException("Index value is too low");
            else if (idx > rgNums.Length-1)
                throw new OverflowException("Index value is too high");
            else
            rgNums[idx] = val;
        }
        //O(1) just accesses one array element to change it
        public int GetAtIndex(int idx)
        {
            
            if (idx < 0)
                throw new UnderflowException("Index value is too low");
            if (idx > rgNums.Length-1)
                throw new OverflowException("Index value is too high");

            return rgNums[idx];
        }
        //O(n) tries all elements in the array.
        public void PrintAllElements()
        {
            for (int i = 0; i < rgNums.Length; i++)
            { Console.WriteLine(rgNums[i]); }

        }
        //O(n) tries all elements in the array.
        public bool Find(int val)
        {
            bool x = false;
            for (int i = 0; i < rgNums.Length; i++)
            {
                if (rgNums[i] == val) { x = true; break; }

                x = false;
            }
            return x;
        }
        public int getSize()
        {
            return rgNums.Length;
        }
    }
    

    public class QueueOfInts : SmartArray
	{
		protected int count = 0;
		protected int frontOfQueue = 0;

		// backOfQueue will be the index of the NEXT space that will be used
		// So it therefore starts out at 0, meaning that 0 is UNoccupied.
		protected int backOfQueue = 0;

		public QueueOfInts()
			: base()
		{
		}
        //O(1) it only checks if there are no elements
		public bool isEmpty()
		{
            if (GetAtIndex(frontOfQueue) <= 0)
                return true;
            else
                return false;
        }
        //O(1) just accesses one element in the array to change it
		public void Enqueue(int item)
		{
            if (count == getSize())
                throw new OverflowException("Wrong");
           

            count++;
            backOfQueue++;
            if (count == getSize())
            { backOfQueue = 0; /*frontOfQueue++;*/ }
           
            SetAtIndex(backOfQueue-1, item);

        }
        //O(1) just accesses one element in the array to display it.
        public int Peek()
		{   if (GetAtIndex(frontOfQueue) == 0)
                throw new UnderflowException("Wrong");
            return GetAtIndex(frontOfQueue);
        }
        //O(1) just accesses one element in the array to change it.
		public int Dequeue()
		{
            if (GetAtIndex(frontOfQueue) == 0)
                throw new UnderflowException("Wrong");

            count--;
            frontOfQueue++;

            

            return GetAtIndex(frontOfQueue-1);


        }

        // While not required, you may find this useful for your own debugging 
        public void PrintQState(string fnx)
		{
			//Console.WriteLine(" QUEUE STATE: =======================");
			//Console.WriteLine(" Calling {0}", fnx);
			//Console.WriteLine("count:{0}", count);
			//Console.WriteLine("this.getSize:{0}", this.getSize());
			//Console.WriteLine("frontOfQueue:{0}", frontOfQueue);
			//Console.WriteLine("backOfQueue:{0}", backOfQueue);

			//int val;
			//for (int i = 0; i < this.getSize(); i++)
			//{
			//    this.GetAtIndex(i, out val);
			//    Console.WriteLine("\tIndex: {0}\tValue:{1}", i, val);
			//}
		}

	}

	public class SmartArrayResizable : SmartArray
	{
        

		public SmartArrayResizable()
		{
          
        }
        //O(n) runs through all elements in the array.
		public void Resize(int newSize)
		{ 
            int[] newArray = new int[newSize];
            for(int i =0; i<newArray.Length;i++)
            {
                if (i < rgNums.Length)
                    newArray[i] = rgNums[i];
                else
                    newArray[i] = 0;
            }
            rgNums = newArray;

          
        }




    }

	class What_Is_A_Generic
	{
		// Put your comment here
	}

	// Your job is to finish this class, by implementing the three missing methods:
	class BasicGeneric<T>
	{
		// T storedItem;

		// You'll need to add a
		// SetItem method

		// You'll need to add a
		// GetItem method

		// You'll need to add a
		// Print method


		// And that's it!
	}

	class ProgramLogic
	{
		private int theData;
		public ProgramLogic()
		{
			theData = 0;
		}

		public void PrintData()
		{
			Console.WriteLine(theData);
		}

		// Method is defined to be virtual in the base class
		public override string ToString()
		{
			return "ProgramLogic object containing: " + theData.ToString();
		}

		public int Data
		{
			get
			{
				return theData;
			}
			set
			{
				theData = value;
			}
		}
	}
}