import msvcrt
from time import perf_counter, sleep
from os import system
from random import randint
from threading import Thread

HIGHSCORE = None

class ReactionGame(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.timer = None
        self.readyForReaction = False

    def run(self):
        while True:
            delay = randint(1, 8)
            sleep(delay)
            print('O')
            self.readyForReaction = True
            self.timer = perf_counter()

if __name__ == '__main__':
    print("WELCOME TO THE REACTION GAME!")
    print("When you start the game and the 'O' appears")
    print("press enter as quickly as possible to calculate your reaction time")
    print(" $ PRESS ANY KEY TO START THE GAME")
    # Wait for player to press any key to start the game
    msvcrt.getch()
    # clear the console so the player is focused
    while True:
        system('cls')
        game = ReactionGame()
        while True:
            print("press \'q\' to quit")
            if HIGHSCORE:
                print(f'HIGHSCORE: {str(round(HIGHSCORE, 2))}s')
            if not game.is_alive():
                game.start()
            # Wait for the user to start the game
            pressedKey = msvcrt.getch()
            # if player presses q quit the program
            if pressedKey == b'q':
                quit()

            elif pressedKey == b'\r':
                if game.readyForReaction:
                    now_timer   = perf_counter()
                    score       = now_timer - game.timer
                    if not HIGHSCORE:
                        HIGHSCORE = score
                    if score < HIGHSCORE:
                        HIGHSCORE = score
                        print('New highscore!')
                        sleep(1)

                    print(f'Your reaction time is {str(round(score, 2))}s')
                    # Reset game
                    game.readyForReaction = False
                    game.timer = None
                    sleep(1)
                    system('cls')

                else:
                    print('You are too quick\nTry again.')
                    sleep(1)
                    system('cls')


