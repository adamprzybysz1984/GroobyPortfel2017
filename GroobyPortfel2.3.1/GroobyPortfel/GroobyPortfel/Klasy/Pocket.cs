using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GroobyPortfel
{
    // Klasa przechowywująca wszystkie wpływy i wydatki 
    class Pocket
    {
        public List<MoneyFlow> MoneyFlows = new List<MoneyFlow>();  // Lista przechowywująca wszystkie wpływy i wydatki

        public double GetBalance()    // Metoda, która zczyta balans konta
        {
            return MoneyFlows.Sum(x => x.Amount); // Wyrażenie Lambda. Powiedzcie, że macie z neta :)
        }

        public double GetBalanceOfIncomes()    // Metoda, która zczytuje sumę wszystkich przychodów
        {
            return MoneyFlows.Where(x => x.IsIncome).Sum(x => x.Amount); // Wyrażenie Lambda. Powiedzcie, że macie z neta :)
        }

        public double GetBalanceOfOutcomes()    // Metoda, która zczytuje sumę wszystkich wydatków
        {
            return MoneyFlows.Where(x => !x.IsIncome).Sum(x => x.Amount); // Wyrażenie Lambda. Powiedzcie, że macie z neta :)
        }

        // Klasa tworząca plik XML i wołająca wszystkie metody w klasie Flow aby się dopisały do danego pliku
        public void Serialize()
        {
            XmlTextWriter writer = new XmlTextWriter("loginy\\" + Config.LoggedUser + "\\product.xml", System.Text.Encoding.UTF8); // Tworzy instancję klasy, która stworzy nam nasz plik XML
            writer.WriteStartDocument(true);         // Dopisuje nagłówek pliku XML
            writer.Formatting = Formatting.Indented; // XML będzie miał ładny wygląd z wcięciami
            writer.Indentation = 2;                  // Szerokość wcięcie
            writer.WriteStartElement("MoneyFlows");  // Dopisuje element rozpoczynający

            foreach (MoneyFlow item in MoneyFlows)   // Woła każdy element aby się dopisał do pliku
            {
                item.SerializeToXml(writer);
            }

            writer.WriteEndElement();                // Kończy </MoneyFlows>
            writer.WriteEndDocument();              // Kończy dokument
            writer.Close();                          // Zamyka plik
        }

        public void Load()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("loginy\\" + Config.LoggedUser + "\\product.xml");

            XmlNode mainNode = xmlDoc.GetElementsByTagName("MoneyFlows")[0];

            foreach (XmlNode item in mainNode.ChildNodes)
            {
                MoneyFlow mf = new MoneyFlow();
                mf.Amount = Convert.ToDouble(item.Attributes[0].Value);
                mf.typ = Convert.ToString(item.Attributes[1].Value);
                mf.Name = Convert.ToString(item.Attributes[2].Value);
                mf.Date = Convert.ToDateTime(item.Attributes[4].Value);
                
                mf.FlowCategory = Convert.ToInt32(item.Attributes[3].Value) == -1 ? null : Categories.categories[Convert.ToInt32(item.Attributes[3].Value)];
                MoneyFlows.Add(mf);
            }
        }
    }
}
