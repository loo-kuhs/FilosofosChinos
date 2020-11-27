using System.Threading;
using FilosofosChinos.VISTAS;
using FilosofosChinos.OTROS;

namespace FilosofosChinos.FILOSOFOS
{
    public class CMonitoMesa
    {
        private int comensal = 4;
        private static object locker = new object();

        public virtual void tomarComensal(int idFilosofo, CLog log)
        {
            lock (this)
            {
                while (comensal == 0)
                {
                    Monitor.Wait(this);
                    
                }
                if (CForm.gettxtLog() != null)
                    log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " es el comensal " + comensal);
                comensal--;
            }
        }

        public virtual void soltarComensal(int idFilosofo, CLog log)
        {
            lock (this)
            {
                comensal++;
                if (CForm.gettxtLog() != null)
                    log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " ya NO es el comensal " + comensal);
                Monitor.Pulse(this);
            }
        }
    }
}
