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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
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
        #endregion
    }
}
