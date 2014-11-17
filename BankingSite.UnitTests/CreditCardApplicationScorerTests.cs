using Moq;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingSite.UnitTests
{
    [TestClass]
    [TestFixture]
    public class CreditCardApplicationScorerTests
    {
        [TestMethod]
        [Test]
        public void ShouldDeclineUnderAgeApplicant()
        {
            var fakeGateway=new Mock<ICreditCheckerGateway>();

            var sut = new CreditCardApplicationScorer(fakeGateway.Object);

            var application = new CreditCardApplication
            {
                ApplicantAgeInYears = 20
            };


            var result = sut.ScoreApplication(application);
            NUnit.Framework.Assert.That(result,Is.False);
        }
                [TestMethod]
        [Test]
        public void ShouldAskGatewayForCreditCheck()
        {
            var fakeGateway = new Mock<ICreditCheckerGateway>();

            var sut = new CreditCardApplicationScorer(fakeGateway.Object);

            var application = new CreditCardApplication
            {
                ApplicantAgeInYears = 30,
                ApplicantName="Jason"
            };

            sut.ScoreApplication(application);

            //accepts a string param, we dont care what it is though
            //fakeGateway.Verify(x => x.HasGoodCreditHistory(It.IsAny<string>())); 

            fakeGateway.Verify(x => x.HasGoodCreditHistory("Jason"),Times.Once); 
        }

        [TestMethod]
        [Test]
        public void ShouldAcceptCorrectAgedApplicantWithGoodCreditHistory()
        {
            var fakeGateway = new Mock<ICreditCheckerGateway>();

            //fakeGateway hasnt been told to reutn true, so it will return false be default
            //so need to tell it to return true

            fakeGateway.Setup(x => x.HasGoodCreditHistory(It.IsAny<string>())).Returns(true);

            var sut = new CreditCardApplicationScorer(fakeGateway.Object);

            var application = new CreditCardApplication
            {
                ApplicantAgeInYears = 30
            };


            var result = sut.ScoreApplication(application);
            NUnit.Framework.Assert.That(result, Is.True);
        }
    }
}
