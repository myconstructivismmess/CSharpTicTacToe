using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace TicTacToe
{
    internal class Program
    {
        public static void Main()
        {
            /*
             * Fonction principale
             */
            
            Dictionary<string, string> game = new Dictionary<string, string>()
            {
                {"A1", " "},
                {"A2", " "},
                {"A3", " "},
                {"B1", " "},
                {"B2", " "},
                {"B3", " "},
                {"C1", " "},
                {"C2", " "},
                {"C3", " "}
            };
            List<string> locations = game.Keys.ToList();
            
            Console.WriteLine("# Jeu du Tic Tac Toe #\n");
            string choice;
            Random random = new Random();
            
            while (locations.Any())
            {
                // Affiche l'état de la partie ( Grille )
                Display(game);
                
                // Player
                Console.WriteLine("\n[Joueur] Choisir un emplacement :");
                while (true)
                {
                    choice = Console.ReadLine()?.Trim().ToUpper();
                    
                    if (locations.Contains(choice))
                    {
                        break;
                    }
                }
                game[choice] = "X";
                locations.Remove(choice);
                if (EndGame(game, choice))
                {
                    Console.WriteLine("[Joueur] J'ai gagné\n");
                    break;
                }

                if (!locations.Any())
                {
                    Console.WriteLine("[Partie] Égalité...\n");
                    break;
                }
                
                // AI
                Console.WriteLine("\n[IA] Je réfléchis...\n");
                Thread.Sleep(1000);
                int index = random.Next(locations.Count);
                Console.WriteLine($"[IA] Je joue en {locations[index]}\n");
                game[locations[index]] = "O";
                if (EndGame(game, locations[index]))
                {
                    Console.WriteLine("[IA] J'ai gagné\n");
                    break;
                }
                locations.Remove(locations[index]);
            }
            
            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour quitter...\n");
            Console.ReadKey();
        }
        
        public static void Display(Dictionary<string, string> game)
        {
            /*
             * Fonction d'affichage de la partie
             *
             * Fait le rendu de la partie dans la console
             *
             * Paramètres:
             * • Dictionnaire 'game' étant la représentation de l'état de la partie
             */
            
            List<string> lines = new List<string> {"A", "B", "C"};
            List<string> columns = new List<string> {"1", "2", "3"};
            
            string sep = "   +---+---+---+";
            
            string coord;
            
            Console.WriteLine($"     1   2   3");
            // Lignes
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(sep);
                Console.Write($" {lines[i]} ");
                // Colonnes
                for (int j = 0; j < 3; j++)
                {
                    coord = lines[i] + columns[j];
                    Console.Write($"| {game[coord]} ");
                }
                Console.Write("|\n");
            }
            Console.WriteLine(sep);
        }
        
        static bool Count3Marks(Dictionary<string, string> game, string symbole, List<string> locations)
        {
            /*
             * Fonction de vérification
             *
             * Vérifie si les positions données contiennent le symbole spécifié
             *
             * Paramètres:
             * • Dictionnaire 'game' étant la représentation de l'état de la partie
             * • Chaîne de caractères 'symbole' qui représente le symbole que l'on cherche à vérifier
             * • Liste de chaîne de caractères 'locations' qui représente la liste des positions sur la grille à vérifier à l'aide de la chaîne de caractères 'symbole'
             *
             * Retour:
             * • Renvoie true si les 3 positions passées contiennent toutes le symbole spécifié dans la variable symbole
             * • Retourne false sinon
             */
            
            int marksCount = 0;

            foreach (string coord in locations)
            {
                if (game[coord] == symbole)
                {
                    marksCount++;
                }
            }

            return marksCount == 3;
        }

        static bool EndGame(Dictionary<string, string> game, string choice)
        {
            /*
             * Fonction de contrôle
             *
             * Vérifie si le coup joué vient de provoquer la fin de la partie
             *
             * Paramètres:
             * • Dictionnaire 'game' étant la représentation de l'état de la partie
             * • Chaîne de caractères 'symbole' qui représente le symbole que l'on cherche à vérifier
             * • Liste de chaîne de caractères 'locations' qui représente la liste des positions sur la grille à vérifier à l'aide de la chaîne de caractères 'symbole'
             *
             * Retour:
             * • Renvoie true si le coup joué vient de provoquer la fin de la partie
             * • Retourne false sinon
             */
            
            string symbole = game[choice];
            List<string> lines = new List<string> {"A", "B", "C"};
            List<string> columns = new List<string> {"1", "2", "3"};

            char l = choice[0];
            char c = choice[1];

            List<string> line = new List<string>();
            foreach (string columnChar in columns)
            {
                line.Add($"{l}{columnChar}");
            }
            
            List<string> col = new List<string>();
            foreach (string lineChar in lines)
            {
                col.Add($"{lineChar}{c}");
            }

            List<string> diag1 = new List<string>() {"A1","B2","C3"};
            List<string> diag2 = new List<string>() {"A3","B2","C1"};

            if (Count3Marks(game, symbole, line) || Count3Marks(game, symbole, col) || Count3Marks(game, symbole, diag1) || Count3Marks(game, symbole, diag2))
            {
                return true;
            }
            
            return false;
        }
    }
}