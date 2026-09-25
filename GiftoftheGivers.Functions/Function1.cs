using System.ComponentModel.DataAnnotations;
using APPR_PART_1_POE.Models;
using Xunit;

namespace APPR_PART_1_POE.Tests
{
    // Helper used across all tests to run the same [Required]/[Range] validation
    // that ASP.NET Core runs automatically when a form is posted.
    public static class ValidationHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, validateAllProperties: true);
            return results;
        }
    }

    public class DonationTests
    {
        [Fact]
        public void Donation_WithValidData_PassesValidation()
        {
            var donation = new Donation
            {
                Amount = 100,
                DonationType = "Cash",
                Currency = "ZAR",
                IsAnonymous = false
            };

            var results = ValidationHelper.Validate(donation);

            Assert.Empty(results);
        }

        [Fact]
        public void Donation_WithZeroAmount_FailsValidation()
        {
            var donation = new Donation
            {
                Amount = 0,
                DonationType = "Cash",
                Currency = "ZAR"
            };

            var results = ValidationHelper.Validate(donation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Donation.Amount)));
        }

        [Fact]
        public void Donation_WithNegativeAmount_FailsValidation()
        {
            var donation = new Donation
            {
                Amount = -50,
                DonationType = "Cash",
                Currency = "ZAR"
            };

            var results = ValidationHelper.Validate(donation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Donation.Amount)));
        }

        [Fact]
        public void Donation_WithMissingDonationType_FailsValidation()
        {
            var donation = new Donation
            {
                Amount = 50,
                DonationType = "",
                Currency = "ZAR"
            };

            var results = ValidationHelper.Validate(donation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Donation.DonationType)));
        }

        [Fact]
        public void Donation_WithMissingCurrency_FailsValidation()
        {
            var donation = new Donation
            {
                Amount = 50,
                DonationType = "Cash",
                Currency = ""
            };

            var results = ValidationHelper.Validate(donation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Donation.Currency)));
        }

        [Fact]
        public void Donation_DefaultDonationDate_IsSetAutomatically()
        {
            var donation = new Donation();

            // DonationDate defaults to DateTime.Now when the object is created,
            // so it should be very close to "now" rather than the default DateTime.MinValue.
            Assert.True((DateTime.Now - donation.DonationDate).TotalSeconds < 5);
        }
    }

    public class VolunteerTests
    {
        [Fact]
        public void Volunteer_WithValidData_PassesValidation()
        {
            var volunteer = new Volunteer
            {
                Name = "Thandeka Nkosi",
                Skills = "First Aid, Logistics",
                Availability = "Weekends"
            };

            var results = ValidationHelper.Validate(volunteer);

            Assert.Empty(results);
        }

        [Fact]
        public void Volunteer_WithMissingName_FailsValidation()
        {
            var volunteer = new Volunteer
            {
                Name = "",
                Skills = "First Aid",
                Availability = "Weekends"
            };

            var results = ValidationHelper.Validate(volunteer);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Volunteer.Name)));
        }

        [Fact]
        public void Volunteer_WithMissingSkills_FailsValidation()
        {
            var volunteer = new Volunteer
            {
                Name = "Thandeka Nkosi",
                Skills = "",
                Availability = "Weekends"
            };

            var results = ValidationHelper.Validate(volunteer);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Volunteer.Skills)));
        }

        [Fact]
        public void Volunteer_WithMissingAvailability_FailsValidation()
        {
            var volunteer = new Volunteer
            {
                Name = "Thandeka Nkosi",
                Skills = "First Aid",
                Availability = ""
            };

            var results = ValidationHelper.Validate(volunteer);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Volunteer.Availability)));
        }
    }

    public class ReliefOperationTests
    {
        [Fact]
        public void ReliefOperation_WithValidData_PassesValidation()
        {
            var operation = new ReliefOperation
            {
                Title = "Flood Relief - KZN",
                Location = "Durban",
                Update = "Distributed 200 food parcels this week."
            };

            var results = ValidationHelper.Validate(operation);

            Assert.Empty(results);
        }

        [Fact]
        public void ReliefOperation_WithMissingTitle_FailsValidation()
        {
            var operation = new ReliefOperation
            {
                Title = "",
                Location = "Durban",
                Update = "Update text"
            };

            var results = ValidationHelper.Validate(operation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(ReliefOperation.Title)));
        }

        [Fact]
        public void ReliefOperation_WithMissingLocation_FailsValidation()
        {
            var operation = new ReliefOperation
            {
                Title = "Flood Relief",
                Location = "",
                Update = "Update text"
            };

            var results = ValidationHelper.Validate(operation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(ReliefOperation.Location)));
        }

        [Fact]
        public void ReliefOperation_WithMissingUpdate_FailsValidation()
        {
            var operation = new ReliefOperation
            {
                Title = "Flood Relief",
                Location = "Durban",
                Update = ""
            };

            var results = ValidationHelper.Validate(operation);

            Assert.Contains(results, r => r.MemberNames.Contains(nameof(ReliefOperation.Update)));
        }

        [Fact]
        public void ReliefOperation_DefaultDatePosted_IsSetAutomatically()
        {
            var operation = new ReliefOperation();

            Assert.True((DateTime.Now - operation.DatePosted).TotalSeconds < 5);
        }
    }

    public class ErrorViewModelTests
    {
        [Fact]
        public void ShowRequestId_IsFalse_WhenRequestIdIsNull()
        {
            var model = new ErrorViewModel { RequestId = null };

            Assert.False(model.ShowRequestId);
        }

        [Fact]
        public void ShowRequestId_IsFalse_WhenRequestIdIsEmpty()
        {
            var model = new ErrorViewModel { RequestId = "" };

            Assert.False(model.ShowRequestId);
        }

        [Fact]
        public void ShowRequestId_IsTrue_WhenRequestIdIsSet()
        {
            var model = new ErrorViewModel { RequestId = "abc123" };

            Assert.True(model.ShowRequestId);
        }
    }
}