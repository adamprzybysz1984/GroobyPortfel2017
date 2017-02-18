using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GroobyPortfel
{
    // Klasa reprezentująca POJEDYNCZY przeplyw gotowki
    class MoneyFlow
    {
        private string _name;           // Nazwa Flowu widoczna poza klasę przez parametr Name
        private Category _flowCategory; // Kategoria Flowu widoczna poza klasę przez parametr FlowCategory
        private string _typ;             //Typ Flowu
        public double Amount { get; set; }      // Ilość wydanej lub kasy, która przyszła
        public string Name                      // Nazwa
        {
            get
            {
                return String.IsNullOrEmpty(_name) ? "Brak nazwy" : _name; // Sprawdza czy nazwa jest pusta. Jeżeli tak zwróci "Brak nazwy". W innym przypadku zwróci nazwę kategorii.
            }
            set
            {
                _name = value;
            }
        }
        public string typ
        {
            get
            {
                return String.IsNullOrEmpty(_typ) ? "---" : _typ;
            }
            set
            {
                _typ = value;
            }
        }

        public Category FlowCategory                      // Nazwa
        {
            get
            {
                return _flowCategory == null ? new Category() { Name = "Brak kategorii", Id = -1 } : _flowCategory; // Sprawdza czy kategoria jest pusta. Jeżeli tak zwróci "Brak kategorii". W innym przypadku zwróci kategorię.
            }
            set
            {
                _flowCategory = value;
            }
        }

        public DateTime Date { get; set; } // Data przychody lub wydatku

        public bool IsIncome               // Atrybut, który zwraca czy podany przepływ jest przypływem czy wydatkiem
                                           // Dla przypływu ilość kasy będzie większa od 0, czyli Zwróci true
        {
            get
            {
                //return Amount >= 0;
                return typ == "Przychód";
            }
        }

        // Ta Metoda ma za zadanie dopisać przepływ kasy do podanego (writer) pliku XMl
        public void SerializeToXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("MoneyFlow");                              // Tu wstawia znacznik <MoneyFlow
            writer.WriteAttributeString("Amount", Convert.ToString(Amount));    // Tu dostawia atrybut Amount itd
            writer.WriteAttributeString("Typ", _typ);
            writer.WriteAttributeString("Name", _name);
            writer.WriteAttributeString("FlowCategory", Convert.ToString(FlowCategory.Id));
            writer.WriteAttributeString("Date", Convert.ToString(Date));
            writer.WriteEndElement();                                           // A tu kończy znacznik MoneyFlow />
        }
    }
}
