﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.ExpenseType
{
    public record DeleteExpenseTypeResponse(Guid Id, string Name, string Description);
}
