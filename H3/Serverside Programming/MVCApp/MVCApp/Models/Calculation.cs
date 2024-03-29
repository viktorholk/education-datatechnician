﻿namespace MVCApp.Models
{
    public enum Operator
    {
        Addition,
        Subtraction,
        Multiplication
    }
    public class Calculation
    {
        public int[] Values { get; private set; }
        public Operator Operator { get; private set; }
        public int Answer { get; set; }

        public Calculation(int[] values, Operator @operator)
        {
            if (values.Length != 2)
                throw new ArgumentException("Value array needs to be containing 2 values");

            Values = values;
            Operator = @operator;

        }

        public int Result()
        {
            return Operator switch
            {
                Operator.Addition => Values[0] + Values[1],
                Operator.Subtraction => Values[0] - Values[1],
                Operator.Multiplication => Values[0] * Values[1],
                _ => 0,
            };
        }

        public bool Validate()
        {
            return Answer == Result();
        }

        public string OperatorAsSymbol()
        {
            return this.Operator switch
            {
                Operator.Addition => "+",
                Operator.Subtraction => "-",
                Operator.Multiplication => "*",
                _ => ""
            };
        }

        public override string ToString()
        {
            return $"{this.Values[0]} {this.Operator} {this.Values[1]} = {this.Answer}";
        }
    }
}
