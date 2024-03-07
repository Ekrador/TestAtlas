using System.ComponentModel.DataAnnotations;

namespace TestAtlas.ViewModels
{
    public class MonthlyCalculatorViewModel
    {
        [Required(ErrorMessage ="Укажите сумму займа")]
        [Range(0, double.MaxValue)]
        [Display(Name = "Сумма займа", Prompt = "желаемая сумма")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Укажите срок займа")]
        [DataType(DataType.Text)]
        [Display(Name = "Срок займа", Prompt = "срок займа")]
        [Range(0, 120)]
        public int Term { get; set; }
        [Required(ErrorMessage = "Укажите ставку")]
        [Display(Name = "Ставка", Prompt = "процентная ставка")]
        public double Rate { get; set; }
    }
}
