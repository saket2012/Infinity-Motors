﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infiniti_Motors
{
    public partial class Form4 : MetroForm
    {
        public Form4(string p)
        {
            InitializeComponent();
            string path = p;
            pictureBox1.Image = Image.FromFile(path);
        }
    }
}
