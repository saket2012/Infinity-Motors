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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace Infiniti_Motors
{
    public partial class Form1 : MetroForm
    {
        public static CarTable C1;
        public static String connectionString = "mongodb://localhost";
        public static MongoClient client = new MongoClient(connectionString);
        public static MongoServer server = client.GetServer();
        public static MongoDatabase database = server.GetDatabase("Car-Showroom");
        public static MongoCollection collection = database.GetCollection<CarTable>("CarTable");

        public Form1(string u)
        {
            InitializeComponent();
            MetroTabControl1.SelectedIndex = 0;
            metroLabel1.Text = u;
        }

        public void display()
        {
            List<CarTable> list = collection.FindAllAs<CarTable>().ToList();
            metroGrid.DataSource = list;
            metroGrid.Columns[0].Visible = false;
            metroGrid.Columns[9].Visible = false;
            Clear();
        }

        public void insert()
        {
            List<CarTable> list = collection.FindAllAs<CarTable>().ToList();
            try
            {
                if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty && TextBox8.Text != string.Empty &&
                    TextBox9.Text != string.Empty && TextBox10.Text != string.Empty && TextBox11.Text != string.Empty &&
                    TextBox18.Text != string.Empty && ComboBox3.Text != string.Empty && ComboBox4.Text != string.Empty)
                {
                    C1 = new CarTable()
                    {
                        Car_company = TextBox1.Text,
                        Car_name = TextBox2.Text,
                        Model = TextBox8.Text,
                        Fuel_type = ComboBox3.Text,
                        Price = TextBox9.Text,
                        Colour = TextBox10.Text,
                        Mileage = TextBox11.Text,
                        Car_class = ComboBox4.Text,
                        Image_path = TextBox18.Text,
                    };
                    collection.Insert(C1);
                    MessageBox.Show("Car added successfully!");
                }
                else
                {
                    MessageBox.Show("Fields cannot be blank");
                }
                
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex + "\nPlease try again"); }
            Clear();
        }

        public void delete()
        {
            if (TextBox17.Text != string.Empty)
            {
                C1.Car_name = TextBox17.Text;
                var query = Query.EQ("Car_name", C1.Car_name);
                collection.Remove(query);
                metroGrid4.DataSource = null;
                metroGrid4.Rows.Clear();
                metroGrid4.ColumnHeadersVisible = false;
                metroGrid4.Refresh();
                MessageBox.Show("Car removed successfully!");
            }
            else
                MessageBox.Show("Fields cannot be blank");
            Clear();
        }

        public void search()
        {
            if (TextBox7.Text != string.Empty && ComboBox2.SelectedItem != null)
            {
                string txt = ComboBox2.SelectedItem.ToString();
                string car = TextBox7.Text;
                switch (txt)
                {
                    case "Car name":
                        var query = Query.EQ("Car_name", car);
                        List<CarTable> list = collection.FindAs<CarTable>(query).ToList();
                        metroGrid1.DataSource = list;
                        break;
                    case "Company":
                        var query1 = Query.EQ("Car_company", car);
                        List<CarTable> list1 = collection.FindAs<CarTable>(query1).ToList();
                        metroGrid1.DataSource = list1;
                        break;
                    case "Model No.":
                        var query2 = Query.EQ("Model", car);
                        List<CarTable> list2 = collection.FindAs<CarTable>(query2).ToList();
                        metroGrid1.DataSource = list2;
                        break;
                    case "Price":
                        var query3 = Query.EQ("Price", car);
                        List<CarTable> list3 = collection.FindAs<CarTable>(query3).ToList();
                        metroGrid1.DataSource = list3;
                        break;
                    case "Colour":
                        var query4 = Query.EQ("Colour", car);
                        List<CarTable> list4 = collection.FindAs<CarTable>(query4).ToList();
                        metroGrid1.DataSource = list4;
                        break;
                    case "Mileage":
                        var query5 = Query.EQ("Mileage", car);
                        List<CarTable> list5 = collection.FindAs<CarTable>(query5).ToList();
                        metroGrid1.DataSource = list5;
                        break;
                    case "Class":
                        var query6 = Query.EQ("Car_class", car);
                        List<CarTable> list6 = collection.FindAs<CarTable>(query6).ToList();
                        metroGrid1.DataSource = list6;
                        break;
                    case "Fuel":
                        var query7 = Query.EQ("Fuel_type", car);
                        List<CarTable> list7 = collection.FindAs<CarTable>(query7).ToList();
                        metroGrid1.DataSource = list7;
                        break;
                }
                metroGrid1.Columns[0].Visible = false;
                metroGrid1.Columns[9].Visible = false;
            }
            else
                MessageBox.Show("Fields cannot be blank");
            Clear();
        }

        public void sort()
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
                            List<CarTable> list = collection.FindAllAs<CarTable>().SetSortOrder(query).ToList();
                            metroGrid2.DataSource = list;
                            break;
                        case "Company":
                            var query1 = SortBy.Ascending("Car_company");
                            List<CarTable> list1 = collection.FindAllAs<CarTable>().SetSortOrder(query1).ToList();
                            metroGrid2.DataSource = list1;
                            break;
                        case "Model No.":
                            var query2 = SortBy.Ascending("Model");
                            List<CarTable> list2 = collection.FindAllAs<CarTable>().SetSortOrder(query2).ToList();
                            metroGrid2.DataSource = list2;
                            break;
                        case "Price":
                            var query3 = SortBy.Ascending("Price");
                            List<CarTable> list3 = collection.FindAllAs<CarTable>().OrderBy(c => double.Parse(c.Price)).ToList();
                            metroGrid2.DataSource = list3;
                            break;
                        case "Colour":
                            var query4 = SortBy.Ascending("Colour");
                            List<CarTable> list4 = collection.FindAllAs<CarTable>().SetSortOrder(query4).ToList();
                            metroGrid2.DataSource = list4;
                            break;
                        case "Mileage":
                            var query5 = SortBy.Ascending("Mileage");
                            List<CarTable> list5 = collection.FindAllAs<CarTable>().OrderBy(c => double.Parse(c.Mileage)).ToList();
                            metroGrid2.DataSource = list5;
                            break;
                        case "Class":
                            var query6 = SortBy.Ascending("Car_class");
                            List<CarTable> list6 = collection.FindAllAs<CarTable>().SetSortOrder(query6).ToList();
                            metroGrid2.DataSource = list6;
                            break;
                        case "Fuel":
                            var query7 = SortBy.Ascending("Fuel_type");
                            List<CarTable> list7 = collection.FindAllAs<CarTable>().SetSortOrder(query7).ToList();
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
                            List<CarTable> list = collection.FindAllAs<CarTable>().SetSortOrder(query).ToList();
                            metroGrid2.DataSource = list;
                            break;
                        case "Company":
                            var query1 = SortBy.Descending("Car_company");
                            List<CarTable> list1 = collection.FindAllAs<CarTable>().SetSortOrder(query1).ToList();
                            metroGrid2.DataSource = list1;
                            break;
                        case "Model No.":
                            var query2 = SortBy.Descending("Model");
                            List<CarTable> list2 = collection.FindAllAs<CarTable>().SetSortOrder(query2).ToList();
                            metroGrid2.DataSource = list2;
                            break;
                        case "Price":
                            var query3 = SortBy.Descending("Price");
                            List<CarTable> list3 = collection.FindAllAs<CarTable>().OrderBy(c => double.Parse(c.Price)).Reverse().ToList();
                            metroGrid2.DataSource = list3;
                            break;
                        case "Colour":
                            var query4 = SortBy.Descending("Colour");
                            List<CarTable> list4 = collection.FindAllAs<CarTable>().SetSortOrder(query4).ToList();
                            metroGrid2.DataSource = list4;
                            break;
                        case "Mileage":
                            var query5 = SortBy.Descending("Mileage");
                            List<CarTable> list5 = collection.FindAllAs<CarTable>().OrderBy(c => double.Parse(c.Mileage)).Reverse().ToList();
                            metroGrid2.DataSource = list5;
                            break;
                        case "Class":
                            var query6 = SortBy.Descending("Car_class");
                            List<CarTable> list6 = collection.FindAllAs<CarTable>().SetSortOrder(query6).ToList();
                            metroGrid2.DataSource = list6;
                            break;
                        case "Fuel":
                            var query7 = SortBy.Descending("Fuel_type");
                            List<CarTable> list7 = collection.FindAllAs<CarTable>().SetSortOrder(query7).ToList();
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
            {
                MessageBox.Show("Fields cannot be blank");
            }
            Clear();
        }

        public void update()
        {
            if (C1 != null)
            {
                C1.Car_company = TextBox3.Text;
                C1.Car_name = TextBox4.Text;
                C1.Model = TextBox12.Text;
                C1.Price = TextBox13.Text;
                C1.Colour = TextBox14.Text;
                C1.Mileage = TextBox15.Text;
                C1.Image_path = TextBox19.Text;
                C1.Fuel_type = ComboBox5.SelectedItem.ToString();
                C1.Car_class = ComboBox6.SelectedItem.ToString();
                IMongoQuery query = Query.EQ("_id", C1._id);
                IMongoUpdate update = MongoDB.Driver.Builders.Update
                    .Set("Car_company", C1.Car_company)
                    .Set("Car_name", C1.Car_name)
                    .Set("Model", C1.Model)
                    .Set("Price", C1.Price)
                    .Set("Colour", C1.Colour)
                    .Set("Mileage", C1.Mileage)
                    .Set("Image_path", C1.Image_path)
                    .Set("Fuel_type", C1.Fuel_type)
                    .Set("Car_class", C1.Car_class);

                collection.Update(query, update);
                metroGrid3.DataSource = null;
                metroGrid3.Rows.Clear();
                metroGrid3.ColumnHeadersVisible = false;
                metroGrid3.Refresh();
                MessageBox.Show("Details updated successfully!");
            }
            else
                MessageBox.Show("The car doesn't exist");
            Clear();
        }

        public void Clear()
        {
            C1 = null;
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox9.Text = string.Empty;
            TextBox10.Text = string.Empty;
            TextBox11.Text = string.Empty;
            TextBox12.Text = string.Empty;
            TextBox13.Text = string.Empty;
            TextBox14.Text = string.Empty;
            TextBox15.Text = string.Empty;
            TextBox16.Text = string.Empty;
            TextBox17.Text = string.Empty;
            TextBox18.Text = string.Empty;
            TextBox19.Text = string.Empty;
            ComboBox1.SelectedItem = null;
            ComboBox2.SelectedItem = null;
            ComboBox3.SelectedItem = null;
            ComboBox4.SelectedItem = null;
            ComboBox5.SelectedItem = null;
            ComboBox6.SelectedItem = null;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
        }

        public class CarTable
        {
            public BsonObjectId _id { get; set; }
            public string Car_company { get; set; }
            public string Car_name { get; set; }
            public string Model { get; set; }
            public string Price { get; set; }
            public string Colour { get; set; }
            public string Mileage { get; set; }
            public string Fuel_type { get; set; }
            public string Car_class { get; set; }
            public string Image_path { get; set; }

        }

        private void Display_Click(object sender, EventArgs e)
        {
            display();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            search();
        }

        private void Sort_Click(object sender, EventArgs e)
        {
            sort();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            update();
        }

        private void Search1_Click(object sender, EventArgs e)
        {
            if (TextBox16.Text != string.Empty)
            {
                string car = TextBox16.Text;
                var query = Query.EQ("Car_name", car);
                var res = collection.FindAs<CarTable>(query).Count();
                if(res == 0)
                {
                    MessageBox.Show("Car not found");
                    goto Ext;
                }
                else
                {                 
                    List<CarTable> list = collection.FindAs<CarTable>(query).ToList();
                    metroGrid3.DataSource = list;
                    metroGrid3.Columns[0].Visible = false;
                    metroGrid3.Columns[9].Visible = false;
                }      
            }
            else
                MessageBox.Show("Fields cannot be blank");
            Ext:
            Clear();
        }

        private void Search2_Click(object sender, EventArgs e)
        {
            if (TextBox17.Text != string.Empty)
            {
                string car = TextBox17.Text;
                var query = Query.EQ("Car_name", car);
                var res = collection.FindAs<CarTable>(query).Count();
                if (res == 0)
                {
                    MessageBox.Show("Car not found");
                    goto Ext1;
                }
                else
                {
                    List<CarTable> list = collection.FindAs<CarTable>(query).ToList();
                    metroGrid4.DataSource = list;
                    metroGrid4.Columns[0].Visible = false;
                    metroGrid4.Columns[9].Visible = false;
                }
            }
            else
                MessageBox.Show("Fields cannot be blank");
            Ext1:
            Clear();
        }

        private void metroGrid3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<CarTable> list = metroGrid3.DataSource as List<CarTable>;
            C1 = list[e.RowIndex];
            TextBox3.Text = C1.Car_company;
            TextBox4.Text = C1.Car_name;
            TextBox12.Text = C1.Model;
            TextBox13.Text = C1.Price;
            TextBox14.Text = C1.Colour;
            TextBox15.Text = C1.Mileage;
            TextBox19.Text = C1.Image_path;
            ComboBox5.SelectedItem = C1.Fuel_type;
            ComboBox6.SelectedItem = C1.Car_class;
        }

        private void metroGrid4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<CarTable> list = metroGrid4.DataSource as List<CarTable>;
            C1 = list[e.RowIndex];
            TextBox17.Text = C1.Car_name;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void metroGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<CarTable> list = metroGrid.DataSource as List<CarTable>;
            C1 = list[e.RowIndex];
            Form4 f = new Form4(C1.Image_path);
            f.Text = C1.Car_name;
            f.Show();
        }

        private void metroGrid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<CarTable> list = metroGrid1.DataSource as List<CarTable>;
            C1 = list[e.RowIndex];
            Form4 f = new Form4(C1.Image_path);
            f.Text = C1.Car_name;
            f.Show();
        }

        private void metroGrid2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<CarTable> list = metroGrid2.DataSource as List<CarTable>;
            C1 = list[e.RowIndex];
            Form4 f = new Form4(C1.Image_path);
            f.Text = C1.Car_name;
            f.Show();
        }     
    }
}
