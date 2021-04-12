import os
import json
from msvcrt import getch
from random import randint
from threading import Thread
from datetime import datetime
from time import perf_counter, sleep

HIGHSCORE_FILE = "reaction_game_highscores.json"

# Reaction Game class
# This class handles the reaction 
class ReactionGame(Thread):
    def __init__(self):
        Thread.__init__(self)
        # Thread attributes
        self.name = "Game Thread"
        self.daemon = True
        # Game object attributes
        self.timer = None
        self.readyForReaction = False
        self.running = True

    def check_reaction(self, player):
        # Check if the timer has started first
        if game.timer:
            now_timer   = perf_counter()
            # get the score
            score       = now_timer - game.timer

            previous_highscore = get_highscore(player)

            # If there is a previous highscore and the new score beats it, save it!
            if previous_highscore:
                if score < previous_highscore:
                    print('New Highscore!')
                    save_highscore(player, score)
            else:
                # If there is no highscore save the score as the new highscore since we cant compare scores to highscores that doesnt exist
                save_highscore(player, score)

            # Depending whether if the game is singleplayer or multiple write diffferent messages
            if not player == "Singleplayer":
                print(f'{player} reaction time is {str(round(score, 2))}s')
            else:
                print(f'Your reaction time is {str(round(score, 2))}s')
            
            # Reset game
            game.readyForReaction = False
            game.timer = None
            sleep(1)
            os.system('cls')
        
        else:
            # Depending whether if the game is singleplayer or multiple write diffferent messages
            if not player == "Singleplayer":
                print(f'{player} are too quick - Try again.')
            else:
                print('You are too quick - Try again.')
            sleep(1)


    def run(self):
        # Run the game
        # First get the random delay and wait the x seconds to tell the players that the game is ready to be pressed
        # and sets the timer
        while self.running:
            delay = randint(1, 8)
            sleep(delay)
            print('O')
            self.readyForReaction = True
            self.timer = perf_counter()

    def stop(self):
        # Stop the game by setting running to false
        # We have the stopping game message, that sometimes can be quite slow
        # It is because the thread waits for the loop to finish, and the delay is from where the game sleeps between the delays
        self.running = False
        print("Stopping Game...")
        self.join()

def save_highscore(player, score):
    # Highscore file data can be None since we are going to check if it exists in the first place
    HIGHSCORE_FILE_DATA = None
    # Store the data in a reasonable format
    data = {
        'player': player,
        'score': score,
        'date': datetime.now().strftime('%d/%m/%Y %H:%M:%S')
    }
    # If the path exists store the existing data in the highscore file data variable
    if os.path.exists(HIGHSCORE_FILE):
        with open(HIGHSCORE_FILE, 'r') as f:
            try:
                HIGHSCORE_FILE_DATA = json.load(f)
            except:
                print('Invalid Json Format')
    
    # Overwrite the highscore file with the newly appended data
    with open(HIGHSCORE_FILE, 'w') as f:
        if HIGHSCORE_FILE_DATA:
            HIGHSCORE_FILE_DATA.append(data)
            f.write(json.dumps(HIGHSCORE_FILE_DATA, indent=4))
        else:
            f.write(json.dumps([data], indent=4))

# Get the highscore by a player parameter
def get_highscore(player):
        data = get_highscores()
        if not data:
            return None

        for i in data[::-1]:
            if i['player'] == player:
                return i['score']
        return None
# read and get all highscores in the highscore file
def get_highscores():
    if os.path.exists(HIGHSCORE_FILE):
        with open(HIGHSCORE_FILE, 'r') as f:
            try:
                return json.load(f)
            except:
                print('Invalid Json Format')
    return None

# Main Program #
if __name__ == '__main__':
    while True:
        os.system('cls')
        # Welcome message and menu
        print("WELCOME TO THE REACTION GAME!")
        print("When you start the game and the 'O' appears")
        print("press enter as quickly as possible to calculate your reaction time")
        print(" $ PRESS 1 for singleplayer")
        print(" $ PRESS 2 for multiplayer")
        print(" $ PRESS 3 to print last 10 highscores")
        print(" $ PRESS q to quit")
        # Wait for player to press any key to start the game
        menu_selection = getch()

        # Single player selection
        if menu_selection == b'1':
            os.system('cls')
            # Create a game object
            game = ReactionGame()
            # The highscore of the player playing in this instance
            HIGHSCORE = None
            while True:
                # info messages
                print("PRESS q to return to menu")
                print("PRESS ENTER when the O appears")
                # Set the player so we can compare between highscore results
                player = "Singleplayer"
                # Print the singleplayer highscore
                if get_highscore(player):
                    print(f'HIGHSCORE: {str(round(get_highscore(player), 2))}s')
                # Start the game if it isn't already started
                if not game.is_alive():
                    game.start()
                # Get keypress and compare with keypress inputs
                keypress = getch()

                # Stop game
                if keypress == b'q':
                    game.stop()
                    break
                # Check for reaction
                elif keypress == b'\r':
                    game.check_reaction(player)
                            
        # Multiplayer selection
        elif menu_selection == b'2':
            os.system('cls')
            # Instance the game object
            game = ReactionGame()
            # Set the player highscores
            PLAYER_ONE_HIGHSCORE = None
            PLAYER_TWO_HIGHSCORE = None
            while True:
                # info messages
                print("PRESS q to return to menu")
                print("PLAYER 1: PRESS ENTER when the O appears")
                print("PLAYER 2: PRESS SPACE when the O appears")
                # Set player names
                player_one = "Player 1"
                player_two = "Player 2"
                # print player highscores if they exist
                if get_highscore(player_one):
                    print(f'{player_one} HIGHSCORE: {str(round(get_highscore(player_one), 2))}s')

                if get_highscore(player_two):
                    print(f'{player_two} HIGHSCORE: {str(round(get_highscore(player_two), 2))}s')
                # start the game if it isn't already started
                if not game.is_alive():
                    game.start()
                # get keypress
                keypress = getch()

                # Return to menu
                if keypress == b'q':
                    game.stop()
                    break
                # Player one reaction check
                elif keypress == b'\r':
                    game.check_reaction(player_one)
                # player two reaction check
                elif keypress == b' ':
                    game.check_reaction(player_two)


        elif menu_selection == b'3':
            print()
            # Get all highscores
            data = get_highscores()
            if data:
                print("Player Highscores")
                # Store the player highscores in a new var containing:
                # {
                #   "player", player_name,
                #    "highscores": []
                # }
                players = []
                # Add players from highscore file
                for i in get_highscores():
                    player = i['player']
                    # if the player has already been added continue
                    if player in [i['player'] for i in players]:
                        continue
                    # create the sublist containing the highscores
                    player_highscore_list = []
                    # Go through all the highscores again and only add the ones related to the instance of i['player']
                    for j in get_highscores():
                        # Check if the player matched the one in the parent loop
                        if j['player'] == player:
                            # the data in a reasonable format
                            player_highscore_list.append({
                                'score': j['score'],
                                'date': j['date']
                            })
                    # Add the player data to the players list
                    players.append({'player': player, 'highscores': player_highscore_list})
                    
                # print each highscore from every player
                for i in players:
                    print(i['player'].upper())
                    for j in i['highscores'][::-1]:
                        print(f'    {str(round(j["score"], 2))}s, {j["date"]}')
            else:
                print("No highscore data")
            # Wait for any input to return to menu
            getch()

        # stop the program
        elif menu_selection == b'q':
            break
