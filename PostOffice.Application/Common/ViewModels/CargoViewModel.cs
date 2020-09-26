using FluentValidation;

namespace PostOffice.Application.Common.ViewModels
{
	public class CargoViewModel
	{
		public double Width { get; set; }
		public double Height { get; set; }
		public double Length { get; set; }
	}

	public class CargoViewModelValidator : AbstractValidator<CargoViewModel>
	{
		public CargoViewModelValidator()
		{
			RuleFor(i => i.Width)
				.NotEmpty();

			RuleFor(i => i.Height)
				.NotEmpty();

			RuleFor(i => i.Length)
				.NotEmpty();
		}
	}
}
