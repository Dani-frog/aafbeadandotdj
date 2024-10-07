using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Bandita : VarosElem
    {

        public Bandita(string nev, int dmg, int hp, int arany)
        {
            this.nev = nev;
            this.dmg = dmg;
            this.hp = hp;
            this.arany = 0;
        }
        
        public void mozgasBanditakat(VarosElem[,] matrix)
        {
            List<string> pozicioEnemy = new List<string>();
            Random rnd = new Random();
            int maxSor = matrix.GetLength(0);
            int maxOszlop = matrix.GetLength(1);
            for (int j = 0; j < maxSor; j++)
            {
                for (global::System.Int32 k = 0; k < maxOszlop; k++)
                {
                    if (matrix[j,k].nev=="E")
                    {
                        pozicioEnemy.Add(j + ";" + k);
                    }
                }
            }
            int i = 0;
            foreach (var item in pozicioEnemy)
            {

                string[] pozicioxy = pozicioEnemy[i].Split(';');
                i++;
                int banditaSor = Convert.ToInt32(pozicioxy[0]);
                int banditaOszlop = Convert.ToInt32(pozicioxy[1]);


                if (matrix[banditaSor, banditaOszlop] is Bandita bandita)
                {

                    bool mozgatva = false;

                    while (!mozgatva)
                    {
                        int ujSor = banditaSor + rnd.Next(-1, 2);
                        int ujOszlop = banditaOszlop + rnd.Next(-1, 2);


                        if (ujSor >= 0 && ujSor < maxSor && ujOszlop >= 0 && ujOszlop < maxOszlop)
                        {
                            if (matrix[ujSor, ujOszlop].nev == "A") // Ha az új hely üres 
                            {
                                // Cseréljük ki a banditát az új pozícióba
                                matrix[ujSor, ujOszlop] = matrix[banditaSor, banditaOszlop];
                                matrix[banditaSor, banditaOszlop] = new VarosElem() { nev = "G" };
                                mozgatva = true; // Mozgatás sikeres
                                bandita.arany++;
                            }
                            if (matrix[ujSor, ujOszlop].nev == "G") // Ha az új hely üres 
                            {
                                // Cseréljük ki a banditát az új pozícióba
                                matrix[ujSor, ujOszlop] = matrix[banditaSor, banditaOszlop];
                                matrix[banditaSor, banditaOszlop] = new VarosElem() { nev = "G" };
                                mozgatva = true; // Mozgatás sikeres
                            }
                        }

                    }

                    Console.Clear();
                }
            }
        }

        public void parbaj(VarosElem[,] matrix, int ellenfelsor, int ellenfeloszlop) 
        { 

            Varos varos = new Varos();
            List<string> pozicioEnemy = new List<string>();
            int maxSor = matrix.GetLength(0);
            int maxOszlop = matrix.GetLength(1);
            int i = 0;
            for (int j = 0; j < maxSor; j++)
            {
                for (global::System.Int32 k = 0; k < maxOszlop; k++)
                {
                    if (matrix[j, k].nev == "E")
                    {
                        pozicioEnemy.Add(j + ";" + k);
                    }
                }
            }

            foreach (var item in pozicioEnemy)
            {
                string[] pozicioxy = pozicioEnemy[i].Split(';');
                i++;
                int banditaSor = Convert.ToInt32(pozicioxy[0]);
                int banditaOszlop = Convert.ToInt32(pozicioxy[1]);
                if (matrix[banditaSor, banditaOszlop] is Bandita bandita && matrix[ellenfelsor, ellenfeloszlop] is Seriff seriff)
                {
                    
                    if (varos.vanMellette(matrix, banditaSor, banditaOszlop, "S"))
                    {
                        while (seriff.hp >= 0 && bandita.hp >=0)
                        {
                            
                            for (global::System.Int32 j = 0; j < 2; j++)
                            {
                                Console.Write("-");
                                Thread.Sleep(100);
                            }
                            seriff.hp -= bandita.dmg;
                            if (seriff.hp <= 0)
                            {
                                Console.WriteLine("Seriff halott játék vége.");
                                Console.ReadLine();
                                System.Environment.Exit(0);
                            }
                            Console.Write(">Sheriffhp:"+ seriff.hp);
                            bandita.hp -= seriff.dmg;
                            if (bandita.hp <= 0)
                            {
                                matrix[banditaSor, banditaOszlop] = new VarosElem() { nev = "G" };
                                seriff.arany += bandita.arany;
                                bandita.arany = 0;
                                Console.WriteLine("Seriff nyert");
                                Thread.Sleep(300);
                                break;
                            }
                            
                            
                            Console.Write(">Banditahp:" + bandita.hp);
                        }
                        
                        
                    }   
                }
                
            }
        }
        
    }
}
