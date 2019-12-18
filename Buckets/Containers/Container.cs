using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buckets.Containers
{
    abstract class Container
    {
        public delegate void OverflowDel(string msg); //Event?
        OverflowDel OFD = Overflow;

        #region Properties
        //Decision: Place properties inside base class or derived classes?
        private double minSize; //Technically only used for bucket. Move to bucket and override the Size property?
        private double size;
        private double content;
        public string Name { get; set; }
        protected double MinSize
        {
            get { return minSize; }
            set
            {
                if (MinSize < 0)
                    minSize = 0;
                else
                    minSize = MinSize;
            }
        }
        public double Size
        {
            get { return size; }
            set
            {
                if (Size < MinSize)
                    size = MinSize;
                else
                    size = Size;
            }
        }
        public double Content
        {
            get { return content; }
            set
            {
                if (Content > Size) //OVERFLOW
                {
                    Debug.WriteLine($"{Name} has overflown {Content - Size} on creation.");
                    content = Size;
                }
                else if (Content < 0)
                {
                    content = 0;
                }
                else
                {
                    content = Content;
                }
            }
        }
        #endregion

        #region Emptying
        public bool Empty()
        {
            Content = 0;
            return true;
        }
        public bool Empty(double amount)
        {
            if (amount > Content)
            {
                //Emptied too much
                Content = 0;
                return false;
            }
            else
            {
                Content -= amount;
                return true;
            }
        }
        #endregion

        public void Fill(double amount)
        {
            double total = amount + Content;
            if (total > Size) //Overflow
            {
                OFD($"{Name} has overflowed {total - Size}.");
                Content = Size;
            }
            else
            {
                Content = total;
            }
        }
        public static void Overflow(string msg) //Delegate?
        {
            Console.WriteLine(msg);
        }
    }

}
