using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyTimer
{
    public struct MyTimerModel : IComparer<MyTimerModel>
    {
        public object obj { get; set; }
        public DateTime whenNotify { get; set; }

        public int Compare([AllowNull] MyTimerModel x, [AllowNull] MyTimerModel y)
        {
            if (x.whenNotify > y.whenNotify)
                return 1;
            else if (x.whenNotify < y.whenNotify)
                return -1;
            else
                return 0;
        }
    }
}
