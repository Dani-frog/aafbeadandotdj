using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Varos: VarosElem
    {
        Bandita bandita = new Bandita("E",0,0,0);
        Seriff seriff = new Seriff("S",0,0,0);

        public bool jatekvege = false;
        public VarosElem[,] objectArray = new VarosElem[25, 25];
        public string pozicioSheriff = "";
        public List<string> pozicioEnemy = new List<string>();
        static Random rnd = new Random();
        public void matrixFeltolt()
        {

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    objectArray[i, j] = new Ground("G", 0, 0,0);
                }
            } 
            randomFeltolt();
        }

        public void randomFeltolt()
        {
            int randomSzam = rnd.Next(0, 25);
            int randomSzam2 = rnd.Next(0, 25);
            int randomDMG = rnd.Next(20, 35);
            barikadFeltolt();
            mindenmasFeltolt();
            
        }

 
        public void barikadFeltolt()
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                int randomSzam = rnd.Next(0, 25);
                int randomSzam2 = rnd.Next(0, 25);

                while (vanMellette(objectArray,randomSzam,randomSzam2,"B"))
                {
                    randomSzam = rnd.Next(0, 25);
                    randomSzam2 = rnd.Next(0, 25);
                }
                objectArray[randomSzam, randomSzam2] = new Barikad("B", 0, 0, 0);
            }
           
        }

        public void mindenmasFeltolt()
        {

            int randomdmgSheriff = rnd.Next(20, 35);
            

            
            feltoltClassRandom(new Whiskey("W",0,0,0),10);
            feltoltClassRandom(new Aranyrog("A",0,0,0),5);
            for (int i = 0; i < 4; i++)
            {
                int randomdmgBandita = rnd.Next(4, 15);
                feltoltClassRandom(new Bandita("E", randomdmgBandita, 100, 0), 1); // E = enemyy
            }
            feltoltClassRandom(new Seriff("S", randomdmgSheriff, 1000,4),1);
            feltoltClassRandom(new Varoshaza("V",0,0,0),1);
        }

        public void feltoltClassRandom(VarosElem varoselem,int db)
        {
            
            int randomfeltolt1 = rnd.Next(0, 25);
            int randomfeltolt2 = rnd.Next(0, 25);


            int i = 0;

            while (i<db)
            {
                if (objectArray[randomfeltolt1, randomfeltolt2].nev == "G")
                {
                    
                    objectArray[randomfeltolt1, randomfeltolt2] = varoselem;
                    if (varoselem.nev == "S") { pozicioSheriff = randomfeltolt1+ ";" + randomfeltolt2; }
                    //if (varoselem.nev == "E") { pozicioEnemy.Add(randomfeltolt1 + ";" + randomfeltolt2); } ez hulyeseg inkabb amikor mozgatom megkeresem mindet
                    
                    i++;
                }
                randomfeltolt1 = rnd.Next(0, 25);
                randomfeltolt2 = rnd.Next(0, 25);
                
            }
            
            Console.WriteLine(pozicioSheriff+"Sheriff");
            foreach (var item in pozicioEnemy) { Console.WriteLine(item+"Enemy"); }

        }

        public bool vanMellette(VarosElem[,] m,int sor, int oszlop,string karakter)
        {

            bool vanMellette = false;

            int maxSor = m.GetLength(0) - 1; 
            int maxOszlop = m.GetLength(1) - 1; 

            if (sor > 0 && m[sor - 1, oszlop].nev == karakter) // fel
                vanMellette = true;
            if (sor > 0 && oszlop > 0 && m[sor - 1, oszlop - 1].nev == karakter) // fel-balra
                vanMellette = true;
            if (oszlop > 0 && m[sor, oszlop - 1].nev == karakter) // balra
                vanMellette = true;
            if (sor < maxSor && m[sor + 1, oszlop].nev == karakter) // le
                vanMellette = true;
            if (sor < maxSor && oszlop < maxOszlop && m[sor + 1, oszlop + 1].nev == karakter) // le-jobbra
                vanMellette = true;
            if (oszlop < maxOszlop && m[sor, oszlop + 1].nev == karakter) // jobbra
                vanMellette = true;
            if (sor > 0 && oszlop < maxOszlop && m[sor - 1, oszlop + 1].nev == karakter) // fel-jobbra
                vanMellette = true;
            if (sor < maxSor && oszlop > 0 && m[sor + 1, oszlop - 1].nev == karakter) // le-balra
                vanMellette = true;

            return vanMellette;
            //ebből kell mégegy ami returnöl positionöket
        }

        public List<(int,int)> vanMelletteReturn(VarosElem[,] m, int sor, int oszlop)
        {

            List<(int, int)> poziciok = new List<(int, int)>();

            int maxSor = m.GetLength(0) - 1;
            int maxOszlop = m.GetLength(1) - 1;

            if (sor > 0 && m[sor - 1, oszlop] != null) // fel
                poziciok.Add((sor - 1, oszlop));
            if (sor > 0 && oszlop > 0 && m[sor - 1, oszlop - 1] != null) // fel-balra
                poziciok.Add((sor - 1, oszlop - 1));
            if (oszlop > 0 && m[sor, oszlop - 1] != null) // balra
                poziciok.Add((sor, oszlop - 1));
            if (sor < maxSor && m[sor + 1, oszlop] != null) // le
                poziciok.Add((sor + 1, oszlop));
            if (sor < maxSor && oszlop < maxOszlop && m[sor + 1, oszlop + 1] != null) // le-jobbra
                poziciok.Add((sor + 1, oszlop + 1));
            if (oszlop < maxOszlop && m[sor, oszlop + 1] != null) // jobbra
                poziciok.Add((sor, oszlop + 1));
            if (sor > 0 && oszlop < maxOszlop && m[sor - 1, oszlop + 1] != null) // fel-jobbra
                poziciok.Add((sor - 1, oszlop + 1));
            if (sor < maxSor && oszlop > 0 && m[sor + 1, oszlop - 1] != null) // le-balra
                poziciok.Add((sor + 1, oszlop - 1));

            return poziciok;
            //ez returnöl pozikat
        }

        public void Jatek() {
            while (!jatekvege)
            {
                int seriffSor = 0;
                int seriffOszlop = 0;
                for (global::System.Int32 i = 0; i < objectArray.GetLength(0); i++)
                {
                    for (global::System.Int32 j = 0; j < objectArray.GetLength(1); j++)
                    {
                        if (objectArray[i,j].nev=="S")
                        {
                            seriffSor = i;
                            seriffOszlop = j;
                        }
                        
                    }
                }
                VarosElem seriffelem = objectArray[seriffSor, seriffOszlop];
                int kiKezd = rnd.Next(0, 2);
                if (kiKezd==0)
                    bandita.parbaj(objectArray, seriffSor, seriffOszlop);
                else
                    seriff.parbaj(objectArray, seriffSor, seriffOszlop);



                bandita.mozgasBanditakat(objectArray);
                seriff.mozgasSeriff(objectArray);
                seriff.seriffKorulSzinez(objectArray);
                Thread.Sleep(300);

                Console.Clear();
            }
        }

       

    }
}
