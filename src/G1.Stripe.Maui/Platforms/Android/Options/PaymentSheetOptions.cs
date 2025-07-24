using Com.Stripe.Android.Paymentsheet;

namespace G1.Stripe.Maui.Options;

partial class PaymentSheetOptions
{
    public PaymentSheet.GooglePayConfiguration? GooglePay { get; set; }

    internal PaymentSheet.Configuration BuildPlatform()
    {
        var configurationBuilder = new PaymentSheet.Configuration.Builder(MerchantDisplayName)
                .AllowsDelayedPaymentMethods(AllowsDelayedPaymentMethods);

        if (BillingDetails is { } billingDetails)
        {
            configurationBuilder.BillingDetailsCollectionConfiguration(billingDetails.ToPlatform());
        }

        if (GooglePay is { } googlePay)
        {
            configurationBuilder.GooglePay(GooglePay);
        }

        if (Customer is { } customer)
        {
            configurationBuilder.Customer(customer.ToPlatform());
        }

        return configurationBuilder.Build();
    }
}