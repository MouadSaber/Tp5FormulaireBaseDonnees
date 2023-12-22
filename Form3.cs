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
    public partial class Form3 : Form
    {
        #region constructeurs 
        public Form3()
        {
            InitializeComponent();
        }
        #endregion



        private const string connectionString = "Server=localhost;Database=bdformulairetp5;User=root;Password=MOsa07@@;";

        #region méthode qu'on a utilisé


        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                MessageBox.Show("pas valide !! l'un des champs est vide");
            }
            else
            {
                MessageBox.Show("veuillez-vous vraiment supprimer ce livre ?");
                // on récupére le titre du livre à rechercher
                string titre = textBox1.Text;

                // on crée la connexion à notre base de données
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // on crée la commande SQL de recherche
                        string query = "SELECT spécialité, année, prix FROM livres WHERE titre = @Titre";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            // on ajoute le paramètre
                            cmd.Parameters.AddWithValue("@Titre", titre);

                            // on exécute la commande et on récupére les résultats
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // l'affichage des informations dans les TextBox correspondantes
                                    textBox2.Text = reader["Specialite"].ToString();
                                    textBox3.Text = reader["Annee"].ToString();
                                    textBox4.Text = reader["Prix"].ToString();

                                    MessageBox.Show("voullez-vous vraiment supprimer ce livre ??!!");

                                    // et on upprime le livre de la base de données en appelant la méthode supprimer()
                                    SupprimerLivre(titre);
                                }
                                else
                                {
                                    MessageBox.Show("Aucun livre trouvé avec le titre spécifié.");

                                    // on vide le TextBox1 en cas d'absence de livre
                                    textBox1.Clear();


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
        /// méthode qui nous permet de supprimer un livre de lq bqse de données
        /// </summary>
        /// <param name="titre"></param>
            private void SupprimerLivre(string titre)
            {
                // Supprimer le livre de la base de données
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Créer une commande SQL de suppression
                        string query = "DELETE FROM VotreTable WHERE Titre = @Titre";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            // Ajouter le paramètre
                            cmd.Parameters.AddWithValue("@Titre", titre);

                            // Exécuter la commande
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Livre supprimé de la base de données.");
                            }
                            else
                            {
                                MessageBox.Show("Erreur lors de la suppression du livre de la base de données.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppression du livre : " + ex.Message);
                    }
                }

                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;

            }

        /// <summary>
        /// méthode qui nous permet de retour vers la fenétre principale de l'application
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
        #endregion


        #region méthodes qu'on a pas utilisé
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("veuillez saisir le titre du livre dans la zone du titre pour étre plus précise");
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
