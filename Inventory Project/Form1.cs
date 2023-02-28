using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-ELS5T5CB;Initial Catalog=userlogin;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            String username, user_password;

            username = txt_user.Text;
            user_password = txt_password.Text;

            try
            {
                String querry = "SELECT * FROM Login WHERE username = '" + txt_user.Text + "' AND password = '" + txt_password.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill (dtable);

                if(dtable.Rows.Count > 0)
                {
                    username = txt_user.Text;
                    user_password = txt_password.Text;

                    //Menu Selanjutnya
                    MenuForm form2 = new MenuForm();
                    form2.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Data anda salah","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_user.Clear();
                    txt_password.Clear();

                    //to focus username
                    txt_user.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Apakah anda yakin untuk keluar?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }
    }
}
