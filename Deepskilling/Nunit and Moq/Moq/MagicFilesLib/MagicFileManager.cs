namespace MagicFilesLib;

public class MagicFileManager
{
    private readonly IDirectoryExplorer _directoryExplorer;

    public MagicFileManager(IDirectoryExplorer directoryExplorer)
    {
        _directoryExplorer = directoryExplorer;
    }

    public List<string> GetAllFiles(string path)
    {
        return _directoryExplorer.GetFiles(path);
    }
}