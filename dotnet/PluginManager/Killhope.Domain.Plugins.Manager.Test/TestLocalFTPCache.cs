using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Manager.Domain.Release;
using Killhope.Plugins.Manager.Domain.Test.Mock;
using System.Collections.Generic;

namespace Killhope.Plugins.Manager.Domain.Test
{
    [TestClass]
    public class TestLocalFTPCache
    {

        //private MockLocaReleaseManager localSiteViewer;
        //private string MockDomain = "HelloWorld.foo.bar.example";
        //private List<string> folders = new List<string> { "hello", "world" };

        //public LinkedList<FieldAccessException> test { get; }

        //private LocalFTPSiteCache getDefault()
        //{
        //    localSiteViewer = new MockLocaReleaseManager();
        //    localSiteViewer.Domain = MockDomain + "LOCAL";
            
        //    localSiteViewer.folders = folders;
        //    return new LocalFTPSiteCache(MockDomain, localSiteViewer);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void TestConstructorFailsOnNull()
        //{
        //    new LocalFTPSiteCache(null, new MockLocaReleaseManager());
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void TestConstructorFailsOnNullViewer()
        //{
        //    new LocalFTPSiteCache("aa", null);
        //}

        //[TestMethod]
        //public void DomainIsPropogated()
        //{
        //    var cache = getDefault();

        //    Assert.AreEqual(MockDomain + "LOCAL", cache.Domain);
        //}

        //[TestMethod]
        //public void LocalSiteIsOnlyQueriedOnceForDomain()
        //{
        //    var cache = getDefault();

        //    Assert.AreEqual(0, localSiteViewer.DomainQueries);

        //    var unused = cache.Domain;

        //    Assert.AreEqual(1, localSiteViewer.DomainQueries);

        //    unused = cache.Domain;

        //    Assert.AreEqual(1, localSiteViewer.DomainQueries);
        //    //A refresh should trigger another send.
        //    cache.Refresh();

        //    Assert.AreEqual(2, localSiteViewer.DomainQueries);
        //}

    }
}
