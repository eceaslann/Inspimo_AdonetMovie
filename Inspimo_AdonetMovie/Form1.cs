using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inspimo_AdonetMovie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Server= DESKTOP-RCI54VP; initial catalog= InspimoMovieDb; integrated security= true");

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgCategory.DataSource = dt;
            connection.Close();

        }

        private void btnCategorySave_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into TblCategory (CategoryName) values (@p1)", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCategory Where CategoryID=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            connection.Close();
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblCategory Set CategoryName=@p1 where CategoryID=@p2", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.Parameters.AddWithValue("@p2", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            connection.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.DataSource = dt;
            connection.Close();
        }

        private void btnMovie_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select MovieName, MovieDuration, MovieImdb, CategoryName From TblMovie Inner Join TblCategory On TblMovie.MovieCategory= TblCategory.CategoryID", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgMovie.DataSource = dt;
            connection.Close();
        }

        private void btnMovieProduce_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Exec MovieListWithCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgMovie.DataSource = dt;
            connection.Close();
        }

        private void btnMovieEkle_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into TblMovie (MovieName, MovieImdb, MovieDuration, MovieCategory) values (@p1,@p2,@p3,@p4)", connection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMoviePuan.Text);
            command.Parameters.AddWithValue("@p3", txtMovieTime.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.ExecuteNonQuery();
            MessageBox.Show("Filminiz Başarılı Bir Şekilde Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
        }

        private void btnMovieDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete From TblMovie Where MovieID=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtMovieID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Film Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            connection.Close();
        }

        private void btnMovieUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblMovie Set MovieName=@p1, MovieImdb=@p2, MovieDuration=@p3, MovieCategory=@p4 where MovieID=@p5", connection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMoviePuan.Text);
            command.Parameters.AddWithValue("@p3", txtMovieTime.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.Parameters.AddWithValue("@p5", txtMovieID.Text);
            MessageBox.Show("Filminiz Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Excl);
            connection.Close();
        }
    }
}
