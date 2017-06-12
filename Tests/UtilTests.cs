using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trycatchthat.Util;
using Xunit;

namespace Trycatchthat.Tests
{

    class TestFastMethods
    {
        internal int Return1() => 1;
        public string Echo(string s) =>  s;
    }

    public class UtilTests
    {
        [Fact]
        public void TestRunMethod()
        {
            FastMethod f = new FastMethod();
            var r = f.RunMethod<TestFastMethods, int>(nameof(TestFastMethods.Return1), new TestFastMethods(), null);
            Assert.Equal(1, r);
            var s = f.RunMethod<TestFastMethods, string>(nameof(TestFastMethods.Echo), new TestFastMethods(), new object[] { "ilias" });
            Assert.Equal("ilias", s);
        }
    }
}
