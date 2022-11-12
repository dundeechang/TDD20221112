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

            Dictionary<string, (int days, int totalDays)> dateDict = CalcDate(startDate, endDate);

            decimal amount = 0;
            foreach ((string ym, (int days, int totalDays)) in dateDict)
            {
                Budget budget = budgets.Find(x => x.YearMonth == ym);
                if (budget != null && budget.Amount > 0)
                {
                    decimal thisAmount = budget.Amount * days / totalDays;
                    amount += thisAmount;
                }
            }
            return amount;
        }

        public Dictionary<string, (int days, int totalDays)> CalcDate(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, (int days, int totalDays)> dateDict = new();

            DateTime tempDate = startDate;
            while (true)
            {
                int totalDays = DateTime.DaysInMonth(tempDate.Year, tempDate.Month);

                int days;
                if (tempDate.Year == endDate.Year && tempDate.Month == endDate.Month)
                {
                    days = (endDate - tempDate).Days + 1;

                    dateDict[tempDate.ToString("yyyyMM")] = (days, totalDays);

                    break;
                }
                else
                {
                    DateTime monthFirstDate = new DateTime(tempDate.Year, tempDate.Month, 1).AddMonths(1);

                    days = (monthFirstDate - tempDate).Days;

                    dateDict[tempDate.ToString("yyyyMM")] = (days, totalDays);

                    tempDate = monthFirstDate;
                }
            }

            return dateDict;
        }
    }
}
