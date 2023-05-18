using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        bool cleared = false;

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = (sender as System.Windows.Forms.Button).Text;
            bool autoClr = false;


            if (label2.Text.Length > 0)
            {
                autoClr = true;
            }

            if (autoClr == true && cleared == false)
            {
                label1.Text = "";
                cleared = true;
            }

            switch (btn)
            {
                case "=":
                    cleared= false;
                    string expression = $"{label1.Text}";

                    if (expression.Length > 0)
                    {
                        if (expression.EndsWith("+") || expression.EndsWith("-") || expression.EndsWith("/") || expression.EndsWith("*"))
                        {
                            expression = expression.TrimEnd('+', '-', '/', '*');
                        }

                        string[] operators = { "+", "-", "*", "/" };
                        List<double> operands = new List<double>();

                        foreach (string part in expression.Split(operators, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (double.TryParse(part, out double operand))
                            {
                                operands.Add(operand);
                            }
                            else
                            {
                                MessageBox.Show($"Invalid operand: {part}");
                                return;
                            }
                        }

                        string[] operatorsUsed = expression.Split(operands.Select(o => o.ToString()).ToArray(), StringSplitOptions.RemoveEmptyEntries);

                        double result = operands[0];
                        for (int i = 0; i < operatorsUsed.Length; i++)
                        {
                            switch (operatorsUsed[i])
                            {
                                case "+":
                                    result += operands[i + 1];
                                    break;
                                case "-":
                                    result -= operands[i + 1];
                                    break;
                                case "*":
                                    result *= operands[i + 1];
                                    break;
                                case "/":
                                    result /= operands[i + 1];
                                    break;
                            }
                        }

                        label1.Text = result.ToString();
                    }
                    break;
                case "CLR":
                    label1.Text = "";
                    break;
                case "DEL":
                    label1.Text = label1.Text.Remove(label1.Text.Length-1, 1);
                    break;
                case "Auto CLR":
                    if(label2.Text.Length > 0)
                    {
                        label2.Text = "";
                    }
                    else
                    {
                        label2.Text = "AUTO CLR Enabled";
                    }
                    break;
                default:
                    if (btn == "+" || btn == "-" || btn == "*" || btn == "/")
                    {
                        if (label1.Text.EndsWith("+") || label1.Text.EndsWith("-") || label1.Text.EndsWith("/") || label1.Text.EndsWith("*"))
                        {
                            label1.Text = label1.Text.TrimEnd('+', '-', '/', '*');
                            label1.Text += btn;
                        }
                        else
                        {
                            label1.Text += btn;
                        }
                    } else
                    {
                        label1.Text += btn;
                    }
                    break;
            }

        }
    }
}
