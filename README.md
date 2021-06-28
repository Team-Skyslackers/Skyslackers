# Skyslackers
Team: the Skyslackers

Level of Achievement: Apollo

## Motivation:
We are fans of Star Wars and we love lightsabers because they are cool. We are also music lovers. So we want to make a super cool game that combines lightsabers and music. Moreover, instead of using a keyboard or mouse, we want to control our lightsaber by holding it physically, which feels more real.

## Project Scope:
One sentence: 
We want to make a motion sensor game in which we will combine lightsaber fighting and music together to give players a totally new Jedi experience.

Detailed: 
We want to develop a motion-sensing music video game. In the game, players can use their “lightsaber” (their smartphones) to deflect blaster bolts from the Dark Side. To make our game more exciting and interesting, we integrate rhythm-matching game features into our game: in order to deflect the blaster bolts successfully, players need to match the rhythm of the background music. After every successful deflection, scores will be awarded. By matching the rhythm perfectly, players get “Combos” and extra scores. Scores and leaderboards will be displayed after every round to motivate players.
	
## Proposed features:
1. The position and the movement of the virtual lightsaber is synchronized with the user’s phone (both IOS and Android supported).
2. When a new round starts, there will be blaster bolts coming towards the player, and the player can wave his/her phone to control the virtual lightsaber blade to           deflect. If successfully deflected, scores will be awarded. 
3. The incoming blaster bolts will change colour when they come near to the player, green - god - green - red. When the player deflects the bolt when it is green, he/she only gets “good” deflection. When he/she deflects the bolt when it is gold, he/she gets “perfect”deflection and higher scores will be awarded.
4. When the player achieves “perfect” deflections consecutively, he/she gets “Combos” and additional scores are awarded.
5·. There are real-time updates of scores and “Combos” during the game.
6. After each round of the game, the total score will be displayed and there is a leaderboard showing the global rank of the player.
7. The background music and the pattern of incoming blaster bolts is customisable. Players can choose to play their favourite songs and design their own blaster bolt patterns for greater challenge and fun.
8. Two-player mode for more interesting gaming experience.
9. An online database that stores all the maps available (a map includes the song itself and specifically designed blaster bolt pattern) with different levels of difficulty. Players can choose maps they like to play with.
10. A complementary program that helps players to make their own maps.. Players can either manually design the blaster bolt pattern or let the program randomly generate patterns. 
11. Easy set-up.


## Features achieved (for milestone 1):
1. Secure and real-time data transfer of iphone motion data to macbook
2. Use phone to control the lightsaber on laptop screen

## Technology used (for milestone1):
1. web server (using “express” JavaScript package)
2. Web API: DeviceMotionEvent in html file running on the phone: obtains real-time device motion and orientation 
3. Websocket using self-created local ssl certificate to achieve secure communication between the html file running on the phone and the server on the laptop (using “ws” JavaScript package)
4. Non-secure websocket that transfer data from server to Unity3D game (both the server and game project are on laptop)


## Core features achieved (for milestone 2):
1. Scenes of the game are created. Game objects like the lightsaber and blaster bolts are well-modelled. 
2. Achieved collision detection between the lightsaber and bolts. When the lightsaber hits the bolts in green or gold, “good” or “perfect” will be shown. If the lightsaber fails to hit the bolts, “miss” will be shown. Corresponding scores will be added to the total score.
3. Added speed detection to the lightsaber: only if the lightsaber hits bolts in a relatively high speed will the bolts be deflected. 
4. Added bolts spawning script. Incoming blaster bolts can therefore appear and move to the correct place at correct timing as designed. 
5. Added Start Menu. Three options are available: “Play”, “Settings” and “Exit”.
6. Added Setting Page. Players can adjust volume, bolt speed and time delay.
7. Added three maps(songs) in increasing levels of difficulty. Players can choose the song after they click “Play” in the Start Menu. (All songs used are Creative Common licensed resources which give us the right to share, use and build upon the work created by the authors.)
8. Added total score and Combo display
9. Added summary page displaying total scores after the game
10. Easy set-up: bundled all javascript files and packages into executables for both Mac OS and Windows OS. Added QR code generating function for easy connection from the phone to the computer. Automatically open the game after connecting the phone to the computer.
11. Wider compatibility: Phone: fully compatible with IOS and Android; Computer: compatible with Mac (only x86 architecture) and Windows (only x86 architecture). M1 Mac or ARM-based Windows PC are sadly not supported.

## Technology used (for milestone 2):
1. Unity: set up game scenes, integration of game objects, scripts and sound effects, graphics design
2. C# scripts in Unity: design the logic of the game
3. Blender: modelling game objects
4. JavasScript Packages: Pkg - A JavaScript package that bundles all JavaScript files and packages into an executable for Mac/Windows; qrcode - generate the QR code linking to a given url; applescript - used to automatically open the game on Mac; child_process - used to automatically open the game on Windows

## Features to be achieved in Milestone 3:
1. Online database that supports players’ upload or download of maps(songs).
2. Two-player mode: can connect two phones (Player 1 and Player 2)
3. Add in more game objects (lightsaber from the enemy, different kinds of blaster bolts) to make the game more fun
4. Design a program that helps players to customise their own maps/songs
5. Improve on graphics and sound effects

## Problems encountered:
1. Unable to fetch device’s sensor data without secure communication: on IOS devices, DeviceMotionEvent API does not return any data. It turned out that IOS only grants permission to access sensor data under secure communication. Hence, we used ssl certificates to establish secure communication between the phone and the computer.
2. Mismatch between background music and blaster bolts: When testing our maps (songs), we found that there is always delay between background music and blaster bolts. Blaster bolts in our game are like music scores and they should match with the song. Such mismatch greatly undermined our gaming experience. We guess that it was our method of spawning the blaster bolts one by one that caused this problem. We changed to a new method of generating all blaster bolts at once to reduce the delay. We also added adjustable delay compensation in our setting to solve this problem.
3. Low FPS(frame per second): due to large-size models being used, our game had very low FPS of around 20. The lightsaber was not responsive and lagging was observed. The overall gaming experience was not smooth. To increase the FPS, we simplified some of our models. Now the FPS is around 60.

