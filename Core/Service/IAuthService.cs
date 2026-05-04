using Core.Input;

namespace Core.Service
{
    public interface IAuthService
    {
        string Login(LoginInput input);
    }
}