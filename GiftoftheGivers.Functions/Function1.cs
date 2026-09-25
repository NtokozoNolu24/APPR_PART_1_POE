using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

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
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")]
            HttpRequestData req)
        {
            _logger.LogInformation("Donation receipt function processed a request.");

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);

            string? donorName = query["donorName"];
            string? amountText = query["amount"];

            if (string.IsNullOrWhiteSpace(donorName) || string.IsNullOrWhiteSpace(amountText))
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                badRequest.WriteString("Please provide donorName and amount.");
                return badRequest;
            }

            donorName = donorName.Trim();

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                badRequest.WriteString("Amount must be a valid number.");
                return badRequest;
            }

            if (amount <= 0)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                badRequest.WriteString("Amount must be greater than zero.");
                return badRequest;
            }

            var now = DateTimeOffset.UtcNow;
            string receiptNumber = $"DON-{now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6]}";

            var receipt = new
            {
                ReceiptNumber = receiptNumber,
                DonorName = donorName,
                Amount = amount,
                Date = now,
                Message = "Thank you for supporting Gift of the Givers."
            };

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(receipt);

            return response;
        }
    }
}