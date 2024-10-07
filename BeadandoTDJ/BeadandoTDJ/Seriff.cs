using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Seriff : VarosElem
    {
        public List<(int, int)> latottPoziciok = new List<(int, int)>();
        
        public Seriff(string nev, int dmg, int hp, int arany)
        {
            this.nev = nev;
            this.dmg = dmg;
            this.hp = hp;
            this.arany = arany;
        }

        public void seriffKorulSzinez(VarosElem[,] matrix) {

            Varos varos = new Varos();
            int seriffSor = 0;
            int seriffOszlop = 0;
            int maxSor = matrix.GetLength(0);
            int maxOszlop = matrix.GetLength(1);


            for (int j = 0; j < maxSor; j++)
            {
                for (global::System.Int32 k = 0; k < maxOszlop; k++)
                {
                    if (matrix[j, k].nev == "S")
                    {
                        seriffSor = j;
                        seriffOszlop = k;
                        
                    }
                }
            }
            
            for (int i = 0; i< 25; i++)
            {
                for (int j = 0; j< 25; j++)
                {

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.White;

                    List<(int, int)> posiciok = varos.vanMelletteReturn(matrix, seriffSor, seriffOszlop);
                    foreach (var (x, y) in posiciok)
                    {
                        if (!latottPoziciok.Contains((x, y)))
                        {
                            latottPoziciok.Add((x, y));
                        }
                    }


                    // Ellenőrizzük, hogy a jelenlegi cella egyike-e a seriff melletti pozícióknak
                    if (latottPoziciok.Contains((i, j)))
                    {
                        //switch case: a zárójelben léve az eset a case
                        //pedig megnézi h az eset-e az, és akkor lefut és brake el kilép
                        switch (matrix[i, j].nev) 
                        {
                            case "G":
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case "B":
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                break;
                            case "W":
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case "V":
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                            case "E":
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case "A":
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                        }
                    }
                    

                    if (matrix[i, j].nev == "S")
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    Console.Write(matrix[i, j].nev);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            if (matrix[seriffSor, seriffOszlop] is Seriff seriff)
            {

                
                //while (!mozgatva) { }
                Console.WriteLine("Seriff\t\t" + seriff.dmg + " sebzés\t" + seriff.hp + " hp\t" + seriff.arany + " arany");
            }
        }

        Whiskey whiskey = new Whiskey("W", 0, 0, 0);
        Aranyrog aranyrog = new Aranyrog("A", 0, 0, 0);
        Varoshaza varoshaza = new Varoshaza("V", 0, 0, 0);

        public void mozgasSeriff(VarosElem[,] matrix)
        {
            int seriffSor = 0;
            int seriffOszlop = 0;
            Random rnd = new Random();
            int maxSor = matrix.GetLength(0);
            int maxOszlop = matrix.GetLength(1);


            for (int j = 0; j < maxSor; j++)
            {
                for (global::System.Int32 k = 0; k < maxOszlop; k++)
                {
                    if (matrix[j, k].nev == "S")
                    {
                        seriffSor = j;
                        seriffOszlop = k;

                    }
                }
            }

            if (matrix[seriffSor, seriffOszlop] is Seriff seriff)
            {
                varoshaza.VaroshazKeres(matrix, latottPoziciok);
                aranyrog.AranyrogKeres(matrix, latottPoziciok);
                if (seriff.hp<40)
                {
                    whiskey.whiskeyKeres(matrix,latottPoziciok);
                }

                bool mozgatva = false;
                while (!mozgatva)
                {
                    int ujSor = seriffSor + rnd.Next(-1, 2);
                    int ujOszlop = seriffOszlop + rnd.Next(-1, 2);


                    if (ujSor >= 0 && ujSor < maxSor && ujOszlop >= 0 && ujOszlop < maxOszlop)
                    {
                        if (matrix[ujSor, ujOszlop].nev == "A")  
                        {
                           
                            matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                            matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                            mozgatva = true; 
                            seriff.arany++;
                        }
                        if (matrix[ujSor, ujOszlop].nev == "G") 
                        {
                            
                            matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                            matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                            mozgatva = true; 
                        }
                    }

                }
            }
        }

        bool whiskeyKeresesben = false;

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

                    if (varos.vanMellette(matrix, banditaSor, banditaOszlop, "E"))
                    {
                        while (seriff.hp >= 0 && bandita.hp >= 0)
                        {

                            if (seriff.hp<=40 && !whiskeyKeresesben)
                            {
                                whiskeyKeresesben = true;
                                whiskey.whiskeyKeres(matrix,latottPoziciok);
                                return;
                            }
                            for (global::System.Int32 j = 0; j < 5; j++)
                            {
                                Console.Write("-");
                                Thread.Sleep(100);
                            }
                            bandita.hp -= seriff.dmg;
                            Console.Write(">Banditahp" + bandita.hp);
                            if (bandita.hp <= 0)
                            {
                                matrix[banditaSor, banditaOszlop] = new VarosElem() { nev = "G" };
                                seriff.arany += bandita.arany;
                                bandita.arany = 0;
                                Console.WriteLine("Seriff nyert");
                                break;
                            }
                            seriff.hp -= bandita.dmg;
                            if (seriff.hp <= 0)
                            {
                                Console.WriteLine("Seriff halott játék vége.");
                                Console.ReadLine();
                                System.Environment.Exit(0);
                            }
                            
                            Console.Write(">Seriff" + seriff.hp);
                        }
                        

                    }
                }

            }
        }

    }
}
