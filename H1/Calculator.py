class Calculator:
    logs = []

    def evaluate(self, string):
        try:
            _eval = self.format_eval(string)
            # Add to log
            Calculator.logs.append(_eval)
            return _eval
        except:
            return 'Invalid input'
    
    def print_logs(self):
        for i in Calculator.logs:
            print(i)

    def format_eval(self, string):
        return str(f'{string} = {eval(string)}')
    


if __name__ == '__main__':
    calculator = Calculator()
    print('H1 Calculator')
    while True:
        user_input  = input('> ')
        if not user_input == 'logs':
            output = calculator.evaluate(user_input)
            print(output)
        else:
            calculator.print_logs()



