namespace AccountManagementServer.Core.Models
{
    public class MonthTotalsDto
    {
        public int Month { get; set; }          // 1–12
        public decimal IncomeTotal { get; set; }
        public decimal ExpenseTotal { get; set; }

    }
}
