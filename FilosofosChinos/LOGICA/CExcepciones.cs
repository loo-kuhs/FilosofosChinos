using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilosofosChinos.OTROS;
using FilosofosChinos.VISTAS;

namespace FilosofosChinos.LOGICA
{
    public class CExcepciones : Exception
    {
        public void uncaughtException (Thread t, Exception e)
        {
            MessageBox.Show("Thread que lanzo la excepcion: {0} \n", t.Name);

            if (CForm.gettxtLog() != null)
            {
                try
                {
                    log.doRegistros("\n Thread que lanzo la excepcion: " + t.Name + "\n");
                    log.doRegistros(e.ToString() + "\n\n");
                }
                catch (ThreadInterruptedException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public CExcepciones(CLog log)
        {
            this.log = log;
        }

        CLog log;
    }
}
