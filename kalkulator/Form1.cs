using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace kalkulator
{
    public partial class Form1 : Form
    {
        private double currentValue = 0;
        private string currentOperator = "";
        private bool isNewInput = true;
        public Form1()
        {
            InitializeComponent();
            CreateRegularButtons();
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
        }
        private void ButtonDot_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains("."))
            {
                textBox1.Text += ".";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSci_Click(object sender, EventArgs e)
        {
            int buttonSize = 50;
            int buttonSpacing = 10;
            int startX = 150;
            int startY = 100;

            string[] brackets = { ")", "(" };
            int j = 2;
            for (int i = 0; i < brackets.Length; i++)
            {
                Button bracketButton = new Button();
                bracketButton.Text = brackets[i];
                bracketButton.Size = new Size(buttonSize, buttonSize);
                bracketButton.Location = new Point(startX + (buttonSize + buttonSpacing) * j, startY - 60);
                bracketButton.Click += Button_Click;
                Controls.Add(bracketButton);
                j--;
            }
            Button percentButton = new Button();
            percentButton.Text = "%";
            percentButton.Size = new Size(buttonSize, buttonSize);
            percentButton.Location = new Point(startX + (buttonSize + buttonSpacing) * 0, startY - 60);
            percentButton.Click += perButton_Click;
            Controls.Add(percentButton);
        }

        private void perButton_Click(object sender, EventArgs e)
        {
            Button percentButton = (Button)sender;
            string text = textBox1.Text;
            double num = double.Parse(text);
            num = num / 100;
            

            if (isNewInput)
            {
                textBox1.Text = "ERROR, PLEASE CLEAR";
            }
            else
            {
                textBox1.Text = num.ToString();
            }

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;


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
    

