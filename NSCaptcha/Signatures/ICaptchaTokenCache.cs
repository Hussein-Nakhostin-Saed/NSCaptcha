namespace NCaptcha;

public interface ICaptchaTokenCache
{
    void Cache(string key, string value);
    Token Retrieve(string key);
    void Clear(string key);
}
