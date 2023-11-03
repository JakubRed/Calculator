using System.Windows.Forms;

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
            enterDigit("0");
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            enterDigit("1");
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            enterDigit("2");
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            enterDigit("3");
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            enterDigit("4");
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            enterDigit("5");
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            enterDigit("6");
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            enterDigit("7");
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            enterDigit("8");
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            enterDigit("9");
        }
        #endregion Digit buttons

        #region Operators buttons
        private void ButtonAddition_Click(object sender, EventArgs e)
        {
            enterOperator('+');
        }

        private void ButtonSubtraction_Click(object sender, EventArgs e)
        {
            enterOperator('-');
        }

        private void ButtonDivision_Click(object sender, EventArgs e)
        {
            enterOperator('/');
        }

        private void ButtonMultiplication_Click(object sender, EventArgs e)
        {
            enterOperator('x');
        }

        private void ButtonExponentiation_Click(object sender, EventArgs e)
        {

            enterOperator('^');
        }

        #endregion Operators buttons

        #region Other buttons
        private void ButtonComma_Click(object sender, EventArgs e)
        {
            if (!Backend.IsCommaInNumber())
            {
                Backend.EnterDigit(",");
                Display.Text = Backend.CurrentDisplay();
            }
        }

        private void ButtonNegation_Click(object sender, EventArgs e)
        {
            Backend.OppositeNumber();
            Display.Text = Backend.CurrentDisplay();
        }

        private void ButtonEqual_Click(object sender, EventArgs e)
        {
            if (!Backend.IsItFirstOperation() && Backend.IsNumberEntered())
            {
                if (0 > Convert.ToDouble(Backend.ShowNumber()))
                {
                    //negative number with parenthesis
                    ResultDisplay.Text = Backend.ShowFormerResult() + " " + Backend.ShowOperator() + " " + "(" + Backend.ShowNumber() + ")" + " =";
                }
                else
                {
                    ResultDisplay.Text = Backend.ShowFormerResult() + " " + Backend.ShowOperator() + " " + Backend.ShowNumber() + " =";
                }

                switch (Backend.DoTheMath())
                {
                    case 0:
                        Display.Text = Convert.ToString(Backend.ShowResult());
                        Backend.CircleOfLife();
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
            ButtonEqual.BackColor = Color.Green;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (Backend.IsNumberEntered())
            {
                Backend.ShortenTheNumber();
            }
            else
            {
                Backend.TabulaRasa();
                ResultDisplay.Text = "";
            }
            Display.Text = Backend.CurrentDisplay();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Backend.TabulaRasa();
            Display.Text = Backend.CurrentDisplay();
            ResultDisplay.Text = Char.ToString((Backend.ShowOperator()));

        }
        #endregion Other buttons
        private void Skin_Click(object sender, EventArgs e)
        {

        }

        private void Operator_Click(object sender, EventArgs e)
        //probably to be removed
        {

        }

        private void Clock_Click(object sender, EventArgs e)
        {

        }

        private void enterDigit(string digit)
        {
            if (Backend.EnterDigit(digit))
            {
                ResultDisplay.Text = "";
            }
            Display.Text = Backend.CurrentDisplay();
        }

        private void enterOperator(char enteredOperator)
        {
            Backend.EnterOperator(enteredOperator);
            ResultDisplay.Text = Convert.ToString(Backend.ShowFormerResult()) + " " + Char.ToString(Backend.ShowOperator());
        }

        private void Frontend_Load(object sender, EventArgs e)
        {

        }

        private void Display_Click(object sender, EventArgs e)
        {

        }

        //Keyboard input management
        private void Frontend_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    Button0.PerformClick();
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    Button1.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    Button2.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    Button3.PerformClick();
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    Button4.PerformClick();
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    Button5.PerformClick();
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        ButtonExponentiation.PerformClick();
                    }
                    else
                    {
                        Button6.PerformClick();
                    }
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    Button7.PerformClick();
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    Button8.PerformClick();
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    Button9.PerformClick();
                    break;
                case Keys.OemMinus:
                    ButtonSubtraction.PerformClick();
                    break;
                case Keys.Oemplus:
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        ButtonEqual.PerformClick();
                    }
                    else
                    {
                        ButtonAddition.PerformClick();
                    }
                    break;
                case Keys.OemQuestion:
                    ButtonDivision.PerformClick();
                    break;
                case Keys.Back:
                    ButtonCancel.PerformClick();
                    break;
                case Keys.Multiply:
                case Keys.X:
                    ButtonMultiplication.PerformClick();
                    break;

            }
        }
    }
}