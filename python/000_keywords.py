from abc import ABC as Abstract
found = False
class Finder(Abstract):
    def find_word(self):
        return None
class WordFinder(Finder):
    nums=[]
    def __init__(self, strs):
        self.strs = strs
    def find_word(self):
        #found = False
        for s in self.strs:
            global found
            assert s is not None
            if s == 'Hello':
                continue
            elif s == 'World' and found is False:
                found = True
            elif s=='World':
                del strs[-1]
                break
            elif s < 100 or s > 1000:
                self.nums.append(s)
            else:
                pass
    def divide(self):
        found_minus = False
        def check_number(nums):
            nonlocal found_minus
            for num in nums:
                if num < 0:
                    found_minus = True
        check_number(self.nums)
        print('found non minus:', not found_minus)
        div = lambda x: 100/x
        for i in range(0, len(self.nums)):
            origin = self.nums[i]
            try:
                if self.nums[i] < 0:
                    with open('/tmp/file', 'r') as my_file:
                        my_file.write('hello world')
                    raise Exception("Sorry, no number smaller than 0 is allowed")
                self.nums[i] = div(self.nums[i])
            except ZeroDivisionError:
                self.nums[i] = 'Zero division'
            except FileNotFoundError:
                self.nums[i] = 'File not found'
            except Exception:
                self.nums[i] = 'General exception'
            finally:
                print(f'divide 100 by {origin}')
    def __iter__(self):
        for num in self.nums:
            yield 'result:'+str(num)
strs=[1,2,3,0, -1,'Hello', 'Hello', 'World', 'World']
wf = WordFinder(strs)
del strs[0]
wf.find_word()
wf.divide()
print(found)
len = len(wf.nums)
i = 0
while i< len:
    print(wf.nums[i])
    i +=1
for r in wf:
    print(r)
