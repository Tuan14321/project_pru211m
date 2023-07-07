using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public void SetVolume()
    {
        audioMixer.SetFloat("volume", volumeSlider.value);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
