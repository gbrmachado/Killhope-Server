using Killhope.Plugins.Rocks.Domain.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Test.Mocks
{
    public class MockMessageService : IMessageService
    {


        private Queue<Tuple<string, MessageResult>> Queue = new Queue<Tuple<string, MessageResult>>();
        private Queue<Action<ValidationResult>> valid = new Queue<Action<ValidationResult>>();

        public void AddAction(Action<ValidationResult> res)
        {
            valid.Enqueue(res);
        }

        public void Enqueue(string message, MessageResult result)
        {
            Queue.Enqueue(new Tuple<string, MessageResult>(message, result));
        }

        public void DisplayValidationResult(ValidationResult v)
        {
            valid.Dequeue()(v);
        }

        public MessageResult DisplayYesNoCancel(string message)
        {
            var res = Queue.Dequeue();
            if (res.Item1 == message)
                return res.Item2;

            //exception to avoid hitting any ExpectedExceptions in the tests.
            throw new Exception(String.Format("Expected: {0}, got {1}", res.Item1, message));
        }

        public MessageResult DisplayYesNo(string message)
        {
            var res = Queue.Dequeue();
            if (res.Item1 == message)
                return res.Item2;

            //exception to avoid hitting any ExpectedExceptions in the tests.
            throw new Exception(String.Format("Expected: {0}, got {1}", res.Item1, message));
        }

        internal void AddOnlyValid()
        {
            this.AddAction((a) =>
            {
                if (!a.isValid)
                {
                    string message = a.ToString();
                    Assert.Fail("Invalid Rock Supplied: " + message);
                }
            });
        }




        public void DisplayError(Exception e, string p)
        {
            throw new NotImplementedException();
        }
    }
}
