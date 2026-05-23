using InBodyApi.Models;

namespace InBodyApi.Validators;

public static class HealthInputValidator
{
    public static ValidationResult ValidateBmi(BmiRequest request)
    {
        var result = new ValidationResult();

        ValidateHeight(result, request.HeightCm);
        ValidateWeight(result, request.WeightKg);

        return result;
    }

    public static ValidationResult ValidateBmr(BmrRequest request)
    {
        var result = new ValidationResult();

        ValidateGender(result, request.Gender);
        ValidateAge(result, request.Age);
        ValidateHeight(result, request.HeightCm);
        ValidateWeight(result, request.WeightKg);

        return result;
    }

    public static ValidationResult ValidateTdee(TdeeRequest request)
    {
        var result = new ValidationResult();

        ValidateGender(result, request.Gender);
        ValidateAge(result, request.Age);
        ValidateHeight(result, request.HeightCm);
        ValidateWeight(result, request.WeightKg);

        if (request.ActivityLevel is null)
        {
            result.AddError("activityLevel", "活動量為必填。");
        }

        return result;
    }

    private static void ValidateHeight(ValidationResult result, double? heightCm)
    {
        if (heightCm is null)
        {
            result.AddError("heightCm", "身高為必填。");
            return;
        }

        if (heightCm <= 0)
        {
            result.AddError("heightCm", "身高必須大於 0。");
        }
    }

    private static void ValidateWeight(ValidationResult result, double? weightKg)
    {
        if (weightKg is null)
        {
            result.AddError("weightKg", "體重為必填。");
            return;
        }

        if (weightKg <= 0)
        {
            result.AddError("weightKg", "體重必須大於 0。");
        }
    }

    private static void ValidateAge(ValidationResult result, int? age)
    {
        if (age is null)
        {
            result.AddError("age", "年齡為必填。");
            return;
        }

        if (age is < 1 or > 120)
        {
            result.AddError("age", "年齡需介於 1 到 120。");
        }
    }

    private static void ValidateGender(ValidationResult result, Gender? gender)
    {
        if (gender is null)
        {
            result.AddError("gender", "性別為必填。");
        }
    }
}