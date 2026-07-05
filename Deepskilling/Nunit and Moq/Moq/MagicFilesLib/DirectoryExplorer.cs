using System.IO;

namespace MagicFilesLib;

public class DirectoryExplorer : IDirectoryExplorer
{
    public List<string> GetFiles(string path)
    {
        return Directory.GetFiles(path).ToList();
    }
}