class CombinationLock:
    def __init__(self, combination):
        #self._status = 'LOCKED'
        self.combination = ''
        for c in combination:
            self.combination += str(c)
        self.entry = ''
    def reset(self):
        # todo - reset lock to LOCKED state
        self.entry = ''
    def enter_digit(self, digit):
        # todo
        self.entry += str(digit)
    @property 
    def status(self):
        if self.entry=='':
            return 'LOCKED'
        if self.combination == self.entry:
            self.entry = ''
            return 'OPEN'
        if self.combination.startswith(self.entry):
            return self.entry
        else:
            self.entry = ''
            return 'ERROR'
if __name__=='__main__':
    cl = CombinationLock([1, 2, 3, 4, 5])
    print('LOCKED', cl.status)
    cl.enter_digit(1)
    print('1', cl.status)
    cl.enter_digit(2)
    print('12', cl.status)
