using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demo20230222.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo20230222.Models;

namespace Demo20230222.Services.Tests
{
    [TestClass()]
    public class EmpMthodsTests
    {
        [TestMethod()]
        public void EditEmpDataTest()
        {
            EmpMthods empMthodsemp = new EmpMthods();
            EmpData empData = new EmpData { EmpNo = "FG4516", Ext = "1011", Name = "陳薇羽"};
            var result = empMthodsemp.EditEmpData(empData);
            if (!result.IsSuccess)
            {
                Assert.Fail();
            }
        }
    }
}