namespace MagicFilesLib;

public interface IDirectoryExplorer
{
    List<string> GetFiles(string path);
}