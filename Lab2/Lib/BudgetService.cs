namespace TestTennis.Lib
{
    public class BudgetService
    {
        private IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
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

            //var foundBudgets = budgets.FindAll(x => int.Parse(x.YearMonth) >= int.Parse(begDate.ToString("yyyyMM")) && int.Parse(x.YearMonth) <= int.Parse(endDate.ToString("yyyyMM")));

            Dictionary<string, (int dates, int totalDates)> dateDict = CalcDate(startDate, endDate);

            decimal amount = 0;
            foreach ((string ym, (int dates, int totalDates)) in dateDict)
            {
                Budget budget = budgets.Find(x => x.YearMonth == ym);
                if (budget != null && budget.Amount > 0)
                {
                    decimal thisAmount = budget.Amount * dates / totalDates;
                    amount += thisAmount;
                }
            }
            return amount;
        }

        public Dictionary<string, (int dates, int totalDates)> CalcDate(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, (int dates, int totalDates)> dateDict = new();

            DateTime tempDate = startDate;
            while (tempDate <= endDate)
            {
                int dates;
                if (tempDate.Year == endDate.Year && tempDate.Month == endDate.Month)
                {
                    dates = (endDate - tempDate).Days + 1;

                    int totalDates = DateTime.DaysInMonth(tempDate.Year, tempDate.Month);

                    dateDict[tempDate.ToString("yyyyMM")] = (dates, totalDates);

                    break;
                }
                else
                {
                    DateTime nextDate1 = new DateTime(tempDate.Year, tempDate.Month + 1, 1);

                    dates = (nextDate1 - tempDate).Days;

                    int totalDates = DateTime.DaysInMonth(tempDate.Year, tempDate.Month);

                    dateDict[tempDate.ToString("yyyyMM")] = (dates, totalDates);
                    tempDate = nextDate1;
                }
            }

            return dateDict;
        }
    }
}
