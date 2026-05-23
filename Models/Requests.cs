namespace InBodyApi.Models;

public sealed class BmiRequest
{
    public double? HeightCm { get; init; }

    public double? WeightKg { get; init; }
}

public sealed class BmrRequest
{
    public Gender? Gender { get; init; }

    public int? Age { get; init; }

    public double? HeightCm { get; init; }

    public double? WeightKg { get; init; }
}

public sealed class TdeeRequest
{
    public Gender? Gender { get; init; }

    public int? Age { get; init; }

    public double? HeightCm { get; init; }

    public double? WeightKg { get; init; }

    public ActivityLevel? ActivityLevel { get; init; }
}

public sealed class ApiSuccessResponse
{
    public bool Success { get; init; } = true;

    public string Metric { get; init; } = string.Empty;

    public double Value { get; init; }

    public string Unit { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;
}

public sealed class ApiErrorResponse
{
    public bool Success { get; init; } = false;

    public string Message { get; init; } = string.Empty;

    public Dictionary<string, string[]> Errors { get; init; } = new();
}

public sealed class ValidationResult
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public bool IsValid => _errors.Count == 0;

    public IReadOnlyDictionary<string, List<string>> Errors => _errors;

    public void AddError(string field, string message)
    {
        if (!_errors.TryGetValue(field, out var messages))
        {
            messages = new List<string>();
            _errors[field] = messages;
        }

        messages.Add(message);
    }

    public Dictionary<string, string[]> ToErrorDictionary()
    {
        return _errors.ToDictionary(entry => entry.Key, entry => entry.Value.ToArray());
    }
}