﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task11.Contracts.IncomeType
{
    public record UpdateIncomeTypeResponse(Guid Id, string Name, string Description);
}
