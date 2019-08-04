using System;
using System.Linq;

namespace FindNextSmallestPalindrome
{
    class Program
    {
        static Random rnd = new Random();
        static int hexInputCharacters = rnd.Next(1, 1000000);
        static void Main(string[] args)
        {
            string hexInput = GenerateBigHexadecimal(hexInputCharacters);
            Console.WriteLine("Input hex is {0}", hexInput);

            findNextSmallestPalindrome(hexInput);
        }

        private static void findNextSmallestPalindrome(string hexInput)
        {
            string scenarioType = findTypeOfScenario(hexInput);
            var nextSmallestPalindrome = string.Empty;

            Console.WriteLine("Scenario type: {0}", scenarioType);

            switch (scenarioType)
            {
                case ScenarioType.InputIsPalindrome:
                    nextSmallestPalindrome = processForInputThatisPalindrome(hexInput);
                    Console.WriteLine("Next smallest palindrome is {0}", nextSmallestPalindrome);
                    break;

                case ScenarioType.LHSIsLargerThanRHS:
                    nextSmallestPalindrome = processForInputThatisLHSIsLargerThanRHS(hexInput);
                    Console.WriteLine("Next smallest palindrome is {0}", nextSmallestPalindrome);
                    break;

                case ScenarioType.RHSIsLargerThanLHS:
                    nextSmallestPalindrome = processForInputThatisRHSIsLargerThanLHS(hexInput);
                    Console.WriteLine("Next smallest palindrome is {0}", nextSmallestPalindrome);
                    break;

                case ScenarioType.Unknown:
                    Console.WriteLine("Unable to find the next smallest palindrome.");
                    break;
            }
        }

        private static string processForInputThatisRHSIsLargerThanLHS(string hexInput)
        {
            string newHex = IncrementHexByOneToMiddleDigit(hexInput);
            char[] newHexArray = newHex.ToCharArray();

            if (newHex.Length % 2 == 0)
            {
                newHexArray = CopyLHStoRHSForInputWithEvenLength(newHexArray);
            }
            else
            {
                newHexArray = CopyLHStoRHSForInputWithOddLength(newHexArray);
            }

            var nextSmallestPalindrome = ConvertCharArrayToString(newHexArray);

            return nextSmallestPalindrome;
        }

        private static string processForInputThatisLHSIsLargerThanRHS(string hexInput)
        {
            char[] hexArray = hexInput.ToCharArray();

            if (hexInput.Length % 2 == 0)
            {
                hexArray = CopyLHStoRHSForInputWithEvenLength(hexArray);
            }
            else
            {
                hexArray = CopyLHStoRHSForInputWithOddLength(hexArray);
            }

            var nextSmallestPalindrome = ConvertCharArrayToString(hexArray);

            return nextSmallestPalindrome;
        }

        private static string processForInputThatisPalindrome(string hexInput)
        {
            string newHex = IncrementHexByOneToMiddleDigit(hexInput);
            char[] newHexArray = newHex.ToCharArray();

            if (newHex.Length != 1)
            {
                if (newHex.Length % 2 == 0)
                {
                    newHexArray = CopyLHStoRHSForInputWithEvenLength(newHexArray);
                }
                else
                {
                    newHexArray = CopyLHStoRHSForInputWithOddLength(newHexArray);
                }
            }

            var nextSmallestPalindrome = ConvertCharArrayToString(newHexArray);

            return nextSmallestPalindrome;
        }

        private static char[] CopyLHStoRHSForInputWithOddLength(char[] hexArray)
        {
            int initialPositionOfCursorToCopyFrom = (hexArray.Length / 2) - 1;
            int initialPositionOfCursorToCopyTo = (hexArray.Length / 2) + 1;

            return CopyLHSValueToRHS(hexArray, initialPositionOfCursorToCopyFrom, initialPositionOfCursorToCopyTo);
        }

        private static char[] CopyLHStoRHSForInputWithEvenLength(char[] hexArray)
        {
            int initialPositionOfCursorToCopyFrom = (hexArray.Length / 2) - 1;
            int initialPositionOfCursorToCopyTo = (hexArray.Length / 2);

            return CopyLHSValueToRHS(hexArray, initialPositionOfCursorToCopyFrom, initialPositionOfCursorToCopyTo);
        }

        private static char[] CopyLHSValueToRHS(char[] hexArray, int initialPositionOfCursorToCopyFrom, int initialPositionOfCursorToCopyTo)
        {
            int positionOfCursorToCopyFrom = initialPositionOfCursorToCopyFrom;
            int positionOfCursorToCopyTo = initialPositionOfCursorToCopyTo;

            while (positionOfCursorToCopyFrom >= 0)
            {
                hexArray[positionOfCursorToCopyTo] = hexArray[positionOfCursorToCopyFrom];

                positionOfCursorToCopyFrom = positionOfCursorToCopyFrom - 1;
                positionOfCursorToCopyTo = positionOfCursorToCopyTo + 1;
            }

            return hexArray;
        }

        private static string findTypeOfScenario(string hexInput)
        {
            char[] hexInputArray = hexInput.ToCharArray();
            string scenarioType = string.Empty;

            if (hexInput.Length % 2 == 0)
            {
                scenarioType = compareLHSAndRHSDigitsForInputWithEvenLength(hexInputArray);
            }
            else
            {
                scenarioType = compareLHSAndRHSDigitsForInputWithOddLength(hexInputArray);
            }

            return scenarioType;
        }

        private static string compareLHSAndRHSDigitsForInputWithOddLength(char[] hexInputArray)
        {
            int positionOfMiddleDigit = (hexInputArray.Length / 2);
            int lhsCursorPosition = 0;
            int rhsCursorPosition = 0;

            if (hexInputArray.Length == 1)
            {
                lhsCursorPosition = 0;
                rhsCursorPosition = 0;
            }
            else
            {
                lhsCursorPosition = positionOfMiddleDigit - 1;
                rhsCursorPosition = positionOfMiddleDigit + 1;
            }
            
            return compareDigitsAndFindScenarioType(hexInputArray, lhsCursorPosition, rhsCursorPosition);
        }

        private static string compareLHSAndRHSDigitsForInputWithEvenLength(char[] hexInputArray)
        {
            int positionOfMiddleDigitLHS = (hexInputArray.Length / 2) - 1;
            int positionOfMiddleDigitRHS = (hexInputArray.Length / 2);

            int lhsCursorPosition = positionOfMiddleDigitLHS;
            int rhsCursorPosition = positionOfMiddleDigitRHS;
            return compareDigitsAndFindScenarioType(hexInputArray, lhsCursorPosition, rhsCursorPosition);
        }

        private static string compareDigitsAndFindScenarioType(char[] hexInputArray, int lhsCursorPosition, int rhsCursorPosition)
        {
            while (lhsCursorPosition >= 0)
            {
                if (isInputPalindrome(lhsCursorPosition, rhsCursorPosition, hexInputArray))
                {
                    return ScenarioType.InputIsPalindrome;
                }

                if (hexInputArray[lhsCursorPosition] > hexInputArray[rhsCursorPosition])
                {
                    return ScenarioType.LHSIsLargerThanRHS;
                }

                if (hexInputArray[rhsCursorPosition] > hexInputArray[lhsCursorPosition])
                {
                    return ScenarioType.RHSIsLargerThanLHS;
                }

                lhsCursorPosition = lhsCursorPosition - 1;
                rhsCursorPosition = rhsCursorPosition + 1;
            }

            return ScenarioType.Unknown;
        }

        private static bool isInputPalindrome(int lhsCursorPosition, int rhsCursorPosition, char[] hexInputArray)
        {
            if (lhsCursorPosition == 0 && rhsCursorPosition == (hexInputArray.Length - 1))
            {
                if (hexInputArray[lhsCursorPosition] == hexInputArray[rhsCursorPosition])
                {
                    return true;
                }
            }

            return false;
        }

        private static string IncrementHexByOneToMiddleDigit(string hexInput)
        {
            string newHex = string.Empty;

            if (hexInput.Length % 2 == 0)
            {
                newHex = addOneToMiddleDigits(hexInput);
            }
            else
            {
                newHex = AddOneToMiddleDigit(hexInput);
            }

            return newHex;
        }

        private static string addOneToMiddleDigits(string hexInput)
        {
            int positionOfMiddleDigitLHS = (hexInput.Length / 2) - 1;
            int positionOfMiddleDigitRHS = (hexInput.Length / 2);

            char[] hexInputArray = hexInput.ToCharArray();

            hexInputArray[positionOfMiddleDigitRHS] = IncrementHex(hexInputArray[positionOfMiddleDigitRHS]);

            hexInputArray = IncrementLHS(positionOfMiddleDigitLHS, hexInputArray);

            string newHex = ConvertCharArrayToString(hexInputArray);

            return newHex;
        }

        private static string AddOneToMiddleDigit(string hexInput)
        {
            int positionOfMiddleDigit = hexInput.Length / 2;

            char[] hexInputArray = hexInput.ToCharArray();
            hexInputArray = IncrementLHS(positionOfMiddleDigit, hexInputArray);

            string newHex = ConvertCharArrayToString(hexInputArray);

            return newHex;
        }

        private static char[] IncrementLHS(int positionOfDigitToStartIncrementing, char[] hexInputArray)
        {
            int carry = 0;

            for (int cursor = positionOfDigitToStartIncrementing; cursor >= 0; cursor--)
            {
                if (cursor == positionOfDigitToStartIncrementing)
                {
                    hexInputArray[cursor] = IncrementHexAndAddCarry(hexInputArray[cursor], ref carry);
                }
                else
                {
                    hexInputArray[cursor] = AddCarry(hexInputArray[cursor], ref carry);
                }

                if (cursor == 0 && carry == 1)
                {
                    hexInputArray = handleOverflowArray(hexInputArray);
                }
            }

            return hexInputArray;
        }

        private static char[] handleOverflowArray(char[] hexInputArray)
        {
            char[] resizedArray = new char[hexInputArray.Length + 1];

            resizedArray[0] = '1';

            for (int i = 0; i < hexInputArray.Length; i++)
            {
                resizedArray[i + 1] = hexInputArray[i];
            }

            return resizedArray;
        }

        private static char AddCarry(char hexChar, ref int carry)
        {
            int numberInput = int.Parse(hexChar.ToString(), System.Globalization.NumberStyles.HexNumber);

            int newNumber = numberInput + carry;

            carry = newNumber > 15 ? 1 : 0;

            string newNumberString = newNumber.ToString("X");

            char newNumberChar = newNumberString.ToCharArray()[newNumberString.Length - 1];

            return newNumberChar;
        }

        private static char IncrementHexAndAddCarry(char hexChar, ref int carry)
        {
            int numberInput = int.Parse(hexChar.ToString(), System.Globalization.NumberStyles.HexNumber);

            int newNumber = numberInput + 1 + carry;

            carry = newNumber > 15 ? 1 : 0;

            string newNumberString = newNumber.ToString("X");

            char newNumberChar = newNumberString.ToCharArray()[newNumberString.Length - 1];

            return newNumberChar;
        }

        private static char IncrementHex(char hexChar)
        {
            int numberInput = int.Parse(hexChar.ToString(), System.Globalization.NumberStyles.HexNumber);

            int newNumber = numberInput + 1;

            string newNumberString = newNumber.ToString("X");

            char newNumberChar = newNumberString.ToCharArray()[newNumberString.Length - 1];

            return newNumberChar;
        }

        static string GenerateBigHexadecimal(int noOfChar)
        {
            Random random = new Random();

            byte[] buffer = new byte[noOfChar / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (noOfChar % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        static string ConvertCharArrayToString(char[] array)
        {
            string result = string.Join("", array);
            return result;
        }
    }

    public class ScenarioType
    {
        public const string InputIsPalindrome = "Input is palindrome";
        public const string LHSIsLargerThanRHS = "Digit from LHS is larger than digit from RHS";
        public const string RHSIsLargerThanLHS = "Digit from RHS is larger than digit from LHS";
        public const string Unknown = "Unable to determine scenario";
    }
}
