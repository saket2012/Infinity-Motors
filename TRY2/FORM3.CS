using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MetroFramework.Forms;
using Infiniti_Motors;

namespace Infiniti_Motors
{
    public partial class Form3 : MetroForm
    {
        private static Form1.CarTable C1;
        private static CustomerTable cu;
        private static String connectionString = "mongodb://localhost";
        private static MongoClient client = new MongoClient(connectionString);
        private static MongoServer server = client.GetServer();
        private static MongoDatabase database = server.GetDatabase("Car-Showroom");
        private static MongoCollection collection = database.GetCollection<Form1.CarTable>("CarTable");

        public Form3(string u)
        {
            InitializeComponent();
            MetroTabControl1.SelectedIndex = 0;
            metroLabel1.Text = u;
        }

        void Clear()
        {
            C1 = null;
            cu = null;
            TextBox7.Text = string.Empty;
            ComboBox1.SelectedItem = null;
            ComboBox2.SelectedItem = null;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox8.Text = string.Empty;
            richTextBox1.Text = string.Empty;
        }

        private void Display_Click(object sender, EventArgs e)
        {
            List<Form1.CarTable> list = collection.FindAllAs<Form1.CarTable>().ToList();
            metroGrid.DataSource = list;
            metroGrid.Columns[0].Visible = false;
            metroGrid.Columns[9].Visible = false;
            Clear();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (TextBox7.Text != string.Empty && ComboBox2.SelectedItem != null)
            {
                string txt = ComboBox2.SelectedItem.ToString();
                string car = TextBox7.Text;
                switch (txt)
                {
                    case "Car name":
                        var query = Query.EQ("Car_name", car);
                        var res = collection.FindAs<Form1.CarTable>(query).Count();
                        if (res == 0)
                        {
                            MessageBox.Show("Car not found");
                            goto Ext;
                        }
                        else
                        {
                            List<Form1.CarTable> list = collection.FindAs<Form1.CarTable>(query).ToList();
                            metroGrid1.DataSource = list;
                        }
                        break;
                    case "Company":
                        var query1 = Query.EQ("Car_company", car);
                        List<Form1.CarTable> list1 = collection.FindAs<Form1.CarTable>(query1).ToList();
                        metroGrid1.DataSource = list1;
                        break;
                    case "Model No.":
                        var query2 = Query.EQ("Model", car);
                        List<Form1.CarTable> list2 = collection.FindAs<Form1.CarTable>(query2).ToList();
                        metroGrid1.DataSource = list2;
                        break;
                    case "Price":
                        var query3 = Query.EQ("Price", car);
                        List<Form1.CarTable> list3 = collection.FindAs<Form1.CarTable>(query3).ToList();
                        metroGrid1.DataSource = list3;
                        break;
                    case "Colour":
                        var query4 = Query.EQ("Colour", car);
                        List<Form1.CarTable> list4 = collection.FindAs<Form1.CarTable>(query4).ToList();
                        metroGrid1.DataSource = list4;
                        break;
                    case "Mileage":
                        var query5 = Query.EQ("Mileage", car);
                        List<Form1.CarTable> list5 = collection.FindAs<Form1.CarTable>(query5).ToList();
                        metroGrid1.DataSource = list5;
                        break;
                    case "Class":
                        var query6 = Query.EQ("Car_class", car);
                        List<Form1.CarTable> list6 = collection.FindAs<Form1.CarTable>(query6).ToList();
                        metroGrid1.DataSource = list6;
                        break;
                    case "Fuel":
                        var query7 = Query.EQ("Fuel_type", car);
                        List<Form1.CarTable> list7 = collection.FindAs<Form1.CarTable>(query7).ToList();
                        metroGrid1.DataSource = list7;
                        break;
                }
                metroGrid1.Columns[0].Visible = false;
                metroGrid1.Columns[9].Visible = false;
            }
            else
                MessageBox.Show("Fields cannot be blank");
            Ext:
            Clear();
        }

        private void Sort_Click(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem != null)
            {
                string txt = ComboBox1.SelectedItem.ToString();
                if (RadioButton1.Checked)
                {
                    switch (txt)
                    {
                        case "Car name":
                            var query = SortBy.Ascending("Car_name");
                            List<Form1.CarTable> list = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query).ToList();
                            metroGrid2.DataSource = list;
                            break;
                        case "Company":
                            var query1 = SortBy.Ascending("Car_company");
                            List<Form1.CarTable> list1 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query1).ToList();
                            metroGrid2.DataSource = list1;
                            break;
                        case "Model No.":
                            var query2 = SortBy.Ascending("Model");
                            List<Form1.CarTable> list2 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query2).ToList();
                            metroGrid2.DataSource = list2;
                            break;
                        case "Price":
                            var query3 = SortBy.Ascending("Price");
                            List<Form1.CarTable> list3 = collection.FindAllAs<Form1.CarTable>().OrderBy(c => double.Parse(c.Price)).ToList();
                            metroGrid2.DataSource = list3;
                            break;
                        case "Colour":
                            var query4 = SortBy.Ascending("Colour");
                            List<Form1.CarTable> list4 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query4).ToList();
                            metroGrid2.DataSource = list4;
                            break;
                        case "Mileage":
                            var query5 = SortBy.Ascending("Mileage");
                            List<Form1.CarTable> list5 = collection.FindAllAs<Form1.CarTable>().OrderBy(c => double.Parse(c.Mileage)).ToList();
                            metroGrid2.DataSource = list5;
                            break;
                        case "Class":
                            var query6 = SortBy.Ascending("Car_class");
                            List<Form1.CarTable> list6 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query6).ToList();
                            metroGrid2.DataSource = list6;
                            break;
                        case "Fuel":
                            var query7 = SortBy.Ascending("Fuel_type");
                            List<Form1.CarTable> list7 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query7).ToList();
                            metroGrid2.DataSource = list7;
                            break;
                    }
                }
                else if (RadioButton2.Checked)
                {
                    switch (txt)
                    {
                        case "Car name":
                            var query = SortBy.Descending("Car_name");
                            List<Form1.CarTable> list = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query).ToList();
                            metroGrid2.DataSource = list;
                            break;
                        case "Company":
                            var query1 = SortBy.Descending("Car_company");
                            List<Form1.CarTable> list1 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query1).ToList();
                            metroGrid2.DataSource = list1;
                            break;
                        case "Model No.":
                            var query2 = SortBy.Descending("Model");
                            List<Form1.CarTable> list2 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query2).ToList();
                            metroGrid2.DataSource = list2;
                            break;
                        case "Price":
                            var query3 = SortBy.Descending("Price");
                            List<Form1.CarTable> list3 = collection.FindAllAs<Form1.CarTable>().OrderBy(c => double.Parse(c.Price)).Reverse().ToList();
                            metroGrid2.DataSource = list3;
                            break;
                        case "Colour":
                            var query4 = SortBy.Descending("Colour");
                            List<Form1.CarTable> list4 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query4).ToList();
                            metroGrid2.DataSource = list4;
                            break;
                        case "Mileage":
                            var query5 = SortBy.Descending("Mileage");
                            List<Form1.CarTable> list5 = collection.FindAllAs<Form1.CarTable>().OrderBy(c => double.Parse(c.Mileage)).Reverse().ToList();
                            metroGrid2.DataSource = list5;
                            break;
                        case "Class":
                            var query6 = SortBy.Descending("Car_class");
                            List<Form1.CarTable> list6 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query6).ToList();
                            metroGrid2.DataSource = list6;
                            break;
                        case "Fuel":
                            var query7 = SortBy.Descending("Fuel_type");
                            List<Form1.CarTable> list7 = collection.FindAllAs<Form1.CarTable>().SetSortOrder(query7).ToList();
                            metroGrid2.DataSource = list7;
                            break;
                    }
                }
                else
                    MessageBox.Show("Fields cannot be blank");
                metroGrid2.Columns[0].Visible = false;
                metroGrid2.Columns[9].Visible = false;
            }
            else
                MessageBox.Show("Fields cannot be blank");
            Clear();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void metroGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<Form1.CarTable> list = metroGrid.DataSource as List<Form1.CarTable>;
            C1 = list[e.RowIndex];
            Form4 f = new Form4(C1.Image_path);
            f.Text = C1.Car_name;
            f.Show();
        }

        private void metroGrid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<Form1.CarTable> list = metroGrid1.DataSource as List<Form1.CarTable>;
            C1 = list[e.RowIndex];
            Form4 f = new Form4(C1.Image_path);
            f.Text = C1.Car_name;
            f.Show();
        }

        private void metroGrid2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<Form1.CarTable> list = metroGrid2.DataSource as List<Form1.CarTable>;
            C1 = list[e.RowIndex];
            Form4 f = new Form4(C1.Image_path);
            f.Text = C1.Car_name;
            f.Show();
        }

        public class CustomerTable
        {
            public BsonObjectId _id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Selected_car { get; set; }
            public string Selected_model { get; set; }
            public string Selected_colour { get; set; }
            public string date { get; set; }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            String connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("Customer-Details");
            MongoCollection collection = database.GetCollection<CustomerTable>("CustomerTable");

            List<CustomerTable> list = collection.FindAllAs<CustomerTable>().ToList();
            try
            {
                if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox8.Text != string.Empty &&
                    textBox4.Text != string.Empty && textBox5.Text != string.Empty &&
                    textBox6.Text != string.Empty && textBox3.Text != string.Empty && richTextBox1.Text != string.Empty)
                {
                    cu = new CustomerTable()
                    {
                        Name = textBox1.Text,
                        Address = richTextBox1.Text,
                        Email = textBox2.Text,
                        Phone = textBox3.Text,
                        Selected_car = textBox4.Text,
                        Selected_model = textBox5.Text,
                        Selected_colour = textBox6.Text,
                        date = textBox8.Text,
                    };
                    collection.Insert(cu);
                    MessageBox.Show("Customer details added successfully!");
                }
                else
                {
                    MessageBox.Show("Fields cannot be blank");
                }

            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex + "\nPlease try again"); }
            Clear();
        }

        private void View_Click(object sender, EventArgs e)
        {
            String connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("Customer-Details");
            MongoCollection collection = database.GetCollection<CustomerTable>("CustomerTable");

            List<CustomerTable> list = collection.FindAllAs<CustomerTable>().ToList();
            metroGrid3.DataSource = list;
            metroGrid3.Columns[0].Visible = false;
            metroGrid3.Columns[2].Visible = false;
            metroGrid3.Columns[3].Visible = false;
            metroGrid3.Columns[4].Visible = false;
            Clear();
        }

        private void metroGrid3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<CustomerTable> list = metroGrid3.DataSource as List<CustomerTable>;
            cu = list[e.RowIndex];
            Form5 f = new Form5(cu);
            f.Show();
        }
    }
}
