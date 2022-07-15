from ast import And
from enum import Enum


class Color(Enum):
    Red = 0,
    Green = 1,
    Blue = 2


class Size(Enum):
    Small = 0,
    Mediuam = 1,
    Large = 2


class Product:
    def __init__(self, color, size):
        self.color = color
        self.size = size

    def __str__(self):
        return f'{self.color} {self.size}'


class Specification:
    def is_satisfied_with(self, product):
        pass


class ColorSpec(Specification):
    def __init__(self, color):
        self.color = color

    def is_satisfied_with(self, product):
        return product.color == self.color


class SizeSpec(Specification):
    def __init__(self, size):
        self.size = size

    def is_satisfied_with(self, product):
        return product.size == self.size


class Filter:
    def filter_by(self, products, spec):
        for p in products:
            if spec.is_satisfied_with(p):
                yield p


p1 = Product(Color.Red, Size.Large)
p2 = Product(Color.Blue, Size.Large)
p3 = Product(Color.Green, Size.Mediuam)

products = [p1, p2, p3]

filter = Filter()
blue_spec = ColorSpec(Color.Blue)

for p in filter.filter_by(products, blue_spec):
    print(str(p))


class AndSpec(Specification):
    def __init__(self, *specs):
        self.specs = specs

    def is_satisfied_with(self, product):
        return all(map(lambda spec: spec.is_satisfied_with(product), self.specs))


large_red_spec = AndSpec(SizeSpec(Size.Large), ColorSpec(Color.Red))
for p in filter.filter_by(products, large_red_spec):
    print(str(p))

# class ColorFilter(Filter):
#     def __init__(self, color):
#         self.color = color
#     def filter_by(products, spec):
#         for p in products:
#             if spec
