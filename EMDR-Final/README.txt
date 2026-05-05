Checkpoint 2 videos:
- [Prototype demonstration & code discussion](https://youtu.be/ePx4P11E2eU)
- [Project updates & areas of completion](https://youtu.be/_TK1D2wyDbY)

Link to OverLeaf:
[The Effects of VR Bilateral Stimulation
Application on Emotional Stress](waiting on link)

Scripts

GameManager.cs

Controls the full game flow through five states: welcome, instructions, countdown, playing, and paused.
 Displays dynamic text in the headset at every stage guiding the user through 
how to start, pause, and resume. Includes a 3-2-1 coroutine countdown, 
and when ball goes out of bounds the state switches to outofbounds and prompts user to 
press A to try again. 

BallController.cs

Moves the ball with a random vertical angle on each launch and bounce so no two bounces look 
the same. Includes miss detection so if the paddle doesnt make contact the ball resets and 
the game returns to the Welcome state. Plays spatial audio pings from whichever side the ball 
bounces on for auditory bilateral stimulation. Triggers haptic vibration on the corresponding 
left or right controller for tactile bilateral stimulation.

PaddleController.cs

Reads Meta Quest controller input directly from hardware using CommonUsages.primary2DAxis and 
InputDevices.GetDeviceAtXRNode, bypassing the XRI action map system entirely for reliable 
on-device input. Moves paddles up and down using the thumbsticks with a dead zone to prevent 
drift and Y-axis clamping so paddles can't leave the play area. Retries controller detection 
every frame until both controllers are found. Button 
press uses a wasPressed flag so it fires once per press rather than every frame. Spacebar and 
arrow keys available for editor testing.

Next Steps
- Begin participant data collection
- Complete results and discussion sections of the research paper 
  once data collection is finished

