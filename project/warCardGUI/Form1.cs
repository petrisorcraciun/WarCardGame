using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace warCardGUI
{
    public partial class Form1 : Form
    {
        public static String[] cards = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king", "ace" };
        public static String[] type = new string[] { "hearts", "diamonds", "spades", "clubs" };

        public static String[] cardsPack;

        public static String[] randomCardsPack;

        public static String[] play1Cards;
        public static String[] play2Cards;

        public static int winner;

        public static bool isGameActive;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static void drawCard()
        {
            ComparePlayerCards(play1Cards[play1Cards.Length - 1], play2Cards[play2Cards.Length - 1]);

            Console.WriteLine("Carti:" + play1Cards[play1Cards.Length - 1] + " " + play2Cards[play2Cards.Length - 1]);
            Console.WriteLine("Carti ramase: " + play1Cards.Length + " " + play2Cards.Length);

            mixCardPack();
        }

        public static int checkWinner()
        {
            if (play1Cards.Length == 0 && play2Cards.Length == cardsPack.Length)
            {
                return 2;
            }
            else if (play2Cards.Length == 0 && play1Cards.Length == cardsPack.Length)
            {
                return 1;
            }
            return 0;
        }

        public static string[] createPackCards(String[] cards, String[] type)
        {
            String[] cardsPack = new string[cards.Length * type.Length];

            int c = 0;

            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < type.Length; j++)
                {
                    cardsPack[c] = cards[i] + type[j];
                    c++;
                }
            }

            return cardsPack;
        }
        public static void splitCardPack()
        {
            play1Cards = randomCardsPack.Take(randomCardsPack.Length / 2).ToArray();
            play2Cards = randomCardsPack.Skip(randomCardsPack.Length / 2).ToArray();
        }

        public static void mixCardPack()
        {
            Randomizer.Randomize(play1Cards);
            Randomizer.Randomize(play2Cards);
        }
        public static void ComparePlayerCards(String cardPlayer1, String cardPlayer2)
        {
            int positionCard1 = Array.IndexOf(cardsPack, cardPlayer1);
            int positionCard2 = Array.IndexOf(cardsPack, cardPlayer2);

            int nrCards = 1;

            if (cardPlayer1.Substring(0, 3) == cardPlayer2.Substring(0, 3))
            {
                //Console.WriteLine(test[0].Substring(0, 3));
                switch (cardPlayer1.Substring(0, 3))
                {
                    case "jac":
                        nrCards = 12;
                        break;
                    case "que":
                        nrCards = 13;
                        break;
                    case "kin":
                        nrCards = 14;
                        break;
                    case "ace":
                        nrCards = 11;
                        break;
                }
            }
            else if (Regex.IsMatch(cardPlayer1.Substring(0, 2), @"\d\d") && Regex.IsMatch(cardPlayer2.Substring(0, 2), @"\d\d"))
            {
                nrCards = 10;
            }
            else if (Regex.IsMatch(cardPlayer1.Substring(0, 1), @"\d") && Regex.IsMatch(cardPlayer2.Substring(0, 1), @"\d")
                && cardPlayer1.Substring(0, 1) == cardPlayer2.Substring(0, 1))
            {
                nrCards = Int32.Parse(cardPlayer1.Substring(0, 1));
            }

            if (play1Cards.Length < nrCards)
            {
                nrCards = play1Cards.Length;
            }
            else if (play2Cards.Length < nrCards)
            {
                nrCards = play2Cards.Length;
            }


            if (nrCards == 1)
            {
                if (positionCard1 < positionCard2)
                {
                    int positionCard = Array.IndexOf(play1Cards, cardPlayer1);
                    play1Cards = RemoveAt(play1Cards, positionCard);
                    play2Cards = AddAt(play2Cards, cardPlayer1);
                }
                else if (positionCard1 > positionCard2)
                {
                    int positionCard = Array.IndexOf(play2Cards, cardPlayer2);
                    play2Cards = RemoveAt(play2Cards, positionCard);
                    play1Cards = AddAt(play1Cards, cardPlayer2);
                }
            }
            else
            {

            }

            Console.WriteLine(nrCards);


        }

        public static string[] RemoveAt(string[] stringArray, int index)
        {
            if (index < 0 || index >= stringArray.Length)
                return stringArray;
            var newArray = new string[stringArray.Length - 1];
            int j = 0;
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (i == index) continue;
                newArray[j] = stringArray[i];
                j++;
            }
            return newArray;
        }

        public static string[] AddAt(string[] array, string newValue)
        {
            int newLength = array.Length + 1;

            string[] result = new string[newLength];

            for (int i = 0; i < array.Length; i++)
                result[i] = array[i];

            result[newLength - 1] = newValue;

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cardsPack = createPackCards(cards, type);
            randomCardsPack = createPackCards(cards, type);

            Randomizer.Randomize(randomCardsPack);
            splitCardPack();

            String path = AppDomain.CurrentDomain.BaseDirectory;

            pictureBox1.Image = Image.FromFile(@"-- ADD PATCH FOLDER IMAGE -- " + play1Cards[play1Cards.Length - 1] + ".png");
            pictureBox2.Image = Image.FromFile(@"-- ADD PATCH FOLDER IMAGE -- " + play2Cards[play2Cards.Length - 1] + ".png");
            label1.Text = path;
            label2.Text = "Carti ramane:" + play2Cards.Length.ToString();

        }



        private void card1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            int check = checkWinner();
            if (check == 0)
            {
                drawCard();
            }
            else
            {
                winner = check;
            }

            pictureBox1.Image = Image.FromFile(@"-- ADD PATCH FOLDER IMAGE -- " + play1Cards[play1Cards.Length - 1] + ".png");
            pictureBox2.Image = Image.FromFile(@"-- ADD PATCH FOLDER IMAGE -- " + play2Cards[play2Cards.Length - 1] + ".png");
          
            label1.Text = "Carti ramane:" + play1Cards.Length.ToString();
            label2.Text = "Carti ramane:" + play2Cards.Length.ToString();



        }

        
    }
}
