using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FilosofosChinos.VISTAS;
using FilosofosChinos.FILOSOFOS;
using FilosofosChinos.OTROS;
using FilosofosChinos.LOGICA;

namespace FilosofosChinos.LOGICA
{
    public class CPrincipal
    {
        private Label[] label_F = new Label[5];
        private Label[] label_P = new Label[5];
        private TextBox[] txt_C = new TextBox[5];
        private Thread[] threads = new Thread[5];
        public CPrincipal(CForm cForm)
        {
            this.label_F = cForm.getlabel_F();
            this.label_P = cForm.getlabel_P();
            this.txt_C = cForm.gettxt_C();

            CPalillo[] palillo = new CPalillo[5];
            CFilosofo[] filosofo = new CFilosofo[5];
            CMonitoMesa comensal = new CMonitoMesa();
            CLog log = new CLog();

            //CExcepciones excepciones = new CExcepciones(log);

            for (int i = 0; i < palillo.Length; i++)
            {
                palillo[i] = new CPalillo(i);
            }

            //for (int t = 0; t < threads.Length; t++)
            //{

            //    threads[t].Start();
            //}

            for (int i = 0; i < filosofo.Length; i++)
            {
                filosofo[i] = new CFilosofo(i, palillo[i], palillo[(i + 1) % 5],
                    comensal, label_F[i], label_P[i], label_P[(i + 1) % 5], log,
                    txt_C[i]);            
            }

            for (int i = 0; i < filosofo.Length; i++)
            {
                //filosofo[i].Run();
                for (int t = 0; t < threads.Length; t++)
                {
                    threads[t] = new Thread(new ThreadStart(filosofo[i].Run));
                    threads[t].Start();
                }
                //Thread t = new Thread(filosofo[i].Run);
                //t.Start();
            }
        }
    }
}
