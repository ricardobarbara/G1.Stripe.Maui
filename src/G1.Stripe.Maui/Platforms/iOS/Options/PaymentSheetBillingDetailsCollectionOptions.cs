using Stripe;

namespace G1.Stripe.Maui.Options;

partial class PaymentSheetBillingDetailsCollectionOptions
{
    internal TSPSBillingDetailsCollectionConfiguration ToPlatform()
    {
        return new TSPSBillingDetailsCollectionConfiguration(
                name: ToPlatform(Name),
                phone: ToPlatform(Phone),
                email: ToPlatform(Email),
                address: ToPlatform(Address),
                attachDefaultsToPaymentMethod: AttachDefaultsToPaymentMethod
        );
    }

    private static TSPSCollectionMode ToPlatform(BillingDetailsCollectionMode billingDetailsCollectionMode)
    {
        return billingDetailsCollectionMode switch
        {
            BillingDetailsCollectionMode.Automatic => TSPSCollectionMode.Automatic,
            BillingDetailsCollectionMode.Always => TSPSCollectionMode.Always,
            BillingDetailsCollectionMode.Never => TSPSCollectionMode.Never,
            _ => throw new NotImplementedException(),
        };
    }

    private static TSPSAddressCollectionMode ToPlatform(AddressCollectionMode billingDetailsCollectionMode)
    {
        return billingDetailsCollectionMode switch
        {
            AddressCollectionMode.Automatic => TSPSAddressCollectionMode.Automatic,
            AddressCollectionMode.Full => TSPSAddressCollectionMode.Full,
            AddressCollectionMode.Never => TSPSAddressCollectionMode.Never,
            _ => throw new NotImplementedException(),
        };
    }
}
