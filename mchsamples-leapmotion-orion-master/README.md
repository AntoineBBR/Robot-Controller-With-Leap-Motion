# mchSamples LeapMotion Orion

A sample project showing how to use a LeapMotion with Orion SDK under Windows, WITHOUT Unity !

## If you want to run my sample, follow these instructions:

1) Download and install Leap Motion SDK "UltraLeap Gemini" (last seen on january, 27th of 2022 here:https://developer.leapmotion.com/)

2) Plug your Leap Motion device

3) Launch the projet under Visual Studio, build and run


## If you want to create your own project, follow these instructions:

1) Download and install Leap Motion SDK "UltraLeap Gemini" (last seen on january, 27th of 2022 here:https://developer.leapmotion.com/)

2) Create a new .NET Framework project under Visual Studio (Console App, WPF App, dll...)

3) Reference the Assembly-CSharp.dll from this repository

4) In the root folder of your project, create an ext folder and copy and paste the following dll into it:
- LeapC.dll (located in the ext folder of this repository)
- UnityEngine.CoreModule.dll (located in the ext folder of this repository)

5) In the post build event of your exe project, add the following lines:
```
xcopy /yr "$(ProjectDir)..\ext\LeapC.dll" "$(TargetDir)"
xcopy /yr "$(ProjectDir)..\ext\UnityEngine.CoreModule.dll" "$(TargetDir)"
```

6) Under the build properties, choose x64 as Target Platform

7) In your code, in order to connect to the LeapMotion and get some hand data, add:
```C#
Leap.Controller ctrl = new Leap.Controller();
ctrl.FrameReady += Ctrl_FrameReady;
ctrl.StartConnection();
```

8) add also the following method:
```C#
private void Ctrl_FrameReady(object sender, Leap.FrameEventArgs e)
{
    var hand = e.frame.Hands.FirstOrDefault();
    var position = hand != null ? hand.PalmPosition : new Leap.Vector();
    float grabStrength = hand != null ? hand.GrabStrength : 0;
}
```
(eventually, make this method static)

9) Plug your Leap Motion device

10) Build and run

11) Adapt your code and have fun
