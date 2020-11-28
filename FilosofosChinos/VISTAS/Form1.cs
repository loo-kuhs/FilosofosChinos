using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilosofosChinos.VISTAS;
using FilosofosChinos.FILOSOFOS;
using FilosofosChinos.LOGICA;


namespace FilosofosChinos
{
    public partial class Form1 : Form
    {
        private static object locker = new object();
        public Form1()
        {
            InitializeComponent();

            this.btnDetener.Enabled = false;

            this.txtRegistro.AppendText("\n Sobre este programa:"
                + "\n\n El problema de La Cena de los Filósofos es un clásico de las Ciencias"
                + "\n de la Computación propuesta por Edsger Dijkstra en 1965.");

            this.txtConteo01.Text = "0";
            this.txtConteo02.Text = "0";
            this.txtConteo03.Text = "0";
            this.txtConteo04.Text = "0";
            this.txtConteo05.Text = "0";

            //this.label_Filo01.Visible = true;
            this.label_Filo01.BackColor = Color.White;
            this.label_Filo02.BackColor = Color.White;
            this.label_Filo03.BackColor = Color.White;
            this.label_Filo04.BackColor = Color.White;
            this.label_Filo05.BackColor = Color.White;

            #region PALILLOS
            this.label_Pal01.Text = " 1 ";
            this.label_Pal01.BackColor = Color.LightGray;
            this.label_Pal01.ForeColor = Color.White;

            this.label_Pal02.Text = " 2 ";
            this.label_Pal02.BackColor = Color.LightGray;
            this.label_Pal02.ForeColor = Color.White;

            this.label_Pal03.Text = " 3 ";
            this.label_Pal03.BackColor = Color.LightGray;
            this.label_Pal03.ForeColor = Color.White;

            this.label_Pal04.Text = " 4 ";
            this.label_Pal04.BackColor = Color.LightGray;
            this.label_Pal04.ForeColor = Color.White;

            this.label_Pal05.Text = " 5 ";
            this.label_Pal05.BackColor = Color.LightGray;
            this.label_Pal05.ForeColor = Color.White;

            #endregion

            #region ESTADOS COLORES

            this.label_Est01.Text = "          ";
            this.label_Est01.BackColor = Color.Pink;

            this.label_Est02.Text = "          ";
            this.label_Est02.BackColor = Color.Cyan;

            this.label_Est03.Text = "          ";
            this.label_Est03.BackColor = Color.Orange;

            this.label_Est04.Text = "          ";
            this.label_Est04.BackColor = Color.Green;

            this.label_Est05.Text = "          ";
            this.label_Est05.BackColor = Color.Blue;

            this.label_Est06.Text = "          ";
            this.label_Est06.BackColor = Color.Red;

            #endregion
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            CFilosofo.finalizado = false;

            if (!cBoxRegistro.Checked) txtRegistro = null;

            CForm form = new CForm(
                label_Filo01, label_Filo02, 
                label_Filo03, label_Filo04,
                label_Filo05, label_Pal01, 
                label_Pal02, label_Pal03, 
                label_Pal04, label_Pal05, 
                txtRegistro, txtConteo01, 
                txtConteo02, txtConteo03, 
                txtConteo04, txtConteo05);

            CPrincipal principal = new CPrincipal(form);

            this.btnDetener.Enabled = true;
            this.btnIniciar.Enabled = false;
            this.cBoxRegistro.Enabled = false;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            CFilosofo.finalizado = true;
            lock (locker)
            {
                try
                {
                    Monitor.Wait(locker, 3000);
                }
                catch (ThreadInterruptedException ex)
                {
                    MessageBox.Show("Error. Descripcion: " + ex.ToString());
                }
            }
            //try
            //{
            //    Monitor.Wait(this);
            //}
            
            this.btnIniciar.Enabled = true;
            this.btnDetener.Enabled = false;
        }

        private void cBoxRegistro_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxRegistro.Checked)
            {
                this.txtRegistro.AppendText("\n\n Atencion:" + "\n Acabas de seleccionar crear un registro. "
                    + "\n Utiliza este registro para fines de comprobacion.\n\n");
                this.txtRegistro.ScrollToCaret();
                //this.txtRegistro.Select(txtRegistro.TextLength, 0);
            }
            if (!cBoxRegistro.Checked)
            {
                this.txtRegistro.ScrollToCaret();
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            CFilosofo.finalizado = true;
            Monitor.Enter(locker);
            try
            {
                Monitor.Wait(locker, 3000);
            }
            catch (ThreadInterruptedException ex)
            {
                MessageBox.Show("Error. " + ex.ToString());
            }
            finally
            {
                Monitor.Exit(locker);
                Application.Restart();
            }
        }
    }
}
