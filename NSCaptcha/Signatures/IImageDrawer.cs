namespace NCaptcha;

public interface IImageDrawer
{
    byte[] DrawText(string text);
}
