using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Library.Core
{
    /// <summary>
    /// Стадии пробуждения мертвеца
    /// </summary>
    public enum Stage
    {
        Sleeps = 0,
        Wakes = 1,
        Rises1 = 2,
        Rises2 = 3,
        Rises3 = 4,
        GetsUp = 5,
        GoesOut = 6
    }
}
