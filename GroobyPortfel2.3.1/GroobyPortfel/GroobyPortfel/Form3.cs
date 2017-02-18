using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroobyPortfel
{
    public partial class Form3 : Form
    {
        Pocket pocket; // Portfel Klienta
        MoneyFlow editedMoneyFlow;

        public Form3()
        {
            InitializeComponent();
            pocket = new Pocket();
            Categories.LoadCategories();
            pocket.Load();
            
            UpdateDataGridView();
            SetLabels();

            labelData.Text = DateTime.Now.ToLongDateString();
            labelGodzina.Text = DateTime.Now.ToLongTimeString();
            timer1.Enabled = true;
        }

        // Metoda aktualizująca wydatki i wpływy w tabelce
        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear(); // Usuwamy wszystkie dotychczasowe wiersze
            foreach (MoneyFlow item in pocket.MoneyFlows)   // I dla każdego flowu dodajemy wiersz w tabeli
            {
                if(item.Name.ToUpper().Contains(textBox1.Text.ToUpper()) || String.IsNullOrEmpty(textBox1.Text))        // Wyszukiwarka 
                    AddMoneyFlowToDataGridRow(item);
            }
        }

        // Metoda dodająca wiersz w tabeli na podstawie podanego MoneyFlow
        private void AddMoneyFlowToDataGridRow(MoneyFlow mf)
        {
            MoneyFlow moneyFlow = new MoneyFlow();
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewTextBoxCell IdCell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell AmountCell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell TypeCell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell CategoryCell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell DataCell = new DataGridViewTextBoxCell();
            

            IdCell.Value = dataGridView1.Rows.Count + 1;
            AmountCell.Value = mf.Amount;
            NameCell.Value = mf.Name;
            CategoryCell.Value = mf.FlowCategory.Name;
            DataCell.Value = mf.Date;
            TypeCell.Value = mf.typ;

            row.Cells.Add(IdCell);
            row.Cells.Add(AmountCell);
            row.Cells.Add(TypeCell);
            row.Cells.Add(NameCell);
            row.Cells.Add(CategoryCell);
            row.Cells.Add(DataCell);
            row.Tag = mf; // W tagu zapisujemy referencje do wydatku

            dataGridView1.Rows.Add(row);
            
        }

        // Metoda dodająca Nowy Flow
        private void btnNewIncome_Click(object sender, EventArgs e)
        {
            MoneyFlow mf = new MoneyFlow();
            mf.Date = DateTime.Now;
            
            pocket.MoneyFlows.Add(mf);
            AddMoneyFlowToDataGridRow(mf);
            pocket.Serialize();
            SetLabels();
        }

        // Show panel
        private void EditPanelShow(MoneyFlow mf)
        {
            if (mf.typ == "Wydatek")
            {
                radioButton2.Checked = true;
            }
            if (mf.typ=="Przychód")
            {
                radioButton1.Checked = true;
            }            
            txtExpend.Text = Convert.ToString(mf.Amount);
            txtExpendName.Text = mf.Name;
            dateTimePickerExpend.Value = mf.Date;
            panel2.Visible = true;
            editedMoneyFlow = mf;

            comboBox1.Items.Clear();
            foreach (Category item in Categories.categories)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedItem = mf.FlowCategory;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            EditPanelShow((MoneyFlow)dataGridView1.SelectedRows[0].Tag);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double amount;
                Double.TryParse(txtExpend.Text, out amount);
                if (radioButton1.Checked)
                {
                    editedMoneyFlow.Amount = amount;
                }
                else
                {
                    editedMoneyFlow.Amount = -amount;
                }
                editedMoneyFlow.Name = txtExpendName.Text;
                editedMoneyFlow.Date = dateTimePickerExpend.Value;
                editedMoneyFlow.FlowCategory = (Category)comboBox1.SelectedItem;
                if (radioButton1.Checked)
                {
                    editedMoneyFlow.typ = radioButton1.Text;
                }
                else
                {
                    editedMoneyFlow.typ = radioButton2.Text;
                }
                UpdateDataGridView();
                pocket.Serialize();
                SetLabels();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                pocket.MoneyFlows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                UpdateDataGridView();
                pocket.Serialize();
                SetLabels();
            }
        }

        private void SetLabels()
        {
            label2.Text = "Saldo: " + pocket.GetBalance();
            label3.Text = "Wpływy: " + pocket.GetBalanceOfIncomes();
            label4.Text = "Wydatki: " + pocket.GetBalanceOfOutcomes();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelGodzina.Text = DateTime.Now.ToLongTimeString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }


    }
}
