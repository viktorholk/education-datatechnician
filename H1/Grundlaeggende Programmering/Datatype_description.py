import sys
# Text
_string = "this is a string"
# Numeric Types
_int    = 1
_float  = 1.0
# Sequence Types
_list   = [1,2,3]
_tuple  = (1,2,3)
# Boolean Type
_bool = True

types = {
    str: "this is a string",
    int: 1,
    float: 1.2,
    list: [1,2,3],
    tuple: (1,2,3),
    bool: True
}

def display(var):
    _value      = var
    _type       = type(var)
    _size       = sys.getsizeof(var)
    _hex = hex(id(hex))
    _max_size   = 0
        
    print(f"{str(_type):<20}{str(_value):<20}{str(_size):<20}{str(_hex):<20}")

if __name__ == '__main__':
    print(f"{str('TYPE'):<20}{str('VALUE'):<20}{str('SIZE'):<20}{str('HEX'):<20}")
    for i in types.items():
        display(i[1])
    print("Select a type to show the size in a byte diagram")
    while True:
        user_input = input(" $ ")
        if user_input == 'q':
            quit()
        try:
            _type = eval(user_input)
            if _type in types:
                # get size
                _size = sys.getsizeof(types[_type])
                for i in range(_size):
                    if i % 4 == 0:
                        print()
                    print("|byte| ", end='')
                print()
            else:
                print("thats not even one of the types i've showed on the list stopid")
        except:
            print('what in the holy mackerel are you trying to do')



