using System;
using System.ComponentModel.DataAnnotations;

public class DateTimeValidation : ValidationAttribute
{
    private readonly int _minVal;
    private readonly int _maxVal;

    public DateTimeValidation(int minVal, int maxVal)
    {
        _minVal = minVal;
        _maxVal = maxVal;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateOnly date)
        {
            int yearDifference = DateOnly.FromDateTime(DateTime.Today).Year - date.Year;
            if (yearDifference > _maxVal || yearDifference < _minVal)
            {
                return new ValidationResult($"This field must be in range {_minVal} - {_maxVal} years.");
            }
            return ValidationResult.Success;
        }
        return new ValidationResult("Incorect data type");
    }
}
