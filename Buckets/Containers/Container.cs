using System;
using System.Diagnostics;

namespace Buckets.Containers
{
    abstract class Container
    {
        #region Properties
        static public bool AllowOverflow = false;
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
            if (amount > Content) //Check if amount to remove exists
            {
                return false;
            }
            else
            {
                Content -= amount;
                return true;
            }
        }
        #endregion

        public bool Fill(double addedAmount)
        {
            double total = addedAmount + Content; //Calculate total of 
            if (total > Size) //Check overflow
            {
                if (AllowOverflow)
                {
                    Console.WriteLine($"{this.Name}({this.Content}/{this.Size}) has overflowed {total - Size}.");
                    Content = Size;
                    return true;
                }
                else
                {
                    Console.WriteLine($"{this.Name}({this.Content}/{this.Size}) would overflow, action aborted.");
                    return false;
                }
            }
            else
            {
                Content = total;
                Console.WriteLine($"Added {addedAmount} to {Name}({Content}/{Size}).");
                return true;
            }
        }
        public Transfer(Container target, int movedAmount = null) //Transfer amount to target
        {
            if (movedAmount == null) { movedAmount = this.Content; } //Check if amount was given, otherwise take amount in source
            //Method that checks everything in advance, not using existing methods, doubling code
            if (movedAmount > this.Content) //Check if source has enough
            {
                if (target.Content + movedAmount < target.Size) //Check if no overflow
                {
                    target.Content += movedAmount;
                    this.Content -= movedAmount;
                    Console.WriteLine($"Moved {movedAmount} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size})."); //Is there a way to be sure doubles get rounded properly when used as string?
                }
                else //It will overflow
                {
                    double overflow = target.Content + movedAmount - target.Size; //Get overflow amount
                    target.Content = target.size;
                    if (AllowOverflow) //Lose the overflow
                    {
                        this.Content -= movedAmount;
                        Console.WriteLine($"Moved {movedAmount} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size}) and overflowed with {overflow}.");
                    }
                    else //Return the overflow to the source
                    {
                        this.Content = overflow;
                        Console.WriteLine($"Moved {movedAmount - overflow} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size})."); 
                    }
                }
            }
            else { Console.WriteLine($"Transfer cancelled, not enough ({movedAmount}) in {this.Name}({this.Content}/{this.Size})."); }


            //Method that uses other methods, and undoes method if later methods fail. 
            //OUTDATED, does not include all possibilities
            /*if (this.Empty(amount))
            {
                if (target.Fill(this.content))
                {
                    Console.WriteLine($"Moved {amount} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size}).");
                }
                else
                {
                    this.Fill(amount);
                }
            }
            else
            {
                Console.WriteLine($"Transfer Failed, {this.Name}({this.Content}/{this.Size}) does not have {amount}");
            }*/
        }
    }

}
