﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Commitee.Controllers
{
    public class CommiteeControllerUtils
    {
        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

    }
}