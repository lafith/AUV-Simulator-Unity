# AUV-Simulator

A simulator used to test control algorithms written for an Autonomous Underwater Vehicle

![demo2](./mischFiles/demo2.gif)

This simulator was developed as a part of my work in Team Tiburon, the Autonomous Underwater Vehicle team of NIT Rourkela. It is developed using Unity3D, written in C# and obstacles were modeled using Blender.

The simulator works by communicating with a control algorithm running on [ROS](https://www.ros.org/) through [ROSBridgeLib](https://github.com/MathiasCiarlo/ROSBridgeLib).

The simulator receives individual thruster speed values from the control algorithm, which is then converted to the forces to be applied. It send simulated sensor data to the control algorithm as feedback. Camera images are sent to the control algorithm every frame. To ensure optimal communication frequency, images are encoded to JPEG before sending.

![demo](./mischFiles/demo.gif)

## Arenas:
Currently there are three arenas:
- SAUVC arena based on the [SAUVC 2020](https://sauvc.org/rulebook/) rulebook.
- ROBOSUB arena based on [ROBOSUB 2019](https://arvp.org/wp-content/uploads/2019/05/2019-RoboSub-Mission-and-Scoring_v1.0.pdf).
- SLAM test arena to test [SLAM](https://en.wikipedia.org/wiki/Simultaneous_localization_and_mapping) algorithms.
## STEPS:

### To use:
- Start AUV Simulator.exe(in Windows) or AUV Simulator.x86_64(in Linux).
- Then, run the control algorithm, set up a client connection to the simulator, and run its server using [rosbridge](http://wiki.ros.org/rosbridge_suite) at the ROS end.
- The simulator provides simulated sensor values as feedback. A total of 8 floating point values are sent, with two digit decimal places, three digits before the decimal, and the sign. This is mentioned for easy decoding of the feedback. The eight values are: the orientation of the vehicle in the x, y, and z axes respectively, the acceleration of the vehicle in the x, y, and z axes respectively, the depth of the vehicle under water, and the forward velocity of the vehicle in its local frame.
- To move around the arena:
	- Use WASD and Arrow keys for moving around.Hold down Left or Right Shift for a faster movment.Hold Left or Right Ctrl for a slower movment.
	- 'Q' for moving/climbing up and 'E' for moving/climbing down.
	- Hold R key and move mouse for rotation.

### In the ROS end:
- Install rosbridge:
```console
foo@bar:~$ sudo apt-get install ros-<rosdistro>-rosbridge-server
```
- Run the command:
```console
roslaunch rosbridge_server rosbridge_websocket.launch
```  
