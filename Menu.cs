using System;
using System.Collections.Generic;

namespace FilmLibrary
{
    internal static class Menu
    {
        static List<string> LibraryMenu = new List<string>();

        public static void CreateMenus()
        {
            LibraryMenu.Add("Types of Movies");
            LibraryMenu.Add("Movies to Watch");
            LibraryMenu.Add("Recently Watched");
            LibraryMenu.Add("Favorite Movies");
            LibraryMenu.Add("Search Movies");
            LibraryMenu.Add("Exit");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("Film Library Menu:");
            for (int i = 0; i < LibraryMenu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {LibraryMenu[i]}");
            }
        }
    }
}