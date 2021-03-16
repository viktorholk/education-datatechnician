using System;
using Xunit;
using System.Collections.Generic;
using GPOpgaver;
//using GPOpgaveløsning;


namespace XUnitTestGPOpgaver
{
    public class AlgoritmerIntroTest
    {
        [Fact]
        public void Exercise1Test()
        {
            int x = 5, y = 10;
            Opgaver.Interchange(ref x, ref y);
            Assert.Equal(10, x);
            Assert.Equal(5, y);
        }
        [Fact]
        public void Exercise2Test()
        {
            int a = 1, b = 2, c = 3;
            Opgaver.InterchangeTriple(ref a, ref b, ref c);
            Assert.Equal(3, a);
            Assert.Equal(1, b);
            Assert.Equal(2, c);
        }

        [Fact]
        public void Exercise3Test()
        {
            string palindrome = "level";
            Assert.True(Opgaver.IsPalindrome(palindrome));
            palindrome = "cammac";
            Assert.True(Opgaver.IsPalindrome(palindrome));
            palindrome = "kayak";
            Assert.True(Opgaver.IsPalindrome(palindrome));
            palindrome = "ward draw";
            Assert.True(Opgaver.IsPalindrome(palindrome));
        }
        [Fact]
        public void Exercise4ATest()
        {
            int[] intArray = { 1, 3, 4, 5, 6, 8, 9, 11 };
            Assert.Equal(8, Opgaver.StepsInLinearSearch(11, intArray));
            Assert.Equal(4, Opgaver.StepsInLinearSearch(5, intArray));
            //Assert.Equal(???, Opgaver.StepsInLinearSearch(9, intArray));
        }
        [Fact]
        public void Exercise4BTest()
        {
            int[] intArray = { 1, 3, 4, 5, 6, 8, 9, 11 };
            Assert.Equal(2, Opgaver.StepsInBinarySearch(intArray, 0, intArray.Length - 1, 3));
            Assert.Equal(1, Opgaver.StepsInBinarySearch(intArray, 0, intArray.Length - 1, 5));
            Assert.Equal(3, Opgaver.StepsInBinarySearch(intArray, 0, intArray.Length - 1, 9));
        }
        [Fact]
        public void Exercise5Test()
        {
            List<int> listA = new List<int> { 1, 2, 3, 5, 6, 8, 9 };
            Assert.Equal(3, Opgaver.InsertSortedList(listA, 4));
            List<int> listB = new List<int> { 1, 2, 3, 5, 6, 8, 9 };
            Assert.Equal(5, Opgaver.InsertSortedList(listB, 7));
        }
        [Fact]
        public void Exercise6Test()
        {
            Assert.Equal(new int[] { 7, 14, 21, 28, 35 }, Opgaver.ArrayOfMultiples(7, 5));
            Assert.Equal(new int[] { 12, 24, 36, 48, 60, 72, 84, 96, 108, 120 }, Opgaver.ArrayOfMultiples(12, 10));
            Assert.Equal(new int[] { 17, 34, 51, 68, 85, 102, 119 }, Opgaver.ArrayOfMultiples(17, 7));
            Assert.Equal(new int[] { 630, 1260, 1890, 2520, 3150, 3780, 4410, 5040, 5670, 6300, 6930, 7560, 8190, 8820 }, Opgaver.ArrayOfMultiples(630, 14));
            Assert.Equal(new int[] { 140, 280, 420 }, Opgaver.ArrayOfMultiples(140, 3));
            Assert.Equal(new int[] { 7, 14, 21, 28, 35, 42, 49, 56 }, Opgaver.ArrayOfMultiples(7, 8));
            Assert.Equal(new int[] { 11, 22, 33, 44, 55, 66, 77, 88, 99, 110, 121, 132, 143, 154, 165, 176, 187, 198, 209, 220, 231 }, Opgaver.ArrayOfMultiples(11, 21));
        }
        [Fact]
        public void Exercise7Test()
        {
            Assert.Equal(1, Opgaver.PowerRanger(5, 31, 33));
            Assert.Equal(3, Opgaver.PowerRanger(4, 250, 1300));
            Assert.Equal(2, Opgaver.PowerRanger(2, 49, 65));
            Assert.Equal(3, Opgaver.PowerRanger(3, 1, 27));
            Assert.Equal(1, Opgaver.PowerRanger(10, 1, 5));
            Assert.Equal(5, Opgaver.PowerRanger(1, 1, 5));
            Assert.Equal(10, Opgaver.PowerRanger(2, 1, 100));
        }
        [Fact]
        public void Exercise8Test()
        {
            Assert.Equal(1, Opgaver.Factorial(0));
            Assert.Equal(2, Opgaver.Factorial(2));
            Assert.Equal(6, Opgaver.Factorial(3));
            Assert.Equal(24, Opgaver.Factorial(4));
            Assert.Equal(120, Opgaver.Factorial(5));
            Assert.Equal(720, Opgaver.Factorial(6));
            Assert.Equal(3628800, Opgaver.Factorial(10));
            Assert.Equal(6227020800, Opgaver.Factorial(13));
            Assert.Equal(87178291200, Opgaver.Factorial(14));
        }
        [Fact]
        public void Exercise9Test()
        {
            Assert.Equal("foo1", Opgaver.IncrementString("foo"));
            Assert.Equal("foobar01003", Opgaver.IncrementString("foobar01002"));
            Assert.Equal("foobar00600", Opgaver.IncrementString("foobar00599"));
            Assert.Equal("foo100", Opgaver.IncrementString("foo099"));
            Assert.Equal("foo10000", Opgaver.IncrementString("foo09999"));
            Assert.Equal("foo10000", Opgaver.IncrementString("foo9999"));
        }

        [Fact]
        public void Aspfk()
        {
            // INVALID PASSWORDS
            Assert.False(Opgaver.ValidatePassword("P1zz@")); //5 characters
            Assert.False(Opgaver.ValidatePassword("P1zz@P1zz@P1zz@P1zz@P1zz@")); //25 characters
            Assert.False(Opgaver.ValidatePassword("mypassword11")); //No uppercase
            Assert.False(Opgaver.ValidatePassword("MYPASSWORD11")); //No lowercase
            Assert.False(Opgaver.ValidatePassword("iLoveYou")); //No numbers
            Assert.False(Opgaver.ValidatePassword("Pè7$areLove")); //è is not a speciel character
            // VALID PASSWORDS
            Assert.True(Opgaver.ValidatePassword("H4(k+x0z"));
            Assert.True(Opgaver.ValidatePassword("Fhweg93@"));
            Assert.True(Opgaver.ValidatePassword("aA0!@#$%^&*()+=_-{}[]:;\""));
            Assert.True(Opgaver.ValidatePassword("zZ9'?<>,."));

        }
    }
}
