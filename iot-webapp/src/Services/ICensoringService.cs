namespace Academicos.Services {
    public interface ICensoringService {
        string CensorText(string text);
        bool ContainsCensoredWords(string text);
    }
}