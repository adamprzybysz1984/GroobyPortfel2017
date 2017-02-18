using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace GroobyPortfel
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnRegister1_Click(object sender, EventArgs e)
        {
            try
            {
                var sw = new System.IO.StreamWriter("loginy\\" + txtUserName1.Text + "\\login.ID");
                sw.Write(txtUserName1.Text + "\n" + txtPassword1.Text);
                sw.Close();
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                System.IO.Directory.CreateDirectory("loginy\\" + txtUserName1.Text);
                var sw = new System.IO.StreamWriter("loginy\\" + txtUserName1.Text + "\\login.ID");
                sw.Write(txtUserName1.Text + "\n" + txtPassword1.Text);
                sw.Close();
            }
            finally
            {
                var sw = new System.IO.StreamWriter("loginy\\" + txtUserName1.Text + "\\categories.txt");
                sw.WriteLine("Brak kategorii");
                sw.WriteLine("Praca");
                sw.WriteLine("Jedzenie");
                sw.WriteLine("Transport");
                sw.WriteLine("Alkohol");
                sw.WriteLine("Kultura");
                sw.Close();
                XmlTextWriter writer = new XmlTextWriter("loginy\\" + txtUserName1.Text + "\\product.xml", System.Text.Encoding.UTF8); // Tworzy instancję klasy, która stworzy nam nasz plik XML
                writer.WriteStartDocument(true);         // Dopisuje nagłówek pliku XML
                writer.Formatting = Formatting.Indented; // XML będzie miał ładny wygląd z wcięciami
                writer.Indentation = 2;                  // Szerokość wcięcie
                writer.WriteStartElement("MoneyFlows");  // Dopisuje element rozpoczynający
                writer.WriteEndElement();                // Kończy </MoneyFlows>
                writer.WriteEndDocument();              // Kończy dokument
                writer.Close();                          // Zamyka plik

                this.Close();
            }
        }
    }
}
