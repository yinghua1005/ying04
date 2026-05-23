using InBodyApi.Models;
using InBodyApi.Validators;

namespace InBodyApi.Tests;

public class HealthInputValidatorTests
{
    [Fact]
    public void ValidateBmi_WhenHeightMissing_ShouldBeInvalid()
    {
        var request = new BmiRequest
        {
            HeightCm = null,
            WeightKg = 70
        };

        var result = HealthInputValidator.ValidateBmi(request);

        Assert.False(result.IsValid);
        Assert.Contains("heightCm", result.Errors.Keys);
    }

    [Fact]
    public void ValidateBmr_WhenAgeOutOfRange_ShouldBeInvalid()
    {
        var request = new BmrRequest
        {
            Gender = Gender.Male,
            Age = 130,
            HeightCm = 170,
            WeightKg = 70
        };

        var result = HealthInputValidator.ValidateBmr(request);

        Assert.False(result.IsValid);
        Assert.Contains("age", result.Errors.Keys);
    }

    [Fact]
    public void ValidateTdee_WhenAllRequiredFieldsPresent_ShouldBeValid()
    {
        var request = new TdeeRequest
        {
            Gender = Gender.Female,
            Age = 25,
            HeightCm = 160,
            WeightKg = 55,
            ActivityLevel = ActivityLevel.Light
        };

        var result = HealthInputValidator.ValidateTdee(request);

        Assert.True(result.IsValid);
    }
}