using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Vallejos_Joaquin_Fundamentos2_Parcial1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Jugador Arisen = new Jugador();

            ListaEnemigos cola = new ListaEnemigos();
            for (int i = 0; i < 6; i++)
                cola.Agregar(GenerarEnemigo(Arisen.verNivel()));

            while (Arisen.verVida() > 0 && Arisen.verNivel() < 10)
            {
                Monstruo Enemigo = cola.Frente();
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("========================================");
                Console.WriteLine(" ");
                Console.WriteLine("NIVEL DEL ARISEN: " + Arisen.verNivel() + ("| VIDA: " + Arisen.verVida()));
                Console.WriteLine(" ");
                Console.WriteLine("ATK: " + Arisen.verAtaque() + ("| DEF: " + Arisen.verDefensa()));
                Console.WriteLine(" ");
                Console.WriteLine("========================================");
                Console.WriteLine(" ");
                Console.WriteLine("UN " + Enemigo.Nombre() + " ENEMIGO!");
                Console.WriteLine(" ");
                Console.WriteLine("NIVEL: " + Enemigo.verNivel() + "| VIDA: " + Enemigo.verVida());
                Console.WriteLine(" ");
                Console.WriteLine("ATK: " + Enemigo.verAtaque() + ("| DEF: " + Enemigo.verDefensa()));
                Console.WriteLine(" ");
                Console.WriteLine("========================================");
                Console.WriteLine(" ");
                Console.WriteLine("1) ATACAR  |  2) USAR OBJETO");
                string opcion = Console.ReadLine();
                if (opcion == "1")
                    Arisen.Atacar(Enemigo);
                else if (opcion == "2")
                    Arisen.usarPocion();
                else
                {
                    Console.WriteLine("NO PUEDO HACER ESO");
                    continue;
                }

                if (!Enemigo.vivo())
                {
                    Console.WriteLine("DERROTASTE AL " + Enemigo.Nombre());
                    Pocion drop = Enemigo.GenerarDrop();
                    if (drop != null) Arisen.agregarPocion(drop);
                    Arisen.SubirNivel();

                    cola.Sacar();
                    cola.Agregar(GenerarEnemigo(Arisen.verNivel()));
                }
                else
                {
                    Arisen.sufrirDañoJ(Enemigo.verAtaque());
                }
            }
            if (Arisen.verVida() > 0)
                Console.WriteLine("GAME OVER | LOGRASTE VENCER A LAS HORDAS");
            else 
                Console.WriteLine("GAME OVER | HAS SIDO DERROTADO");

            Console.ReadLine();
        }

        static Monstruo GenerarEnemigo(int nivelMaximo)
        {
            int nivel = RNG.r.Next(1, nivelMaximo + 1);

            int tipo = RNG.r.Next(3);
            if (tipo == 0) return new Orco(nivel);
            if (tipo == 1) return new Espectro(nivel);
            return new Goblin(nivel);
        }
        
    }

    //Arisen
    class Jugador
    {
        int nivel, vida, ataque, defensa;
        int bonusAtaque, bonusDefensa;

        public Jugador()
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
        public void SubirNivel()
        {
            nivel = nivel + 1;
            ActualizarStats();
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

        public void Atacar(Monstruo enemigo)
        {
            int DMG = Combate.calcularDaño(ataque, enemigo.verDefensa());
            bonusAtaque = 0;
            enemigo.sufrirDaño(DMG);
            Console.WriteLine("ATACAS AL " + enemigo.Nombre() + ", ¡¡INFLINGES " + DMG + " DE DAÑO!!");
        }
        public void sufrirDañoJ(int ataqueEnemigo)
        {
            int DMG = Combate.calcularDaño(ataqueEnemigo, defensa);
            bonusDefensa = 0;
            vida = vida - DMG;
            Console.WriteLine("RECIBES " + DMG + " DE DAÑO");
        
        }

        Inventario mochila = new Inventario();
        public void Curar(int cantidad)
        {
        vida = vida + cantidad;
        }
        public void BonusATQ(int cantidad)
        {
            bonusAtaque = bonusAtaque + cantidad;
        }
        public void BonusDEF(int cantidad)
        {
            bonusDefensa = bonusDefensa + cantidad;
        }
        public void agregarPocion(Pocion p)
        {
            if (mochila.Push(p))
                Console.WriteLine("GUARDASTE " + p.Nombre());
            else
                Console.WriteLine("MOCHILA LLENA, PERDISTE " + p.Nombre());
        }
        public void usarPocion()
        {
            Pocion p = mochila.Pop();
            if (p == null)
            {
                Console.WriteLine("NO TIENES POCIONES");
                return;
            }
            Console.WriteLine("USASTE " + p.Nombre());
            p.Aplicar(this);
        }
    }

    //POCIONES
    abstract class Pocion
    {
        protected int nivel;
        public Pocion(int nivel)
        {
            this.nivel = nivel;
        }
        public abstract string Nombre();
        public abstract void Aplicar(Jugador j);
    }

    //P VIDA
    class PocionVida : Pocion
    {
        public PocionVida(int nivel) : base(nivel) { }
        public override string Nombre()
        {
            return "POCION DE SALUD NV " + nivel;
        }
        public override void Aplicar(Jugador j)
        {
            j.Curar(5 * nivel);
        }
    }
    //P ATAQUE
    class PocionAtaque : Pocion
    {
        public PocionAtaque(int nivel) : base(nivel) { }
        public override string Nombre()
        {
            return "POCION DE ATAQUE NV " + nivel;
        }
        public override void Aplicar(Jugador j)
        {
            j.BonusATQ(3 * nivel);
        }
    }
    //P DEFENSA
    class PocionDefensa : Pocion
    {
        public PocionDefensa(int nivel) : base(nivel) { }
        public override string Nombre()
        {
            return "POCION DE DEFENSA NV " + nivel;
        }
        public override void Aplicar(Jugador j)
        {
            j.BonusDEF(3 * nivel);
        }
    }

    //INVENTARIO
    class Inventario
    {
        List<Pocion> datos = new List<Pocion>();
        const int max = 5;

        public bool Push(Pocion p)
        {
            if (datos.Count >= max) 
            return false;
            datos.Add(p);
            return true;
        }
        public Pocion Pop() 
        {
            if (datos.Count == 0) return null;
            Pocion p = datos[datos.Count - 1];
            datos.RemoveAt(datos.Count - 1);
            return p;
        }
    }

    //ENEMIGOS
    abstract class Monstruo
    {
        protected int nivel, vida, ataque, defensa;

        public Monstruo(int nivel, int vida, int ataque, int defensa)
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

        public virtual Pocion GenerarDrop()
        {
            return null;
        }
    }

    //Orquito
    class Orco : Monstruo
    {
        public Orco(int nivel) : base(nivel, 5 * nivel, 2 * nivel, 2 * nivel) { }
        public Orco(int nivel, int vida, int ataque, int defensa) : base(nivel, vida, ataque, defensa) { }
        public override string Nombre()
        {
            return ("ORCO");
        }
        public override Pocion GenerarDrop()
        {
            if (RNG.r.Next(100) < 50)
                return new PocionVida(nivel);
            return null;
        }
    }

    //Fantasmon
    class Espectro : Monstruo
    {
        public Espectro(int nivel) : base(nivel, 2 * nivel, 5 * nivel, 2 * nivel) { }
        public Espectro(int nivel, int vida, int ataque, int defensa) : base(nivel, vida, ataque, defensa) { }
        public override string Nombre()
        {
            return ("EsPeCtRo");
        }
        public override Pocion GenerarDrop()
        {
            if (RNG.r.Next(100) < 50)
                return new PocionAtaque(nivel);
            return null;
        }
    }

    //Duendecito
    class Goblin : Monstruo
    {
        public Goblin(int nivel) : base(nivel, 3 * nivel, 3 * nivel, 2 * nivel) { }
        public Goblin(int nivel, int vida, int ataque, int defensa) : base(nivel, vida, ataque, defensa) { }
        public override string Nombre()
        {
            return ("Goblin...");
        }
        public override Pocion GenerarDrop()
        {
            if (RNG.r.Next(100) < 50)
                return new PocionDefensa(nivel);
            return null;
        }
    }

    class ListaEnemigos
    {
        List<Monstruo> datos = new List<Monstruo>();
        public void Agregar(Monstruo m)
        {
            datos.Add(m);
        }
        public int cantidad()
        {
            return datos.Count;
        }

        public Monstruo Frente()
        {
            if (datos.Count == 0) return null;
            return datos[0];
        }
        public Monstruo Segundo()
        {
            if (datos.Count < 2) return null;
            return datos[1];
        }
        public Monstruo Sacar()
        {
            if (datos.Count == 0) return null;
            Monstruo m = datos[0];
            datos.RemoveAt(0);
            return m;
        }
        public void MoverFrente(Monstruo m)
        {
            datos.Insert(0, m);
        }
    }
    //FLUJO DEL JUEGO
    static class Combate
    {
        public static int calcularDaño (int ataqueC, int defensaC)
        {
            int DMG = ataqueC - defensaC;
            if (DMG < 1) DMG = 1;
            return DMG;
        }
    }
    static class RNG
    {
        public static Random r = new Random();
    }
}
