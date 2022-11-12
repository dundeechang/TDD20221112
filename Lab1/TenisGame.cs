using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public enum Player
    {
        None = 0,
        Player1 = 1,
        Player2 = 2,
    }


    internal class TenisGame
    {
        private string[] nameArray = { "love", "fifteen", "thirty" };

        public string play1 { get; set; }
        public string play2 { get; set; }

        private int score1 = 0;
        private int score2 = 0;

        public string Click(Player player)
        {
            if (player == Player.Player1)
            {
                score1++;
            }
            else if (player == Player.Player2)
            {
                score2++;
            }
            return Score(score1, score2);
        }

        public string Score(int x, int y)
        {
            if (x <= 2 && y <= 2)
            {
                if (x == y)
                {
                    return $"{nameArray[x]} all";
                }
                else
                {
                    return $"{nameArray[x]} : {nameArray[y]}";
                }
            }
            else
            {
                if (x == y)
                {
                    return "deuce";
                }
                else
                {
                    int z = x - y;
                    if (z == 1)
                    {
                        return $"{play1} adv";
                    }
                    else if (z == -1)
                    {
                        return $"{play2} adv";
                    }
                    else if (z > 1)
                    {
                        return $"{play1} win";
                    }
                    else //if (z < -1)
                    {
                        return $"{play2} win";
                    }
                }
            }
        }
    }
}
