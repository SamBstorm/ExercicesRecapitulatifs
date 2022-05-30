using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Mastemind
{
    public struct MastermindConsole
    {
        public Mastermind jeu;
        public ConsoleZone dialogZone;
        public ConsoleZone playerCodesZone;
        public List<MastermindAttempt> playerTries;
        public const char POINT = '\x25cf';

        public ColoredText[] WriteCode(Couleurs[] code) {
            List<ColoredText> text = new List<ColoredText>();
            ColoredText ct;
            ct.text = "[";
            ct.color = ConsoleColor.Gray;
            text.Add(ct);
            foreach (Couleurs couleur in code)
            {
                ct.color = (ConsoleColor)(couleur);
                ct.text = POINT.ToString();
                text.Add(ct);
            }
            ct.color = ConsoleColor.Gray;
            ct.text = "]";
            text.Add(ct);
            return text.ToArray();
        }

        public Couleurs[] SetCode() {
            Couleurs[] code = new Couleurs[Mastermind.CODE_SIZE];
            dialogZone.Clear();
            for (int i = 0; i < code.Length; i++)
            {
                dialogZone.Write("Veuillez choisir une couleur : ");
                code[i] = ChooseColor();
            }
            return code;
        }

        public Couleurs ChooseColorNumber()
        {
            Couleurs[] colors = Enum.GetValues<Couleurs>();
            int choice;
            do
            {
                for (int i = 0; i < colors.Length; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)colors[i];
                    Console.Write($"{i + 1}[{POINT}]  ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <1 || choice>colors.Length );
            return colors[choice-1];
        }
        public Couleurs ChooseColor()
        {
            Couleurs[] colors = Enum.GetValues<Couleurs>();
            int choice = 0;
            ConsoleKey key;
            do
            {
                List<ColoredText> text = new List<ColoredText>();
                for (int i = 0; i < colors.Length; i++)
                {
                    ColoredText ct;
                    ct.color = (ConsoleColor)colors[i];
                    if(i == choice) ct.text = $"[{POINT}]  ";
                    else ct.text=$" {POINT}   ";
                    text.Add(ct);
                }
                dialogZone.Write(text,1);
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow && choice > 0) choice--;
                else if (key == ConsoleKey.RightArrow && choice < colors.Length - 1) choice++;
            }
            while (key != ConsoleKey.Enter);
            return colors[choice];
        }

        public void Start(string[] args) {
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(60,60);
            dialogZone.originX = 5;
            dialogZone.originY = 35;
            dialogZone.width = 40;
            dialogZone.height = 10;
            playerCodesZone.originX = 10;
            playerCodesZone.originY = 5;
            playerCodesZone.width = 20;
            playerCodesZone.height = 30;
            Couleurs[] cpu = jeu.GenerateRandom();
            if (args.Length > 0 && args.Contains("--cheat"))
            {
                playerCodesZone.Write(WriteCode(cpu));
                Console.ReadLine();
            }
            playerTries = new List<MastermindAttempt>();
            int resultOk, resultColor;
            do
            {
                Couleurs[] player = SetCode();
                resultOk = jeu.CompareCorrectCode(cpu, player);
                resultColor = jeu.CompareColorCode(cpu, player);
                playerTries.Add(new MastermindAttempt() { attempt = player, colorsOk = resultOk, positionOk = resultColor });
                List<ColoredText> text = new List<ColoredText>();
                ColoredText ct;
                ct.text = $"Essais : Ok | Bof";
                ct.color = ConsoleColor.Gray;
                text.Add(ct);
                ct.text = "\n";
                text.Add(ct);
                foreach (MastermindAttempt attempt in playerTries)
                {
                    text.AddRange(WriteCode(attempt.attempt));
                    ct.text = $" :  {attempt.colorsOk} | {attempt.positionOk}";
                    text.Add(ct);
                    ct.text = "\n";
                    text.Add(ct);
                }
                playerCodesZone.Write(text);
            } while (resultOk < cpu.Length);
            dialogZone.Write("Vous avez trouvé le code!");
        }
    }
}
