using Killhope.Plugins.Manager.Domain.Release;
using Killhope.Plugins.Manager.Domain;
using Killhope.Plugins.Manager.Domain.Release.Local_IO;

namespace Killhope.Plugins.Manager.Presentation
{
    class LocalTempSideFactory : ReleaseSideFactory
    {
        private readonly LocalReleaseManager releaseManager;

        public LocalTempSideFactory(LocalReleaseManager releaseManager)
        {
            this.releaseManager = releaseManager;
        }

        public override ReleaseSide GetInstance()
        {
            return new TempReleaseSide(this.releaseManager);
        }
    }
}
