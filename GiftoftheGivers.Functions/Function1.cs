using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GiftoftheGivers.Functions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("GenerateDonationReceipt")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Donation receipt function processed a request.");

            string donorName = req.Query["donorName"];
            string amountText = req.Query["amount"];

            if (string.IsNullOrEmpty(donorName) || string.IsNullOrEmpty(amountText))
            {
                return new BadRequestObjectResult(
                    "Please provide donorName and amount."
                );
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                return new BadRequestObjectResult(
                    "Amount must be a valid number."
                );
            }

            string receiptNumber = $"DON-{DateTime.Now:yyyyMMddHHmmss}";

            var receipt = new
            {
                ReceiptNumber = receiptNumber,
                DonorName = donorName,
                Amount = amount,
                Date = DateTime.Now,
                Message = "Thank you for supporting Gift of the Givers."
            };

            return new OkObjectResult(receipt);
        }
    }
}