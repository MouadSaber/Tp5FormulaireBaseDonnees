using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace tp_n4
{
    public partial class Form1 : Form
    {


        #region les instansciations des autres formes
        // La déclaration du second formulaire
        Form pagedecreation = new Form2();

        // La déclaration du troisième formulaire
        Form pagedesuppression = new Form3();

        // La déclaration du quatrième formulaire
        Form pagedemodification = new Form4();
        #endregion

        private const string connectionString = "Server=localhost;Database=bdformulairetp5;User=root;Password=MOsa07@@;";

        #region constructeurs
        /// <summary>
        /// constructeur par défaut de forme 1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #endregion


        #region méthodes pour naviger entre les fenétre
        private void button5_Click(object sender, EventArgs e)
        {
            // pour afficher la page de création et retour vers la page principale
            pagedecreation.Owner = this;
            pagedecreation.Hide();
            pagedecreation.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // pour afficher la page de suppression et retour vers la page principale
            pagedesuppression.Owner = this;
            pagedesuppression.Hide();
            pagedesuppression.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // pour afficher la page de modification et retour vers la page principale
            pagedemodification.Owner = this;
            pagedemodification.Hide();
            pagedemodification.ShowDialog();
        }

        /// <summary>
        /// le botton d'annulation
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            // pour quitte le programme
            Application.Exit();
        }
        #endregion


        #region méthode de recherche
        /// <summary>
        /// la méthode qui nous permet de chercher un livre en basant sur son titre
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("clickez ok pour confirmer le recherche ");

            // On récupére le titre du livre à rechercher

            string titre = textBox2.Text;

            // On crée la connexion à la base de données
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Créer une commande SQL de recherche
                    string query = "SELECT spécialité, année, prix FROM livres WHERE titre = @Titre";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Ajouter le paramètre
                        cmd.Parameters.AddWithValue("@Titre", titre);

                        // Exécuter la commande et récupérer les résultats
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Afficher les informations dans les TextBox correspondantes
                                textBox3.Text = reader["Specialite"].ToString();
                                textBox1.Text = reader["Annee"].ToString();
                                textBox2.Text = reader["Prix"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Aucun livre trouvé avec le titre spécifié.");

                                // on vide le TextBox1 en cas d'absence de livre
                                textBox2.Clear();
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
        #endregion

        #region bottons qu'on a pas utilisées
        private void Form1_Load(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

 
    }
}
