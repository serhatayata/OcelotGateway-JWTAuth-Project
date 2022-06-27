namespace Core.Utilities.Enums
{
    public enum EnumProcessType : byte
    {
        Send = 1,//Cüzdandan Cüzdana Transfer
        DepositFromBank = 2,//Bankadan cüzdan hesabına Para Yatırma işlemi
        Withdrawal = 3,//Cüzdandan Banka hesabına Para Çekme İşlemi
        Exchange = 4//Müşterinin kendi cüzdanları arasında döviz alım/satım işlemi.
    }
}