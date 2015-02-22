using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Rocks.Domain.Application
{
    public class ValidationResult
    {

        //TODO: Superclass with AddIf() visible.

        public bool isValid { get { return !messages.Any(); } }

        private List<string> messages = new List<string>();

        public void AddIfNullOrEmpty(string str, string p)
        {
            AddIf(String.IsNullOrEmpty(str), String.Format("{0} must have a non-zero length.", p));
        }

        public void AddIfNull(object value, string p)
        {
            AddIf(value == null, String.Format("{0} must not be null.", p));
        }

        public void AddIf(bool predicate, string errorMessage)
        {
            if (predicate)
                messages.Add(errorMessage);
        }

        public void AddIfEmpty<T>(IEnumerable<T> Content, string message)
        {

            AddIf(Content == null || !Content.Any(), message);
                
        }

        public void ThowIfInvalid()
        {
            if (!this.isValid)
                throw ToException();
        }

        public override string ToString()
        {
            if (isValid)
                return "No Errors";

            return "Errors: " +  String.Join("\n", this.messages);
        }

        public ValidationException ToException()
        {
            return new ValidationException(this.messages);
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(IEnumerable<string> messages) : base(ListToString(messages)) { }

        private static string ListToString(IEnumerable<string> s)
        {
            return "Errors: \n" + String.Join("\n", s);
        }
    }
}
