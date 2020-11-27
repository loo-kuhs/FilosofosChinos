using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilosofosChinos;
using FilosofosChinos.VISTAS;
using FilosofosChinos.LOGICA;

namespace FilosofosChinos.OTROS
{
    public class CLog
    {
        public void doRegistros(String log)
        {
            lock (this)
            {
                CForm.settxtInfo(log);
            }  
        }
    }
}
