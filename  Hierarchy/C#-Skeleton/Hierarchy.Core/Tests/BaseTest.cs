namespace Hierarchy.Tests
{
    using Core;
    using NUnit.Framework;
    [TestFixture]
    public class BaseTest
    {
        public IHierarchy<int> Hierarchy { get; private set; }

        public const int DefaultRootValue = 5;


        [SetUp] public void Init()
        {
            this.Hierarchy = new Hierarchy<int>(5);
        }
    }
}
