using FluentValidation;

namespace PostOffice.Application.Common.ViewModels
{
	public class LocationViewModel
	{
		public string City { get; set; }
		public string Street { get; set; }
	}

	public class LocationViewModelValidator : AbstractValidator<LocationViewModel>
	{
		public LocationViewModelValidator()
		{
			RuleFor(i => i.City)
				.NotEmpty();

			RuleFor(i => i.Street)
				.NotEmpty();
		}
	}
}
