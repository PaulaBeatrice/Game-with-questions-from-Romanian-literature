using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat_JocCulturaGenerala_Romana_
{
    class intrebare
    {
            private int id;
            private string enunt, a, b, c, d, corect, raspuns, dificultatea;
            public int ID { set { id = value; } get { return id; } }
            public string Enunt { set { enunt = value; } get { return enunt; } }
            public string A { set { a = value; } get { return a; } }
            public string B { set { b = value; } get { return b; } }
            public string C { set { c = value; } get { return c; } }
            public string D { set { d = value; } get { return d; } }
            public string Corect { set { corect = value; } get { return corect; } }
            public string Raspuns { set { raspuns = value; } get { return raspuns; } }
            public string Dificultate { set { dificultatea = value; } get { return dificultatea; } }

            public intrebare() { }
            public intrebare(int id, string enunt, string a, string b, string c, string d, string corect, string dificultatea )
            {
                ID = id;
                Enunt = enunt;
                A = a; B = b; C = c; D = d;
                Corect = corect;
                raspuns = "";
                Dificultate = dificultatea;
            }
    }
}
