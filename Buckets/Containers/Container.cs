using System;
using System.Diagnostics;

namespace Buckets.Containers
{
    abstract class Container
    {
        #region Properties
        static public bool AllowOverflow = false;
        //Decision: Place properties inside base class or derived classes?
        private double _minSize; //Technically only used for bucket. Move to bucket and override the Size property?
        private double _size;
        private double _content;
        public string Name { get; set; }
        protected double MinSize
        {
            get => _minSize;
            set
            {
                if (value < 0)
                    _minSize = 0;
                else
                    _minSize = value;
            }
        }
        protected double Size
        {
            get => _size;
            set
            {
                if (value < MinSize)
                    _size = MinSize;
                else
                    _size = value;
            }
        }
        protected double Content
        {
            get => _content;
            set
            {
                if (value > Size) //OVERFLOW
                {
                    Debug.WriteLine($"{Name} has overflown {value - Size} on creation.");
                    _content = Size;
                }
                else if (value < 0)
                {
                    _content = 0;
                }
                else
                {
                    _content = value;
                }
            }
        }
        #endregion

        #region Empty
        /// <summary>
        /// Empty the container entirely
        /// </summary>
        /// <returns>Returns true if succeeded</returns>
        public bool Empty()
        {
            Content = 0;
            Console.WriteLine($"Emptied {Name}({Content}/{Size}).");
            return true;
        }
        /// <summary>
        /// Empty the container by an amount
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Returns true if succeeded</returns>
        public bool Empty(double amount) //Empty container by amount.
        {
            if (amount > Content) //Check if amount to remove exists
            {
                Console.WriteLine($"Emptying cancelled, not enough ({amount}) in {Name}({Content}/{Size}).");
                return false;
            }
            else
            {
                Content -= amount;
                Console.WriteLine($"Emptied {amount} from {Name}({Content}/{Size}).");
                return true;
            }
        }
        #endregion

        #region Fill
        /// <summary>
        /// Fill up the container entirely
        /// </summary>
        /// <returns>Returns true if succeeded</returns>
        public bool Fill()
        {
            Content = Size;
            Console.WriteLine($"Filled {Name}({Content}/{Size}).");
            return true;
        }
        /// <summary>
        /// Fill up the container by amount
        /// </summary>
        /// <param name="addedAmount">Amount to Add</param>
        /// <returns>Returns true if succeeded</returns>
        public bool Fill(double addedAmount) //Fill container with amount.
        {
            double total = addedAmount + Content; //Calculate total of projected 
            if (total > Size) //Check overflow
            {
                if (AllowOverflow)
                {
                    Content = Size;
                    Console.WriteLine($"Added {addedAmount} to {Name}({Content}/{Size}) and it overflowed by {total - Size}.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Adding {addedAmount} to {Name}({Content}/{Size}) would overflow, action aborted.");
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
        #endregion

        #region Transfer
        /// <summary>
        /// Transfer container content to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="nullableAmount"></param>
        public void Transfer(Container target, double? nullableAmount = null) //Transfer amount to target
        {
            double amount = nullableAmount == null ? this.Content : (double)nullableAmount; //Check if amount was given. Otherwise grab content of source.
            //Method that checks everything in advance, not using existing methods, doubling code
            if (amount <= this.Content) //Check if source has enough
            {
                if (target.Content + amount < target.Size) //Check if no overflow
                {
                    target.Content += amount;
                    this.Content -= amount;
                    Console.WriteLine($"Transferred {amount} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size})."); //Is there a way to be sure doubles get rounded properly when used as string?
                }
                else //It will overflow
                {
                    double overflow = target.Content + amount - target.Size; //Get overflow amount
                    target.Content = target._size;
                    if (AllowOverflow) //Lose the overflow
                    {
                        this.Content -= amount;
                        Console.WriteLine($"Transferred {amount} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size}) and overflowed with {overflow}.");
                    }
                    else //Return the overflow to the source
                    {
                        this.Content = overflow;
                        Console.WriteLine($"Transferred {amount - overflow} from {this.Name}({this.Content}/{this.Size}) to {target.Name}({target.Content}/{target.Size}).");
                    }
                }
            }
            else { Console.WriteLine($"Transfer to {target.Name} cancelled, not enough ({amount}) in {this.Name}({this.Content}/{this.Size})."); }


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
            #endregion

        }
    }

}
