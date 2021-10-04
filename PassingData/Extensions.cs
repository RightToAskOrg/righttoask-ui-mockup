using System;
using System.Collections.Generic;
using System.Linq;

namespace PassingData
{
    public static class Extensions 
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !(enumerable?.Any() ?? false);

        }
    }
    
}