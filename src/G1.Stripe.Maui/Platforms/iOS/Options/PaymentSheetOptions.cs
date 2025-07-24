using Stripe;

namespace G1.Stripe.Maui.Options;

partial class PaymentSheetOptions
{
    public TSPSApplePayConfiguration? ApplePayConfiguration { get; set; }

    internal TSPSConfiguration BuildPlatform()
    {
        var configuration = new TSPSConfiguration
        {
            MerchantDisplayName = MerchantDisplayName,
            AllowsDelayedPaymentMethods = AllowsDelayedPaymentMethods
        };

        if (ApplePayConfiguration is { } applePayConfiguration) 
        {
            configuration.ApplePay = ApplePayConfiguration;
        }

        if(BillingDetails is { } billingDetails)
        {
            configuration.BillingDetailsCollectionConfiguration = billingDetails.ToPlatform();
        }

        if (Customer is { } customer)
        {
            configuration.Customer = customer.ToPlatform();
        }

        return configuration;
    }
}