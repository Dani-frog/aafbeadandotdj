﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoTDJ
{
    internal class Barikad : VarosElem
    {
        public Barikad(string nev, int dmg, int hp, int arany)
        {
            this.nev = nev;
            this.dmg = dmg;
            this.hp = hp;
            this.arany = arany;
        }
    }
}
