﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task11.Contracts.IncomeType
{
    public record UpdateExpenseTypeRequest(string Id, string Name, string Description);
}
