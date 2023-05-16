namespace Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel command);
        bool IsAuthenticated();
        string GetRoleId();
        void SignOut();

        AuthViewModel GetAccountAuthViewModel(long accountId);
    }
}
