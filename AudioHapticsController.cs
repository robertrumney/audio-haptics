using UnityEngine;
using UnityEngine.InputSystem;

public class AudioHapticsController : MonoBehaviour
{
    public float lowFrequencyThreshold = 0.2f;
    public float highFrequencyThreshold = 0.8f;
    public float maxHapticIntensity = 1.0f;

    private Gamepad gamepad;

    private void Start()
    {
        if (Gamepad.current == null)
        {
            enabled = false;
            return;
        }
        else
        {
            gamepad = Gamepad.current;
        }
    }

    private void Update()
    {
        // Analyze the audio data
        float[] spectrumData = new float[256];
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        float lowFrequencyValue = 0f;
        float highFrequencyValue = 0f;

        // Calculate low and high frequency values
        for (int i = 0; i < spectrumData.Length; i++)
        {
            if (i < spectrumData.Length * lowFrequencyThreshold)
            {
                lowFrequencyValue += spectrumData[i];
            }
            else if (i > spectrumData.Length * highFrequencyThreshold)
            {
                highFrequencyValue += spectrumData[i];
            }
        }

        // Apply haptic feedback based on audio data
        float lowFreqIntensity = Mathf.Clamp(lowFrequencyValue, 0, maxHapticIntensity);
        float highFreqIntensity = Mathf.Clamp(highFrequencyValue, 0, maxHapticIntensity);

        gamepad.SetMotorSpeeds(lowFreqIntensity, highFreqIntensity);
    }

    private void OnApplicationQuit()
    {
        // Reset haptic feedback when the application quits
        if (gamepad != null)
        {
            gamepad.ResetHaptics();
        }
    }
}
