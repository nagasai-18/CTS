namespace PlayersManagerLib;

public interface IPlayerMapper
{
    bool RegisterPlayer(Player player);

    Player GetPlayerById(int id);
}