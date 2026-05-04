# EMDR - VR 

## Videos 
- [Checkpoint Update](https://youtu.be/AxYwy7aAxfk)
- [Prototype Demoooo](https://youtu.be/qDXZ6uSjKYY)

## Code Overview

### GameManager.cd
Controls the game state (idle, playing, stopped).
Start/stop/reset logic lives here 

### BallController.cs
Moves the ball left and right, in charge of paddle bouncing, and plays spatial audio pings on contact for auditory bilateral simulation

### PaddleController.cs 
Reads Meta Quest controller input and in editor uses spacebar to trigger start, stop, and reset through GameManager

## Overleaf Link

- [Research Paper](https://www.overleaf.com/8782227375xkpchpncdhqk#13cf77)
## Next Steps 
- Get prototype running on Meta Quest headset
- Add paddlle movement using VR controller joysticks so users can move paddles up and down
- Add haptic feedback so controllers vibrate when the ball hits each paddle 
- Improve environment design and visuals 

