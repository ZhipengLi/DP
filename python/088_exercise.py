class Event(list):
    def __call__(self, *args, **kwargs):
        for item in self:
            item(*args, **kwargs)
class Game:
    def __init__(self):
        self.events = Event()
    def update_attack(self):
        self.events(len(self.events))

class Rat:
    def __init__(self, game):
        self.game = game
        self.attack = 1
        self.game.events.append(self.set_attack)
        self.game.update_attack()
    def set_attack(self, value):
        self.attack = value
    def __enter__(self):
        return self
    def __exit__(self, exc_type, exc_value, exc_traceback):
        self.game.events.remove(self.set_attack)
        self.attack = 1
        self.game.update_attack()
if __name__=='__main__':
    game = Game()
 
    rat = Rat(game)
    print(1, rat.attack)
 
    rat2 = Rat(game)
    print(2, rat.attack)
    print(2, rat2.attack)
 
    with Rat(game) as rat3:
        print(3, rat.attack)
        print(3, rat2.attack)
        print(3, rat3.attack)
 
    print(2, rat.attack)
    print(2, rat2.attack)
        
        
