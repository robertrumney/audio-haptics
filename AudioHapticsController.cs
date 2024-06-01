using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class AudioHapticsController : MonoBehaviour
{
    private readonly float[] spectrumData = new float[256];

    private void Awake()
    {
        InputSystem.ResetHaptics();
    }

    private void OnEnable()
    {
        InputSystem.ResumeHaptics();
    }

    private void OnDisable()
    {
        InputSystem.PauseHaptics();
    }

    public float cutoff = 20;

    // Threshold to avoid slight buzzing
    public float threshold = 0.01f;

    private void Update()
    {
        if (Gamepad.current == null) return;

        // Get spectrum data from the AudioListener to capture all audio output
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Define the cutoff frequency index based on more perceptual weighting (logarithmic approach)
        int cutoffIndex = spectrumData.Length / 4; // Reducing the range for low frequencies

        float lowFreqAverage = spectrumData.Take(cutoffIndex).Average();
        float highFreqAverage = spectrumData.Skip(cutoffIndex).Take(spectrumData.Length - cutoffIndex).Average();

        // Reduce the multiplication factor for bass response and adjust the upper range more perceptually
        float lowFreqVibrationIntensity = lowFreqAverage < threshold ? 0 : Mathf.Clamp(lowFreqAverage * 10, 0, 1); // Lower multiplier for bass
        float highFreqVibrationIntensity = highFreqAverage < threshold ? 0 : Mathf.Clamp(highFreqAverage * 50, 0, 1); // Adjusted multiplier for treble

        // Apply vibration to the gamepad motors
        Gamepad.current.SetMotorSpeeds(lowFreqVibrationIntensity, highFreqVibrationIntensity);
    }
}
