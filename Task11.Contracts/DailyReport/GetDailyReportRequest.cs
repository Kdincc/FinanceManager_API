﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.DailyReport
{
    public record GetDailyReportRequest(DateOnly Date);
}
