namespace PlayersManagerLib;

public class PlayerManager
{
    private readonly IPlayerMapper _playerMapper;

    public PlayerManager(IPlayerMapper playerMapper)
    {
        _playerMapper = playerMapper;
    }

    public bool RegisterPlayer(Player player)
    {
        return _playerMapper.RegisterPlayer(player);
    }

    public Player GetPlayer(int id)
    {
        return _playerMapper.GetPlayerById(id);
    }
}