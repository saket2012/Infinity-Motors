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
using Transitions;

namespace Infiniti_Motors
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Transition t1 = new Transition(new TransitionType_Deceleration(1000));
            t1.add(pictureBox2, "Left", 600);
            t1.add(pictureBox2, "Top", 470);
            t1.run();
        }

        public void Delayed(int delay, Action action)
        {
            Timer timer = new Timer();
            
            timer.Interval = delay;
            timer.Tick += (s, e) =>
            {
                action();
                timer.Stop();
            };
            timer.Start();
        }
        
        class LoginTable
        {
            public BsonObjectId _id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox2.PasswordChar = '*';
        }

        private void Login_Click(object sender, EventArgs e)
        {
            String connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("Login-Details");
            MongoCollection collection = database.GetCollection<LoginTable>("LoginTable");
            LoginTable t = new LoginTable();
            t.username = TextBox1.Text;
            t.password = TextBox2.Text;
            var query = Query.And(
                Query.EQ("username", t.username),
            Query.EQ("password", t.password)
            );

            var result = collection.FindAs<LoginTable>(query).Count();
            if (String.IsNullOrEmpty(TextBox1.Text) || String.IsNullOrEmpty(TextBox2.Text))
            {
                MessageBox.Show("Fields cannot be blank");
            }
            else
            {
                if (result > 0)
                {
                    Transition t2 = new Transition(new TransitionType_Acceleration(1000));
                    t2.add(pictureBox2, "Left", 000);
                    t2.add(pictureBox2, "Top", 550);
                    t2.run();
                    if (TextBox1.Text == "Staff")
                    {
                        Form3 f3 = new Form3("Logged in as Staff");
                        Delayed(650, () => f3.Show());
                    }
                    else
                    {
                        Form1 f1 = new Form1("Logged in as Manager");
                        Delayed(650, () => f1.Show());
                    }
                    Delayed(650, () => this.Hide());
                    TextBox1.Text = string.Empty;
                    TextBox2.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                    TextBox1.Text = string.Empty;
                    TextBox2.Text = string.Empty;
                    this.WindowState = FormWindowState.Maximized;
                    this.Show();
                }
            }
        }
    }
}
