class Token:
    def __init__(self, value=0):
        self.value = value
class Memento(list):
    pass
class TokenMachine:
    def __init__(self):
        self.tokens = []

    def add_token_value(self, value):
        return self.add_token(Token(value))

    def add_token(self, token):
        # todo
        self.tokens.append(token)
        m = Memento()
        for t in self.tokens:
            m.append(t.value)
        return m

    def revert(self, memento):
        # todo
        self.tokens = []
        for t in memento:
            self.tokens.append(Token(t))
            
if __name__ == '__main__':
    machine = TokenMachine()
    machine.add_token_value(5)
    m = machine.add_token_value(2)
    print(m)

        
        
