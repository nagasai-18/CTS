using Moq;
using NUnit.Framework;
using PlayersManagerLib;

namespace PlayersManagerLib.Tests;

[TestFixture]
public class PlayerManagerTests
{
    private Mock<IPlayerMapper> _mapperMock;
    private PlayerManager _manager;

    [OneTimeSetUp]
    public void Init()
    {
        _mapperMock = new Mock<IPlayerMapper>();

        _mapperMock
            .Setup(x => x.RegisterPlayer(It.IsAny<Player>()))
            .Returns(true);

        _mapperMock
            .Setup(x => x.GetPlayerById(It.IsAny<int>()))
            .Returns(new Player
            {
                Id = 1,
                Name = "Virat",
                Age = 35
            });

        _manager = new PlayerManager(_mapperMock.Object);
    }

    [Test]
    public void RegisterPlayer_Should_Return_True()
    {
        var player = new Player
        {
            Id = 1,
            Name = "Virat",
            Age = 35
        };

        bool result = _manager.RegisterPlayer(player);

        Assert.That(result, Is.True);
    }

    [Test]
    public void GetPlayer_Should_Return_Player()
    {
        var player = _manager.GetPlayer(1);

        Assert.That(player.Name, Is.EqualTo("Virat"));
        Assert.That(player.Age, Is.EqualTo(35));
    }
}