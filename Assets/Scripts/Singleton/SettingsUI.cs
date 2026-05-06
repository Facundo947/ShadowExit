using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (SettingsManager.Instance != null)
        {
            if (musicSlider != null)
                musicSlider.value = SettingsManager.Instance.GetMusicVolume();

            if (sfxSlider != null)
                sfxSlider.value = SettingsManager.Instance.GetSfxVolume();
        }
    }

    public void OnMusicSliderChanged(float value)
    {
        SettingsManager.Instance?.SetMusicVolume(value);
    }

    public void OnSfxSliderChanged(float value)
    {
        SettingsManager.Instance?.SetSfxVolume(value);
    }
}
