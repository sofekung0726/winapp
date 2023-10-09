using MySql.Data;
using MySql.Data.MySqlClient;

namespace winapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=;database=testconnnection";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                MessageBox.Show("Connect");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=testconnnection";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string username = textBox1.Text;
                    string password = textBox2.Text;

                    RegisterUser(connection, username, password);

                    MessageBox.Show("User registered successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=testconnnection";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string username = textBox1.Text;
                    string password = textBox2.Text;

                    string retrievedUsername = GetUsername(connection, username, password);

                    if (!string.IsNullOrEmpty(retrievedUsername))
                    {
                        MessageBox.Show($"Welcome, {retrievedUsername}!");
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        static bool CheckIfUserExists(MySqlConnection connection, string username, string password)
        {
            string query = "SELECT COUNT(*) FROM user WHERE user_name = @username AND user_pass = @password";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        static string GetUsername(MySqlConnection connection, string username, string password)
        {
            string query = "SELECT user_name FROM user WHERE user_name = @username AND user_pass = @password";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }
        static void RegisterUser(MySqlConnection connection, string username, string password)
        {
            string query = "INSERT INTO user (user_name, user_pass) VALUES (@username, @password)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
            }
        }
    }



}
