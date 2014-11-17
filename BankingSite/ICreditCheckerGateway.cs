namespace BankingSite
{
    public interface ICreditCheckerGateway
    {
        bool HasGoodCreditHistory(string personsName);
    }
}