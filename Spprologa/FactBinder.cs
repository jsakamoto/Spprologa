using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Spprologa
{
    public class FactBinder
    {
        public FactBinder()
        {

        }

        public string this[string query]
        {
            get
            {
                Console.WriteLine($"factbinder[\"{query}\"].get()");
                return "";
            }
            set
            {
                Console.WriteLine($"factbinder[\"{query}\"].set({value})");

            }
        }
    }
}
