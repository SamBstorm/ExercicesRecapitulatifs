using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Mastemind
{
    public struct Mastermind
    {
        public const int CODE_SIZE = 4;
        public static Random RNG = new Random();
        public Couleurs[] GenerateRandom()
        {
            RNG ??= new Random();
            Couleurs[] colors = Enum.GetValues<Couleurs>();
            Couleurs[] code = new Couleurs[CODE_SIZE];
            for (int i = 0; i < CODE_SIZE; i++)
            {
                Couleurs couleur = colors[RNG.Next(0, colors.Length)];
                bool avoir_double = true;
                while (avoir_double)
                {
                    avoir_double = false;
                    for (int j = 0; j < i && !avoir_double; j++)
                    {
                        if (couleur == code[j])
                        {
                            avoir_double = true;
                            couleur = colors[RNG.Next(0, colors.Length)];
                        }
                    }
                }
                code[i] = couleur;
            }
            return code;
        }

        public int CompareCorrectCode(Couleurs[] codeCpu, Couleurs[] codeUser) {
            int estOk = 0;
            for (int i = 0; i < CODE_SIZE; i++)
            {
                if (codeCpu[i] == codeUser[i]) estOk++;
            }
            return estOk;
        }

        public int CompareColorCode(Couleurs[] codeCpu, Couleurs[] codeUser) {
            int estOk = 0;
            for (int i = 0; i < CODE_SIZE; i++)
            {
                bool trouve = false;
                for (int j = 0; j < CODE_SIZE && !trouve; j++)
                {
                    if (codeCpu[i] == codeUser[j] && i!=j ) { 
                        estOk++;
                        trouve = true;
                    }
                }
            }
            return estOk;
        }
    }
}
