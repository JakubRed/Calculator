﻿namespace KalkulatorKS
{
    static class Backend
    {
        static double result = 0; //random easily recognizable number
        static string number = "";
        static double formerResult = 0; //random easily recognizable number
        static char equationOperator;
        static bool formerResultEmpty = true;
        static bool newCycle = true;


        static public bool EnterDigit(string digit) //or comma
        {
            bool startedNewCycle = false;
            if (number.Equals("") && newCycle)
            {
                TabulaRasa();
                startedNewCycle = true;
            }
            number += digit;
            newCycle = false;
            return startedNewCycle;
        }

        static public string ShowNumber()
        {
            if (number.EndsWith(","))
            {
                number = number.Remove(number.Length - 1, 1);
            }
            return number;
        }     
        
        static public double ShowFormerResult()
        {
            return formerResult;
        }

        static public char ShowOperator()
        {
            return equationOperator;
        }   
        static public double ShowResult()
        {
            return result;
        }

        static public bool IsItFirstOperation() 
        {
            return formerResultEmpty;
        }

        static public bool IsCommaInNumber()
        {
            return number.Contains(",");
        }  

        static public bool IsNumberEntered()
        {
            return !number.Equals("");
        }

        static public void ShortenTheNumber()
        {
            number = number.Remove(number.Length - 1);
        }

        static public void OppositeNumber()
        {
            if (!number.Equals("") && "-" == number.Substring(0,1))
            {
              number = number.Remove(0, 1);
            }
            else
            {
                number = "-" + number;               
            }
        }

        static public void CircleOfLife()
        {
            formerResult = result;
            number = "";
            newCycle = true;

        }

        static public bool EnterOperator(char enteredOperator)
        {
            bool resultChanged = false;
            equationOperator = enteredOperator;
            if (formerResultEmpty && !string.IsNullOrWhiteSpace(number))
            {
                formerResult = Convert.ToDouble(number);
                number = "";
                resultChanged = true;
                formerResultEmpty = false;
            }
            newCycle = false;
            return resultChanged;
        }

        static public string CurrentDisplay() //Not complete
        {
            return number;
        }

        static public void TabulaRasa(/*bool includeResult*/)
        {
            //if (includeResult)
            //{
            result = 0;
            //}
            formerResult = 0;
            formerResultEmpty = true;
            number = "";
            equationOperator = '\0';
            newCycle = true;
        }

        static public int DoTheMath()
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
    }
}
