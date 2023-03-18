using Realms;
public class GameModel : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public string Id { get; set; }

    [MapTo("high_score")]
    public int HighScore { get; set; }

    public GameModel() { }

    public GameModel(string userId)
    {
        this.Id = userId;
        this.HighScore = 0;
    }
}