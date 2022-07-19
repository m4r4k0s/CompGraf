import random
from math import sqrt 


n1=0
n2=0
x = random.random()
y = random.random()
z = sqrt(x*x + y*y)
for i in range(10000000):
    if z <= 1:
        n1+=1
    n2+=1
    x = random.random()
    y = random.random()
    z = sqrt(x*x + y*y)

print(4*n1/n2)