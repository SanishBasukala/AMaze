using UnityEngine;
using UnityEngine.UI;
public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider = null;

    private void Start()
    {
        LoadVolume();
    }

    public void SaveVolume()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeValue);
        LoadVolume();
    }

    public void LoadVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
