**Personal Project / Summer 2020 - Ongoing**

2D Plaformer created on Unity using the Sunnyland asset pack. Development is still in progress.

Main tutorial series used:

https://www.youtube.com/playlist?list=PLpj8TZGNIBNy51EtRuyix-NYGmcfkNAuH

Game playable on itch.io:

https://nekopudding.itch.io/sunnyland

## Notes for Development
Fix Player from Sliding Down Ramps - can try changing rigidbody from dynamic to kinematic while touching ground

Finish Implementing Frog - add attack animations, player health & interaction scripts, enemy movement AI

Add Title Screen - create a separate scene


### General Procedure for Setting up Game in Unity
1. download required assets from asset store
2. tilesets - reset pixels by unit to 16px, change sprite rendering to multiple and manually slice in sprite editor, create folder for tile palette and create tile palette and drag in the sprites you made, now create the scene
3. add sorting layers for each set to order rendering
4. add player as empty game object, drag a sprite onto it
5. add rigidbody2d and box collider onto player, tilemap collider and rigidbody 2d onto tilemap, composite collider and used by composite
6. attach player controller script to control movement and graphics, add camera controller to follow player
7. create animation in selected folder, drag onto player (this will create animator, now selecting the player, drag the sprites onto the animation window, configure the animation speed and set loop time to on (by clicking in the animation folder)
8. add animator transitions, add parameters and set conditions for transitions, then manipulate parameters within the playercontroller script using Animator
    1. set up enum State {}  to control animation/movement based on current state in playercontroller
    2. change animator parameters to one called state, which you associate to State based on index number
    3. remove exit time and transition time for each transition
9. setup prefab folder to store player data, tilemap, and sunnyland.prefab
10. create collectibles - add sprite renderer to empty game object and drag sprite onto it, then create animation as before. Add tag and boxcollider2D with isTrigger on, now create OnTriggerEnter2D function in playerController to collect them.


