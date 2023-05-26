using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace kalkulator
{
    public partial class Form1 : Form
    {
        private bool isNewInput = true;
        private bool butVisible = false;
        private double currentValue = 0;
        private string currentOperator = "";
        private Button percentButton, bracket1Button, bracket2Button, factorialButton, lnButton, logButton, sqrtButton, pwrButton, sinButton, piButton, hisButton;
        string[] sincostan = { "sin", "cos", "tan", "EXP" };
        Button[] buttons = new Button[4];
        string[] history;
        public Form1()
        {
            InitializeComponent();
            CreateRegularButtons();
            history = new string[0];
        }

        private void CreateRegularButtons()
        {
            int buttonSize = 50;
            int buttonSpacing = 10;
            int startX = 150;
            int startY = 100;

            for (int i = 1; i <= 9; i++)
            {
                Button button = new Button();
                button.Text = i.ToString();
                button.Size = new Size(buttonSize, buttonSize);
                button.Location = new Point(startX, startY);
                button.Click += Button_Click;
                Controls.Add(button);

                startX += buttonSize + buttonSpacing;
                if (i % 3 == 0)
                {
                    startX = 150;
                    startY += buttonSize + buttonSpacing;
                }
            }

            //0
            Button buttonZero = new Button();
            buttonZero.Text = "0";
            buttonZero.Size = new Size(buttonSize, buttonSize);
            buttonZero.Location = new Point(startX, startY);
            buttonZero.Click += Button_Click;
            Controls.Add(buttonZero);

            //.
            Button buttonDot = new Button();
            buttonDot.Text = ".";
            buttonDot.Size = new Size(buttonSize, buttonSize);
            buttonDot.Location = new Point(startX + buttonSize + buttonSpacing, startY);
            buttonDot.Click += Button_Click;
            Controls.Add(buttonDot);

            string[] operators = { "+", "-", "*", "/" };
            startX = 150;
            startY = 100;

            for (int i = 0; i < operators.Length; i++)
            {
                Button operatorButton = new Button();
                operatorButton.Text = operators[i];
                operatorButton.Size = new Size(buttonSize, buttonSize);
                operatorButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 3, startY);
                operatorButton.Click += Button_Click;
                Controls.Add(operatorButton);

                startY += buttonSize + buttonSpacing;
            }

            //C
            Button clearButton = new Button();
            clearButton.Text = "C";
            clearButton.Size = new Size(buttonSize, buttonSize);
            clearButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 3, startY-300);
            clearButton.Click += btnClear_Click;
            Controls.Add(clearButton);

            //=
            Button equalsButton = new Button();
            equalsButton.Text = "=";
            equalsButton.Size = new Size(buttonSize, buttonSize);
            equalsButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 2, startY-60);
            equalsButton.Click += btnEquals_Click;
            Controls.Add(equalsButton);

            //NAUKOWY WYBOR
            Button SciButton = new Button();
            SciButton.Text = "NAUKOWY";
            SciButton.Size = new Size(buttonSize+30, buttonSize);
            SciButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 6, startY - 300);
            SciButton.Click += btnSci_Click;
            Controls.Add(SciButton);

            //)
            bracket1Button = new Button();
            bracket1Button.Text = ")";
            bracket1Button.Size = new Size(buttonSize, buttonSize);
            bracket1Button.Location = new Point(startX + (buttonSize + buttonSpacing) * 2 , startY - 300);
            bracket1Button.Click += Button_Click;
            Controls.Add(bracket1Button);
            bracket1Button.Hide();

            //(
            bracket2Button = new Button();
            bracket2Button.Text = "(";
            bracket2Button.Size = new Size(buttonSize, buttonSize);
            bracket2Button.Location = new Point(startX + (buttonSize + buttonSpacing), startY - 300);
            bracket2Button.Click += Button_Click;
            Controls.Add(bracket2Button);
            bracket2Button.Hide();

            //%
            percentButton = new Button();
            percentButton.Text = "%";
            percentButton.Size = new Size(buttonSize, buttonSize);
            percentButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 0, startY - 300);
            percentButton.Click += opButton_Click;
            Controls.Add(percentButton);
            percentButton.Hide();

            //x!
            factorialButton = new Button();
            factorialButton.Text = "x!";
            factorialButton.Size = new Size(buttonSize, buttonSize);
            factorialButton.Location = new Point(startX + (buttonSize + buttonSpacing) * -1, startY - 300);
            factorialButton.Click += opButton_Click;
            Controls.Add(factorialButton);
            factorialButton.Hide();

            //ln
            lnButton = new Button();
            lnButton.Text = "ln";
            lnButton.Size = new Size(buttonSize, buttonSize);
            lnButton.Location = new Point(startX + (buttonSize + buttonSpacing) * -1, startY - 240);
            lnButton.Click += opButton_Click;
            Controls.Add(lnButton);
            lnButton.Hide();

            //log
            logButton = new Button();
            logButton.Text = "log";
            logButton.Size = new Size(buttonSize, buttonSize);
            logButton.Location = new Point(startX + (buttonSize + buttonSpacing) * -1, startY - 180);
            logButton.Click += opButton_Click;
            Controls.Add(logButton);
            logButton.Hide();

            //sqrt
            sqrtButton = new Button();
            sqrtButton.Text = "√";
            sqrtButton.Size = new Size(buttonSize, buttonSize);
            sqrtButton.Location = new Point(startX + (buttonSize + buttonSpacing) * -1, startY - 120);
            sqrtButton.Click += opButton_Click;
            Controls.Add(sqrtButton);
            sqrtButton.Hide();

            //x^y
            pwrButton = new Button();
            pwrButton.Text = "^";
            pwrButton.Size = new Size(buttonSize, buttonSize);
            pwrButton.Location = new Point(startX + (buttonSize + buttonSpacing) * -1, startY - 60);
            pwrButton.Click += Button_Click;
            Controls.Add(pwrButton);
            pwrButton.Hide();

            //sin cos tan
            for(int i = 0; i<sincostan.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Text = sincostan[i];
                buttons[i].Size = new Size(buttonSize, buttonSize);
                buttons[i].Location = new Point(startX + (buttonSize + buttonSpacing) * -2, startY - (300 - 60 * (i + 1)));
                buttons[i].Click += opButton_Click;
                Controls.Add(buttons[i]);
                buttons[i].Hide();

            }

            //π
            piButton = new Button();
            piButton.Text = "π";
            piButton.Size = new Size(buttonSize, buttonSize);
            piButton.Location = new Point(startX + (buttonSize + buttonSpacing) * -2, startY - 300);
            piButton.Click += Button_Click;
            Controls.Add(piButton);
            piButton.Hide();

            //historia
            hisButton = new Button();
            hisButton.Text = "HISTORIA";
            hisButton.Size = new Size(buttonSize+30, buttonSize);
            hisButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 6, startY - 240);
            hisButton.Click += hisButton_Click;
            Controls.Add(hisButton);
        }

        private void hisButton_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2(history);
            Form2.Show();
        }
        private void opButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            string replacement = "";
            string pattern = @"\b\d+\b";

            MatchCollection matches = Regex.Matches(textBox1.Text, pattern);

            if (matches.Count > 0)
            {
                Match lastMatch = matches[matches.Count - 1];
                int number = Int32.Parse(lastMatch.Value);

                switch (buttonText)
                {
                    case "%":
                        if (isNewInput)
                        {
                            textBox1.Text = "ERROR, PLEASE CLEAR";
                        }
                        else
                        {
                            replacement = lastMatch.Value + "/100";
                        }
                        break;
                    case "ln":
                        replacement = "ln(" + lastMatch + ")";
                        break;
                    case "log":
                        replacement= "log10(" + lastMatch + ")";
                        break;
                    case "x!":
                        for (int i = number; i > 0; i--)
                        {
                            if (i == 1)
                            {
                                replacement += $"{i}";
                            }
                            else
                            {
                                replacement += $"{i}*";
                            }
                        }
                        break;
                    case "√":
                        replacement = "sqrt(" + lastMatch + ")";
                        break;
                    case "sin":
                        replacement = "sin(" + lastMatch + ")";
                        break;
                    case "cos":
                        replacement = "cos(" + lastMatch + ")";
                        break;
                    case "tan":
                        replacement = "tan(" + lastMatch + ")";
                        break;
                    case "EXP":
                        replacement = "exp(" + lastMatch + ")";
                        break;
                }
                textBox1.Text = textBox1.Text.Remove(lastMatch.Index, lastMatch.Length).Insert(lastMatch.Index, replacement);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnSci_Click(object sender, EventArgs e)
        {
            if (butVisible)
            {
                for(int i = 0; i < 4; i++)
                {
                    buttons[i].Visible = false;
                }
                piButton.Visible = false;
                pwrButton.Visible = false;
                logButton.Visible = false;
                sqrtButton.Visible = false;
                lnButton.Visible = false;
                bracket1Button.Visible = false;
                bracket2Button.Visible = false;
                percentButton.Visible = false;
                factorialButton.Visible = false;
                butVisible = false;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    buttons[i].Visible = true;
                }
                piButton.Visible = true;
                pwrButton.Visible = true;
                sqrtButton.Visible = true;
                logButton.Visible = true;
                butVisible = true; 
                lnButton.Visible = true;
                bracket1Button.Visible = true;
                bracket2Button.Visible = true;
                percentButton.Visible = true;
                factorialButton.Visible = true;
                butVisible = true;
            }                    
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;
            if(buttonText == "π")
            {
                buttonText = "3.14";
            }

            if (isNewInput)
            {
                textBox1.Text = buttonText;
                isNewInput = false;
            }
            else
            {
                textBox1.Text += buttonText;
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            org.mariuszgromada.math.mxparser.Expression MAT = new org.mariuszgromada.math.mxparser.Expression(textBox1.Text);
            string text = textBox1.Text + " = " + MAT.calculate().ToString();

            if (!string.IsNullOrEmpty(text))
            {
                Array.Resize(ref history, history.Length + 1);
                history[history.Length - 1] = text;
            }

            textBox1.Text = MAT.calculate().ToString();

            isNewInput = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            currentValue = 0;
            currentOperator = "";
            isNewInput = true;
        }
    }
}