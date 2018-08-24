using MetroFramework.Forms;
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
    public partial class Form5 : MetroForm
    {
        public Form5(Form3.CustomerTable cu)
        {
            InitializeComponent();
            Disp(cu);
        }

        public void Disp(Form3.CustomerTable d)
        {
            metroLabel12.Text = d.Name;
            metroLabel11.Text = d.Address;
            metroLabel1.Text = d.Email;
            metroLabel10.Text = d.Phone;
            metroLabel16.Text = d.Selected_car;
            metroLabel15.Text = d.Selected_model;
            metroLabel14.Text = d.Selected_colour;
            metroLabel13.Text = d.date;
        }
    }
}
