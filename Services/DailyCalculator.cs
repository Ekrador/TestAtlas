using TestAtlas.Models;
using TestAtlas.ViewModels;

namespace TestAtlas.Services
{
    public class DailyCalculator : ICalculator<DailyCalculatorViewModel>
    {
        private DailyCalculatorViewModel _data;
        private ResultViewModel _result;
        private double _monthlyRate;
        private int _step;
        private int _id;
        private int _paymentsDays;
        public DailyCalculator()
        {
            _result = new ResultViewModel();
        }

        public async Task<ResultViewModel> Calculate(DailyCalculatorViewModel data)
        {
            _data = data;
            _data.Rate /= 100;
            _step = _data.Step;
            _id = 0;
            // Рассчет количества платежей. Если введенное количество дней не делится без остатка на шаг,
            // прибавляем один день и последний день займа считаем последним платежем
            _paymentsDays = _data.Term % _step == 0? _data.Term / _step : _data.Term / _step + 1;
            await Calc(DateOnly.FromDateTime(DateTime.UtcNow), _data.Amount, _data.Term);
            return _result;
        }

        private async Task Calc(DateOnly date, double restPayment, int amountPayments)
        {
            if (amountPayments <= 0) return;
            await Task.Run(async () =>
            {
                // Рассчет по формуле
                var K = (_data.Rate * Math.Pow((1 + _data.Rate), _paymentsDays)) / (Math.Pow((1 + _data.Rate), _paymentsDays) - 1);
                var A = K * _data.Amount;
                var dailyPercents = restPayment * _data.Rate;
                var payment = new Payment();
                payment.Id = ++_id;
                // Проверить не выходит ли шаг за количество дней, на которые выдан займ
                // Если шаг больше, последний день займа являеется последним днем рассчета
                if (amountPayments < _step)
                {
                    payment.Date = date.AddDays(amountPayments);
                    amountPayments = 0;
                }
                else
                {
                    payment.Date = date.AddDays(_step);
                    amountPayments = amountPayments - _step;
                }
                payment.PaymentAmount = A;
                payment.Debt = A - dailyPercents;
                payment.Percents = dailyPercents;
                var rest = restPayment - payment.Debt;
                payment.Rest = rest > 0 ? rest : 0;
                _result.Payments.Add(payment);
                await Calc(payment.Date, payment.Rest, amountPayments);
            });
        }
    }

}
