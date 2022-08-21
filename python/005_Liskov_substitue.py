# Liskov Substitute principle:
# Anything that works with parent class, it should still work with child class
#
class Rectangle:
    def __init__(self, width, height):
        self._height = height
        self._width = width
    
    # Mark this function as property, so you call this function like an attribute
    #
    @property
    def area(self):
        return self._width *self._height
    def __str__(self):
        return f'Width: {self.width}, height: {self._height}'
    
    @property
    def width(self):
        return self._width
    
    # this function then can be used in '=' expression
    #
    @width.setter
    def width(self, value):
        self._width = value
    
    @property
    def height(self):
        return self._height
    
    @height.setter
    def height(self, value):
        self._height = value
    
def use_it(rc):
    w = rc.width
    rc.height = 10
    expected = int(w*10)
    print(f'Expected an area of {expected}, got {rc.area}')

# This class is considered as a break of Liskov principle, because the use_it scenario
#   that works with the parent class, no longer works with this child class
#
class Square(Rectangle):
    def __init__(self, size):
        Rectangle.__init__(self, size, size)
    
    # still a property, but we need to specify it is a property override for the parent class
    #
    @Rectangle.width.setter
    def width(self, value):
        self._width = self._height = value

    @Rectangle.height.setter
    def height(self, value):
        self._width = self._height = value;
rc = Rectangle(2,3)
use_it(rc)

sq = Square(5)
use_it(sq)
