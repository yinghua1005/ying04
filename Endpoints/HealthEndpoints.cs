using InBodyApi.Models;
using InBodyApi.Services;
using InBodyApi.Validators;

namespace InBodyApi.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/health");

        group.MapPost("/bmi", (BmiRequest request, IHealthCalculationService service) =>
        {
            var validation = HealthInputValidator.ValidateBmi(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(CreateValidationErrorResponse(validation));
            }

            var value = service.CalculateBmi(request.HeightCm!.Value, request.WeightKg!.Value);
            return Results.Ok(new ApiSuccessResponse
            {
                Metric = "BMI",
                Value = value,
                Unit = "kg/m2",
                Message = "BMI 計算成功。"
            });
        });

        group.MapPost("/bmr", (BmrRequest request, IHealthCalculationService service) =>
        {
            var validation = HealthInputValidator.ValidateBmr(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(CreateValidationErrorResponse(validation));
            }

            var value = service.CalculateBmr(
                request.Gender!.Value,
                request.Age!.Value,
                request.HeightCm!.Value,
                request.WeightKg!.Value);

            return Results.Ok(new ApiSuccessResponse
            {
                Metric = "BMR",
                Value = value,
                Unit = "kcal/day",
                Message = "BMR 計算成功。"
            });
        });

        group.MapPost("/tdee", (TdeeRequest request, IHealthCalculationService service) =>
        {
            var validation = HealthInputValidator.ValidateTdee(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(CreateValidationErrorResponse(validation));
            }

            var value = service.CalculateTdee(
                request.Gender!.Value,
                request.Age!.Value,
                request.HeightCm!.Value,
                request.WeightKg!.Value,
                request.ActivityLevel!.Value);

            return Results.Ok(new ApiSuccessResponse
            {
                Metric = "TDEE",
                Value = value,
                Unit = "kcal/day",
                Message = "TDEE 計算成功。"
            });
        });

        return app;
    }

    private static ApiErrorResponse CreateValidationErrorResponse(ValidationResult validation)
    {
        return new ApiErrorResponse
        {
            Message = "輸入資料驗證失敗。",
            Errors = validation.ToErrorDictionary()
        };
    }
}