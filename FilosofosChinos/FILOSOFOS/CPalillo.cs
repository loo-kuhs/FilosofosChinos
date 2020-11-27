using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FilosofosChinos.OTROS;
using FilosofosChinos.VISTAS;
using FilosofosChinos.LOGICA;

namespace FilosofosChinos.FILOSOFOS
{
    public class CPalillo
    {
        private Random r = new Random();
        private int idPalillo;
        private bool Libre = true;

        private static object locker = new object();
        //public int IdPalillo { get => idPalillo; set => idPalillo = value; }
        public CPalillo(int idPalillo)
        {
            this.idPalillo = idPalillo;
        }

        public void tomarPalillo(int idFilosofo, CLog log)
        {
            lock (locker)
            {
                while (!Libre)
                {
                    Monitor.Wait(locker);
                }
                if (CForm.gettxtLog() != null)
                    log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " toma el palillo " + (idPalillo + 1));
                Libre = false;
            }
        }

        public bool tomarPalilloIzq(int idFilosofo, CLog log)
        {
            lock (locker)
            {     
                while (!Libre)
                {
                    Monitor.Wait(locker, r.Next(1000) + 500);
                    return false;
                }

                if (CForm.gettxtLog() != null)
                    log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " tomo el palillo " + (idPalillo + 1));
                Libre = false;
                return true;
            }
        }

        public void soltarPalillo(int idFilosofo, CLog log)
        {
            lock (locker)
            {
                Libre = true;

                if (CForm.gettxtLog() != null)
                    log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " suelta el palillo " + (idPalillo + 1));
                Monitor.Pulse(locker);
            }
        }
    }
}
