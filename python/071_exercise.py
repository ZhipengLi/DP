from enum import Enum, auto
class Token:
    class Type(Enum):
        INTEGER = auto() 
        PLUS = auto()
        MINUS = auto() 
        VAR = auto()
    def __init__(self, type, text):
        self.type = type 
        self.text = text 
    def __str__(self):
        return f'`{self.text}`'
def lex(input):
    result = []
    i = 0 
    while i < len(input):
        if input[i] == '+':
            result.append(Token(Token.Type.PLUS, '+'))
        elif input[i] == '-':
            result.append(Token(Token.Type.MINUS, '-'))
        elif input[i].isdigit():
            digits = [input[i]]
            for j in range(i+1, len(input)):
                if input[j].isdigit():
                    digits.append(input[j])
                    i +=1 
                else:
                    break
            result.append(Token(Token.Type.INTEGER, ''.join(digits)))
        else:
            var = [input[i]]
            for j in range(i+1, len(input)):
                if input[j].isalpha():
                    var.append(input[j])
                    i += 1 
                else:
                    break
            result.append(Token(Token.Type.VAR, ''.join(var)))
        i+=1 
    print('tokens:', [t.text for t in result])
    return result

def parse(tokens, variables):
    is_plus = True
    sum = 0
    i=0
    while i < len(tokens):
        token = tokens[i]
        if token.type == Token.Type.INTEGER: 
            if is_plus:
                sum += int(token.text)
            else:
                sum -= int(token.text)
        elif token.type == Token.Type.PLUS:
            is_plus = True
        elif token.type == Token.Type.MINUS:
            is_plus = False
        elif token.type == Token.Type.VAR:
            if not token.text in variables:
                return 0
            var_value = int(variables[token.text])
            if is_plus:
                sum += var_value
            else:
                sum -= var_value
        i +=1 
    return sum
    
class ExpressionProcessor:
    def __init__(self):
        #self.variables = {'xy':5}
        self.variables = {}

    def calculate(self, expression):
        tokens = lex(expression)
        return parse(tokens, self.variables)
    
if __name__=="__main__":
    processor = ExpressionProcessor()
    print(processor.calculate("1+2+xy"))
