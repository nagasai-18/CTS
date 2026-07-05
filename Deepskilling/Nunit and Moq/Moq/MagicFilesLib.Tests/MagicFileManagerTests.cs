using MagicFilesLib;
using Moq;
using NUnit.Framework;

namespace MagicFilesLib.Tests;

[TestFixture]
public class MagicFileManagerTests
{
    private Mock<IDirectoryExplorer> _directoryMock;

    private MagicFileManager _manager;

    [OneTimeSetUp]
    public void Init()
    {
        _directoryMock = new Mock<IDirectoryExplorer>();

        _directoryMock
            .Setup(x => x.GetFiles(It.IsAny<string>()))
            .Returns(new List<string>
            {
                "File1.txt",
                "File2.txt",
                "File3.txt"
            });

        _manager = new MagicFileManager(_directoryMock.Object);
    }

    [Test]
    public void GetAllFiles_Should_Return_ThreeFiles()
    {
        var files = _manager.GetAllFiles(@"C:\DummyFolder");

        Assert.That(files.Count, Is.EqualTo(3));

        Assert.That(files[0], Is.EqualTo("File1.txt"));
    }
}