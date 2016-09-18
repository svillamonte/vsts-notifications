using System;
using Xunit;
using VstsNotifications.Services;

namespace VstsNotifications.Tests.Services
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            var app = new Application();
            Assert.True(app.Method1() == 1);
        }
    }
}
