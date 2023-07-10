using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    [SerializeField] private GameObject SoundPanel;

    private void Start()
    {
        SoundPanel.SetActive(false);
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("volume", volumeSlider.value);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
