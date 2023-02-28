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
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;

namespace Inventory_Project
{
    public partial class MenuForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-ELS5T5CB;Initial Catalog=PetHouse;Integrated Security=True");
        public MenuForm()
        {
            InitializeComponent();
        }
        string Diskon;
        string imglocation = "";
        SqlCommand cmd;

        private void btn_input_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream streem = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(streem);
            images = brs.ReadBytes((int)streem.Length);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [data_barang] (Produk,Harga_Beli,Stok_Barang,Harga_Jual,Expired,Diskon,Foto) values ('"+txt_produk.Text+ "','" + txt_hargabeli.Text + "','" + txt_stokbarang.Text + "','" + txt_hargajual.Text + "','"+date_expired.Text+ "','"+Diskon+"',@images)";
            cmd.Parameters.Add(new SqlParameter("@images", images));
            cmd.ExecuteNonQuery();
            conn.Close();
            txt_produk.Text = "";
            txt_hargabeli.Text = "";
            txt_stokbarang.Text = "";
            txt_hargajual.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            date_expired.Value = DateTime.Now;
            pictureBox1.ImageLocation = null;
            display_data();
            MessageBox.Show("Data berhasil ditambahkan");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Diskon = "Ya";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Diskon = "Tidak";
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imglocation;
            }
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [data_barang]";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);
            dataGridView1.DataSource = dta;
            conn.Close();
        }
        private void btn_display_Click(object sender, EventArgs e)
        {
            display_data();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "delete from [data_barang] where produk = '" + txt_produk.Text + "'";
            cmd.ExecuteNonQuery();
            conn.Close();
            txt_produk.Text = "";
            display_data();
            MessageBox.Show("Data berhasil dihapus");
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream streem = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(streem);
            images = brs.ReadBytes((int)streem.Length);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update [data_barang] set Produk='" + this.txt_produk.Text + "', Harga_Beli='" + this.txt_hargabeli.Text + "', Stok_Barang='" + this.txt_stokbarang.Text + "', Harga_Jual='" + this.txt_hargajual.Text + "', Expired='" + date_expired.Text + "', Diskon='" + Diskon + "', Foto=@images where Produk='"+this.txt_produk.Text+"'";
            cmd.Parameters.Add(new SqlParameter("@images", images));
            cmd.ExecuteNonQuery();
            conn.Close();
            txt_produk.Text = "";
            txt_hargabeli.Text = "";
            txt_stokbarang.Text = "";
            txt_hargajual.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            date_expired.Value = DateTime.Now;
            pictureBox1.ImageLocation = null;
            display_data();
            MessageBox.Show("Data berhasil diubah");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [data_barang] where Produk = '" + txt_search.Text + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            txt_search.Text = "";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_produk.Text = "";
            txt_hargabeli.Text = "";
            txt_stokbarang.Text = "";
            txt_hargajual.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            date_expired.Value = DateTime.Now;
            pictureBox1.ImageLocation = null;
        }
    }
}
