using System;
using Killhope.Plugins.Manager.Domain;

namespace Killhope.Plugins.Manager.Presentation.Test.Mocks
{
    class MockFTPReleaseSide : FTPReleaseSideFactory
    {
        public MockFTPReleaseSide() : base(null, null, null)
        {
        }

        public override ReleaseSide GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}
