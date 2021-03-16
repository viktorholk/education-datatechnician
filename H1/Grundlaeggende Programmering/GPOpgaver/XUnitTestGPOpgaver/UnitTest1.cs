using System;
using Xunit;
using System.Collections.Generic;
using GPOpgaver;


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
            Assert.Equal( 3, a);
            Assert.Equal( 1, b);
            Assert.Equal( 2, c);
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
            Assert.Equal( 8, Opgaver.StepsInLinearSearch(11, intArray));
            Assert.Equal( 4, Opgaver.StepsInLinearSearch(5, intArray));
            //Assert.Equal(???, Opgaver.StepsInLinearSearch(9, intArray));
        }
        [Fact]
        public void Exercise4BTest()
        {
            int[] intArray = { 1, 3, 4, 5, 6, 8, 9, 11 };
            Assert.Equal( 2, Opgaver.StepsInBinarySearch(intArray, 0, intArray.Length - 1, 3));
            Assert.Equal( 1, Opgaver.StepsInBinarySearch(intArray, 0, intArray.Length - 1, 5));
            //Assert.Equal(???, Opgaver.StepsInBinarySearch(intArray, 0, intArray.Length - 1, 9));
        }
        [Fact]
        public void Exercise5Test()
        {
            List<int> lista = new List<int>{ 1, 2, 3, 5, 6, 8, 9};
            Assert.Equal( 3, Opgaver.InsertSortedList(lista, 4));
            List<int> listb = new List<int> { 1, 2, 3, 5, 6, 8, 9 };
            Assert.Equal( 5, Opgaver.InsertSortedList(listb, 7));
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
    }
}
