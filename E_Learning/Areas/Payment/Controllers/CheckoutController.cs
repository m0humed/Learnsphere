using E_Learning.Areas.Payment.Models;
using E_Learning.Helper;
using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;


namespace E_Learning.Areas.Payment.Controllers
{
    [Area("Payment")]
    public class CheckoutController : Controller
    {
        public string TotalAmount { get; set; } = null;
        private readonly PaypalClient _paypalClient;
        private readonly IPaymentRepository _payment;
        private readonly IEnrollmentRepository _enrollment;

        public CheckoutController(PaypalClient paypalclient , IEnrollmentRepository enrollment , IPaymentRepository payment)
        {
            _paypalClient = paypalclient;
            _payment = payment;
            _enrollment = enrollment;
        }


        [HttpGet]
        public IActionResult Index(CourseSummaryViewModel Csummary)
        {
            ViewBag.ClientId = _paypalClient.ClientId;

            try
            {
                var cart = Csummary;
                ViewBag.cart = cart;
                ViewBag.DollarAmount = cart.total;
                ViewBag.total = ViewBag.DollarAmount;
                int total = (int)ViewBag.total;
                TotalAmount = total.ToString();
                TempData["TotalAmount"] = TotalAmount;
                ViewBag.CoursesId = Csummary.CourseIdMony.Keys;

            }
            catch (Exception)
            {


            }
            return View();
        }

        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            //var optionCust = new CustomerCreateOptions
            //{
            //    Email = stripeEmail,
            //    Name = "Rizwan Yousaf",
            //    Phone = "338595119"
            //};
            //var serviceCust = new CustomerService();
            //Customer customer = serviceCust.Create(optionCust);
            //var optionsCharge = new ChargeCreateOptions
            //{
            //    Amount = Convert.ToInt64(TempData["TotalAmount"]),
            //    Currency = "USD",
            //    Description="Pet Selling amount",
            //    Source=stripeToken,
            //    ReceiptEmail=stripeEmail

            //};
            //var serviceCharge = new ChargeService();
            //Charge charge = serviceCharge.Create(optionsCharge);
            //if(charge.Status=="successded")
            //{
            //    ViewBag.AmountPaid = charge.Amount;
            //    ViewBag.Customer = customer.Name;
            //}
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {
                // set the transaction price and currency
                var price = TempData.Peek("TotalAmount")?.ToString();
                var currency = "USD";

                // "reference" is the transaction key
                var reference = GetRandomInvoiceNumber();// "INV002";

                var response = await _paypalClient.CreateOrder(price!, currency, reference);

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                // Put your logic to save the transaction here
                // You can use the "reference" variable as a transaction key

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
        public async Task<IActionResult> Success()
        {
            CourseSummaryViewModel proccess = ViewBag.cart;
            foreach (var cId in proccess.CourseIdMony)
            {
                var Id = Guid.NewGuid().ToString();
                var payment = new E_Learning.Models.Payment()
                {
                    CourseId = cId.Key,
                    Id = Id,
                    UserId = proccess.UserId,
                    PaymentTime = DateTime.UtcNow,
                    PaymentMethod = "Paypal",
                    PaymentAmount = cId.Value
                };
                var Enrollment = new E_Learning.Models.Enrollment()
                {
                    Id = Id,
                    CourseId = cId.Key,
                    Date = DateTime.UtcNow,
                    CompletionStatus = false,
                    Percentage = 0,
                    UserId = proccess.UserId

                };
                await _payment.AddAsync(payment);
                await _enrollment.AddAsync(Enrollment);
            }
            

            return View();
        }
    }
}