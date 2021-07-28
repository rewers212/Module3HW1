using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3HW1
{
    public class ItemComparer<T> : IComparer<T>
        where T : NewList<T>
    {
    public int Compare(T x, T y)
    {
        if (x.Count > y.Count)
        {
            return -1;
        }
        else if (x.Count < y.Count)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    }
}
