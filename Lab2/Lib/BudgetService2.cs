using System.Globalization;

namespace TestTennis.Lib
{
    public class BudgetService2
    {
        private IBudgetRepo _budgetRepo;

        public BudgetService2(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return 0;
            }

            // 查詢預算資料
            List<Budget> budgets = _budgetRepo.GetAll();

            if (budgets == null || budgets.Count == 0)
            {
                return 0;
            }

            decimal amount = 0;

            foreach (Budget budget in budgets)
            {
                amount += CalcAmount(budget, startDate, endDate);
            }

            return amount;
        }

        public decimal CalcAmount(Budget budget, DateTime startDate, DateTime endDate)
        {
            DateTime firstDate = DateTime.ParseExact($"{budget.YearMonth}01", "yyyyMMdd", CultureInfo.InvariantCulture);
            int totalDays = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);
            DateTime lastDate = new DateTime(firstDate.Year, firstDate.Month, totalDays);

            if (firstDate >= startDate)
            {
                if (lastDate <= endDate)
                {
                    return budget.Amount;
                }
                else
                {
                    int days = CalcDays(firstDate, endDate);
                    return CalcPartialAmount(budget.Amount, days, totalDays);
                }
            }
            else
            {
                if (lastDate <= endDate)
                {
                    int days = CalcDays(startDate, lastDate);
                    return CalcPartialAmount(budget.Amount, days, totalDays);
                }
                else
                {
                    int days = CalcDays(startDate, endDate);
                    return CalcPartialAmount(budget.Amount, days, totalDays);
                }
            }
        }

        public decimal CalcPartialAmount(int amount, int days, int totalDays)
        {
            return Math.Round(amount * days / (decimal)totalDays, 2, MidpointRounding.AwayFromZero);
        }

        public int CalcDays(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days + 1;
        }
    }
}
