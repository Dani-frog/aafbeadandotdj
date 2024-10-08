using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Varoshaza : VarosElem
    {
        public Varoshaza(string nev, int dmg, int hp, int arany)
        {
            this.nev = nev;
            this.dmg = dmg;
            this.hp = hp;
            this.arany = arany;
        }
        public void VaroshazKeres(VarosElem[,] matrix, List<(int, int)> pos)
        {
            int seriffSor = 0;
            int seriffOszlop = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].nev == "S")
                    {
                        seriffSor = i;
                        seriffOszlop = j;
                    }
                }
            }

            if (matrix[seriffSor, seriffOszlop] is Seriff seriff && seriff.arany >= 4)
            {
                Console.WriteLine("A seriff elérte a 4 aranyat, megkeresi a Városházát.");
                foreach (var (x, y) in pos)
                {
                    if (matrix[x, y].nev == "V")
                    {
                        Console.WriteLine("Városháza megtalálva.");
                        int ujSor = seriffSor;
                        int ujOszlop = seriffOszlop;
                        int maxSor = matrix.GetLength(0);
                        int maxOszlop = matrix.GetLength(1);
                        while (matrix[x, y].nev == "V")
                        {
                            if (ujSor < x)
                                ujSor++;
                            if (ujOszlop < y)
                                ujOszlop++;
                            if (ujSor > x)
                                ujSor--;
                            if (ujOszlop > y)
                                ujOszlop--;

                            if (matrix[ujSor, ujOszlop].nev == "A")
                            {
                                matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                                matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                                seriffSor = ujSor;
                                seriffOszlop = ujOszlop;
                                seriff.arany++;
                            }
                            else if (matrix[ujSor, ujOszlop].nev == "G")
                            {
                                matrix[ujSor, ujOszlop] = matrix[seriffSor, seriffOszlop];
                                matrix[seriffSor, seriffOszlop] = new VarosElem() { nev = "G" };
                                seriffSor = ujSor;
                                seriffOszlop = ujOszlop;
                            }
                            else if (matrix[ujSor, ujOszlop].nev == "V")
                            {
                                Console.WriteLine("A seriff elérte a Városházát!");
                                Console.ReadLine();
                                System.Environment.Exit(0);
                                
                            }
                            seriff.seriffKorulSzinez(matrix);
                        }
                    }
                }
            }
        }
    }
}
