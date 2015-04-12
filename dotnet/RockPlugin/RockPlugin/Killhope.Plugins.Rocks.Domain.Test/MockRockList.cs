using Killhope.Plugins.Rocks.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    class MockRockList : IJSONModificationService
    {

        public MockRockList() { }
        public MockRockList(RockList list) { UseAsOutput = list; }

        public RockList UseAsOutput;
        public RockList LastResult;

        public string Load()
        {
            return RockList.ToJson(UseAsOutput);
        }

        public void Save(string JSON)
        {
            LastResult = RockList.FromJson(JSON);
        }
    }
}
