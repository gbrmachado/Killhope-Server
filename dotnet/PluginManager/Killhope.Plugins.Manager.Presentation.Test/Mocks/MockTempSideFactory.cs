using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Presentation;
using System;

namespace Killhope.Plugins.Manager.Presentation.Test.Mocks
{
    internal class MockTempSideFactory : LocalTempSideFactory
    {
        public MockTempSideFactory() : base(null)
        {
        }

        public override ReleaseSide GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}