# Unity Audio Haptics Feedback

This is a simple script used to translate audio response from a single AudioListener into Gamepad vibration feedback, without the need to configure each vibration and audiosource manually.

## Features

- **Real-time Audio Analysis**: Analyze the audio output in real-time to extract frequency data.
- **Dynamic Haptic Feedback**: Convert audio frequencies into nuanced haptic feedback.
- **Customizable Frequency Ranges**: Easily adjust the frequency range for haptic feedback to suit different game types or preferences.
- **Plug-and-Play**: Designed to be easily integrated into any Unity project with minimal setup.

## Getting Started

### Prerequisites

- Unity 2019.4 LTS or later
- Gamepad compatible with Unity's new Input System

### Usage

Simply play your scene. The script automatically analyzes audio output and translates it into haptic feedback on the connected gamepad. Adjust the script parameters to fine-tune the haptic feedback.

## Configuration

- **Spectrum Data Size**: Adjust the `spectrumData` array size based on the complexity of the audio.
- **Frequency Cutoff**: Modify the cutoff index to separate low and high frequencies according to your needs.

## Contributing

I welcome contributions and suggestions! Please fork the repository and submit pull requests for any improvements you wish to make.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
