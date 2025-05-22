using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator1
{
    public class Form1 : Form
    {
        private TextBox txtDisplay = null!;
        private double _firstNumber = 0;
        private string _operation = "";
        private bool _isOperationPerformed = false;

        public Form1()
        {
            InitializeCalculatorUI();
        }

        private void InitializeCalculatorUI()
        {
            // Form settings
            this.Text = "Simple Calculator";
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Display TextBox
            txtDisplay = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(340, 40),
                Font = new Font("Segoe UI", 18),
                ReadOnly = true,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(txtDisplay);

            // Button labels
            string[,] buttons =
            {
                { "7", "8", "9", "/" },
                { "4", "5", "6", "*" },
                { "1", "2", "3", "-" },
                { "0", "Clear", "=", "+" }
            };

            int startX = 20, startY = 80;
            int buttonWidth = 80, buttonHeight = 60;
            int padding = 10;

            // Create buttons dynamically
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string text = buttons[row, col];
                    Button btn = new Button
                    {
                        Text = text,
                        Size = new Size(buttonWidth, buttonHeight),
                        Location = new Point(startX + col * (buttonWidth + padding), startY + row * (buttonHeight + padding)),
                        Font = new Font("Segoe UI", 14)
                    };

                    if (text == "Clear")
                        btn.Click += ClearButton_Click;
                    else if (text == "=")
                        btn.Click += EqualsButton_Click;
                    else if ("+-*/".Contains(text))
                        btn.Click += OperatorButton_Click;
                    else
                        btn.Click += NumberButton_Click;

                    this.Controls.Add(btn);
                }
            }
        }

        // Number button click
        private void NumberButton_Click(object? sender, EventArgs e)
        {
            if (txtDisplay.Text == "0" || _isOperationPerformed)
            {
                txtDisplay.Clear();
                _isOperationPerformed = false;
            }

            Button button = sender as Button;
            if (button != null)
            {
                txtDisplay.Text += button.Text;
            }
        }

        // Operator button click
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                _operation = button.Text;
                if (double.TryParse(txtDisplay.Text, out _firstNumber))
                {
                    _isOperationPerformed = true;
                }
                else
                {
                    MessageBox.Show("Invalid input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDisplay.Text = "0";
                }
            }
        }

        // Equals button click
        private void EqualsButton_Click(object? sender, EventArgs e)
        {
            if (!double.TryParse(txtDisplay.Text, out double secondNumber))
            {
                MessageBox.Show("Invalid input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double result = 0;

            switch (_operation)
            {
                case "+": result = _firstNumber + secondNumber; break;
                case "-": result = _firstNumber - secondNumber; break;
                case "*": result = _firstNumber * secondNumber; break;
                case "/":
                    if (secondNumber != 0)
                        result = _firstNumber / secondNumber;
                    else
                    {
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("Select an operation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            txtDisplay.Text = result.ToString();
            _isOperationPerformed = true;
        }

        // Clear button click
        private void ClearButton_Click(object? sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            _firstNumber = 0;
            _operation = "";
            _isOperationPerformed = false;
        }
    }
}


        
        
    

