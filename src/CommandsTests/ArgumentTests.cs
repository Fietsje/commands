namespace Commands.Tests
{
	[TestCategory("Arguments")]
	[TestClass()]
	public class ArgumentTests
	{
		[TestMethod()]
		public void Argument_WithStringValue_ThenValuesAreEqual()
		{
			// Arrange
			var systemUnderTest = new Argument<string>("test");

			// Act

			// Assert
			Assert.AreEqual("test", systemUnderTest.Value);
		}

		[TestMethod()]
		public void InArgument1_WithStringValue_ThenValuesAreEqual()
		{
			// Arrange
			var systemUnderTest = new InArgument<string>("test");

			// Act

			// Assert
			Assert.AreEqual("test", systemUnderTest.Value);
		}



		[TestMethod()]
		public void InArgument2_WithStringValue_ThenValuesAreEqual()
		{
			// Arrange
			InArgument<string> systemUnderTest = "test";

			// Act

			// Assert
			Assert.AreEqual("test", systemUnderTest.Value);
		}


		[TestMethod()]
		public void OutArgument_WithStringValue_ThenValuesAreEqual()
		{
			// Arrange
			var systemUnderTest = new OutArgument<string>("test");

			// Act

			// Assert
			Assert.AreEqual("test", systemUnderTest.Value);
		}
	}
}