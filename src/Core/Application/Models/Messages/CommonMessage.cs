using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Messages
{
    public static class CommonMessage
    {
        public static class BusinessMessages
        {
            public const string WrongPassword = "Wrong password or email";
        }
        
        public static class Validation
        {
            public static string NotNull(string fieldName) => $"{fieldName} field cannot be empty!";
            public static string Length(string fieldName, int size) => $"{fieldName} field must be {size} characters long!";
            public static string MaxLength(string fieldName, int maxSize) => $"{fieldName} field can be maximum {maxSize} characters long!";
            public static string MinLength(string fieldName, int minSize) => $"{fieldName} field must be at least {minSize} characters long!";
            public static string BetweenLength(string fieldName, int minSize, int maxSize) => $"{fieldName} field must be between {minSize} and {maxSize} characters long!";
            public static string GreaterThan(string fieldName, int minSize) => $"{fieldName} field must be greater than {minSize}!";
            public static string LessThan(string fieldName, int maxSize) => $"{fieldName} field must be less than {maxSize}!";
            public static string GreaterThanOrEqual(string fieldName, int minSize) => $"{fieldName} field must be greater than or equal to {minSize}!";
            public static string LessThanOrEqual(string fieldName, int maxSize) => $"{fieldName} field must be less than or equal to {maxSize}!";
            public const string Email = "Please enter a valid email!";
            public const string PasswordsNotMatches = "Passwords do not match!";
        }

        public static class RegexErr
        {
            public static string WhiteSpace(string fieldName) => $"Whitespace is not allowed in {fieldName} field!";
            public static string DuplicateWhiteSpace(string fieldName) => $"Consecutive whitespace is not allowed in {fieldName} field!";
            public static string OnlyNumber = "Please enter only numbers!";
        }

    }
}
