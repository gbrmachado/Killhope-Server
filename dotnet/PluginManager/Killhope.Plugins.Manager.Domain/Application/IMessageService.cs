using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Rocks.Domain.Application
{
    public interface IMessageService
    {
        void DisplayValidationResult(ValidationResult v);

        MessageResult DisplayYesNoCancel(string message);

        MessageResult DisplayYesNo(string message);

        void DisplayError(Exception e, string p);
    }

    public enum MessageResult
    {
        Cancel = 0,
        Yes = 1,
        No = 2,
    }
}
