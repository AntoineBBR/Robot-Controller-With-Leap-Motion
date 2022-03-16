# Robot Controller with Leap Motion

A simple project to control a `LegoV3MindStorms` thanks to a `Leap Motion`

## Getting started

### Part 1

1. DownLoad & Install the Leap Motion software [here](https://developer.leapmotion.com/tracking-software-download)
2. Plug your Leap Motion device
3. Activate the Bluetooth on your computer
4. Pair your LegoV3MindStorms and your computer (Computer's Settings -> add a Bluetooth device -> click on the "DeviceName" to Connect -> accept the paring on your brick -> add a ping code on it -> fill the connection field with the same code on your computer -> Everything's done!)
5. Launch the `RobotLego.sln` located in the folder: `./App/RobotLego`
6. Build & Run the solution. (make sure that `LegoController` is the starting project. If not, right click on `LegoController` in solution explorer -> Set as startup project)
7. Click the button "Scan" on the top-right corner of the window
8. Wait the LegoDevice to appear on the list and then click one time to connect it (A sound should be played!)

### Part 2.1

How to use the `Graphic Interface`

1. The first 4 buttons are some tests to check if the motors works correctly
2. The 3 "Action" button are save preset to present all the ways the robot can move
3. The next 11 buttons represent the 11 ways the robot can go
4. The slider next to them is the power gauge of the motors, tweak it to see the robot change it powerness
5. Finally the sensors in the bottom of the windows give informations about externals obstacles (1 to 100cm)

### Part 2.2

How to use the `Leap Motion`

1. Put your right hand verticaly allign with the device at a heigh of 20cm
2. Put your left hand next to it at the same heigth
3. Close your right hand to engage the system
4. Choose one of the 8 cardinals direction reconize by the system with your right hand (The robot must move the way you choose)
5. Close your left hand to start the speed control system
6. Try to `rise/down` your left hand vertically to see the robot `accelerate/decelerate`
7. You can also turn your right wrist to turn the robot itself (turn left for left, right for right)
8. Make sure to keep your hands not far away from the leap motion  
   
> **WARNING !!** To stop the robot, open your right hand!! Don't left to use the graphic interface to stop the robot if it is **running away!**

## Advices

### Leap Motion

1. Open the `UltraLeap Tracking Visualizer` to easiliy see what you are doing. (basicly if you are center to the leap motion device)
2. We strongly recommend to move slowly to start to understand the way the system works.
3. If you sometimes loose the signal, please make sure your hand stay in the caption range of the device. (If you still get some issue, try to not completely close your hands.)


## Credits

- Co-author: [Valentin Clergue](https://github.com/HandyS11)
  
- Co-author: [Antoine Imbert](https://github.com/AntoineBBR)

- Professor: [Marc Chevaldonné](https://gitlab.iut-clermont.uca.fr/macheval) (Author of the librairies that make `LegoV3MindStorms` & `Leap Motion` working with C#)
