[System.Serializable]
public class GameData
{
    public string slotText;
    public int saveSlot;

    //the values defined in this constructor will be default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.slotText = "Empty";
        this.saveSlot = 0;
    }
}
