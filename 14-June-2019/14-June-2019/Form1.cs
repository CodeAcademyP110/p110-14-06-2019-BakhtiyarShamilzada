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
using System.Configuration;

namespace _14_June_2019
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMarka();
            string connString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            using (var conn = new SqlConnection(connString))
            {
                string Marka = cmbMarka.Text;
                string comText = "select Model.Id, Model.Name " +
                                 "from Model " +
                                 "join Marka on Model.MarkaId = Marka.Id " +
                                 $"where Marka.Name = '{Marka}'";
                using (var comm = new SqlCommand(comText, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            cmbModel.Items.Add(reader["Name"]);
                        }
                        cmbModel.SelectedIndex = 0;
                    }
                    else
                    {
                        //MessageBox.Show($"There is no Model for this {Marka}");
                    }
                }
            }
        }
        void LoadMarka()
        {
            cmbMarka.Items.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string commandText = "select * from Marka";
                using (SqlCommand sqlCommand = new SqlCommand(commandText, conn))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            cmbMarka.Items.Add(reader["Name"]);
                        }
                        //cmbMarka.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("There is no Marka in the list");
                    }
                }
            }
        }

        private void CmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbModel.Text = "";
            cmbModel.Items.Clear();

            string connString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            using (var conn = new SqlConnection(connString))
            {
                string Marka = cmbMarka.Text;
                string comText = "select Model.Id, Model.Name " +
                                 "from Model " +
                                 "join Marka on Model.MarkaId = Marka.Id " +
                                 $"where Marka.Name = '{Marka}'";
                using (var comm = new SqlCommand(comText, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cmbModel.Items.Add(reader["Name"]);
                        }
                        cmbModel.SelectedIndex = 0;
                    }
                    else
                    {
                        //MessageBox.Show($"There is no Model for this {Marka}");
                    }
                }
            }
        }

        private void CmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string Model = cmbModel.Text;
                string commandText = "select Masin.Id, Masin.Name, Marka.Name Marka, Model.Name Model, BanNovu.Name [Ban novu], Reng.Name Reng, Yurus, Qiymet, Seher.Name Seher, YanacaqNovu.Name [Yanacaq Novu], Oturucu.Name Oturucu, SuretQutusu.Name [Suret Qutusu], BuraxilisIli [Buraxilis ili], MuherrikHecmi [Muherrik Hecmi] " +
                                     "from Masin " +
                                     "join Model on Masin.ModelId = Model.Id " +
                                     "join Marka on Model.MarkaId = Marka.Id " +
                                     "join BanNovu on Masin.BanNovuId = BanNovu.Id " +
                                     "join Reng on Masin.RengId = Reng.Id " +
                                     "join Seher on Masin.SeherId = Seher.Id " +
                                     "join YanacaqNovu on Masin.YanacaqNovuId = YanacaqNovu.Id " +
                                     "join Oturucu on Masin.OturucuId = Oturucu.Id " +
                                     "join SuretQutusu on Masin.SuretQutusuId = SuretQutusu.Id " +
                                     $"where Model.name = '{Model}'";
                using (SqlCommand command = new SqlCommand(commandText, conn))
                {
                    conn.Open();
                    
                        SqlDataReader reader = command.ExecuteReader();
                    
                        if (reader.HasRows)
                        {
                        reader.Close();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dgwMasin.DataSource = table;
                        }
                    }
                        else
                        {
                            //MessageBox.Show($"There is no Automobil for this {Model}");
                        }
                    
                        
                }
            }
        }
    }
}
