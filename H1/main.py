class Calculator:
    def evaluate(self, string):
        try:
            return eval(string)
        except:
            return 'Invalid input'


if __name__ == '__main__':
    calculator = Calculator()
    print('H1 Calculator')
    while True:
        _input  = input('> ')
        _output = calculator.evaluate(_input)
 
        print(_input + " = " + str(_output))


