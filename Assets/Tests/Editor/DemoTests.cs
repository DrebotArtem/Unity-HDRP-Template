using DrebotGS;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class DemoTests
    {
        [Test]
        public void WhenIncreaseValue_AndCreateNewDemoScriptClass_ThenValueShouldBe1()
        {
            // Arrage.
            DemoScript demo = new DemoScript();

            // Act.
            demo.IncreaseValue();

            // Assert.
            new { Value = demo.Value }.Should().Be(new { Value = 1 });
        }
    }
}