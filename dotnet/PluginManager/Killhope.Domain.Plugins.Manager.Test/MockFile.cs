using System.Text;

namespace Killhope.Plugins.Manager.Domain.Test
{
    class MockFile : File
    {
        private string ret;

        public MockFile(ItemLocation itemLocation, string p) : base(itemLocation)
        {
            this.ret = p;
        }

        public override byte[] GetContent()
        {
            return Encoding.Unicode.GetBytes(ret);
        }
    }
}
