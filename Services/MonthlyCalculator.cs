using TestAtlas.ViewModels;
using TestAtlas.Models;
using System.Security.Cryptography;

namespace TestAtlas.Services
{
    public class MonthlyCalculator : ICalculator<MonthlyCalculatorViewModel>
    {
        private MonthlyCalculatorViewModel _data;
        private ResultViewModel _result;
        private double _monthlyRate;
        private int _id;
        public MonthlyCalculator()
        {
            _result = new ResultViewModel();
        }

        public async Task<ResultViewModel> Calculate(MonthlyCalculatorViewModel data)
        {
            _data = data;
            _data.Rate /= 100;
            _monthlyRate = _data.Rate / 12;
            _id = 0;

            await Calc(DateOnly.FromDateTime(DateTime.UtcNow), _data.Amount, _data.Term);
            return _result;
        }

        private async Task Calc(DateOnly date, double restPayment, int amountPayments)
        {
            if (amountPayments == 0) return;
            await Task.Run(async () =>
            {
                // Рассчет по формуле
                var K = (_monthlyRate * Math.Pow((1 + _monthlyRate), _data.Term)) / (Math.Pow((1 + _monthlyRate), _data.Term) - 1);
                var A = K * _data.Amount;
                var montlyPercents = restPayment * _data.Rate / 12;
                var payment = new Payment();
                payment.Id = ++_id;
                payment.Date = date.AddMonths(1);
                payment.PaymentAmount = A;
                payment.Debt = A - montlyPercents;
                payment.Percents = montlyPercents;
                var rest = restPayment - payment.Debt;
                payment.Rest = rest > 0 ? rest : 0;
                _result.Payments.Add(payment);
                await Calc(payment.Date, payment.Rest, amountPayments - 1);
            });
        }
    }
}
