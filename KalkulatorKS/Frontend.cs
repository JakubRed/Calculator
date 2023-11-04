using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace KalkulatorKS
{
    public partial class Frontend : Form
    {
        #region skin fonts
        Font NormalFontBig = new Font("Segoe UI", 32); //font for skin - Default
        Font NormalFont = new Font("Segoe UI", 24);
        Font NormalFontSmall = new Font("Segoe UI", 12);
        Font NormalFontButton = new Font("Segoe UI", 20); //font for skin - Default and Digital

        Font CatFontBig = new Font("Tempus Sans ITC", 32); //font for skin - Cat
        Font CatFont = new Font("Tempus Sans ITC", 24);
        Font CatFontSmall = new Font("Tempus Sans ITC", 18);
        Font CatFontButton = new Font("Tempus Sans ITC", 20);

        Font DigitalFontBig = new Font("Digital-7", 40); //font for skin - Digital
        Font DigitalFont = new Font("Digital-7", 24);
        Font DigitalFontSmall = new Font("Digital-7", 18);
        #endregion skin fonts

        private void Frontend_Load(object sender, EventArgs e)
        {
            timer1.Start(); //for clocks
            CatPic.Visible = false;
            JackRubberDuck.Visible = false;

            //for analog clock
            //create bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            //center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;
        }
        #region AnalogClock

        bool isDigitalClockVisible = true; //true - digital clock visible, false - analog clock visible
        int WIDTH = 200, HEIGHT = 200, SEC_HAND = 94, MIN_HAND = 66, HR_HAND = 54;

        //center 
        int cx, cy;
        Bitmap bmp;
        Graphics AnalogClockLook;

        private void button10_Click(object sender, EventArgs e)
        {
            isDigitalClockVisible = !isDigitalClockVisible;
        }
        private void AnalogClock_Click(object sender, EventArgs e)
        {
            isDigitalClockVisible = !isDigitalClockVisible;
        }
        private void Clock_Click(object sender, EventArgs e)
        {
            isDigitalClockVisible = !isDigitalClockVisible;
        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6; // each minute and second hands make 6 deegre move
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            // each hour it makes 30 deegre move
            // each minute it makes 0.5 deegre move
            int val = (int)((hval * 30) + (mval * 0.5));
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //analog clock
            //create graphics
            AnalogClockLook = Graphics.FromImage(bmp);
            //get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            if (!isDigitalClockVisible) //digital clock
            {
                Clock.BackColor = this.BackColor;
                Clock.Text = DateTime.Now.ToString("H:m:ss"); //24-hour format 
                AnalogClock.Visible = false;
                Clock.Visible = true;
            }
            else //analog clock
            {
                AnalogClock.Visible = true;
                Clock.Visible = false;
                //clear
                AnalogClockLook.Clear(this.BackColor);

                //draw circle
                AnalogClockLook.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, WIDTH - 2, HEIGHT - 2);

                //draw figure
                //numbers
                //AnalogClockLook.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(90, 2));
                //AnalogClockLook.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(180, 84));
                //AnalogClockLook.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(90, 180));
                //AnalogClockLook.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 88));

                //lines
                AnalogClockLook.DrawString("|", new Font("Arial", 12), Brushes.Black, new PointF(95, 0));
                AnalogClockLook.DrawString("—", new Font("Arial", 12), Brushes.Black, new PointF(178, 90));
                AnalogClockLook.DrawString("|", new Font("Arial", 12), Brushes.Black, new PointF(95, 178));
                AnalogClockLook.DrawString("—", new Font("Arial", 12), Brushes.Black, new PointF(0, 90));

                //second hand
                handCoord = msCoord(ss, SEC_HAND);
                AnalogClockLook.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                //minut hand
                handCoord = msCoord(mm, MIN_HAND);
                AnalogClockLook.DrawLine(new Pen(Color.Black, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                //hour hand
                handCoord = hrCoord(hh % 12, mm, HR_HAND);
                AnalogClockLook.DrawLine(new Pen(Color.Black, 4f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                //load bmp in AnalogClock
                AnalogClock.Image = bmp;

                //dispose
                AnalogClockLook.Dispose();
            }
        }
        #endregion AnalogClock

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
                        //add sth if needed
                        break;
                    case -2:
                        Display.Text = "Cannot divide by 0";
                        break;
                    default:
                        //add sth if needed
                        break;
                }
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (Backend.IsNumberEntered())
            {
                Backend.ShortenTheNumber();
            }
            else
            {
                ButtonClear.PerformClick();
            }
            Display.Text = Backend.CurrentDisplay();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Backend.TabulaRasa();
            Display.Text = Backend.CurrentDisplay(); //if cleaning function does not work, this will not be empty (error check)
            ResultDisplay.Text = Char.ToString((Backend.ShowOperator())); //if cleaning function does not work, this will not be empty (error check)

        }
        #endregion Other buttons

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
                        ButtonAddition.PerformClick();
                    }
                    else
                    {
                        ButtonEqual.PerformClick();
                    }
                    break;
                case Keys.Enter:
                    ButtonEqual.PerformClick();
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

        //diferent skin options
        private void SKIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Skin.Items[Skin.SelectedIndex])
            {
                case "Default":
                    this.BackColor = Color.LightGray;
                    Clock.ForeColor = Color.Black;
                    Clock.Font = NormalFontBig;

                    Display.Font = NormalFont;
                    Display.ForeColor = Color.Black;
                    Display.BackColor = Color.White;

                    ResultDisplay.ForeColor = Color.Black;
                    ResultDisplay.BackColor = Color.Silver;
                    ResultDisplay.Font = NormalFontSmall;
                    CatPic.Visible = false;
                    JackRubberDuck.Visible = false;

                    #region coloring normal buttons
                    ButtonCancel.BackColor = Color.LightGray;
                    ButtonCancel.ForeColor = Color.Black;
                    ButtonClear.BackColor = Color.LightGray;
                    ButtonClear.ForeColor = Color.Black;
                    Button0.BackColor = Color.LightGray;
                    Button1.BackColor = Color.LightGray;
                    Button2.BackColor = Color.LightGray;
                    Button3.BackColor = Color.LightGray;
                    Button4.BackColor = Color.LightGray;
                    Button5.BackColor = Color.LightGray;
                    Button6.BackColor = Color.LightGray;
                    Button7.BackColor = Color.LightGray;
                    Button8.BackColor = Color.LightGray;
                    Button9.BackColor = Color.LightGray;
                    ButtonEqual.BackColor = SystemColors.ActiveCaption;
                    ButtonSubtraction.BackColor = Color.LightGray;
                    ButtonDivision.BackColor = Color.LightGray;
                    ButtonMultiplication.BackColor = Color.LightGray;
                    ButtonNegation.BackColor = Color.LightGray;
                    ButtonAddition.BackColor = Color.LightGray;
                    ButtonComma.BackColor = Color.LightGray;
                    ButtonExponentiation.BackColor = Color.LightGray;
                    #endregion coloring normal buttons
                    #region button font                   
                    ButtonCancel.Font = NormalFontButton;
                    ButtonCancel.Font = NormalFontButton;
                    ButtonClear.Font = NormalFontButton;
                    ButtonClear.Font = NormalFontButton;
                    Button0.Font = NormalFontButton;
                    Button1.Font = NormalFontButton;
                    Button2.Font = NormalFontButton;
                    Button3.Font = NormalFontButton;
                    Button4.Font = NormalFontButton;
                    Button5.Font = NormalFontButton;
                    Button6.Font = NormalFontButton;
                    Button7.Font = NormalFontButton;
                    Button8.Font = NormalFontButton;
                    Button9.Font = NormalFontButton;
                    ButtonEqual.Font = NormalFontButton;
                    ButtonSubtraction.Font = NormalFontButton;
                    ButtonDivision.Font = NormalFontButton;
                    ButtonMultiplication.Font = NormalFontButton;
                    ButtonNegation.Font = NormalFontButton;
                    ButtonAddition.Font = NormalFontButton;
                    ButtonComma.Font = NormalFontButton;
                    ButtonExponentiation.Font = NormalFontButton;
                    #endregion button font
                    break;
                case "Cat": //cute cat
                    this.BackColor = Color.LightPink;
                    Clock.ForeColor = Color.Black;
                    ResultDisplay.BackColor = Color.Pink;
                    Display.BackColor = Color.White;

                    Clock.Font = CatFontBig;
                    Display.Font = CatFont;
                    ResultDisplay.Font = CatFontSmall;
                    Display.ForeColor = Color.Black;
                    ResultDisplay.ForeColor = Color.Black;
                    CatPic.Visible = true;
                    JackRubberDuck.Visible = false;
                    #region coloring normal buttons
                    ButtonCancel.BackColor = Color.Pink;
                    ButtonCancel.ForeColor = Color.Black;
                    ButtonClear.BackColor = Color.Pink;
                    ButtonClear.ForeColor = Color.Black;
                    Button0.BackColor = Color.Pink;
                    Button1.BackColor = Color.Pink;
                    Button2.BackColor = Color.Pink;
                    Button3.BackColor = Color.Pink;
                    Button4.BackColor = Color.Pink;
                    Button5.BackColor = Color.Pink;
                    Button6.BackColor = Color.Pink;
                    Button7.BackColor = Color.Pink;
                    Button8.BackColor = Color.Pink;
                    Button9.BackColor = Color.Pink;
                    ButtonEqual.BackColor = SystemColors.ActiveCaption;
                    ButtonSubtraction.BackColor = Color.Pink;
                    ButtonDivision.BackColor = Color.Pink;
                    ButtonMultiplication.BackColor = Color.Pink;
                    ButtonNegation.BackColor = Color.Pink;
                    ButtonAddition.BackColor = Color.Pink;
                    ButtonComma.BackColor = Color.Pink;
                    ButtonExponentiation.BackColor = Color.Pink;
                    #endregion coloring normal buttons
                    #region button font                   
                    ButtonCancel.Font = CatFontButton;
                    ButtonCancel.Font = CatFontButton;
                    ButtonClear.Font = CatFontButton;
                    ButtonClear.Font = CatFontButton;
                    Button0.Font = CatFontButton;
                    Button1.Font = CatFontButton;
                    Button2.Font = CatFontButton;
                    Button3.Font = CatFontButton;
                    Button4.Font = CatFontButton;
                    Button5.Font = CatFontButton;
                    Button6.Font = CatFontButton;
                    Button7.Font = CatFontButton;
                    Button8.Font = CatFontButton;
                    Button9.Font = CatFontButton;
                    ButtonEqual.Font = CatFontButton;
                    ButtonSubtraction.Font = CatFontButton;
                    ButtonDivision.Font = CatFontButton;
                    ButtonMultiplication.Font = CatFontButton;
                    ButtonNegation.Font = CatFontButton;
                    ButtonAddition.Font = CatFontButton;
                    ButtonComma.Font = CatFontButton;
                    ButtonExponentiation.Font = CatFontButton;
                    #endregion button font 
                    break;
                case "Digital": //digital skin 

                    this.BackColor = SystemColors.ControlDarkDark;
                    Clock.ForeColor = Color.White;
                    Clock.Font = DigitalFontBig;

                    ButtonCancel.BackColor = Color.Red;
                    ButtonCancel.ForeColor = Color.White;
                    ButtonClear.BackColor = Color.Red;
                    ButtonClear.ForeColor = Color.White;

                    Display.Font = DigitalFont;
                    Display.ForeColor = Color.Green;
                    Display.BackColor = Color.Black;

                    ResultDisplay.ForeColor = Color.Green;
                    ResultDisplay.BackColor = Color.Black;
                    ResultDisplay.Font = DigitalFontSmall;

                    CatPic.Visible = false;
                    JackRubberDuck.Visible = true;

                    #region coloring normal buttons
                    Button0.BackColor = Color.DarkGray;
                    Button1.BackColor = Color.DarkGray;
                    Button2.BackColor = Color.DarkGray;
                    Button3.BackColor = Color.DarkGray;
                    Button4.BackColor = Color.DarkGray;
                    Button5.BackColor = Color.DarkGray;
                    Button6.BackColor = Color.DarkGray;
                    Button7.BackColor = Color.DarkGray;
                    Button8.BackColor = Color.DarkGray;
                    Button9.BackColor = Color.DarkGray;
                    ButtonEqual.BackColor = Color.DarkGray;
                    ButtonSubtraction.BackColor = Color.DarkGray;
                    ButtonDivision.BackColor = Color.DarkGray;
                    ButtonMultiplication.BackColor = Color.DarkGray;
                    ButtonNegation.BackColor = Color.DarkGray;
                    ButtonAddition.BackColor = Color.DarkGray;
                    ButtonComma.BackColor = Color.DarkGray;
                    ButtonExponentiation.BackColor = Color.DarkGray;
                    #endregion coloring normal buttons
                    #region button font                   
                    ButtonCancel.Font = NormalFontButton;
                    ButtonCancel.Font = NormalFontButton;
                    ButtonClear.Font = NormalFontButton;
                    ButtonClear.Font = NormalFontButton;
                    Button0.Font = NormalFontButton;
                    Button1.Font = NormalFontButton;
                    Button2.Font = NormalFontButton;
                    Button3.Font = NormalFontButton;
                    Button4.Font = NormalFontButton;
                    Button5.Font = NormalFontButton;
                    Button6.Font = NormalFontButton;
                    Button7.Font = NormalFontButton;
                    Button8.Font = NormalFontButton;
                    Button9.Font = NormalFontButton;
                    ButtonEqual.Font = NormalFontButton;
                    ButtonSubtraction.Font = NormalFontButton;
                    ButtonDivision.Font = NormalFontButton;
                    ButtonMultiplication.Font = NormalFontButton;
                    ButtonNegation.Font = NormalFontButton;
                    ButtonAddition.Font = NormalFontButton;
                    ButtonComma.Font = NormalFontButton;
                    ButtonExponentiation.Font = NormalFontButton;
                    #endregion button font
                    break;
            }
        }
        #region easterEgg
        private void CatPic_Click(object sender, EventArgs e)
        {
            Display.Text = "MEOOOW";
        }
        private void JackRubberDuck_Click(object sender, EventArgs e)
        {
            Display.Text = "QUUUUACK";
        }
        #endregion

    }
}