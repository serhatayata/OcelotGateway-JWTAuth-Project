namespace Core.Utilities.Enums
{
    public enum EnumRuleSetResultType : byte
    {
        WaitingConfirm = 1, //İlem Onaya düşsün
        Confirmed = 2, //İşlem Onaylansın
        Blocked = 3//İşlem Engellensin
    }
}