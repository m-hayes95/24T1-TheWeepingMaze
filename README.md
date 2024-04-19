# 24T1-TheWeepingMaze
Project for 24T1 trimester

How to play:
Escape the maze by reaching the goal before your torch battery runs out.
Controls: 
Space = Toggle torch On / Off
WASD = Move Up / Left / Down / Right
Mouse = Torch movement

Known bugs / outstanding works:
- Enemies spawn on top of each other and move together
- Red colour when hit does not reset
- Torch light goes through walls
- Setting menu not hooked up

Folder Layout:
For this project, I tried moving each mechanic and its asscoiated files into its own folder oppposed to having seperate files for scripts and prefabs, etc.

Scripts within in the Maze folder we created using the Maze2 maze generator tutorial on catlike coding: https://catlikecoding.com/unity/tutorials/prototypes/maze-2 and the maze manager script was adapted for use of the game

The player input script was made with help from Code Monkey https://youtu.be/Yjee_e4fICc?si=ynZjAi5Q3pcLWq-y

The animator manager was made with help from Lost Relic Games
https://youtu.be/nBkiSJ5z-hE?si=VthhGR1bRSVU37zp  

The main mechanic is the torch, as it is the lose condition and has the most interaction with other components.

The player can use the torch to freeze enemies and see more clearer in the dark.

The light intensity from the torch is directly influenced by the battery's remaining health. 
