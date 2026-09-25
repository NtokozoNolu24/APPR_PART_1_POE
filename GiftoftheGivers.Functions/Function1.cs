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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
            HttpRequestData req)
        {
            _logger.LogInformation("Donation receipt function processed a request.");

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);

            string? donorName = query["donorName"];
            string? amountText = query["amount"];

            if (string.IsNullOrEmpty(donorName) || string.IsNullOrEmpty(amountText))
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                badRequest.WriteString("Please provide donorName and amount.");
                return badRequest;
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                badRequest.WriteString("Amount must be a valid number.");
                return badRequest;
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

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(receipt);

            return response;
        }
    }
}