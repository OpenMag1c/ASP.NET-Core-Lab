using FluentValidation;

namespace Business.Authentication.Validators
{
	public static class ValidatorExtensions
	{
		public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			var options = ruleBuilder
				.NotEmpty()
				.MinimumLength(6).WithMessage("Password must be at least 6 characters")
				.Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");

			return options;
		}
	}
}
