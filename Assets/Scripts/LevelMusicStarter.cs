using UnityEngine;

public class LevelMusicStarter : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance?.PlayGameplayMusic();
    }
}