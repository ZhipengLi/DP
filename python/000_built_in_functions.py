class Checker:
    def __init__(self, nums):
        self.nums = nums
    def check(self):
        def iterate_num(nums):
            for num in nums:
                yield num>=0
        print('all() result:', all(iterate_num(self.nums)))
        print('any() result:', any(iterate_num(self.nums)))
        print('ascii(self) result:', ascii(self))
        print('ascii(测试) result:', ascii('测试'))
        print('bin(123) result:', bin(123))
        print('bool(-1) result:', bool(-1))
        print('bool(0) result:', bool(0))
        #breakpoint()
        print('bytearray(测试) result:', bytearray('测试', 'utf8'))
        print('bytes(测试) result:', bytes('测试', 'utf8'))
        print('callable(super) result:', callable(super))
        print('chr(97) result:', chr(97))
        print('complex 1+2j:', complex('1+2j'))
        print('dict():', dict([('sape', 4139), ('guido', 4127), ('jack', 4098)]))
        print('dir():', dir())
        print('divmod(5,2):', divmod(5,2))
        #seasons = ['spring', 'summer', 'autumn', 'winter']
        #list_enumerate = dict(enumerate(seasons, start=1))
        print('enumerate:', dict(enumerate(['spring', 'summer', 'autumn', 'winter'], start=1)))
        x=1
        print('eval(x+1):',eval('x+1'))
        print('filter:', tuple(filter(lambda x:x%2==0, [1,2,3,4,5,6,7])))
        print('float(-3):', float('-3'))
        print('format(5) in binary:', format(5,'b'))
        print('frozenset:', frozenset([1,2,3,4,5,6,7]))
        self.name='test name'
        print('getattr:',getattr(self, 'name'))
        print('globals():', globals())
        print('hasattr:', hasattr(self, 'name'))
        print('hash(self):', hash(self))
        #print('help:', help())
        print('hex(255):', hex(255))
        print('id(self):', id(self))
        #input
        print('int(\'020\', 16):', int('020', 16))
        print('isinstance:', isinstance(self, Checker))
        print('issubclass', issubclass(Checker, Checker))
        iters = iter(self.nums)
        print('iter\'s next():', next(iters))
        print('len(self.nums)', len(self.nums))
        print('list():', list(('apple', 'pear', 'orange')))
        print('locals():', locals())
        print('map()', list(map(lambda x:x*2, self.nums)))
        print('max()', max(self.nums))
        print('min()', min(self.nums))
        print('object()', dir(object()))
        
        for num in self.nums:
            print(f'bool() value of {num}:', bool(num))
            if num < 0:
                print(f'abs() value of {num}:', abs(num))
                print(f'bin() value of {num}:', bin(num))
    def test_method(self):
        print('this is the object method')
num_list = [1,2,3,0,-1]
checker = Checker(num_list)
checker.check()
Checker.class_method = classmethod(Checker.test_method)
Checker.class_method()
delattr(Checker, 'class_method')
checker.name='test name'
