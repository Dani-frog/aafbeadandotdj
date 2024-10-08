using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Whiskey : VarosElem
    {
        public Whiskey(string nev, int dmg, int hp, int arany)
        {
            this.nev = nev;
            this.dmg = dmg;
            this.hp = hp;
            this.arany = arany;
        }

        public void whiskeyKeres(VarosElem[,] matrix, List<(int,int)> pos)
        {
            int seriffSor = 0;
            int seriffOszlop = 0;
            Random rnd = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j].nev=="S")
                    {
                        seriffSor = i;
                        seriffOszlop = j;
                    }
                }
            }
            if (matrix[seriffSor,seriffOszlop] is Seriff seriff)
            {
                foreach (var (x, y) in pos)
                {
                    if (matrix[x, y].nev == "W")
                    {
                        Console.WriteLine("Van található Whiskey");
                        //while () { } !mozgatva, vagy míg seriffSor<x && seriffSor<y (a <> megfordul attól
                        //függően h nagyobb e, amit meg kell nézni ez elött) és minden lépés után ki kell rajozlin
                        //seriff.seriffKorulSzinez(matrix);
                        int ujSor = seriffSor;
                        int ujOszlop = seriffOszlop;
                        int maxSor = matrix.GetLength(0);
                        int maxOszlop = matrix.GetLength(1);
                        while (matrix[x, y].nev == "W")
                        {

                            if (ujSor<x)
                                ujSor++;
                            if (ujOszlop<y)
                                ujOszlop++;
                            if (ujSor>x)
                                ujSor--;
                            if (ujOszlop > y)
                                ujOszlop--;

                            if (matrix[ujSor, ujOszlop].nev == "B" && ujSor<x)
                                ujSor ++;
                            if (matrix[ujSor, ujOszlop].nev == "B" && ujSor > x)
                                ujSor--;
                            Console.WriteLine("Keres...");
                            if (matrix[ujSor, ujOszlop].nev == "A") 
                            {
                                matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                                matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                                seriffSor = ujSor; 
                                seriffOszlop = ujOszlop;
                                seriff.arany++;
                            }
                            if (matrix[ujSor, ujOszlop].nev == "G")
                            {

                                matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                                matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                                seriffSor = ujSor; 
                                seriffOszlop = ujOszlop;
                            }
                            if (matrix[ujSor, ujOszlop].nev == "W")
                            {

                                matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                                matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                                seriffSor = ujSor; 
                                seriffOszlop = ujOszlop;
                                seriff.hp += 50;

                                bool whiskeyElhelyezve = false;
                                while (!whiskeyElhelyezve)
                                {
                                    int randomSor = rnd.Next(0, maxSor);
                                    int randomOszlop = rnd.Next(0, maxOszlop);

                                    if (matrix[randomSor, randomOszlop].nev == "G")
                                    {
                                        matrix[randomSor, randomOszlop] = new VarosElem() { nev = "W" };
                                        whiskeyElhelyezve = true;
                                        //Console.WriteLine($"Whiskey új helyen: ({randomSor}, {randomOszlop})");
                                    }
                                }
                                return;
                            }
                            
                            
                        }
                        seriff.seriffKorulSzinez(matrix);

                    }
                }
            }
            

        }
    }
}
