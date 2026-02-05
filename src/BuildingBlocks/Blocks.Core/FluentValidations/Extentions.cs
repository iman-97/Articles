using FluentValidation;

namespace Blocks.Core.FluentValidations;

public static class Extentions
{
    public static IRuleBuilderOptions<T, TProperty> WithMessageForInvalidId<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule,
        string propertyName)
        => rule.WithMessage(x => ValidationMessages.InvalidId.FormatWith(propertyName));

    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
        string propertyName)
        => ruleBuilder
                .NotEmpty()
                .WithMessage(x => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));
}
