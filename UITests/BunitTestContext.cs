using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;
using MudBlazor.Services;
namespace UITests
{
    public abstract class BunitTestContext : TestContextWrapper
    {
        [TestInitialize]
        public void Setup()
        {
            TestContext = new Bunit.TestContext();
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            TestContext.Services.AddMudServices();
        }

        [TestCleanup]
        public void TearDown() => TestContext?.Dispose();
    }
}
