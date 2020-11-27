using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilosofosChinos.OTROS;
using FilosofosChinos.VISTAS;
using FilosofosChinos.LOGICA;

namespace FilosofosChinos.FILOSOFOS
{
    public class CFilosofo
    {
        private Random r = new Random();

        private int idFilosofo; //Identificador del Filosofo
        private CPalillo izq, der; //Identificador de cada palillo
        private CMonitoMesa comensal; 
        private Label label_F, label_P_der, label_P_izq; //Elementos graficos
        private CLog log; //Escribir en los registros
        private TextBox txt_C; //Contador de comidas

        public static Boolean finalizado = false;

        private object locker = new object();
        public CFilosofo(
            int idFilosofo, CPalillo der, 
            CPalillo izq, CMonitoMesa comensal,
            Label label_F, Label label_P_der, 
            Label label_P_izq, CLog log, TextBox txt_C)
        {
            //Asignacion de variables compartidas
            this.idFilosofo = idFilosofo;
            this.der = der;
            this.izq = izq;
            this.comensal = comensal;
            this.label_F = label_F;
            this.label_P_der = label_P_der;
            this.label_P_izq = label_P_izq;
            this.log = log; // O puede ser null, de esta forma no escribiria registros
            this.txt_C = txt_C;
        }
        public void Run()
        {
            lock (locker)
            {
                while (true)
                {
                    //Monitor.Enter(locker);
                    try
                    {
                        comensal.tomarComensal(idFilosofo, log);
                        this.label_F.BackColor = Color.Pink;

                        der.tomarPalillo(idFilosofo, log);
                        this.label_F.BackColor = Color.Cyan;
                        this.label_P_der.BackColor = Color.Blue;

                        if (!izq.tomarPalilloIzq(idFilosofo, log))
                        {
                            if (CForm.gettxtLog() != null)
                                log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " tendra que soltar el palillo " + (idFilosofo + 1) + " por exceso de tiempo y salir a pensar. ");

                            der.soltarPalillo(idFilosofo, log);
                            this.label_P_der.BackColor = Color.Red;

                            comensal.soltarComensal(idFilosofo, log);
                            if (CForm.gettxtLog() != null)
                                log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " esta pensando. ");
                            try
                            {
                                //Thread.Sleep(1000 + 100);
                                Monitor.Wait(locker, r.Next(1000) + 100);
                            }
                            catch (ThreadInterruptedException ex)
                            {
                                if (CForm.gettxtLog() != null)
                                    log.doRegistros("\n\n Error. Descripcion: " + ex.ToString() + "\n\n");
                            }
                            //Monitor.Pulse(locker);
                            continue;
                        }

                        this.label_P_izq.BackColor = Color.Blue;
                        this.label_F.BackColor = Color.Orange;
                        this.label_F.ForeColor = Color.Blue;

                        txt_C.Text = (" " + (++CForm.F_Conteo[idFilosofo]));
                        if (CForm.gettxtLog() != null)
                            log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " esta comiendo. ");
                        try
                        {
                            //Thread.Sleep(1000 + 500);
                            Monitor.Wait(locker, r.Next(1000 + 500));
                        }
                        catch (ThreadInterruptedException ex)
                        {
                            if (CForm.gettxtLog() != null)
                                log.doRegistros("\n\n Error. Descripcion: " + ex.ToString() + "\n\n");
                        }

                        this.label_F.BackColor = Color.Green;
                        this.label_F.ForeColor = Color.Black;

                        izq.soltarPalillo(idFilosofo, log);
                        this.label_P_izq.BackColor = Color.LightGray;

                        der.soltarPalillo(idFilosofo, log);
                        this.label_P_der.BackColor = Color.LightGray;

                        comensal.soltarComensal(idFilosofo, log);
                        if (CForm.gettxtLog() != null)
                            log.doRegistros(" El Filosofo " + (idFilosofo + 1) + " esta pensando. ");
                        try
                        {
                            //Thread.Sleep(1000 + 100);
                            Monitor.Wait(locker, r.Next(1000) + 100);
                        }
                        catch (ThreadInterruptedException ex)
                        {
                            if (CForm.gettxtLog() != null)
                                log.doRegistros("\n\n Error. Descripcion: " + ex.ToString() + "\n\n");
                        }
                    }
                    catch (ThreadInterruptedException ex)
                    {
                        if (CForm.gettxtLog() != null)
                            log.doRegistros("\n\n Error. Descripcion: " + ex.ToString() + "\n\n");
                    }

                    if (finalizado)
                    {
                        break;
                    }
                }
            }

            try
            {
                if (CForm.gettxtLog() != null)
                    log.doRegistros(" La cena ha terminado para todos: El Filosofo " + (idFilosofo + 1) + " se ha puesto a pensar. \n\nPulsar INICIAR para continuar. \n\n");

            }
            catch (ThreadInterruptedException ex)
            {
                if (CForm.gettxtLog() != null)
                    log.doRegistros("\n\n Error. Descripcion: " + ex.ToString() + "\n\n");
            }
        }
    }
}
