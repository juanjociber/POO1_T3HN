using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;//Para el ArrayList
using System.IO;//Para el manejo de archivos

namespace POO1_T3HN
{
    public partial class Vista : Form
    {
        //Colección tipo ArrayList
        ArrayList libro = new ArrayList();
        BindingSource SRCLibros;

        //Colección tipo List
        List<Clases.Participantes> participante = new List<Clases.Participantes>();

        //Declarando variable que almacena nombre de archivo
        public string archivo = "";

        public Vista()
        {
            InitializeComponent();
            /*Clases.Libros l = new Clases.Libros();
            l.Codigo = "200";
            l.Titulo = "Historia de la Web";
            l.Materia = "COMPUTACION";
            l.Autor = "Juan Huiza";
            l.Edicion = 2010;
                        
            SRCLibros = new BindingSource(l, null);
            dgvLibros.DataSource = SRCLibros;*/
              
        }
        private void HabilitarTextBox(bool sw)
        {
            txtCodigo.Enabled = sw;
            txtTitulo.Enabled = sw;
            cboMateria.Enabled = sw;
            txtAutor.Enabled = sw;
            txtEdicion.Enabled = sw;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Hablitando ingreso de datos
            HabilitarTextBox(true);
            //Limpiando TextBox
            txtCodigo.Clear();
            txtTitulo.Clear();
            cboMateria.Text = "";
            txtAutor.Clear();
            txtEdicion.Clear();
            txtCodigo.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            HabilitarTextBox(false);

            try
            {
                Clases.Libros reg = new Clases.Libros()
                {
                    Codigo = txtCodigo.Text,
                    Titulo = txtTitulo.Text,
                    Materia = cboMateria.Text,
                    Autor = txtAutor.Text,
                    Edicion = int.Parse(txtEdicion.Text)
                };
                libro.Add(reg);

                SRCLibros = new BindingSource(libro, null);
                dgvLibros.DataSource = SRCLibros;
            }
            catch
            {
                //MessageBox.Show("Mensake","Título",Botones,iconos);
                MessageBox.Show("Debe ingresar un registro", "Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (Clases.Libros it in libro)
                {
                    if (it.Codigo == txtCodigo.Text)
                    {
                        libro.Remove(it); break;
                    }
                }
            SRCLibros = new BindingSource(libro, null);
            dgvLibros.DataSource = SRCLibros;
        }

        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgvLibros.CurrentRow;
            txtCodigo.Text = fila.Cells[0].Value.ToString();
            txtTitulo.Text = fila.Cells[1].Value.ToString();
            cboMateria.Text = fila.Cells[2].Value.ToString();
            txtAutor.Text = fila.Cells[3].Value.ToString();
            txtEdicion.Text = fila.Cells[4].Value.ToString();
        }

        //======= BOTONES DE PARTICIPANTES =========
        private void brnAdicionar_Click(object sender, EventArgs e)
        {   
            if (participante.Where(p => p.dni == txtDNI.Text).FirstOrDefault() != null)
                {
                    MessageBox.Show("Ingrese otro número de DNI","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                
                Clases.Participantes reg = new Clases.Participantes()
                {
                    dni = txtDNI.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text
                };
                participante.Add(reg);
                dgvParticipante.DataSource = participante.ToArray();

                /*txtDNI.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtEmail.Clear();
                txtDNI.Focus();*/
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            foreach (Clases.Participantes it in participante)
            {
                if (it.dni == txtDNI.Text)
                {
                    participante.Remove(it); break;
                }
            }
            dgvParticipante.DataSource = participante.ToArray();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Clases.Participantes reg = participante.Find(p => p.dni == txtDNI.Text);
            if (reg == null)
            {
                MessageBox.Show("No existe el DNI");
            }
            else
            {
                reg.Nombre = txtNombre.Text;
                reg.Apellido = txtApellido.Text;
                reg.Telefono = txtTelefono.Text;
                reg.Email = txtEmail.Text;
            }
            dgvParticipante.DataSource = participante.ToArray();
        }

        private void dgvParticipante_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgvParticipante.CurrentRow;
            txtDNI.Text = fila.Cells[0].Value.ToString();
            txtNombre.Text = fila.Cells[1].Value.ToString();
            txtApellido.Text = fila.Cells[2].Value.ToString();
            txtTelefono.Text = fila.Cells[3].Value.ToString();
            txtEmail.Text = fila.Cells[4].Value.ToString();
        }

        /*============ BOTONES BLOC DE NOTAS ===========*/
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBloc.Text = "";
            txtBloc.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog();
            op.Filter = "archivo de texto |*.txt";
            if(op.ShowDialog() == DialogResult.OK)
            {
                StreamWriter escritor = new StreamWriter(op.FileName);
                escritor.Write(txtBloc.Text);
                escritor.Close();
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "archivo de texto |*.txt";
            if(op.ShowDialog() == DialogResult.OK)
            {
                StreamReader lector = new StreamReader(op.FileName);
                txtBloc.Text = lector.ReadToEnd();
                lector.Close();
            }
        }

        private void btnGuardarComo_Click(object sender, EventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog();
            op.Filter = "archivo de texto|*.txt";
            if(op.ShowDialog() == DialogResult.OK)
            {
                archivo = op.FileName;
                StreamWriter escritor = new StreamWriter(op.FileName);
                escritor.Write(txtBloc.Text);
                escritor.Close();
            }
        }

    }
}
