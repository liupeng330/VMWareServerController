using System;
using System.Collections.Generic;
using System.Text;

namespace AppUtil
{
   public class ArgumentHandlingException: Exception
    {
        public ArgumentHandlingException(String msg)
            : base(msg)
        {            
                Console.WriteLine(msg);
                Console.Write("Press Enter Key To Exit: ");
                Console.ReadLine();
                Environment.Exit(1); 

        }
    }
}
