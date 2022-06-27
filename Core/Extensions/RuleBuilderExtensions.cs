using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder,
            int minimumLength = 6, int maximumLength = 14)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(ErrorMessages.PasswordEmpty)
                .Length(minimumLength, maximumLength)
                .WithMessage(string.Format(ErrorMessages.PasswordLength, minimumLength, maximumLength))
                .Must(IsAUpperCharacter).WithMessage(ErrorMessages.PasswordUppercaseLetter)
                .Must(IsALowerCharacter).WithMessage(ErrorMessages.PasswordLowercaseLetter)
                .Must(IsANumber).WithMessage(ErrorMessages.PasswordDigit)
                .Must(IsASpecialCharacter).WithMessage(ErrorMessages.PasswordSpecialCharacter)
                .Must(IsConsecutiveNumber).WithMessage(ErrorMessages.PasswordConsecutiveDigit)
                .Must(IsRepetitiveNumber).WithMessage(ErrorMessages.PasswordConsecutiveDigit);
            return options;
        }

        public static IRuleBuilderOptions<T, string> PasswordWithoutMessage<T>(this IRuleBuilder<T, string> ruleBuilder,
            int minimumLength = 6, int maximumLength = 14)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(minimumLength, maximumLength)
                .Must(IsAUpperCharacter)
                .Must(IsALowerCharacter)
                .Must(IsANumber)
                .Must(IsASpecialCharacter)
                .Must(IsConsecutiveNumber)
                .Must(IsRepetitiveNumber);
            return options;
        }

        public static IRuleBuilder<T, string> PasswordNumeric<T>(this IRuleBuilder<T, string> ruleBuilder,
            int minimumLength = 6, int maximumLength = 8)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(ErrorMessages.PasswordEmpty)
                .Length(minimumLength, maximumLength)
                .WithMessage(string.Format(ErrorMessages.PasswordLength, minimumLength, maximumLength))
                .Must(IsJustNumber).WithMessage(ErrorMessages.PasswordJustDigit)
                .Must(IsConsecutiveNumber).WithMessage(ErrorMessages.PasswordConsecutiveDigit)
                .Must(IsRepetitiveNumber).WithMessage(ErrorMessages.PasswordRepetitiveDigit);  
            return options;
        }

        public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder,
            int minimumLength = 6, int maximumLength = 20)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(minimumLength, maximumLength)
                .Matches("[0-9 ]{6,20}").WithMessage(ErrorMessages.PhoneNotValid);
                //.Must(StartsWithWith_0);
            return options;
        }

        public static IRuleBuilderOptions<T, string> PhoneNumberWithoutMessage<T>(this IRuleBuilder<T, string> ruleBuilder,
            int minimumLength = 6, int maximumLength = 20)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(minimumLength, maximumLength)
                .Matches("[0-9 ]{6,20}");
            //.Must(StartsWithWith_0);
            return options;
        }

        public static IRuleBuilder<T, string> Letter<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Must(IsLetter).WithMessage(ErrorMessages.OnlyLetter);
            return options;
        }

        public static IRuleBuilder<T, string> LetterWithoutEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Must(IsLetterWithoutEmpty).WithMessage(ErrorMessages.OnlyLetter);
            return options;
        }

        private static bool StartsWithWith_0(string arg)
        {
            return !string.IsNullOrEmpty(arg) && arg.StartsWith("0");
        }

        public static bool IsAUpperCharacter(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                @"[A-ZÇĞİÖÜ]{1,}");
        }

        public static bool IsALowerCharacter(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "[a-zçğıöü]{1,}");
        }

        public static bool IsANumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "[0-9]{1,}");
        }

        //Yalnızca sayı içerir
        public static bool IsJustNumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "^[0-9]*$");
        }

        //Ardışık sayı içerir
        public static bool IsConsecutiveNumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && !Regex.IsMatch(arg,
                "(012|123|234|345|456|567|678|789|890|098|987|876|765|654|543|432|321|210)");
        }

        //Tekrarlı sayı içerir
        public static bool IsRepetitiveNumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && !Regex.IsMatch(arg,
                "([0-9])\\1\\1");
        }

        public static bool IsASpecialCharacter(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "[#?!@$%^&*-+]{1,}");
        }

        public static bool IsLetter(string arg)//Sadece harf ve Boşluk
        {
            return !string.IsNullOrEmpty(arg) && !Regex.IsMatch(arg,
                "([^a-zA-Z çÇğĞıİöÖşŞüÜ])");
        }

        public static bool IsLetterWithoutEmpty(string arg)//Sadece harf ve Boşluk
        {
            return !Regex.IsMatch(arg,
                "([^a-zA-Z çÇğĞıİöÖşŞüÜ])");
        }
    }
}
