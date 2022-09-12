class Checker:
    def __init__(self, nums):
        self.nums = nums
    def check(self):
        def iterate_num(nums):
            for num in nums:
                yield num>=0
        print('all() result:', all(iterate_num(self.nums)))
        print('any() result:', any(iterate_num(self.nums)))
        for num in self.nums:
            print(f'bool() value of {num}:', bool(num))
            if num < 0:
                print(f'abs() value of {num}:', abs(num))
                print(f'bin() value of {num}:', bin(num))

list = [1,2,3,0,-1]
checker = Checker(list)
checker.check()
