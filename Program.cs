﻿using System;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void BannerMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine("||||||||||||||");
            Console.WriteLine(message);
            Console.WriteLine("||||||||||||||");
            Console.WriteLine();
        }

        static string PromptForString(string prompt)
        {
            Console.WriteLine(prompt);

            var userInput = Console.ReadLine().ToUpper().Trim();
            return userInput;
        }
        static void Main(string[] args)
        {
            var context = new EnigmaRecordsContext();

            var Artists = context.Artists;
            var Albums = context.Albums;

            BannerMessage("Enigma Records");

            var userHasChosenToQuit = false;

            var userHasChosenToGoBackToMenu = false;

            while (userHasChosenToQuit == false)
            {
                Console.WriteLine("--------------");
                Console.WriteLine("Menu:");
                Console.WriteLine();
                Console.WriteLine("Artists");
                Console.WriteLine("Albums");
                Console.WriteLine("Quit");
                Console.WriteLine("--------------");

                var userResponse = PromptForString("Which Menu option would you like to choose?");

                userHasChosenToGoBackToMenu = false;

                while (userResponse == "ARTISTS" && userHasChosenToGoBackToMenu == false)
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("All Artists");
                    Console.WriteLine("Signed Artists");
                    Console.WriteLine("Unsigned Artists");
                    Console.WriteLine("Sign Artist");
                    Console.WriteLine("Drop Artist");
                    Console.WriteLine();
                    Console.WriteLine("Back to Main Menu");
                    Console.WriteLine("Quit");
                    Console.WriteLine("-----------------");

                    var userArtistsResponse = PromptForString("Which option would you like to choose?");

                    Console.WriteLine();

                    if (userArtistsResponse == "ALL ARTISTS")
                    {
                        foreach (var Artist in context.Artists)
                        {
                            Console.WriteLine(Artist.Name);
                            Console.WriteLine();
                        }
                    }

                    if (userArtistsResponse == "SIGNED ARTISTS")
                    {
                        foreach (var Artist in context.Artists)
                        {
                            Console.WriteLine(Artist.Name);
                            Console.WriteLine();
                        }
                    }

                    if (userArtistsResponse == "UNSIGNED ARTISTS")
                    {

                    }

                    if (userArtistsResponse == "SIGN ARTIST")
                    {

                    }

                    if (userArtistsResponse == "DROP ARTIST")
                    {

                    }

                    if (userArtistsResponse == "BACK TO MAIN MENU")
                    {
                        userHasChosenToGoBackToMenu = true;
                    }

                    if (userArtistsResponse == "QUIT")
                    {
                        userHasChosenToGoBackToMenu = true;
                        userHasChosenToQuit = true;
                    }
                }

                userHasChosenToGoBackToMenu = false;

                while (userResponse == "ALBUMS" && userHasChosenToGoBackToMenu == false)
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("All Albums");
                    Console.WriteLine("Albums By Artist");
                    Console.WriteLine("Add Album");
                    Console.WriteLine("Add Song");
                    Console.WriteLine();
                    Console.WriteLine("Back to Main Menu");
                    Console.WriteLine("Quit");
                    Console.WriteLine("-----------------");

                    var userAlbumResponse = PromptForString("Which option would you like to choose?");

                    if (userAlbumResponse == "ALL ALBUMS")
                    {
                        var albumsInOrder = Albums.OrderBy(album => album.ReleaseDate);
                        foreach (var album in albumsInOrder.Include(album => album.Artist))
                        {
                            Console.WriteLine();
                            Console.WriteLine(album.Artist.Name);
                            Console.WriteLine(album.Title);
                            Console.WriteLine(album.ReleaseDate);
                            Console.WriteLine();
                        }
                    }

                    if (userAlbumResponse == "ALBUMS BY ARTIST")
                    {
                        foreach (var album in context.Albums.Include(album => album.Artist))
                        {
                            // Console.WriteLine()
                        }
                    }

                    if (userAlbumResponse == "ADD ALBUM")
                    {

                    }

                    if (userAlbumResponse == "ADD SONG")
                    {

                    }

                    if (userAlbumResponse == "BACK TO MAIN MENU")
                    {
                        userHasChosenToGoBackToMenu = true;
                    }

                    if (userAlbumResponse == "QUIT")
                    {
                        userHasChosenToGoBackToMenu = true;
                        userHasChosenToQuit = true;
                    }
                }

                if (userResponse == "QUIT")
                {
                    userHasChosenToQuit = true;
                }
            }
        }
    }
}
