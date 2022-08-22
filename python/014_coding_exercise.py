class CodeBuilder:
    def __init__(self, root_name):
        # todo
        self.root_name = root_name
        self.pairs = []
    def add_field(self, type, name):
        # todo
        self.pairs.append((type, name))
        return self
    def __str__(self):
        # todo
        code = []
        code.append(f'class {self.root_name}')
        code.append(f'  def __init__(self):')
        for pair in self.pairs:
            code.append(f'    self.{pair[0]} = {pair[1]}')
        return '\n'.join(code)
            
cb = CodeBuilder('Person').add_field('name', '""') \
                          .add_field('age', '0')
print(cb)              
            
