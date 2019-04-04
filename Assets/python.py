# -*- coding:UTF-8 -*-
import sys

reload(sys)
sys.setdefaultencoding('utf8')

def Test1(para1,para2):
    return para1+para2;

def Test2():
    return '等等我'

print("Hello, World!")
print(Test1(sys.argv[1],sys.argv[2]))
print(Test2())
