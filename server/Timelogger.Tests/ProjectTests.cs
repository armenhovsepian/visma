using System;
using Timelogger.Entities;
using Xunit;

namespace Timelogger.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void CanRegisterTime_DateRangeMustBe30SecOrLonger_ToProject()
        {
            var project = new Project("test 2", DateTime.Now);
            var result = project.RegisterTime(DateTime.Now, DateTime.Now.AddMinutes(31));

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CanNotRegisterTime_DateRangeMustNotBeLessThan30Sec_ToProject()
        {
            var project = new Project("test 3", DateTime.Now);
            var result = project.RegisterTime(DateTime.Now, DateTime.Now.AddMinutes(10));

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void CanNotRegisterTime_ToCompletedProject()
        {
            var project = new Project("test 4", DateTime.Now);
            project.Complete();
            var result = project.RegisterTime(DateTime.Now, DateTime.Now.AddMinutes(31));

            Assert.False(result.IsSuccess);
        }
    }
}
