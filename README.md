# Robot Controller with Leap Motion

A simple project to control a LegoV3MindStorms thanks to a Leap Motion

## Getting started

### Part 1

1. DownLoad & Install the Leap Motion software [here](https://developer.leapmotion.com/tracking-software-download)
2. Plug your Leap Motion device
3. Activate the Bluetooth on your computer
4. Pair your LegoV3MindStorms and your computer (add a Bluetooth device -> click on the "DeviceName" to Connect -> accept the paring on your brick -> add a ping code on it -> fill the connection field with the same code on your computer -> Everything's done!)
5. Launch the `RobotLego.sln` located in the folder: `./App/RobotLego`
6. Build & Run the solution. (make sure that `LegoController` is the starting project)
7. Click the button "Connect" on the bottom of the window
8. Wait the LegoDevice and then click one time to connect it (A sound should be played!)

### Part 2.1

How t ouse the `Graphic Interface`

1. The first 4 buttons are some tests to check if the motors works correctly
2. The 11 buttons that represent the 11 ways the robot can go
3. The slider is the power gauge of the motors, tweak it to see the robot change it powerness

### Part 2.2

How to use the `Leap Motion`

1. Put your hand verticaly allign with the device at a heigh of 15cm
2. Close your fist to engage the system
3. Choose one of the 8 cardinals direction reconize by the system (The robot must move the way you choose)
4. Try to `rise/down` your hand vertically to see the robot `accelerate/decelerate`
5. You can also turn your wrist to turn the robot itself
   
   
> **WARNING !!** To stop it, you should place your hand at the starting place. The feature to stop it while he doesn't found you is in progress !! Don't left to use the graphic interface to stop the robot if it is **running away!**

## Advices

### Leap Motion

1. Open the `UltraLeap Tracking Visualizer` to easiliy see what you are doing. (basicly if you are center to the leap motion device)
2. We strongly recommend to move slowly to start to understand the way the system works.
3. If you sometimes loose the signal, please make sure your hand stay in the caption range of the device. (If you still get some issue, try to doesn't completely close your hand.)


## Credits

- Co-author: [Clergue Valentin](https://github.com/HandyS11)
- Co-author: [Imbert Antoine](https://www.youtube.com/watch?v=dQw4w9WgXcQ)