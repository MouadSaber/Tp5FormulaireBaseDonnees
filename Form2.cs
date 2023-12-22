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
    public partial class Form2 : Form
    {

        private const string connectionString = "Server=localhost;Database=bdformulairetp5;User=root;Password=MOsa07@@;";

        #region condstructeurs

        /// <summary>
        /// constructeur par défaut de forme 2
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }
        #endregion


        #region méthodes qu'on a utilisé

        /// <summary>
        /// méthode qui nous permet de créer un nouveau livre
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty)
            {
                MessageBox.Show("pas valide !! l'un des champs est vidse");
            }
            else
            {
                // le message de confirmation si vous voullez créer le livre ou non 
                MessageBox.Show("voullez-vous vraiment créer ce livre ?");

                // On récupére les valeurs des TextBoxs

                string titre = textBox1.Text;
                string specialite = textBox2.Text;
                int annee = int.Parse(textBox3.Text);
                decimal prix = decimal.Parse(textBox4.Text);


                // Création da connexion à la base de données
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // La commande d'insertion
                        string query = "INSERT INTO livres (titre, spécialité, année, prix) VALUES (@Titre, @Specialite, @Annee, @Prix)";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            // Ajouter les paramètres
                            cmd.Parameters.AddWithValue("@Titre", titre);
                            cmd.Parameters.AddWithValue("@Specialite", specialite);
                            cmd.Parameters.AddWithValue("@Annee", annee);
                            cmd.Parameters.AddWithValue("@Prix", prix);

                            // L'exécution de commande
                            cmd.ExecuteNonQuery();

                            // le message de succées
                            MessageBox.Show("Livre créé avec succès!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'insertion du livre : " + ex.Message);
                    }
                }
              

                // on vide tous les zone de textes
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
            }
        }

        /// <summary>
        /// méthode qui nous permet de retour vers la fenétre principa&le
        /// </summary>

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        #endregion


        #region méthodes qu'on a pas utilisées
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
