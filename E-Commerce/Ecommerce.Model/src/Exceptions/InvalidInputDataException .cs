using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class InvalidInputDataException : Exception
    {
        public string FieldName { get; }
        public string Issue { get; }

        public InvalidInputDataException(string fieldName, string issue)
            : base($"Invalid input data: {fieldName} - {issue}")
        {
            FieldName = fieldName;
            Issue = issue;
        }

        public InvalidInputDataException(string message = "")
            : base(message == "" ? "Invalid input data" : message) { }
    }
}
