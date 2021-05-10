using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUD_CSharp_and_MongoDB.Models;
using CRUD_CSharp_and_MongoDB.Data;

namespace CRUD_CSharp_and_MongoDB
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        #region events
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            AddColumns();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            AddColumns();
        }
        #endregion

        #region private methods
        private void Save()
        {
            var contact = new Contact();
            contact.Name = txtName.Text;
            contact.Mail = txtMail.Text;
            contact.Number = txtNumber.Text;

            var db = new DB();
            if (db.Save(contact))           
                MessageBox.Show("Novo Contato cadastrato com sucesso!", "Contato Cadastrado");
            else
                MessageBox.Show("Não foi possível concluir o cadastro", "Contato Não Cadastrado");
        }

        private void Clear()
        {
            txtName.Clear();
            txtMail.Clear();
            txtNumber.Clear();
        }

        private void AddColumns()
        {
            var lstContacts = new DB().GetContacts();
            dgvContacts.DataSource = lstContacts;

            dgvContacts.Columns[0].HeaderText = "Id";
            dgvContacts.Columns[1].HeaderText = "Nome";
            dgvContacts.Columns[2].HeaderText = "E-Mail";
            dgvContacts.Columns[3].HeaderText = "Número";

            dgvContacts.Columns[0].Width = 0;
            dgvContacts.Columns[1].Width = 187;
            dgvContacts.Columns[2].Width = 270;
            dgvContacts.Columns[3].Width = 150;

            dgvContacts.Columns[0].Visible = false;
        }
        #endregion

    }
}
