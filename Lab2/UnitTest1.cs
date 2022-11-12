using NSubstitute;
using TestTennis.Lib;

namespace TestTennis
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void query_single_day()
        {
            IBudgetRepo repo = Substitute.For<IBudgetRepo>();
            var budgetService = new BudgetService(repo);
            repo.GetAll().Returns(new List<Budget>
            {
                new Budget() {YearMonth = "202211", Amount = 3000}
            });
            var result = budgetService.Query(new DateTime(2022, 11, 12), new DateTime(2022, 11, 12));
            Assert.AreEqual(100, result);

            //var budgetService = new BudgetService(null).CalcDate(new DateTime(2022, 11, 12), new DateTime(2022, 11, 12));
        }

        [TestMethod]
        public void query_single_month()
        {
            IBudgetRepo repo = Substitute.For<IBudgetRepo>();
            var budgetService = new BudgetService(repo);
            repo.GetAll().Returns(new List<Budget>
            {
                new Budget() {YearMonth = "202207", Amount = 62}
                //new Budget() {YearMonth = "202208", Amount = 93},
                //new Budget() {YearMonth = "202209", Amount = 600},
                //new Budget() {YearMonth = "202210", Amount = 0},
                //new Budget() {YearMonth = "202211", Amount = 60000},
                //new Budget() {YearMonth = "202212", Amount = 0}
            });
            var result = budgetService.Query(new DateTime(2022, 7, 1), new DateTime(2022, 7, 31));
            Assert.AreEqual(62, result);
        }

        [TestMethod]
        public void query_month()
        {
            IBudgetRepo repo = Substitute.For<IBudgetRepo>();
            var budgetService = new BudgetService(repo);
            repo.GetAll().Returns(new List<Budget>
            {
                new Budget() {YearMonth = "202211", Amount = 60000}
            });
            var result = budgetService.Query(new DateTime(2022, 11, 1), new DateTime(2022, 11, 3));
            Assert.AreEqual(6000, result);
        }

        [TestMethod]
        public void query_error_month()
        {
            IBudgetRepo repo = Substitute.For<IBudgetRepo>();
            var budgetService = new BudgetService(repo);
            repo.GetAll().Returns(new List<Budget>
            {
                new Budget() {YearMonth = "202211", Amount = 0}
            });
            var result = budgetService.Query(new DateTime(2022, 11, 3), new DateTime(2022, 11, 1));
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void query_multi_month()
        {
            IBudgetRepo repo = Substitute.For<IBudgetRepo>();
            var budgetService = new BudgetService(repo);
            repo.GetAll().Returns(new List<Budget>
            {
                new Budget() {YearMonth = "202209", Amount = 600},
                new Budget() {YearMonth = "202210", Amount = 0},
                new Budget() {YearMonth = "202211", Amount = 60000}
            });
            var result = budgetService.Query(new DateTime(2022, 9, 30), new DateTime(2022, 11, 3));
            Assert.AreEqual(6020, result);
        }
    }
}