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
            jugador jugador = new jugador();
            Console.WriteLine("NIVEL " + jugador.verNivel());
            Console.WriteLine("VIDA " + jugador.verVida());
            Console.WriteLine("ATK " + jugador.verAtaque());
            Console.WriteLine("DEF " + jugador.verDefensa());

            jugador.SubirNivel();
            Console.WriteLine("=====SUBISTE DE NIVEL=====");
            Console.WriteLine("NIVEL++ " + jugador.verNivel());
            Console.WriteLine("VIDA++ " + jugador.verVida());

            Console.ReadLine();
        }
    }

    //jugador
    class jugador
    {
        int nivel;
        int vida;
        int ataque;
        int defensa;

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
            nivel = nivel + 1;
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
}
