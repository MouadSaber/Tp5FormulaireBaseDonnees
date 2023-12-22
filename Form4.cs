using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;

namespace tp_n4
{
    public partial class Form4 : Form
    {

        private const string connectionString = "Server=localhost;Database=bdformulairetp5;User=root;Password=MOsa07@@;";


        #region constructeurs 
        public Form4()
        {
            InitializeComponent();
        }
        #endregion



        #region méthodes qu'on a utilisé


        /// <summary>
        /// méthode qui nous permet de chercher un livre dans notre base de données
        /// </summary>

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty)
            {
                MessageBox.Show("pas valide !! l'un des champs est vidse");
            }
            else
            {
                string titre = textBox2.Text;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT spécialité, année, prix FROM livres WHERE titre = @Titre";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Titre", titre);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // l'afficheage des informations dans les TextBox correspondantes
                                    textBox3.Text = reader["spécialité"].ToString();
                                    textBox4.Text = reader["année"].ToString();
                                    textBox5.Text = reader["prix"].ToString();


                                    MessageBox.Show("voullez-vous vraiment modifier ce livre");

                                    string Titre = textBox2.Text;
                                    string specialite = textBox3.Text;
                                    int annee;
                                    decimal prix;

                                    if (int.TryParse(textBox4.Text, out annee) && decimal.TryParse(textBox5.Text, out prix))
                                    {
                                        MettreAJourLivre(Titre, specialite, annee, prix);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Veuillez saisir des valeurs valides pour l'année et le prix.");
                                    }
                                }
                                else
                                {
                                    // l'affichage de message d'échec si le livre n'existe pas dans notre base de données
                                    MessageBox.Show("Aucun livre trouvé avec le titre spécifié.");
                                    ViderTextBox2();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la recherche du livre : " + ex.Message);
                    }
                }
            }
        }


            /// <summary>
            /// méthode qui nous permet de mettre à jour les informations s'un livre
            /// </summary>
            private void MettreAJourLivre(string titre, string specialite, int annee, decimal prix)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "UPDATE livres SET spécialité = @Specialite, année = @Annee, prix = @Prix WHERE titre = @Titre";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Titre", titre);
                            cmd.Parameters.AddWithValue("@Specialite", specialite);
                            cmd.Parameters.AddWithValue("@Annee", annee);
                            cmd.Parameters.AddWithValue("@Prix", prix);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Informations du livre mises à jour avec succès!");
                            }
                            else
                            {
                                MessageBox.Show("Erreur lors de la mise à jour des informations du livre.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la mise à jour du livre : " + ex.Message);
                    }
                }
            }

        /// <summary>
        /// méthode qui nous permet de vider tous les champs aprés la modification
        /// </summary>
            private void ViderTextBox2()
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

        }
        /// <summary>
        /// méthode qui nous permet de retour vers la fenétre principale
        /// </summary>

       private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
        #endregion



        #region méthodes qu'on pas utilisé
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion 

    }

}
