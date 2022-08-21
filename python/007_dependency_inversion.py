# Dependency inversion has NOTHING to do with dependency injection
# Dependency inversion means higher level module should never rely on lower level module
# low level module can be storage module

from enum import Enum
from abc import abstractmethod

class Relationship(Enum):
    PARENT = 0
    CHILD = 1
    SIBLING = 2
    
class Person:
    def __init__(self, name):
        self.name = name

class RelationshipBrowser:
    @abstractmethod
    def find_all_children_of(self, name):pass

# low level module because it deals with storage
#
class Relationships(RelationshipBrowser):
    def __init__(self):
        self.relations = []
        
    def add_parent_and_child(self, parent, child):
        self.relations.append(
            (parent, Relationship.PARENT, child)
        )
        self.relations.append(
            (child, Relationship.CHILD, parent)
        )
    def find_all_children_of(self, name):
        for r in self.relations:
            if r[0].name == name and r[1]== Relationship.PARENT:
                yield r[2].name

# high level module
#
class Research:
    # this is considered a break of the DIP principle, because it relies on some internal
    # implemention of how relations are stored
    #
    def __init1__(self, relationships):
        relations = relationships.relations
        for r in relations:
            if r[0].name == 'John' and r[1] == Relationship.PARENT:
                print(f'John has a child called {r[2].name}.')
    
    def __init__(self, browser):
        for p in browser.find_all_children_of('John'):
            print(f'John has a child called {p}')

parent = Person('John')
child1 = Person('Chris')
child2 = Person('Matt')

relationships = Relationships()
relationships.add_parent_and_child(parent, child1)
relationships.add_parent_and_child(parent, child2)

Research(relationships)
