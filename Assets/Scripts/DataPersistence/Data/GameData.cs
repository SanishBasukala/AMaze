[System.Serializable]
public class GameData
{
    public string slotText1;
    public string slotText2;
    public string slotText3;

    //the values defined in this constructor will be default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.slotText1 = "Empty";
        this.slotText2 = "Empty";
        this.slotText3 = "Empty";
    }
}
