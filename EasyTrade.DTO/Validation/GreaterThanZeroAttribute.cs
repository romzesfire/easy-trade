using System.ComponentModel.DataAnnotations;

namespace EasyTrade.DTO.Validation;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    public GreaterThanZeroAttribute()
    {
        ErrorMessage = "Value must bet greater than 0";
    }
    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        return (decimal)value > 0;
    }
}