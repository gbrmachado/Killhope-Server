using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Manager.Domain.Test.Mock
{
    class MockReleaseSide : ReleaseSide
    {
        public override bool CanAdd { get { return true; } }
        public override bool CanRemove { get { return true; } }
        public override bool CanRefresh { get { return true; } }




        public override void Add(File file)
        {
            throw new NotSupportedException();
        }

        public override void Remove(ItemLocation fileLocation)
        {
            throw new NotSupportedException();
        }

        public override void Refresh()
        {
            throw new NotSupportedException();
        }

        public override void InitialLoad()
        {
            throw new NotSupportedException();
        }
    }
}
