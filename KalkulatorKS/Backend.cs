namespace KalkulatorKS
{
    static class Backend
    {
        static double result;
        static string number = "";
        static double formerResult;
        static char equationOperator;
        static bool formerResultEmpty = true;


        static public void enterDigit(string digit) //or comma
        {
            number += digit;
        }
        static public string showNumber()
        {
            return number;
        }        static public double showFormerResult()
        {
            return formerResult;
        }
        static public char showOperator()
        {
            return equationOperator;
        }        static public double showResult()
        {
            return result;
        }
        static public bool isItFirstOperation() 
        {
            return formerResultEmpty;
        }
        static public bool isCommaInNumber()
        {
            return number.Contains(",");
        }  
        static public bool isNumberEntered()
        {
            return !number.Equals("");
        }
        static public void shortenTheNumber()
        {
            number = number.Remove(number.Length - 1);
        }
        static public void oppositeNumber()
        {
            number = "-" + number;
        }
        static public void cyrcleOfLIfe()
        {
            formerResult = result;
            //result = 0;
            number = "";
        }
        static public bool enterOperator(char enteredOperator)
        {
            bool resultChanged = false;
            equationOperator = enteredOperator;
            if (formerResultEmpty)
            {
                formerResult = Convert.ToDouble(number);
                number = "";
                resultChanged = true;   
                formerResultEmpty = false;    
            }

            return resultChanged;
        }
        static public string currentDisplay() //Not complete
        {
            return number;
        }
        static public void tabulaRasa(/*bool includeResult*/)
        {
            //if (includeResult)
            //{
            //    result = 0;
            //}
            formerResult = 0;
            formerResultEmpty = true;
            number = "";
            equationOperator = '\0';
        }
        static public int doTheMath()
        {
        int errorCode = 0; //by default set to success
            switch (equationOperator)
            {
                case '+':
                    result = formerResult + Convert.ToDouble(number);
                break;
                case '-':
                    result = formerResult - Convert.ToDouble(number); ;
                break;
                case '/':
                    if (0 == Convert.ToDouble(number))
                    {
                        errorCode = -2; // Attempt to divide by 0
                    }
                    else
                    {
                        result = formerResult / Convert.ToDouble(number);
                    }
                break;
                case 'x':
                    result = formerResult * Convert.ToDouble(number);
                    break;
                case '^':
                    result = Math.Pow(formerResult, Convert.ToDouble(number));
                break;
                default:
                    errorCode = -1; //unknown error
                break;
            }
            return errorCode;
        }
        static public void cyrcleOfLife()
        {
            formerResult = result;
            result = Convert.ToDouble(number);
        }
    }
}
