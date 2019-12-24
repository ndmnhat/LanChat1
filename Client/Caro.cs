using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Caro : Form
    {
        const int Caro_Width = 15;
        const int Caro_Height = 15;
        const int Square_Width = 30;
        const int Square_Height = 30;
        int index = 0;
        int pointx = 0;
        int pointo = 0;
        
        List<List<Button>> lBanCo;
        public Caro()
        {
            InitializeComponent();
            DrawChessBoard();
            ptcbxPlayerChess.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\x.png");

            pgsCooldown.Step = 100;
            pgsCooldown.Maximum = 10000;
            pgsCooldown.Value = 0;
            
        }
        void DrawChessBoard()
        {
            pnlBanco.Enabled = true;
            lBanCo = new List<List<Button>>();
            Button preButton = new Button()
            {
                Width = 0,
                Location = new Point(0, 0)
            };
            for(int i = 0; i < Caro_Height; i++)
            {
                lBanCo.Add(new List<Button>());
                for(int j = 0; j < Caro_Width; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Square_Width,
                        Height = Square_Height,
                        Location = new Point(preButton.Location.X + preButton.Width, preButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btn_Click;
                    pnlBanco.Controls.Add(btn);
                    lBanCo[i].Add(btn);
                    preButton = btn;
                }
                preButton.Location = new Point(0, preButton.Location.Y + Square_Height);
                preButton.Width = preButton.Height = 0;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return;
            if (index == 0)
            {
                tmCoolDown.Start();
                pgsCooldown.Value = 0;
                btn.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\x.png");
                ptcbxPlayerChess.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\o.png");
                btn.Tag += " x";
                //gan ten nguoi choi vao textbox
            }
            else
            {
                tmCoolDown.Start();
                pgsCooldown.Value = 0;
                btn.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\o.png");
                ptcbxPlayerChess.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\x.png");
                List<string> newdata = new List<string>();
                btn.Tag += " o";
                //gan ten nguoi choi vao textbox
            }
            index = index == 0 ? 1 : 0;

            if (isEnd(btn)==true)
            {
                EndGame();
            }

        }

        private void EndGame()
        {
            MessageBox.Show("You Win!");
            tmCoolDown.Stop();
            pgsCooldown.Value = 0;
        }

        private bool isEnd(Button btn)
        {
            return WinDoc(btn) || WinNgang(btn) || WinCheoChinh(btn) || WinCheoPhu(btn);
        }
       

        private Point GetChessPoint(Button btn)
        {
            string tag = (string)btn.Tag;
            int doc = Convert.ToInt32(tag.Split(' ')[0]);
            int ngang = lBanCo[doc].IndexOf(btn);
            Point point = new Point(ngang,doc);
            return point;
        }
        
        private bool WinNgang(Button btn)
        {
            int left = 0;
            Point point = GetChessPoint(btn);
            string xo1 = (string)btn.Tag;
            for (int i = point.X; i >= 0; i--)
            {
                string xo2 = (string)lBanCo[point.Y][i].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        left++;
                    }
                    else break;

                }
                catch (Exception)
                {
                    break;
                }
            }
            int right = 0;
            for (int i = point.X+1; i <Caro_Width; i++)
            {
                string xo2 = (string)lBanCo[point.Y][i].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        right++;
                    }
                    else break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            return left + right >= 5;
        }

        private bool WinDoc(Button btn)
        {
            Point point = GetChessPoint(btn);
            string xo1 = (string)btn.Tag;
            int top = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                string xo2 = (string)lBanCo[i][point.X].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        top++;
                    }
                    else break;
                }
                catch(Exception)
                {
                    break;
                }
            }
            int bottom = 0;
            for (int i = point.Y + 1; i < Caro_Height; i++)
            {
                string xo2 = (string)lBanCo[i][point.X].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        bottom++;
                    }
                    else break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            return top + bottom >= 5;
        }

        private bool WinCheoChinh(Button btn)
        {
            Point point = GetChessPoint(btn);
            string xo1 = (string)btn.Tag;
            int top = 0;
            for (int i = 0; i <=point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;
                string xo2 = (string)lBanCo[point.Y-i][point.X-i].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        top++;
                    }
                    else break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            int bottom = 0;
            for (int i = 1; i <= Caro_Width - point.X; i++)
            {
                if (point.Y + i >= Caro_Height || point.X + i >= Caro_Width)
                    break;
                string xo2 = (string)lBanCo[point.Y+i][point.X+i].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        top++;
                    }
                    else break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            return top + bottom >= 5;
        }

        private bool WinCheoPhu(Button btn)
        {
            Point point = GetChessPoint(btn);
            string xo1 = (string)btn.Tag;
            int top = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i >=Caro_Width || point.Y - i < 0)
                    break;
                string xo2 = (string)lBanCo[point.Y-i][point.X+i].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        top++;
                    }
                    else break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            int bottom = 0;
            for (int i = 1; i <= Caro_Width - point.X; i++)
            {
                if (point.Y + i >= Caro_Height || point.X - i <0)
                    break;
                string xo2 = (string)lBanCo[point.Y + i][point.X - i].Tag;
                try
                {
                    if (xo1.Split(' ')[1] == xo2.Split(' ')[1])
                    {
                        top++;
                    }
                    else break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            return top + bottom >= 5;
        }

        private void btnNewgame_Click(object sender, EventArgs e)
        {
            pnlBanco.Controls.Clear();
            //pnlBanco.Dispose();
            DrawChessBoard();
        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            pgsCooldown.PerformStep();
            DialogResult drl = new DialogResult();
            if (pgsCooldown.Value >= pgsCooldown.Maximum)
            {
                tmCoolDown.Stop();
                drl = MessageBox.Show("Time out !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (drl == DialogResult.OK)
                {
                    pnlBanco.Enabled = false;
                    pgsCooldown.Value = 0;
                }
            }
           
        }
    }
}
