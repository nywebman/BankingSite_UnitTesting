namespace BankingSite
{
    public class CreditCardApplicationScorer
    {
        private readonly ICreditCheckerGateway _creditCheckerGateway;

        public CreditCardApplicationScorer(ICreditCheckerGateway creditCheckerGateway)
        {
            _creditCheckerGateway = creditCheckerGateway;
        }

        public bool ScoreApplication(CreditCardApplication application)
        {
            var isApplicantTooYoung = application.ApplicantAgeInYears < 21;

            if (isApplicantTooYoung)
            {
                return false;
            }

            var hasGoodCreditHistory =  _creditCheckerGateway.HasGoodCreditHistory(application.ApplicantName);

            return hasGoodCreditHistory;
        }
    }
}