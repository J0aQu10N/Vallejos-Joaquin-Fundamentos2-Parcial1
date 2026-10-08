using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vallejos_Joaquin_Fundamentos2_Parcial1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            jugador cbllr = new jugador();
            Console.WriteLine("NIVEL " + cbllr.verNivel());
            Console.WriteLine("VIDA " + cbllr.verVida());
            Console.WriteLine("ATK " + cbllr.verAtaque());
            Console.WriteLine("DEF " + cbllr.verDefensa());

            cbllr.SubirNivel();
            Console.WriteLine("=====SUBISTE DE NIVEL=====");
            Console.WriteLine("NIVEL++ " + cbllr.verNivel());
            Console.WriteLine("VIDA++ " + cbllr.verVida());

            ///////////////////////////////////////////////////

            Orco orc = new Orco(1);
            Console.WriteLine("¡¡¡ORCO!!!");
            Console.WriteLine("NIVEL " + orc.verNivel());
            Console.WriteLine("VIDA " + orc.verVida());
            Console.WriteLine("ATK " + orc.verAtaque());
            Console.WriteLine("DEF " + orc.verDefensa());

            orc.sufrirDaño(3);
            Console.WriteLine("DESPUÉS DE RECIBIR DAÑO " + orc.verVida());
            Console.WriteLine("SIGUE VIVA??? " + orc.vivo());

            ///////////////////////////////////////////////////

            Espectro sptr = new Espectro(1);
            Console.WriteLine("~<uN fAnTaSmA>~");
            Console.WriteLine("NIVEL " + sptr.verNivel());
            Console.WriteLine("VIDA " + sptr.verVida());
            Console.WriteLine("ATK " + sptr.verAtaque());
            Console.WriteLine("DEF " + sptr.verDefensa());

            sptr.sufrirDaño(3);
            Console.WriteLine("DESPUÉS DE RECIBIR DAÑO " + sptr.verVida());
            Console.WriteLine("SIGUE VIVA??? " + sptr.vivo());

            ///////////////////////////////////////////////////

            Goblin gbln = new Goblin(1);
            Console.WriteLine("Un duende...");
            Console.WriteLine("NIVEL " + gbln.verNivel());
            Console.WriteLine("VIDA " + gbln.verVida());
            Console.WriteLine("ATK " + gbln.verAtaque());
            Console.WriteLine("DEF " + gbln.verDefensa());

            gbln.sufrirDaño(3);
            Console.WriteLine("DESPUÉS DE RECIBIR DAÑO " + gbln.verVida());
            Console.WriteLine("SIGUE VIVA??? " + gbln.vivo());

            Console.ReadLine();
        }
    }

    //CABALLERO
    class jugador
    {
        int nivel, vida, ataque, defensa;

        public jugador()
        {
            nivel = 1;
            ActualizarStats();
        }
        void ActualizarStats()
        {
            vida = 15 * nivel;
            ataque = 3 * nivel;
            defensa = 3 * nivel;
        }
        public void SubirNivel ()
        {
            nivel = nivel +1;
            ActualizarStats ();
        }
        public int verNivel()
        {
            return nivel;
        }
        public int verVida()
        {
            return vida;
        }
        public int verAtaque()
        {
            return ataque;
        }
        public int verDefensa()
        {
            return defensa;
        }
    }
    //ENEMIGOS
    abstract class monstruo
    {
        protected int nivel, vida, ataque, defensa;

        public monstruo(int nivel, int vida, int ataque, int defensa)
        {
            this.nivel = nivel;
            this.vida = vida;
            this.ataque = ataque;
            this.defensa = defensa;
        }
        public int verNivel()
        {
            return nivel;
        }
        public int verVida()
        {
            return vida;
        }
        public int verAtaque()
        {
            return ataque;
        }
        public int verDefensa()
        {
            return defensa;
        }
        public bool vivo()
        {
            return vida > 0;
        }
        public void sufrirDaño(int DMG)
        {
            vida -= DMG;
        }
        public abstract string Nombre ();
    }
    //Orquito
    class Orco : monstruo
    {
        public Orco(int nivel) : base(nivel, 5 * nivel, 2 * nivel, 2 * nivel) { }
        public Orco(int nivel, int vida, int ataque, int defensa) : base(nivel, vida, ataque, defensa) { }
        public override string Nombre()
        {
            return ("Orco");
        }
    }
    //Fantasmon
    class Espectro : monstruo
    {
        public Espectro(int nivel) : base(nivel, 2 * nivel, 5 * nivel, 2 * nivel) { }
        public Espectro(int nivel, int vida, int ataque, int defensa) : base(nivel, vida, ataque, defensa) { }
        public override string Nombre()
        {
            return ("espectro");
        }
    }
    //Duendecito
    class Goblin : monstruo
    {
        public Goblin(int nivel) : base(nivel, 3 * nivel, 3 * nivel, 2 * nivel) { }
        public Goblin(int nivel, int vida, int ataque, int defensa) : base(nivel, vida, ataque, defensa) { }
        public override string Nombre()
        {
            return ("Goblin");
        }
    }
}
