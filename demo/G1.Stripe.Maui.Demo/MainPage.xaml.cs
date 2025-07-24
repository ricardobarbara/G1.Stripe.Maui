using System.Net.Http.Json;
#if IOS
using Stripe;
#endif

namespace G1.Stripe.Maui.Demo
{
    public partial class MainPage : ContentPage
    {
        private HttpClient client = new HttpClient(GetInsecureHandler());
        private IPaymentSheet _paymentSheet;

        public MainPage(IPaymentSheet paymentSheet)
        {
            InitializeComponent();
            _paymentSheet = paymentSheet;
        }

        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                return true;
            };
            return handler;
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            var address = "https://589174c9e08b.ngrok-free.app/intent";
#if ANDROID
            address = "https://10.0.2.2:7095/intent";
#endif

            var data = await client.GetFromJsonAsync<PaymentInfo>(address);

            _paymentSheet.Initialize(data!.PublishableKey);
            var result = await _paymentSheet.Open(new Options.PaymentSheetOptions
            {
                ClientSecret = data.ClientSecret,
                Customer = new Options.PaymentSheetCustomerOptions(data.Ephemeral, data.CustomerId),
                MerchantDisplayName = "Test",
                AllowsDelayedPaymentMethods = true,
                BillingDetails = new Options.PaymentSheetBillingDetailsCollectionOptions
                {
                    Name = Options.BillingDetailsCollectionMode.Always,
                    Phone = Options.BillingDetailsCollectionMode.Always,
                    Email = Options.BillingDetailsCollectionMode.Always,
                    Address = Options.AddressCollectionMode.Full,
                    AttachDefaultsToPaymentMethod = false
                },

#if IOS
                ApplePayConfiguration = new TSPSApplePayConfiguration("your.merchant.id", "us", PassKit.PKPaymentButtonType.Checkout, null, null)
#elif ANDROID
                GooglePay = new Com.Stripe.Android.Paymentsheet.PaymentSheet.GooglePayConfiguration(Com.Stripe.Android.Paymentsheet.PaymentSheet.GooglePayConfiguration.Environment.Test!, "us")
#endif
            });

            await DisplayAlert("stripe result", result.ToString(), "Cancel");
        }


        public record PaymentInfo(string PublishableKey, string ClientSecret, string CustomerId, string Ephemeral);
    }
}
