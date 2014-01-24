using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VMHostController.BIZ;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IVMHostBIZ biz = new VMHostBIZ();
            biz.UpdateVMInfo();
        }
    }
}
