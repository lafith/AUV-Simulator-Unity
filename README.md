# AUV-Simulator

![auv](https://user-images.githubusercontent.com/39316548/64912007-9d678300-d746-11e9-838c-53a10896be5b.png)

A simulator used to test control algorithms written for an Autonomous Underwater Vehicle

This simulator was developed as a part of my work in Team Tiburon, the autonomous underwater vehicle team of NIT Rourkela. It is developed using Unity3D and written in C#.

The simulator works by communicating with a control algorithm through TCP sockets. The control algorithm may run on a seperate machine.

The simulator receives from the control algorithm, the values of the forces to be applied to the individual thrusters. It send simulated sensor data to the control algorithm as feedback. Amongst the sensors used are cameras. Camera images are sent to the control algorithm every frame. To ensure optimal communication frequency, edge detection is performed on the images before sending.

The code for simulating underwater forces like planing force, slamming force etc., i.e. the contents of ./Underwater Forces, is experimental and not currently in use. This code was taken from the tutorial posted by Erik Nordeus, 'Make a realistic boat in Unity with C#', and all rights for that part of the code belong to him.

http://www.habrador.com/tutorials/unity-boat-tutorial/


STEPS:

The executable files are in Builds.zip.
For Windows,an installer-AUV Simulator setup.exe-is available inside the Builds.zip.

To use:
- Start AUV Simulator.exe(in Windows) or AUV Simulator.x86_64(in Linux).
- In the Menu enter IP Address,if control algorithm runs in another laptop.Press 'Local Host' button if the control algorithm runs on the same laptop.
- Select the Arena from the SAUVC,SAVe and ROBOSUB.  
- Then, run the control algorithm, set up a client connection to the simulator, and run its server**.
- The control algorithm must send to the simulator forces to be applied at each thruster, as float values. The number, position, and orientation of thrusters on the vehicle can easily be changed.
- The simulator provides simulated sensor values as feedback. A total of 8 floating point values are sent, with two digit decimal places, three digits before the decimal, and the sign. This is mentioned for easy decoding of the feedback. The eight values are: the orientation of the vehicle in the x, y, and z axes respectively, the acceleration of the vehicle in the x, y, and z axes respectively, the depth of the vehicle under water, and the forward velocity of the vehicle in its local frame.
- To use camera images as feedback, uncomment the like '#usePics' in ControlThrusters.cs. You must decode the image from the values of the edges.
- To move around the arena:
	- Use WASD and Arrow keys for moving around.Hold down Left or Right Shift for a faster movment.Hold Left or Right Ctrl for a slower movment.
	- 'Q' for moving/climbing up and 'E' for moving/climbing down.
	- Hold Space Bar and move mouse for rotation.
- To view the Log viewer/Console Logs-using the mouse click and drag to make a circular gesture.
- Threshold Max and Min can be changed from panels,which can be opened from the dropdown list-'Adjust Threshold'.

**In the ROS end:
- Install rosbridge-sudo apt-get install ros-kinetic-rosbridge-server.
- Run the command roslaunch rosbridge_server rosbridge_websocket.launch.
- Run rosun watchdog watchdog.
- Run simulator_receive_image simulator_receive_image.
  
