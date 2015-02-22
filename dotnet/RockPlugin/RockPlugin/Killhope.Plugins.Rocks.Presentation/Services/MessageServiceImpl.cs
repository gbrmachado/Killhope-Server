using Killhope.Plugins.Rocks.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Killhope.Plugins.Rocks.Presentation
{
    class MessageServiceImpl : IMessageService
    {
        public void DisplayValidationResult(ValidationResult v)
        {
            throw new NotImplementedException();
        }

        public MessageResult DisplayYesNoCancel(string message)
        {
            var result = MessageBox.Show(message, "", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
                return MessageResult.Yes;
            if (result == DialogResult.No)
                return MessageResult.No;
            if (result == DialogResult.Cancel)
                return MessageResult.Cancel;

            throw new NotImplementedException();
        }

        public MessageResult DisplayYesNo(string message)
        {
            var result = MessageBox.Show(message, "", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
                return MessageResult.Yes;
            if (result == DialogResult.No)
                return MessageResult.No;

            throw new NotImplementedException();
        }


        public void DisplayError(Exception e, string p)
        {
            MessageBox.Show(String.Format("{0} : {1} \r\n\r\n {2}", p, e.Message, e.StackTrace));
        }
    }
}
