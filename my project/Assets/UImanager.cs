using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UImanager : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle fxToggle;

    public Button opemButton;
    public Button ExitButton;

    public Slider bgmSlider;
    public Slider fxSlider;

    public GameObject panel;
    private void Awake()
    {
        bgmToggle.onValueChanged.AddListener(OnBGMToggleChange);
        fxToggle.onValueChanged.AddListener(OnFXToggleChange);

        opemButton.onClick.AddListener(OpenOptionPanel);
        ExitButton.onClick.AddListener(ExitOptionPanel);

        bgmSlider.onValueChanged.AddListener(OnBGMSliderChange);
        fxSlider.onValueChanged.AddListener(OnFXSliderChange);
    }

    private void OnBGMToggleChange(bool isOn)
    {
        Soundmanager.Instance.OnOffBGM(isOn);
       
    }

    private void OnFXToggleChange(bool isOn)
    {
    
        Soundmanager.Instance.OnOffFx(isOn);
    }

    private void OnBGMSliderChange(float volume)
    {
        Soundmanager.Instance.ChangeBGMVolume(volume);
    }

    private void OnFXSliderChange(float volume)
    {
        Soundmanager.Instance.ChangeFxVolume(volume);
    }
    private void OpenOptionPanel()
    {
        panel.SetActive(true);
    }

    private void ExitOptionPanel()
    {
        panel.SetActive(false);
    }
}
