using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace KalkulatorKS
{
    public partial class Frontend : Form
    {
        #region AnalogClock

        bool isDigitalClockVisible = true; //true - digital clock visible, false - analog clock visible
        int WIDTH = 200, HEIGHT = 200, SEC_HAND = 94, MIN_HAND = 66, HR_HAND = 54;

        //center 
        int cx, cy;
        Bitmap bmp;
        Graphics g;

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
        #endregion AnalogClock
        private void Frontend_Load(object sender, EventArgs e)
        {
            timer1.Start(); //for clocks

            //for analog clock
            //create bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            //center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //backcolor
            AnalogClock.BackColor = Color.White;
            // tak się to robi dla form this.BackColor = Color.White;

            //coordinates for minute and second hand
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
            g = Graphics.FromImage(bmp);

            //get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            if (!isDigitalClockVisible) //digital clock
            {
                Clock.Text = DateTime.Now.ToString("hh:mm:ss");
                AnalogClock.Visible = false;
                Clock.Visible = true;
            }
            else //analog clock
            {
                AnalogClock.Visible = true;
                Clock.Visible = false;
                //clear
                g.Clear(Color.LightGray);

                //draw circle
                g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, WIDTH - 2, HEIGHT - 2);

                //draw figure
                //numbers
                //g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(90, 2));
                //g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(180, 84));
                //g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(90, 180));
                //g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 88));

                //lines
                g.DrawString("|", new Font("Arial", 12), Brushes.Black, new PointF(95, 0));
                g.DrawString("—", new Font("Arial", 12), Brushes.Black, new PointF(178, 90));
                g.DrawString("|", new Font("Arial", 12), Brushes.Black, new PointF(95, 178));
                g.DrawString("—", new Font("Arial", 12), Brushes.Black, new PointF(0, 90));

                //second hand
                handCoord = msCoord(ss, SEC_HAND);
                g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                //minut hand
                handCoord = msCoord(mm, MIN_HAND);
                g.DrawLine(new Pen(Color.Black, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                //hour hand
                handCoord = hrCoord(hh % 12, mm, HR_HAND);
                g.DrawLine(new Pen(Color.Black, 4f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                //load bmp in AnalogClock
                AnalogClock.Image = bmp;

                //dispose
                g.Dispose();
            }
        }
        //coordinates for hour hand
        #region DigitalClock
        private void Operator_Click(object sender, EventArgs e)
        //probably to be removed
        {

        }

        #endregion DigitalClock

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