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

namespace _1.ConexionBDMySQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runQuery();
        }

        private void runQuery()
        {
            //ASIGNAMOS LA VARIABLE QUERY AL TEXTBOX1
            string query = textBox1.Text;
            //SI LA VARIABLE ESTA VACIA ENVIAMOS UN MENSAJE
            if (query == "")
            {
                MessageBox.Show("Please insert some sql query !");
                return;
            }
            //REALIZAMOS LA CONEXION AL SERVIDOR
            string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=testing;SslMode=none";
            //PREPARAMOS LA CONEXION A LA BASE DE DATOS
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                //SI SE REALIZA CORRECTAMENTE LA CONEXION ABRIMOS LA BD
                databaseConnection.Open();
                //CREAMOS UNA VARIABLE QUE LEA LA BD
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                //LEE LA BD
                if (myReader.HasRows)
                {
                    //MUESTRA MENSAJE
                    MessageBox.Show("Your query generated result, please see the console");
                    //SI LA VARIABLE LEE DATOS LOS MUESTRA POR CONSOLA
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetString(0) + " - " + myReader.GetString(1) + " - " + myReader.GetString(2) + " - " + myReader.GetString(3));
                    }
                }
                //ENVIA MENSAJE DE QUE FUE INSERTADA LA SENTENCIA CORRECTAMENTE
                MessageBox.Show("Query successfully execute");
                //EXCEPCION "SI OCURRE UN ERROR"
            }catch (Exception e)
            {
                //MUESTA MENSAJE DE ERROR CON EL TIPO DE ERROR
                MessageBox.Show("Query Error" + e.Message);
            }
        }
    }
}
