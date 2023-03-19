using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<BackgroundMusic>().Length;
        if (numMusicPlayers != 1)
        {
            Destroy(this.gameObject);
        }
        // if more then one music player is in the scene
        //destroy ourselves
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
