using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemAndSolution.Infrastructure
{
  public  class DuplicationException:Exception
    {
        //public string DuplicateItemName { get; private set; }

        public DuplicationException(string message) : base(message)
        {
           // DuplicateItemName = itemName;
        }
    }
}
