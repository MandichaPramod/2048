using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        int[,] numbermatrix = new int[4,4];
        int score = 0;
        bool shiftsuccessful = false;
        bool numberCombined = false;
        bool endgame = false;
        public void HideAllButtons()
        {
            foreach (Control c in Controls)
            {
                c.BackColor = Color.FromArgb(255, 255, 255);
                c.Enabled=false;
            }
        }
        public string GetButtonAndNumberMapping(int num)
        {
            if (num == 0)
                return "Btn1";
            else if (num == 1)
                return "Btn2";
            else if (num == 2)
                return "Btn3";
            else if (num == 3)
                return "Btn4";
            else if (num == 4)
                return "Btn5";
            else if (num == 5)
                return "Btn6";
            else if (num == 6)
                return "Btn7";
            else if (num == 7)
                return "Btn8";
            else if (num == 8)
                return "Btn9";
            else if (num == 9)
                return "Btn10";
            else if (num == 10)
                return "Btn11";
            else if (num == 11)
                return "Btn12";
            else if (num == 12)
                return "Btn13";
            else if (num == 13)
                return "Btn14";
            else if (num == 14)
                return "Btn15";
            else
                return "Btn16";
        }
        public void ColorUptheButtons(int num,Control c)
        {
            if (num == 2)
                c.BackColor = Color.FromArgb(255,255,255);
            else if (num == 4)
                c.BackColor = Color.FromArgb(204,255, 204);
            else if (num == 8)
                c.BackColor = Color.FromArgb(153,255, 204);
            else if (num == 16)
                c.BackColor = Color.FromArgb(153,255, 153);
            else if (num == 32)
                c.BackColor = Color.FromArgb(102,255, 178);
            else if (num == 64)
                c.BackColor = Color.FromArgb(102,255, 102);
            else if (num == 128)
                c.BackColor = Color.FromArgb(51,255, 153);
            else if (num == 256)
                c.BackColor = Color.FromArgb(51,255, 51);
            else if (num == 512)
                c.BackColor = Color.FromArgb(0,255, 128);
            else if (num == 1024)
                c.BackColor = Color.FromArgb(0,255, 0);
            else if (num == 2048)
                c.BackColor = Color.FromArgb(0,204, 102);
            else if (num == 4096)
                c.BackColor = Color.FromArgb(0,204, 0);
            else if (num == 8192)
                c.BackColor = Color.FromArgb(0,155, 0);
        }
        public void FillUpButtons()
        {
            label2.Text = score.ToString();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int block = (i * 4) + j;
                    string btnblock = GetButtonAndNumberMapping(block);
               
                    foreach (Control c in Controls)
                    {
                        if (c.Name == btnblock)
                        {
                            if (numbermatrix[i, j] > 0)
                            {
                                c.Enabled = true;
                                c.Text = numbermatrix[i, j].ToString();
                                ColorUptheButtons(numbermatrix[i, j],c);
                            }
                            else
                            {
                                c.Text = "";
                                c.Enabled = false;
                                c.BackColor = default(Color);
                                c.ForeColor = default(Color);
                            }
                        }
                    }
                }
            }
        }
        public void InitializeAndStartGame()
        {
            for(int i=0;i<4;i++)
            {
                for (int j = 0; j < 4; j++)
                    numbermatrix[i, j] = 0;
            }

            Random rnd = new Random();
            int block1, block2;
            string btnblock1, btnblock2;
            block1 = rnd.Next(0, 16);
            block2 = rnd.Next(0, 16);
            while (block1 == block2)
                block2 = rnd.Next(0, 16);

            btnblock1 = GetButtonAndNumberMapping(block1);
            btnblock2 = GetButtonAndNumberMapping(block2);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int block = (i * 4) + j;
                    if(block== block1 || block == block2)
                    {
                        numbermatrix[i, j] = 2;

                        foreach (Control c in Controls)
                        {
                            if (c.Name == btnblock1 || c.Name == btnblock2)
                            {
                                c.Enabled=true;
                                c.Text = numbermatrix[i, j].ToString();
                                c.BackColor = Color.FromArgb(255, 255, 255);
                            }

                        }
                    }
                }
            }
        }
        public bool GenerateRandomNumber()
        {
            bool numberNotGenerated = true, completebreak = false;
            while (numberNotGenerated)
            {
                Random rnd = new Random();
                int block1 = rnd.Next(0, 16);
                string btnblock1 = GetButtonAndNumberMapping(block1);

                foreach (Control c in Controls)
                {
                    if (c.Name == btnblock1)
                    {
                        if (c.Enabled == true)
                            break;
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                int block = (i * 4) + j;
                                if (block == block1)
                                {
                                    numbermatrix[i, j] = 2;

                                    c.Enabled = true;
                                    c.Text = numbermatrix[i, j].ToString();
                                    c.BackColor = Color.FromArgb(255, 255, 255);

                                    numberNotGenerated=false;
                                    completebreak = true;
                                }
                            }
                            if (completebreak == true)
                                break;
                        }
                        if (completebreak == true)
                            break;
                    }
                }
            }
            return true;
        }
        public Form1()
        {
            InitializeComponent();
            HideAllButtons();
            InitializeAndStartGame();
            DoubleBuffered = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
        }
        public void ShiftUp()
        {
            //Combine Same Numbers
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (numbermatrix[j, i] != 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numbermatrix[k, i] != 0)
                            {
                                if (numbermatrix[j, i] == numbermatrix[k, i])
                                {
                                    numbermatrix[j, i] = numbermatrix[j, i] + numbermatrix[k, i];
                                    score = score + numbermatrix[j, i];
                                    numbermatrix[k, i] = 0;
                                    numberCombined = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            //Shift Numbers to Left
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (numbermatrix[j, i] == 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numbermatrix[k, i] != 0)
                            {
                                numbermatrix[j, i] = numbermatrix[k, i];
                                numbermatrix[k, i] = 0;
                                shiftsuccessful = true;
                                break;
                            }
                        }
                    }
                }
            }
            FillUpButtons();
        }
        public void ShiftDown()
        {
            //Combine Same Numbers
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j > 0; j--)
                {
                    if (numbermatrix[j, i] != 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (numbermatrix[k, i] != 0)
                            {
                                if (numbermatrix[j, i] == numbermatrix[k, i])
                                {
                                    numbermatrix[j, i] = numbermatrix[j, i] + numbermatrix[k, i];
                                    score = score + numbermatrix[j, i];
                                    numbermatrix[k, i] = 0;
                                    numberCombined = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            //Shift Numbers to Left
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j >0; j--)
                {
                    if (numbermatrix[j, i] == 0)
                    {
                        for (int k = j - 1; k >=0; k--)
                        {
                            if (numbermatrix[k, i] != 0)
                            {
                                numbermatrix[j, i] = numbermatrix[k, i];
                                numbermatrix[k, i] = 0;
                                shiftsuccessful = true;
                                break;
                            }
                        }
                    }
                }
            }
            FillUpButtons();
        }
        public void ShiftLeft()
        {
            //Combine Same Numbers
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0 ; j < 3; j++)
                {
                    if (numbermatrix[i, j] != 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numbermatrix[i, k] != 0)
                            {
                                if (numbermatrix[i, j] == numbermatrix[i, k])
                                {
                                    numbermatrix[i, j] = numbermatrix[i, j] + numbermatrix[i, k];
                                    score = score + numbermatrix[i, j];
                                    numbermatrix[i, k] = 0;
                                    numberCombined = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            //Shift Numbers to Left
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (numbermatrix[i, j] == 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numbermatrix[i, k] != 0)
                            {
                                numbermatrix[i, j] = numbermatrix[i, k];
                                numbermatrix[i, k] = 0;
                                shiftsuccessful = true;
                                break;
                            }
                        }
                    }
                }
            }
            FillUpButtons();
        }
        public void ShiftRight()
        {
            //Combine Same Numbers
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j > 0; j--)
                {
                    if (numbermatrix[i, j] != 0)
                    {
                        for (int k = j -1; k >= 0; k--)
                        {
                            if (numbermatrix[i, k] != 0)
                            {
                                if (numbermatrix[i, j] == numbermatrix[i, k])
                                {
                                    numbermatrix[i, j] = numbermatrix[i, j] + numbermatrix[i, k];
                                    score = score + numbermatrix[i, j];
                                    numbermatrix[i, k] = 0;
                                    numberCombined = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            //Shift Numbers to Left
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j > 0; j--)
                {
                    if (numbermatrix[i, j] == 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (numbermatrix[i, k] != 0)
                            {
                                numbermatrix[i, j] = numbermatrix[i, k];
                                numbermatrix[i, k] = 0;
                                shiftsuccessful = true;
                                break;
                            }
                        }
                    }
                }
            }
            FillUpButtons();
        }
        public bool isMatrixComplete()
        {
            bool matrixcomplete = true;
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    if (numbermatrix[i, j] == 0)
                        matrixcomplete = false;
                }
            }
            return matrixcomplete;
        }
        public void GameOver()
        {
            HideAllButtons();
            label3.Visible = true;
            label4.Text = label4.Text + score;
            label4.Visible = true;
            label5.Visible = true;
            endgame = true;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (endgame == true)
            {
                if (e.KeyValue == (Char)Keys.Enter)
                {
                    
                }
                else
                    return;
            } 
            shiftsuccessful = false;
            numberCombined = false;
            
            if (e.KeyValue == (Char)Keys.Up)
            {
                ShiftUp();

                if (shiftsuccessful == false && numberCombined == false && isMatrixComplete())
                    GameOver();
                else if (shiftsuccessful == true || numberCombined == true)
                    GenerateRandomNumber();
                label2.Focus();
            }
            else if (e.KeyValue == (Char)Keys.Down)
            {
                ShiftDown();
                if (shiftsuccessful == false && numberCombined == false && isMatrixComplete())
                    GameOver();
                else if (shiftsuccessful == true || numberCombined == true)
                    GenerateRandomNumber();
                label2.Focus();
            }
            else if (e.KeyValue == (Char)Keys.Right)
            {
                ShiftRight();
                if (shiftsuccessful == false && numberCombined == false && isMatrixComplete())
                    GameOver();
                else if (shiftsuccessful == true || numberCombined == true)
                    GenerateRandomNumber();
                label2.Focus();
            }
            else if (e.KeyValue == (Char)Keys.Left)
            {
                ShiftLeft();
                if (shiftsuccessful == false && numberCombined == false && isMatrixComplete())
                    GameOver();
                else if (shiftsuccessful == true || numberCombined == true)
                    GenerateRandomNumber();
                label2.Focus();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                       Color.Gray,
                                                                       Color.Black,
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
