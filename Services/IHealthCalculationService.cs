using InBodyApi.Models;

namespace InBodyApi.Services;

public interface IHealthCalculationService
{
    double CalculateBmi(double heightCm, double weightKg);

    int CalculateBmr(Gender gender, int age, double heightCm, double weightKg);

    int CalculateTdee(Gender gender, int age, double heightCm, double weightKg, ActivityLevel activityLevel);
}