namespace Core.Utilities.Enums
{
    public enum EnumRuleSetReceiverType : byte
    {
        SameCustomerOrWallet = 1, //Aynı Müşeteri veya Cüzdan
        DifferentCustomerOrWallet = 2, //Farklı Müşeteri veya Cüzdan
        AnyCustomerOrWallet = 3//Farketmez. Herhangi Müşeteri veya Cüzdan
    }
}