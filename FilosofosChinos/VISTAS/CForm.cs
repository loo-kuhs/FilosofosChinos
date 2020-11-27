using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilosofosChinos;
using FilosofosChinos.VISTAS;
using FilosofosChinos.FILOSOFOS;
using FilosofosChinos.OTROS;

namespace FilosofosChinos.VISTAS
{
    public class CForm
    {
        private Label[] label_F = new Label[5];
        private Label[] label_P = new Label[5];
        private static RichTextBox txtLog;
        private TextBox[] txt_C = new TextBox[5];
        public static int[] F_Conteo = new int[5];

        public CForm(
            Label label_Filo01, 
            Label label_Filo02, 
            Label label_Filo03, 
            Label label_Filo04,
            Label label_Filo05, 
            Label label_Pal01, 
            Label label_Pal02, 
            Label label_Pal03,
            Label label_Pal04, 
            Label label_Pal05, 
            RichTextBox txtRegistro,
            TextBox txtConteo01, 
            TextBox txtConteo02, 
            TextBox txtConteo03, 
            TextBox txtConteo04,
            TextBox txtConteo05)
        {

            this.label_F[0] = label_Filo01;
            this.label_F[1] = label_Filo02;
            this.label_F[2] = label_Filo03;
            this.label_F[3] = label_Filo04;
            this.label_F[4] = label_Filo05;

            this.label_P[0] = label_Pal01;
            this.label_P[1] = label_Pal02;
            this.label_P[2] = label_Pal03;
            this.label_P[3] = label_Pal04;
            this.label_P[4] = label_Pal05;

            CForm.txtLog = txtRegistro;

            this.txt_C[0] = txtConteo01;
            this.txt_C[1] = txtConteo02;
            this.txt_C[2] = txtConteo03;
            this.txt_C[3] = txtConteo04;
            this.txt_C[4] = txtConteo05;

            CForm.F_Conteo[0] = 0;
            CForm.F_Conteo[1] = 0;
            CForm.F_Conteo[2] = 0;
            CForm.F_Conteo[3] = 0;
            CForm.F_Conteo[4] = 0;
        }

        #region GETTER y SETTER

        public Label[] getlabel_F()
        {
            return label_F;
        }

        public Label[] getlabel_P()
        {
            return label_P;
        }

        public TextBox[] gettxt_C()
        {
            return txt_C;
        }

        public static RichTextBox gettxtLog()
        {
            return txtLog;
        }

        public static void settxtInfo(String mensaje)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() =>
                txtLog.AppendText(mensaje + "\n")));
            }
            if (txtLog.InvokeRequired)
                txtLog.Invoke(new Action(() => txtLog.ScrollToCaret()));
        }

        #endregion
    }
}

