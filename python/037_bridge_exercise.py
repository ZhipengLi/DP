from abc import ABC
class Renderer(ABC):
    @property
    def what_to_render_as(self):
        return None
class RasterRenderer(Renderer):
    @property
    def what_to_render_as(self):
        return 'as pixels'
class VectorRenderer(Renderer):
    @property
    def what_to_render_as(self):
        return 'as lines'   
class Shape:
    def __init__(self, name, renderer):
        self.name = name
        self.renderer = renderer
class Square(Shape):
    def __init__(self, renderer):
        super().__init__('Square', renderer)
    def __str__(self):
        return f'Drawing {self.name} {self.renderer.what_to_render_as}'
class Triangle(Shape):
    def __init__(self, renderer):
        super().__init__('Triangle', renderer)
    def __str__(self):
        return f'Drawing {self.name} {self.renderer.what_to_render_as}'

print(str(Triangle(RasterRenderer())))
