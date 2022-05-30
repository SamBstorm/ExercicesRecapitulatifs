using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Mastemind
{
    public struct ConsoleZone
    {
        public ushort originX;
        public ushort originY;
        public ushort width;
        public ushort height;

        public void Clear()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(originX + j, originY + i);
                    Console.Write(" ");
                }
            }
        }

        public void Write(string text, int line = 0)
        {
            if (line == 0) Clear();
            int index = 0;
            for (int i = line; i < height; i++)
            {
                for (int j = 0; j < width && index < text.Length; j++)
                {
                    Console.SetCursorPosition(originX + j, originY + i);
                    if (index < text.Length)
                    {
                        if (text[index] == '\n') { i++; j = -1; }
                        else Console.Write(text[index]);
                    }
                    index++;
                }
            }
        }

        public void Write(IEnumerable<ColoredText> text, int line = 0)
        {
            ColoredText[] textArray = text.ToArray();
            if (line == 0) Clear();
            int textLength = 0;
            foreach (ColoredText ct in textArray) textLength += ct.text.Length;
            int currentIndex = 0;
            int textIndex = 0;
            for (int i = line; i < height; i++)
            {
                for (int j = 0; j < width && currentIndex < textArray.Length; j++)
                {
                    Console.SetCursorPosition(originX + j, originY + i);
                    string currentText = textArray[currentIndex].text;
                    Console.ForegroundColor = textArray[currentIndex].color;
                    if (currentText == "\n")
                    {
                        currentIndex++;
                        j = width;
                    }
                    else
                    {
                        Console.Write(currentText[textIndex]);
                        textIndex++;
                        if (textIndex >= currentText.Length)
                        {
                            currentIndex++;
                            textIndex = 0;
                        }
                    }
                }
            }
        }
    }
}
