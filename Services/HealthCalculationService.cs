using InBodyApi.Models;

namespace InBodyApi.Services;

public sealed class HealthCalculationService : IHealthCalculationService
{
    public double CalculateBmi(double heightCm, double weightKg)
    {
        var heightMeter = heightCm / 100d;
        var bmi = weightKg / (heightMeter * heightMeter);

        return Math.Round(bmi, 2, MidpointRounding.AwayFromZero);
    }

    public int CalculateBmr(Gender gender, int age, double heightCm, double weightKg)
    {
        var baseBmr = 10d * weightKg + 6.25d * heightCm - 5d * age;
        var bmr = gender == Gender.Male ? baseBmr + 5d : baseBmr - 161d;

        return (int)Math.Round(bmr, 0, MidpointRounding.AwayFromZero);
    }

    public int CalculateTdee(Gender gender, int age, double heightCm, double weightKg, ActivityLevel activityLevel)
    {
        var bmr = CalculateBmr(gender, age, heightCm, weightKg);
        var activityFactor = ResolveActivityFactor(activityLevel);
        var tdee = bmr * activityFactor;

        return (int)Math.Round(tdee, 0, MidpointRounding.AwayFromZero);
    }

    private static double ResolveActivityFactor(ActivityLevel activityLevel)
    {
        return activityLevel switch
        {
            ActivityLevel.Sedentary => 1.2d,
            ActivityLevel.Light => 1.375d,
            ActivityLevel.Moderate => 1.55d,
            ActivityLevel.High => 1.725d,
            ActivityLevel.VeryHigh => 1.9d,
            _ => throw new ArgumentOutOfRangeException(nameof(activityLevel), activityLevel, "Unsupported activity level")
        };
    }
}