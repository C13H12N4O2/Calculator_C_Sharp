using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        private static List<string> _elemList = new List<string>();
        private static string _currentValue = "";
        private static bool _dupliFlag = false;

        public Form1()
        {
            InitializeComponent();
            _initBtn();
        }

        private void _initBtn()
        {
            _btnOne.Click += _btn_Click;
            _btnTwo.Click += _btn_Click;
            _btnThree.Click += _btn_Click;
            _btnFour.Click += _btn_Click;
            _btnFive.Click += _btn_Click;
            _btnSix.Click += _btn_Click;
            _btnSeven.Click += _btn_Click;
            _btnEight.Click += _btn_Click;
            _btnNine.Click += _btn_Click;
            _btnPlus.Click += _btn_Click;
            _btnMinus.Click += _btn_Click;
            _btnMult.Click += _btn_Click;
            _btnDivision.Click += _btn_Click;
            _btnClear.Click += _btn_Click;
            _btnResult.Click += _btn_Click;
        }

        private bool _isOperator(string str)
        {
            if (str == "+" || str == "-" ||
                    str == "×" || str == "%")
                return true;
            return false;
        }

        private bool _isEmpty()
        {
            return _elemList.Count == 0 ? true : false;
        }

        private void _labelClear()
        {
            _currentValue = "";
            _labelResult.Text = "";
            _dupliFlag = false;
            _elemList.Clear();
        }

        private string _calculate()
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>();
            List<string> list = new List<string>();
            string curOperator, tmp = "", str = "";
            bool flag = false;
            int left, right;

            if (_currentValue != "")
                _elemList.Add(_currentValue);

            foreach (string item in _elemList)
            {
                if (_isOperator(item))
                    if (stack.Count == 0 && (item == "×" || item == "%"))
                    {
                        tmp = item;
                        flag = true;
                    }
                    else
                        stack.Push(item);
                else
                {
                    list.Add(item);

                    if (flag)
                    {
                        list.Add(tmp);
                        flag = false;
                    }
                }
                    
                str += item + " ";
            }

            foreach (string item in stack)
                list.Add(item);

            foreach (string item in list)
            {
                if (_isOperator(item))
                {
                    curOperator = item;
                    left = int.Parse(stack.Pop());
                    right = int.Parse(stack.Pop());

                    if (curOperator == "+")
                        stack.Push(Convert.ToString(right + left));
                    else if (curOperator == "-")
                        stack.Push(Convert.ToString(right - left));
                    else if (curOperator == "×")
                        stack.Push(Convert.ToString(right * left));
                    else
                        stack.Push(Convert.ToString(right / left));
                }
                else
                    stack.Push(item);
            }

            return str + "= " + stack.Pop();

            foreach (string item in _elemList)
            {
                if (_isOperator(item))
                {
                    if (item == "+" || item == "-")
                        queue.Enqueue(queue.Dequeue());
                    stack.Push(item);
                }
                else { 
                    queue.Enqueue(item);
                }

                str += item + ' ';
            }

            while (stack.Count > 0)
            {
                curOperator = stack.Pop();
                left = int.Parse(queue.Dequeue());
                right = int.Parse(queue.Dequeue());

                if (curOperator == "+")
                    queue.Enqueue(Convert.ToString(right + left));
                else if (curOperator == "-")
                    queue.Enqueue(Convert.ToString(right - left));
                else if (curOperator == "×")
                    queue.Enqueue(Convert.ToString(right * left));
                else
                    queue.Enqueue(Convert.ToString(right / left));
            }

            return str + "= " + queue.Dequeue();
        }

        private void _btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == "C")
            {
                _labelClear();
            }
            else if (btn.Text == "=" && !_isEmpty())
            {
                _listboxHistory.Items.Add(_calculate());
                _labelClear();
            }
            else if (!_isOperator(btn.Text))
            {
                _currentValue += btn.Text;
                _labelResult.Text += btn.Text;
                _dupliFlag = false;
            }
            else if (_isOperator(btn.Text) && !_dupliFlag)
            {
                _elemList.Add(_currentValue);
                _elemList.Add(btn.Text);
                _labelResult.Text += ' ' + btn.Text + ' ';
                _currentValue = "";
                _dupliFlag = true;
            }
        }
    }
}
