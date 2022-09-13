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
        for num in self.nums:
            print(f'bool() value of {num}:', bool(num))
            if num < 0:
                print(f'abs() value of {num}:', abs(num))
                print(f'bin() value of {num}:', bin(num))

list = [1,2,3,0,-1]
checker = Checker(list)
checker.check()
