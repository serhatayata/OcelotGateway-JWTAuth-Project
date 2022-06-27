namespace Core.Utilities.Enums
{
    public enum EnumProcessStatus : byte
    {
        WaitingConfirm = 0,//Onay bekliyor
        Confirmed = 1,//Onaylandı
        Cancel = 2,//İptal
        Rejection = 3//Red
    }
}