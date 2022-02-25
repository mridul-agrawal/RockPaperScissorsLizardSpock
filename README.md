# Rock, Paper, Scissors, Lizard, Spock
<br>
I have made an extended version of the classic Rock, Paper, Scissors game in Unity.
<br>
I have implemented a simple game loop. In Main Menu Screen the highscore is displayed along with a graphical representation of basic rules of the game. Once you press the play button, Round 1 starts where you have to press the correct input button (Hand Emojis at the bottom) to score. 
<br>
I have implemented a 2D Array (Effectiveness Matrix) which determines what object is effective on which object. This matrix is used to determine the outcome of a Round and accordingly either the player progresses to the next round or goes back to the homescreen. If it is a draw then the same round repeats again.
<br>
I have implemented a timer of 2 seconds for every round using Coroutines. I have used Singleton Design Pattern to implement SoundManager & UIManager. I have also implemented Observer Pattern in InputManager to notify upon any Input, Win, Lose & Draw Events.
