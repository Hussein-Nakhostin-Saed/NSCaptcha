namespace NCaptcha;

public interface ICaptchaTokenProtector
{
    string Encrypt(string token);
    string Decrypt(string token);
}
