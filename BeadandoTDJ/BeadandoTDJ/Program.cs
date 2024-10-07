using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VarosElem varosElem = new VarosElem();
            Varos varos = new Varos();
            varos.matrixFeltolt();
            varos.Jatek();
            Console.ReadLine();
        }
    }
}
