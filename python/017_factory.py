from enum import Enum
from math import *
class CoordinateSystem(Enum):
    CARTESIAN = 1
    POLAR = 2

class Point:
    def __init__(self, x=0, y=0):
        self.x = x 
        self.y = y 
    def __str__(self):
        return f'x:{self.x}, y:{self.y}'

    class PointFactory:        
        def new_cartesian_point(self, x, y):
            p = Point()
            p.x = x
            p.y = y
            return p
    
        def new_polar_point(self, rho, theta):
            return Point(rho*cos(theta), rho*sin(theta))
    factory = PointFactory()

p = Point(2,3)
p2 = Point.factory.new_polar_point(1,2)
print(p,p2)
        
