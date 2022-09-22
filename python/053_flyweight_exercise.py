class Sentence:
    def __init__(self, plain_text):
        # todo
        #self.capitalize = [False * len(plain_text.split(' '))]
        class Capitalize:
            def __init__(self, cap = False):
                self.capitalize = cap
        self.words = plain_text.split(' ')
        self.capitalize = [Capitalize() for x in range(len(self.words))]
    def __getitem__(self, key):
        return self.capitalize[key]
    def __setitem__(self, key, val):
        self.capitalize[key] = val
    def __str__(self):
        ret = []
        for i in range(len(self.words)):
            ret.append(self.words[i].upper() if self.capitalize[i].capitalize else self.words[i])
        return ' '.join(ret)
if __name__=='__main__':
    sentence = Sentence('hello world')
    sentence[1].capitalize = True
    print(sentence)
