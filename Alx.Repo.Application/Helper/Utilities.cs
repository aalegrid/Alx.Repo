using Alx.Repo.Application.Auth.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Alx.Repo.Application.Helper
{
    public static class Utilities
    {

        private static IConfiguration? _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Format a model error message by removing specific substrings and capitalizing the first letter.
        /// </summary>
        /// <param name="errorMessage">The string to format</param>
        /// <returns>Formatted error message</returns>

        public static string FormatModelStateValidationError(string errorMessage)
        {

            var stripList = _configuration?
                .GetSection("ModelStateValidation:StripList")
                .GetChildren()
                .Select(s => s.Value)
                .Where(v => !string.IsNullOrEmpty(v))
                .Cast<string>()
                .ToList() ?? new List<string>();

            var toRemove = new[] { typeof(LoginUser).FullName!, typeof(RegisterUser).FullName! }
                .Concat(stripList);

            foreach (var word in toRemove)
                errorMessage = errorMessage.Replace(word, "");

            return CapitalizeFirstLetter(FormatFieldNames(errorMessage.Trim()));


        }

        /// <summary>
        /// Capitalizes the first letter of a given string.
        /// </summary>
        /// <param name="input">The string to capitalize.</param>
        /// <returns>The string with its first letter capitalized, or an empty string if the input is null or empty.</returns>
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }
        /// <summary>
        /// Formats field names enclosed in single quotes by capitalizing the first letter of each word.
        /// </summary>
        /// <remarks>This method processes only the text enclosed in single quotes. Words within the
        /// quotes are split by spaces,  and each word's first letter is converted to uppercase while the rest of the
        /// word remains unchanged.</remarks>
        /// <param name="input">The input string containing field names enclosed in single quotes.</param>
        /// <returns>A string where each field name enclosed in single quotes has its words capitalized.  Words are delimited by
        /// spaces, and the original casing of other parts of the string remains unchanged.</returns>

        public static string FormatFieldNames(string input)
        {
            return Regex.Replace(input, @"'([^']*)'", m =>
            {
                var capitalized = string.Join(" ", m.Groups[1].Value
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(w => char.ToUpper(w[0]) + w.Substring(1)));
                return $"'{capitalized}'";
            });
        }
    }
}
