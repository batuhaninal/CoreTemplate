using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            public static string NotNull() => "{PropertyName} field cannot be empty!";

            public static string Length(string fieldName, int size) => $"{fieldName} field must be {size} characters long!";
            public static string Length(string fieldName) => $"{fieldName} field must be {{TotalLength}} characters long!";
            public static string Length() => "{PropertyName} field must be {TotalLength} characters long!";

            public static string MaxLength(string fieldName, int maxSize) => $"{fieldName} field can be maximum {maxSize} characters long!";
            public static string MaxLength(string fieldName) => $"{fieldName} field can be maximum {{MaxLength}} characters long!";
            public static string MaxLength() => "{PropertyName} field can be maximum {MaxLength} characters long!";

            public static string MinLength(string fieldName, int minSize) => $"{fieldName} field must be at least {minSize} characters long!";
            public static string MinLength(string fieldName) => $"{fieldName} field must be at least {{MinLength}} characters long!";
            public static string MinLength() => "{PropertyName} field must be at least {MinLength} characters long!";

            public static string BetweenLength(string fieldName, int minSize, int maxSize) => $"{fieldName} field must be between {minSize} and {maxSize} characters long!";
            public static string BetweenLength(string fieldName) => $"{fieldName} field must be between {{MinLength}} and {{MaxLength}} characters long!";
            public static string BetweenLength() => "{PropertyName} field must be between {MinLength} and {MaxLength} characters long!";

            public static string GreaterThan(string fieldName, int minSize) => $"{fieldName} field must be greater than {minSize}!";
            public static string GreaterThan(string fieldName) => $"{fieldName} field must be greater than {{MinLength}}!";
            public static string GreaterThan() => "{PropertyName} field must be greater than {MinLength}!";

            public static string LessThan(string fieldName, int maxSize) => $"{fieldName} field must be less than {maxSize}!";
            public static string LessThan(string fieldName) => $"{fieldName} field must be less than {{MaxLength}}!";
            public static string LessThan() => "{PropertyName} field must be less than {MaxLength}!";

            public static string GreaterThanOrEqual(string fieldName, int minSize) => $"{fieldName} field must be greater than or equal to {minSize}!";
            public static string GreaterThanOrEqual(string fieldName) => $"{fieldName} field must be greater than or equal to {{MinLength}}!";
            public static string GreaterThanOrEqual() => "{PropertyName} field must be greater than or equal to {MinLength}!";

            public static string LessThanOrEqual(string fieldName, int maxSize) => $"{fieldName} field must be less than or equal to {maxSize}!";
            public static string LessThanOrEqual(string fieldName) => $"{fieldName} field must be less than or equal to {{MaxLength}}!";
            public static string LessThanOrEqual() => "{PropertyName} field must be less than or equal to {MaxLength}!";

            public const string Email = "Please enter a valid email!";
            public const string PasswordsNotMatches = "Passwords do not match!";
        }

        public static class RegexErr
        {
            public static string WhiteSpace(string fieldName) => $"Whitespace is not allowed in {fieldName} field!";
            public static string WhiteSpace() => "Whitespace is not allowed in {PropertyName} field!";

            public static string DuplicateWhiteSpace(string fieldName) => $"Consecutive whitespace is not allowed in {fieldName} field!";
            public static string DuplicateWhiteSpace() => "Consecutive whitespace is not allowed in {PropertyName} field!";

            public static string OnlyNumber = "Please enter only numbers!";
        }

    }
}
