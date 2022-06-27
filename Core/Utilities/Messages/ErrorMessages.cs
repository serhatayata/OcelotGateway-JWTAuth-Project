using System;

namespace Core.Utilities.Messages
{
    public class ErrorMessages
    {
        public static string OnlyLetter = "Yanlızca harf girmelisiniz";
        public static string PasswordEmpty = "Şifre alanı boş geçilemez";
        public static string PasswordLength = "Şifre uzunluğu {0} ile {1} arasında olmalıdır";
        public static string PasswordUppercaseLetter = "Şifre en az bir büyük harf içermelidir";
        public static string PasswordLowercaseLetter = "Şifre en az bir küçük harf içermelidir";
        public static string PasswordDigit = "Şifre en az bir rakam içermelidir";
        public static string PasswordJustDigit = "Şifre sadece rakamlardan oluşmalıdır";
        public static string PasswordConsecutiveDigit = "Şifre ardışık rakamlardan oluşmamalıdır";
        public static string PasswordRepetitiveDigit = "Şifre tekrarlı 3 rakamdan oluşmamalıdır";
        public static string PasswordSpecialCharacter = "Şifre en az bir özel karakter içermelidir";
        public static string PasswordError = "Şifre Hatalı";
        public static string PasswordEndTime = "Şifrenizin süresi doldu. Lütfen şifrenizi yenileyiniz.";
        public static string ConfirmPasswordIsNotEqualToPassword = "Şifre ile onay şifresi eşleşmemektedir.";


        public static string KycAlreadyVerified = "KYC zaten doğrulanmış";
        public static string IncorrectIdentityNumber = "TC Kimlik numarası eksik yada hatalı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string IdentityNumberAlreadyExists = "Bu kimlik numarası farklı bir kullanıcıda tanımlanmış";
        public static string CredentialsNotVerified = "Kimlik bilgileri doğrulanamadı";
        public static string EmailAlreadyExists = "Bu email adresi zaten mevcut";
        public static string PhoneNotValid = "Telefon numaranızı tekrar kontrol ediniz.";
        public static string PhoneAlreadyExists = "Bu telefon numarası zaten mevcut";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string AccountIsLocked = "Hesabınız geçici olarak kilitlendi.";
        public static string MultipleIncorrectLogins = "Çok sayıda hatalı giriş sebebiyle hesabınız geçici olarak kilitlendi.";
        public static string IncorrectPhoneNumber = "Telefon numarası eksik yada hatalı";
        public static string IncorrectLogin = "Kullanıcı adı veya şifreniz hatalı";
        public static string EmailNotConfirmed = "Email adresiniz henüz onaylanmamış. Email adresinize gönderilen onaylama linkine tıklayınız.";

        public static string RuleSetIsInUse = "Bu Kural Seti kullanıcılara atandığı için silinemez";
        public static string SecurityQuestionIsInUse = "Bu Güvenlik Sorusu kullanıcılara atandığı için silinemez";
        public static string IncorrectSecurityQuestionOrAnswer = "Güvenlik sorusu veya cavabı eksik ya da hatalı";

        public static string IncorrectVerifyCode = "Doğrulama kodu hatalı.Tekrar deneyiniz.";

        public static string EmailNotConfirming = "E-postanızı onaylarken hata oluştu.";
        public static string IncorrectUserId = "'{0}' ID sine sahip kullanıcı yüklenemiyor.";
        public static string IncorrectCustomerId = "'{0}' ID sine sahip müşteri yüklenemiyor.";
        public static string LastPassword = "Yeni şifreniz son {0} şifreden farklı olmalı.";

        public static string SecureCodeTimeOut = "Güvenlik kodunun süresi doldu.";
        public static string OperationFailed = "İşlem Başarısız.";

        public static string ExchangeCurrencyNotEqualCurrency = "Exchange Para birimi işlem parabirimi ile aynı seçilemez";
        public static string NotificationNotSent = "Bildirim gönderilemedi";

    }
}
