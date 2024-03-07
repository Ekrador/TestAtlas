using TestAtlas.ViewModels;

namespace TestAtlas.Services
{
    public interface ICalculator<T>
    {
        Task<ResultViewModel> Calculate(T data);
    }
}
