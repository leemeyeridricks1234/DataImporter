using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Workflow;

namespace WorkflowTest
{
    [TestFixture]
    public class DataTest
    {
        [Test]
        public void ImportTest()
        {
            var data = new Data();
            data.Import();
        }
    }
}
