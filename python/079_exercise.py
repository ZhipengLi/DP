class Event(list):
    def __call__(self, *args, **kwargs):
        for item in self:
            item(*args, **kwargs)
class Mediator:
    def __init__(self):
        self.events = Event()
    def fire(self, participant, value):
        self.events(participant, value)
class Participant:
    def __init__(self, mediator):
        self.value = 0
        self.mediator = mediator
        self.mediator.events.append(self.notify)
    def say(self, value):
        # todo
        self.mediator.fire(self, value)
    def notify(self, participant, value):
        if participant != self:
            self.value = value
    def __str__(self):
        return f'value: {self.value}'
if __name__ == '__main__':
    mediator = Mediator()
    p1 = Participant(mediator)
    p2 = Participant(mediator)
    p1.say(2)
    p2.say(1)
    print('p1:', p1)
    print('p2:', p2)

        
        
