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
        // La d�claration du second formulaire
        Form pagedecreation = new Form2();

        // La d�claration du troisi�me formulaire
        Form pagedesuppression = new Form3();

        // La d�claration du quatri�me formulaire
        Form pagedemodification = new Form4();
        #endregion

        private const string connectionString = "Server=localhost;Database=bdformulairetp5;User=root;Password=MOsa07@@;";

        #region constructeurs
        /// <summary>
        /// constructeur par d�faut de forme 1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #endregion


        #region m�thodes pour naviger entre les fen�tre
        private void button5_Click(object sender, EventArgs e)
        {
            // pour afficher la page de cr�ation et retour vers la page principale
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


        #region m�thode de recherche
        /// <summary>
        /// la m�thode qui nous permet de chercher un livre en basant sur son titre
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("clickez ok pour confirmer le recherche ");

            // On r�cup�re le titre du livre � rechercher

            string titre = textBox2.Text;

            // On cr�e la connexion � la base de donn�es
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Cr�er une commande SQL de recherche
                    string query = "SELECT sp�cialit�, ann�e, prix FROM livres WHERE titre = @Titre";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Ajouter le param�tre
                        cmd.Parameters.AddWithValue("@Titre", titre);

                        // Ex�cuter la commande et r�cup�rer les r�sultats
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
                                MessageBox.Show("Aucun livre trouv� avec le titre sp�cifi�.");

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

        #region bottons qu'on a pas utilis�es
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("veuillez saisir le titre du livre dans la zone du titre pour �tre plus pr�cise");
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
