

print("FizzBuzz")
print("Count from 0-", end="")

count = ""
while (type(count) != int):
    try:
        count = int(input())
    except ValueError:
        print("That is not a valid number!")

for i in range(0, count + 1):
    res = ""

    if i % 3 == 0:
        res += "Fizz"

    if i % 5 == 0:
        res += "Buzz"
    
    if i % 7 == 0:
        res += "!"

    if i % 11 == 0:
        res += "?"

    if i % 13 == 0:
        res += "#"

    if i % 17 == 0:
        res += "&"

    if i % 19 == 0:
        res += "1337"

    print(i) if res == "" else print(res)


    


