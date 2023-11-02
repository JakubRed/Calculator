namespace KalkulatorKS
{
    public partial class Frontend : Form
    {
        public Frontend()
        {
            InitializeComponent();
        }
        #region Digit buttons
        private void Button0_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("0");
            Display.Text = Backend.currentDisplay();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("1");
            Display.Text = Backend.currentDisplay();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("2");
            Display.Text = Backend.currentDisplay();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("3");
            Display.Text = Backend.currentDisplay();
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("4");
            Display.Text = Backend.currentDisplay();
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("5");
            Display.Text = Backend.currentDisplay();
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("6");
            Display.Text = Backend.currentDisplay();
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("7");
            Display.Text = Backend.currentDisplay();
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("8");
            Display.Text = Backend.currentDisplay();
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            Backend.enterDigit("9");
            Display.Text = Backend.currentDisplay();
        }
        #endregion Digit buttons

        #region Operators buttons
        private void ButtonAddition_Click(object sender, EventArgs e)
        {
            if (Backend.enterOperator('+')) //assiogn operator
            {   //the first operation
                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator());
            }
            else // not the first operation
            {
                //if (0 != Backend.doTheMath())
                //{
                //    //error
                //}
                //else
                //{
                    ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator()) /*+ " " + Backend.showNumber() + " ="*/;
                    //ResultDisplay.Text += " " + Backend.showNumber() + " =";
                    Display.Text = Convert.ToString(Backend.showResult());
                //}

            }
        }
        private void ButtonSubtraction_Click(object sender, EventArgs e)
        {
            if (Backend.enterOperator('-')) //assiogn operator
            {   //the first operation
                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator());
            }
            else // not the first operation
            {
     
                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator()) /*+ " " + Backend.showNumber() + " ="*/;
                Display.Text = Convert.ToString(Backend.showResult());
            }
        }
        private void ButtonDivision_Click(object sender, EventArgs e)
        {
            if (Backend.enterOperator('/')) //assiogn operator
            {   //the first operation
                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator());
            }
            else // not the first operation
            {

                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator()) /*+ " " + Backend.showNumber() + " ="*/;
                Display.Text = Convert.ToString(Backend.showResult());
            }
        }
        private void ButtonMultiplication_Click(object sender, EventArgs e)
        {
            if (Backend.enterOperator('x')) //assiogn operator
            {   //the first operation
                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator());
            }
            else // not the first operation
            {

                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator()) /*+ " " + Backend.showNumber() + " ="*/;
                Display.Text = Convert.ToString(Backend.showResult());
            }
        }
        private void ButtonExponentiation_Click(object sender, EventArgs e)
        {
            if (Backend.enterOperator('^')) //assiogn operator
            {   //the first operation
                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator());
            }
            else // not the first operation
            {

                ResultDisplay.Text = Convert.ToString(Backend.showFormerResult()) + " " + Char.ToString(Backend.showOperator()) /*+ " " + Backend.showNumber() + " ="*/;
                Display.Text = Convert.ToString(Backend.showResult());
            }
        }

        #endregion Operators buttons

        #region Other buttons
        private void ButtonComma_Click(object sender, EventArgs e)
        {

            if (!Backend.isCommaInNumber())
            {
                Backend.enterDigit(",");
                Display.Text = Backend.currentDisplay();
            }
        }
        private void ButtonNegation_Click(object sender, EventArgs e)
        {
            Backend.oppositeNumber();
            Display.Text = Backend.currentDisplay();
        }
        private void ButtonEqual_Click(object sender, EventArgs e)
        {
            if (!Backend.isItFirstOperation() && Backend.isNumberEntered())
            {
                ResultDisplay.Text = Backend.showFormerResult() + " " + Backend.showOperator() + " " + Convert.ToString(Backend.showNumber()) + " =";

                switch (Backend.doTheMath())
                {
                    case 0:
                        Display.Text = Convert.ToString(Backend.showResult());
                        Backend.cyrcleOfLIfe();
                        break;
                    case -1:
                        //add sth
                        break;
                    case -2:
                        Display.Text = "Nie można dzielić przez 0";
                        break;
                    default:
                        //add sth
                        break;
                }
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (Backend.isNumberEntered())
            {
                Backend.shortenTheNumber();

            }
            else
            {
                Backend.tabulaRasa();
                ResultDisplay.Text = "";
            }
            Display.Text = Backend.currentDisplay();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Backend.tabulaRasa();
            Display.Text = Backend.currentDisplay();
            ResultDisplay.Text = Char.ToString((Backend.showOperator()));

        }
        #endregion Other buttons
        private void Skin_Click(object sender, EventArgs e)
        {

        }
        private void Operator_Click(object sender, EventArgs e)
        {

        }

        private void Clock_Click(object sender, EventArgs e)
        {

        }
    }
}