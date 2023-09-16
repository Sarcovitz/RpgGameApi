namespace RpgGame.Services.Interfaces;

public interface ICryptographyService
{
    string Sha256Hash(string text);
}
