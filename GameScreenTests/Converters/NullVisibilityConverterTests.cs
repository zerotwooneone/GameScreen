using GameScreen.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Globalization;
using System.Windows;

namespace GameScreenTests.Converters
{
    [TestClass]
    public class NullVisibilityConverterTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private NullVisibilityConverter CreateNullVisibilityConverter()
        {
            return new NullVisibilityConverter();
        }

        [TestMethod]
        public void Convert_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateNullVisibilityConverter();
            object value = null;
            Type targetType = Visibility.Collapsed.GetType();
            object parameter = null;
            CultureInfo culture = CultureInfo.CurrentCulture;

            // Act
            var result = unitUnderTest.Convert(
                value,
                targetType,
                parameter,
                culture);

            // Assert
            Assert.Fail();
        }
    }
}