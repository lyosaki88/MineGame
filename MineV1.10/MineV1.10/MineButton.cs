using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Mine
{
    class MineButton : Button
    {
        private int x;
        private int y;
        private int isMine;
        private int isOpen;
        private int tip;

        public int X {
            set {
                x = value;
            }
            get {
                return x;
            }
        }
        public int Y {
            set {
                y = value;
            }
            get {
                return y;
            }
        }
        public int IsMine {
            set {
                isMine = value;
            }
            get {
                return isMine;
            }
        }
        public int Tip {
            get {
                return tip;
            }
            set {
                tip = value;
            }
        }
        public int IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }        
        
        public MineButton() {
            X = 0;
            Y = 0;
            Tag = 0;
            IsMine = 0;
            IsOpen = 0;
            Size = new System.Drawing.Size(30, 30);
        }
    }
}
