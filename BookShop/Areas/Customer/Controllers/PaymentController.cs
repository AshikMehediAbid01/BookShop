using BookShop.Models;
using BookShop.Models.AamarPay;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BookShop.Areas.Customer.Controllers;

[Area("Customer")]
public class PaymentController(IHttpClientFactory _httpClientFactory,ILogger<PaymentController> _logger) : Controller
{
    private readonly string storeId = "aamarpaytest";
    private readonly string signatureKey = "dbb74894e82415a2f7ff0ec3a97e4183";

    private const string SandboxApiUrl = "https://sandbox.aamarpay.com/jsonpost.php";

    [HttpGet("pay")]
    
    public async Task<IActionResult> pay()
    {

        var CustomerName = TempData["CustomerName"] as string;
        var CustomerEmail = TempData["CustomerEmail"] as string;
        var CustomerPhone = TempData["customerNumber"] as string;
        var TotalPrice = TempData["TotalAmount"] as string;
        //  var CustomerAddress = TempData["CustomerAddress"] as string;

        var paymentData = new AamarPayRequest
        {
            store_id = storeId,
            tran_id = Guid.NewGuid().ToString(),
            success_url = "https://localhost:7062/Customer/Payment/success",
            fail_url = "https://localhost:7062/Customer/Payment/fail",
            cancel_url = "https://localhost:7062/payment/cancel",
            amount = TotalPrice,
            currency = "BDT",
            signature_key = signatureKey,
            desc = "Merchant Registration Payment",
            cus_name = CustomerName ?? "anonymous",
            cus_email = CustomerEmail ?? "payer@merchantcusomter.com",
            cus_add1 = "ss",// CustomerAddress,
            cus_add2 = "Mohakhali DOHS",
            cus_city = "Dhaka",
            cus_state = "Dhaka",
            cus_postcode = "1206",
            cus_country = "Bangladesh",
            cus_phone = CustomerPhone,
            type = "json"
        };

        var httpClient = _httpClientFactory.CreateClient();

        var jsonContent = JsonSerializer.Serialize(paymentData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(SandboxApiUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);

        if (responseData.TryGetValue("payment_url", out var paymentUrl))
        {
            return Redirect(paymentUrl);
        }
        return BadRequest("Payment URL not found in response.");
    }

    [HttpPost("/Customer/Payment/success")]
    public async Task<IActionResult> Success([FromForm] string mer_txnid)
    {
        _logger.LogInformation("✅ Payment Success");
        if (string.IsNullOrEmpty(mer_txnid))
        {
            return Content("❌ Invalid callback");
        }

        string store_id = "aamarpaytest";
        string signature_key = "dbb74894e82415a2f7ff0ec3a97e4183";
        string url = $"https://sandbox.aamarpay.com/api/v1/trxcheck/request.php?request_id={mer_txnid}&store_id={store_id}&signature_key={signature_key}&type=json";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<AamarPayValidationResponse>(json);

        return View("Success", result);


    }

    [HttpPost("/Customer/Payment/fail")]
    public IActionResult Fail()
    {
        return View("Fail");
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return Content("⚠️ Payment Cancelled");
    }

}
