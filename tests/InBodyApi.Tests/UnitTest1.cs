using InBodyApi.Models;
using InBodyApi.Services;

namespace InBodyApi.Tests;

public class HealthCalculationServiceTests
{
    private readonly HealthCalculationService _service = new();

    [Fact]
    public void CalculateBmi_ShouldReturnRoundedTwoDecimals()
    {
        var result = _service.CalculateBmi(170, 70);

        Assert.Equal(24.22, result);
    }

    [Fact]
    public void CalculateBmr_Male_ShouldMatchMifflinStJeor()
    {
        var result = _service.CalculateBmr(Gender.Male, 30, 170, 70);

        Assert.Equal(1618, result);
    }

    [Fact]
    public void CalculateBmr_Female_ShouldMatchMifflinStJeor()
    {
        var result = _service.CalculateBmr(Gender.Female, 30, 170, 70);

        Assert.Equal(1452, result);
    }

    [Fact]
    public void CalculateTdee_ShouldApplyActivityFactorAndRound()
    {
        var result = _service.CalculateTdee(Gender.Male, 30, 170, 70, ActivityLevel.Moderate);

        Assert.Equal(2508, result);
    }
}
