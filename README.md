# I3EAssignment1
I3E Assignment 1
A 3D virtual environment using C# in unity that contains collectibles that users can
collect, and hazards and obstacles that damage and obstruct the user. The user
controls an avatar through a first-person camera view.
Design Process
To create the project, I first planned out what features and gameObjects i wanted to
create since i needed to create some gameobjects that are
necessary for requirements in the assignment. Then i created a level that I would be
able to use all of the features that i have already created
in a way that is able to show off the features well. Then added the more detail oriented
features like BGM and SFX.
Features
1. Locked doors: Doors that require a specific key to open. Doors close automatically
after 2 seconds upon opening. Different SFXs are played when trying to open the door
without the key, opening the door and closing the door
2. Keys: collecting the key will allow players to open specfic doors. Collecting the key
also shows the corresponding key UI icon. Collecting the key plays a key collecting SFX
3. Chest: Requires specific key to unlock. Upon unlock, will spawn three coins for player
to collect. Plays SFX upon opening chest
4. Health system: Healthbar shows how much health the player has through both UI text
and also a UI healthbar gameobject that shows the player's current health as compared
to the player's max health. Player's play a SFX when taking damage. Player's will die
when health hit's 0 or less.
5. Spikes (non-animated): spikes give players a certain amount of damage upon
contact.
6. Spikes (animated): Functions the same as non-animated spikes. Animated in Maya
before importing over to unity. SFX for spikes are not synced to the animation.
7. Water Traps: Kill's player's upon contact
8. Checkpoints: Player's current checkpoint are immediately updated upon contact
with checkpoint zones. Players are respawned at current checkpoint upon death.
9. Coins: player body must collide with coin to collect. Upon collecting coin, coin
collecting SFX plays. certain number of coins are required to be collected to gain the
achievement.
10. End Screen: upon completing the level by escaping the area, An end screen will
appear to show player's stats like number of coins collected and number of deaths.
Players will also see an assigned rank for their performance in the level ranging from S to
F.
11. BGM: Ambient BGM is implemented. BGM will stop upon completing the game.
Credits
Content
Videos I've watched while working on this project:
How to save and load player position 2D/3D - Unity Tutorial:
https://youtu.be/xnbvK4iRCfI?si=_DkFVmxIGiaHLpqp
Spawn Objects at Random Positions in Unity 2023 (Updated):
https://youtu.be/VeBMinfCFEI?si=I2YAYYYotM_FRvGS
Spawn Objects at Random Position in Unity:
https://youtu.be/IbiwNnOv5So?si=iybXbovlRYrYQIht
How to DELAY A FUNCTION in Unity (INVOKE method):
https://youtu.be/9NvMELlMTQI?si=wqRQ8KjfCabLBsdr
Simple Health Bar Unity Tutorial: https://youtu.be/lYZayXViTN8?si=L5BiKXL13XcXkJaW
How to hide and show an object in unity:
https://youtu.be/cRFjuRvDWXU?si=ywpAp0kmDXz6qWTI
Create Sprite from Image In Unity:
https://youtu.be/O2P3WRdtUuQ?si=yYynTuvBBkPxCN1U
Media
The SFX and BGM used in the project are obtained from
[pixabay.com](https://pixabay.com)
Acknowledgements
I received inspiration for this project from similar roblox obby type games