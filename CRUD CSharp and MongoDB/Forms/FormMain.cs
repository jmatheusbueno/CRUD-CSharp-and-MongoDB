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
            if (btnSave.Text == "Cadastrar")
                Save();
            else
            {
                var selected = dgvContacts.SelectedRows;
                var contact = (Contact)selected[0].DataBoundItem;
                UpdateContact(contact);
            }

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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            EditContact();
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
                MessageBox.Show("Não foi possível concluir o cadastro.", "Contato Não Cadastrado");
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

        private void Delete()
        {
            var selected = dgvContacts.SelectedRows;
            if (selected.Count == 0)
                MessageBox.Show("Não foi possível deletar o contato, certifique-se de ter selecionado toda a linha antes de deletar", "Contato Não Deletado");

            else if (selected.Count == 1)
            {
                if (new DB().Delete(selected[0].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Contato deletado com sucesso!", "Contato Deletado");
                    AddColumns();
                }
                else
                    MessageBox.Show("Não foi possível deletar o contato, certifique-se de ter selecionado toda a linha antes de deletar.", "Contato Não Deletado");
            }

            else
                MessageBox.Show("Não foi possível deletar o contato, certifique-se de ter selecionado apenas um contato antes de deletar.", "Contato Não Deletado");
        }

        private void UpdateContact(Contact contact)
        {
            contact.Name = txtName.Text;
            contact.Mail = txtMail.Text;
            contact.Number = txtNumber.Text;

            if (new DB().Update(contact.Id, contact))
            {
                MessageBox.Show("Contato editado com sucesso!", "Contato Editado");
                AddColumns();
            }
            else
                MessageBox.Show("Não foi possível editar o contato.", "Contato Não Editado");

            gprContact.Text = "Cadastrar Contato";
            txtName.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtNumber.Text = string.Empty;
            btnSave.Text = "Cadastrar";
        }

        private void EditContact()
        {
            var selected = dgvContacts.SelectedRows;
            if (selected.Count == 0)
            {
                MessageBox.Show("Não foi possível selecionar o contato, certifique-se de ter selecionado toda a linha antes.", "Contato Não Selecionado");
                return;
            }
            else if (selected.Count == 1)
            {
                var contact = (Contact)selected[0].DataBoundItem;
                gprContact.Text = "Editar Contato";
                txtName.Text = contact.Name;
                txtMail.Text = contact.Mail;
                txtNumber.Text = contact.Number;
                btnSave.Text = "Editar";
            }
            else
            {
                MessageBox.Show("Não foi possível selecionar o contato, certifique-se de ter selecionado apenas um contato.", "Contato Não Selecionado");
                return;
            }
        }
        #endregion
    }
}
