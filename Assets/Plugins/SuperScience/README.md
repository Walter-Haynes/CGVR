# Science!  Super Science!

At Unity Labs we perform a great deal of experiments.  These frequently produce 'gems' or small algorithms that are useful to the community by themselves.  This is a repository of those gems.

## Experimental Status
This repository is frequently evolving and expanding.  It is tested against the latest stable version of unity.  However, it is presented on an experimental basis - there is no formal support.

## How to Use ##
Each gem is located in a separate folder in this repository.  They are presented with an example scene (if appropriate) and well-commented code to educate the reader.  

This repository can be put in an empty Unity project or cloned into a child folder of an existing project.  

Use the example scripts directly, or tweak and alter the algorithms inside to fit your needs.  A list of included gems follows:

## Stabilizr : Object Stabilization for XR
"The Fishing Rod Problem" - Virtual objects or rays locked to a controller can shake in an unnatural way.  In the real world, long objects have weight, which gives them stabilization through inertia.  In XR, this lack of stabilization makes objects feel fake and precise selection difficult.

Stabilzr is a solution to this problem.  It smoothes the rotation of virtual objects in three scenarios
- Steady Motion: Holding an object at a precise angle while moving the controller
- Orbiting (endpoint) Motion: Holding the end of an object or ray at a particular spot while moving the controller
- Still Motion: Holding an object at a precise angle while clicking a button on the controller

Stabilzr works without adding lag to large sweeping motions - precise control is enabled while **in the worst case** only diverging from ground truth by 2 degrees for a single frame.

For an example of Stabilzr in action, check out the included 'TestScene'.  A 6 foot broom and 12 foot pointing stick are attached to the right and left XR controllers.  To compare before/after, two additional gameobjects (labeled Non-Stabilized Overlay) can be enabled.  These are non-stabilized copies of the broom and pointer that render of top of the originals.

## GizmoModule: Gizmos for EditorXR/Runtime
The normal Gizmos.Draw[Primitive] and Debug.DrawLine APIs don't work in EditorXR, and don't work in Runtime. The GizmoModule can be loaded alongside EditorXR, or included in a player build to provide similar functionality through the Graphics.DrawMesh API.

The module can be accessed staticly using a singleton reference, or referenced like a normal MonoBehaviour to draw rays/lines, spheres, and cubes.  Like the normal Gizmos/Debug API, you must call Draw[Primitive] every frame that you want to see it.

Check out the example scene, which draws spheres on tracked controllers, and a line between them. If you don't have an HMD, don't worry. Just run the scene to see it work.

Here are some advanced examples from EditorXR:

![The blue rays are used to detect objects for snapping](https://github.com/Unity-Technologies/SuperScience/raw/docs-assets/GizmoModule/example-1.png)
![The red ray is showing how close we are to breaking the snapping distance](https://github.com/Unity-Technologies/SuperScience/raw/docs-assets/GizmoModule/example-2.png)
![This example shows that the third ray has encountered an object and shows where the non-snapped object would be](https://github.com/Unity-Technologies/SuperScience/raw/docs-assets/GizmoModule/example-3.png)

## PhysicsTracker: Bridging the gap between game code and physics simulation
One of the more difficult problems in games is translating motion from non-physics sources, like animations, custom scripts, and XR input, into the physics simulation.  An additional concern on the input side is that while some XR Input devices provide physics data, others do not.  Tracked objects in AR usually do not have velocity or other physics data associated with them.

The convential approach to this problem is to integrate velocity by looking at how an object has moved frame to frame.  This data can vary pretty wildly (especially at low speeds) and 'feel' incorrect for things like rotational motion, changing directions, shaking, and so on.  Presenting something that looks and feels like motion the player intended usually requires a lot of trial and error, tweaks, and hacks.

The PhysicsTracker provides a solution.  It works by seperating the problem of tracking velocity into tracking speed and direction separetely.  It smooths and predicts these values and how they change, and then recombines them into a final velocity, acceleration, and angular velocity.

The PhysicsTracker is versatile;  it is a class that can be used inside and outside monobehaviours.  They can follow any kind of changing set of positions and rotations and output appropriate physics data.  Do you need to get the velocity at the end of a bat?  Stick a PhysicsTracker there.  Rotation of an XR Input device?  Stick a PhysicsTracker there.  Want to get some physics values for that 'monster attack' animation in your game?  Stick a PhysicsTracker there.

The included 'TestScene' for PhysicsTracker shows the smooth physics data being generated for the left and right hands.  An attached component 'DrawPhysicsData' on each hand does the tracking and drawing of data.  Various pieces of data can be visualized - velocity, acceleration, angular velocity, and direct integrated velocity (for reference).  I recommend only having one or two options on at a time - the data can get too busy with them all active at once.  Velocity is drawn in blue, Acceleration in green, Angular Velocity in white, and Direct Integration in Red.

To use the PhysicsTracker in your own scripts, just create a new 'PhysicsTracker' in your script, and call the 'Update' method with the most recent position and rotation values of the object you wish to track.  The physics tracker then will calculate up to date values for Speed, Velocity, Acceleration, Direction, Angular Speed, and Angular Velocity.

For smoothest visual results, use a fixed or smooth delta time with the PhysicsTracker update functions.  For single-frame use (gameplay-to-physics events), delta time is fine.