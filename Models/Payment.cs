namespace TestAtlas.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public double PaymentAmount { get; set; }
        public double Debt { get; set; }
        public double Percents { get; set; }
        public double Rest { get; set; }
    }
}
