using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;
using Task11.Infrastructure.Persistence;

namespace Task11.IntegrationTests
{
    public static class SeedData
    {
        private readonly static IncomeTypeId[] incomeTypeIds =
        [
            IncomeTypeId.CreateUniq(),
            IncomeTypeId.CreateUniq(),
            IncomeTypeId.CreateUniq(),
        ];

        private readonly static ExpenseTypeId[] expenseTypeIds =
        [
            ExpenseTypeId.CreateUniq(),
            ExpenseTypeId.CreateUniq(),
            ExpenseTypeId.CreateUniq(),
        ];

        private static void ClearData(FinanceDbContext dbContext)
        {
            dbContext.IncomeFinanceOperations.ToList().ForEach(i => dbContext.IncomeFinanceOperations.Remove(i));
            dbContext.ExpenseFinanceOperations.ToList().ForEach(e => dbContext.ExpenseFinanceOperations.Remove(e));
            dbContext.IncomeTypes.ToList().ForEach(i => dbContext.IncomeTypes.Remove(i));
            dbContext.ExpenseTypes.ToList().ForEach(e => dbContext.ExpenseTypes.Remove(e));

            dbContext.SaveChanges();
        }

        public static void SeedDb(FinanceDbContext dbContext)
        {
            ClearData(dbContext);

            SeedExpenseTypes(dbContext);
            SeedIncomeTypes(dbContext);
            SeedIncomeFinanceOperations(dbContext);
            SeedExpenseFinanceOperations(dbContext);
        }

        private static void SeedIncomeTypes(FinanceDbContext dbContext)
        {
            dbContext.IncomeTypes.AddRange(new List<IncomeType>
            {
                new(incomeTypeIds[0], "Work", "Programmer"),
                new(incomeTypeIds[1], "Bank", "Deposit"),
                new(incomeTypeIds[2], "Gift", "Birthday gift"),
            });

            dbContext.SaveChanges();
        }

        private static void SeedIncomeFinanceOperations(FinanceDbContext dbContext)
        {
            dbContext.IncomeFinanceOperations.AddRange(new List<IncomeFinanceOperation>
            {
                new(IncomeFinanceOperationId.CreateUniq(), new DateOnly(2021, 10, 10), incomeTypeIds[0],  Amount.Create(1000), "Salary!!!"),
                new(IncomeFinanceOperationId.CreateUniq(), new DateOnly(2022, 10, 11), incomeTypeIds[1],  Amount.Create(200), "Money!!!"),
                new(IncomeFinanceOperationId.CreateUniq(), new DateOnly(2021, 11, 12), incomeTypeIds[2],  Amount.Create(300), "My birtday!!!"),
            });

            dbContext.SaveChanges();
        }

        private static void SeedExpenseTypes(FinanceDbContext dbContext)
        {
            dbContext.ExpenseTypes.AddRange(new List<ExpenseType>
            {
                new(expenseTypeIds[0], "Food", "Bought some food"),
                new(expenseTypeIds[1], "Transport", "Bought some tickets"),
                new(expenseTypeIds[2], "Entertainment", "Bought some games"),
            });

            dbContext.SaveChanges();
        }

        private static void SeedExpenseFinanceOperations(FinanceDbContext dbContext)
        {
            dbContext.ExpenseFinanceOperations.AddRange(new List<ExpenseFinanceOperation>
            {
                new(ExpenseFinanceOperationId.CreateUniq(), new DateOnly(2021, 10, 10), expenseTypeIds[0],  Amount.Create(100), "Bought some food"),
                new(ExpenseFinanceOperationId.CreateUniq(), new DateOnly(2022, 10, 11), expenseTypeIds[1],  Amount.Create(200), "Bought some tickets"),
                new(ExpenseFinanceOperationId.CreateUniq(), new DateOnly(2021, 11, 12), expenseTypeIds[2],  Amount.Create(300), "Bought some games"),
            });

            dbContext.SaveChanges();
        }
    }
}
