namespace NCaptcha;

public interface ICaptchaTokenSerializer
{
    void Serialize(string token);
    string Deserialize();
    void Clear();
}
