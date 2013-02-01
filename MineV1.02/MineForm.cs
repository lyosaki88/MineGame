

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Media;

namespace Mine
{
    public partial class MineForm : Form
    {
        public MineForm()
        {
            InitializeComponent();
            InitialGame(10,10,10);
        }
        //private string init = System.Environment.CurrentDirectory + "\\image\\face.bmp";

        private int totalMine;
        private int restMine;
        private int hang;
        private int lie;
        private int time;
        private int openNum;

        public int TotalMine
        {
            get { return totalMine; }
            set { totalMine = value; }
        }
        public int RestMine {
            get { return restMine; }
            set { restMine = value; }
        }
        public int Hang
        {
            get { return hang; }
            set { hang = value; }
        }
        public int Lie
        {
            get { return lie; }
            set { lie = value; }
        }
        public int Time
        {
            get { return time; }
            set { time = value; }
        }               
        public int OpenNum
        {
            get { return openNum; }
            set { openNum = value; }
        }

        private MineButton[,] button;
        Timer t1;

        private void InitialGame(int total, int row, int column) {
            TotalMine = total;
            RestMine = total;
            Hang = row;
            Lie = column;
            Time = 0;
            OpenNum = 0;
            button = new MineButton[Hang, Lie];
            t1 = new Timer();
            t1.Tick += new EventHandler(t1_Tick);
            t1.Interval = 1000;
            lbl_2.Text = RestMine.ToString();
            lbl_4.Text = Time.ToString();
            InitMine();
        }
        private void InitMine() {
            for (int i = 0; i < Hang; i++){
                for (int j = 0; j < Lie; j++) {
                    button[i, j] = new MineButton();
                    button[i, j].Location = new Point(5 + i * 30, 10 + j * 30);
                    button[i, j].X = i;
                    button[i, j].Y = j;
                    button[i, j].IsOpen = 0;
                    button[i, j].IsMine = 0;
                    groupBox1.Controls.Add(button[i, j]);
                    button[i, j].MouseDown += new MouseEventHandler(bt_MouseDown);
                }
            }
            Random rd = new Random();
            for (int i = 0; i < TotalMine; i++) {
                int p1 = rd.Next(0, Hang);
                int p2 = rd.Next(0, Lie);
                if (button[p1, p2].IsMine == 1) {
                    i--;
                    continue;
                }
                button[p1, p2].IsMine = 1;
            }
            for (int i = 0; i < Hang; i++) {
                for (int j = 0; j < Lie; j++) {
                    if (button[i, j].IsMine == 1) {
                        if (i - 1 >= 0 && j - 1 >= 0) {
                            button[i - 1, j - 1].Tip++;
                        }
                        if (i - 1 >= 0) {
                            button[i - 1, j].Tip++;
                        }
                        if (i - 1 >= 0 && j + 1 < Lie) {
                            button[i - 1, j + 1].Tip++;
                        }
                        if (j - 1 >= 0) {
                            button[i, j - 1].Tip++;
                        }
                        if (j + 1 < Lie) {
                            button[i, j + 1].Tip++;
                        }
                        if (i + 1 < Hang && j - 1 >= 0) {
                            button[i + 1, j - 1].Tip++;
                        }
                        if (i + 1 < Hang) {
                            button[i + 1, j].Tip++;
                        }
                        if (i + 1 < Hang && j + 1 < Lie) {
                            button[i + 1, j + 1].Tip++;
                        }
                    }
                }
            }
        }
        private void t1_Tick(object sender, EventArgs e)
        {
            if (OpenNum == Hang * Lie - TotalMine)
            {
                t1.Stop();
                MessageBox.Show("Congratulations!You Win!");
                groupBox1.Enabled = false;
            }
            else
            {
                Time++;
                lbl_4.Text = Time.ToString();
            }
        }


        private void bt_MouseDown(object sender, MouseEventArgs e) {
            MineButton b = (MineButton)sender;
            int i = b.X;
            int j = b.Y;
            if (t1.Enabled == false) {
                t1.Start();
            }
            if (e.Button == MouseButtons.Right)
            {
                if (button[i, j].Text == "!"){
                    button[i, j].Text = "";
                    RestMine++;
                    lbl_2.Text = RestMine.ToString();
                }
                else {
                    if (RestMine > 0) {
                        button[i, j].Text = "!";
                        RestMine--;
                        lbl_2.Text = RestMine.ToString();
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (button[i, j].Text != "!") {
                    if (b.IsMine == 1) {
                        b.Text = "*";
                        b.IsOpen = 1;
                        b.Enabled = false;
                        MessageBox.Show("Bomb!");
                        groupBox1.Enabled = false;
                        t1.Stop();
                    }
                    else {
                        OpenMine(b);
                    }
                }
            }
        }
        private void OpenMine(MineButton b) {
            int i = b.X;
            int j = b.Y;
            b.IsOpen = 1;
            b.Enabled = false;
            OpenNum++;
            if (b.Tip == 0)
            {
                b.Text = "";
                if (j - 1 >= 0) {
                    if (button[i, j - 1].IsOpen == 0) {
                        OpenMine(button[i, j - 1]);
                    }
                }
                if (i + 1 < Hang) {
                    if (button[i + 1, j].IsOpen == 0) {
                        OpenMine(button[i + 1, j]);
                    }
                }
                if (j + 1 < Lie) {
                    if (button[i, j + 1].IsOpen == 0) {
                        OpenMine(button[i, j + 1]);
                    }
                }
                if (i - 1 >= 0) {
                    if (button[i - 1, j].IsOpen == 0) {
                        OpenMine(button[i - 1, j]);
                    }
                }
            }
            else
            {
                b.Text = b.Tip.ToString();
            }
        }
        private void FreeRes() {
            lbl_1.Dispose();
            lbl_2.Dispose();
            lbl_3.Dispose();
            lbl_4.Dispose();
            Btn_Start.Dispose();
            groupBox1.Dispose();
            t1.Dispose();
            toolStrip1.Dispose();
        }
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            FreeRes();
            InitializeComponent();
            InitialGame(10,10,10);
        }

        //Start
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FreeRes();
            InitializeComponent();
            InitialGame(10, 10, 10);
        }

        //Set
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        //Hero Log
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        //Exit
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FreeRes();
            this.Close();
        }

        //About
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}