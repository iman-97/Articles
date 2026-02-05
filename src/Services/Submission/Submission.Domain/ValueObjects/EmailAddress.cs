using Blocks.Core;
using System.Text.RegularExpressions;

namespace Submission.Domain.ValueObjects;

public class EmailAddress
{
    public string Value { get; private set; }

    private EmailAddress(string value) => Value = value;

    public static EmailAddress Create(string value)
    {
        Guard.ThrowIfNullOrWhiteSpace(value);

        if (IsValidEmail(value) == false)
            throw new ArgumentException("Invalid Email Format");

        return new EmailAddress(value);
    }

    private static bool IsValidEmail(string email)
    {
        const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(emailRegex, email, RegexOptions.IgnoreCase);
    }
}
