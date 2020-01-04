﻿using System;
using System.Diagnostics;

namespace Buckets.Containers
{
    abstract class Container
    {
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
            double total = amount + Content; //Calculate total of 
            if (total > Size) //Check overflow
            {
                Console.WriteLine($"{Name} has overflowed {total - Size}.");
                Content = Size;
            }
            else
            {
                Content = total;
            }
        }
        public bool Transfer(Container source, Container target, int amount = null) //Transfer from source to target with possible value
        {
            if(amount == null) { amount = source.Content; } //Check if amount was given, otherwise take amount in source
            if (amount <= source.Content) //Check no overflow
            {
                target.Fill(amount);
                source.Empty(amount);
                return true;
            }
            else { return false; } //Overflow
        }
    }

}
