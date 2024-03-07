using System.ComponentModel.DataAnnotations;

namespace TestAtlas.ViewModels
{
    public class DailyCalculatorViewModel:MonthlyCalculatorViewModel
    { 

        [Required(ErrorMessage = "Укажите периодичность платежей")]
        [Range(1, 31)]
        [DataType(DataType.Text)]
        [Display(Name = "Периодичность платежей", Prompt = "период между оплатой")]
        public int Step { get; set; }
    }
}
