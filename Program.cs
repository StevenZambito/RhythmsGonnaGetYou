using System;
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

        static string PromptForStringUpper(string prompt)
        {
            Console.WriteLine(prompt);

            var userInput = Console.ReadLine().ToUpper().Trim();
            return userInput;
        }

        static string PromptForString(string prompt)
        {
            Console.WriteLine(prompt);

            var userInput = Console.ReadLine();
            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.WriteLine(prompt);

            var userInput = int.Parse(Console.ReadLine());
            return userInput;
        }

        static bool PromptForBool(string prompt)
        {
            Console.Write(prompt);

            var userInput = bool.Parse(Console.ReadLine());
            return userInput;
        }

        static void Main(string[] args)
        {
            var context = new EnigmaRecordsContext();
            var Artists = context.Artists;
            var Albums = context.Albums;
            var Songs = context.Songs;

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

                var userResponse = PromptForStringUpper("Which Menu option would you like to choose?");

                userHasChosenToGoBackToMenu = false;

                while (userResponse == "ARTISTS" && userHasChosenToGoBackToMenu == false)
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("All Artists");
                    Console.WriteLine("Signed Artists");
                    Console.WriteLine("Unsigned Artists");
                    Console.WriteLine("Sign Artist");
                    Console.WriteLine("Drop Artist");
                    Console.WriteLine("Re-Sign Artist");
                    Console.WriteLine();
                    Console.WriteLine("Back to Main Menu");
                    Console.WriteLine("Quit");
                    Console.WriteLine("-----------------");

                    var userArtistsResponse = PromptForStringUpper("Which option would you like to choose?");
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
                        var signedArtists = Artists.Where(artist => artist.IsSigned == true);
                        foreach (var artist in signedArtists)
                        {
                            Console.WriteLine(artist.Name);
                            Console.WriteLine();
                        }
                    }

                    if (userArtistsResponse == "UNSIGNED ARTISTS")
                    {
                        var unSignedArtists = Artists.Where(artist => artist.IsSigned == false);
                        foreach (var artist in unSignedArtists)
                        {
                            Console.WriteLine(artist.Name);
                            Console.WriteLine();
                        }
                    }

                    if (userArtistsResponse == "SIGN ARTIST")
                    {
                        var newName = PromptForString("What is the name of the artist you would like to sign?");
                        var newCountryOfOrigin = PromptForString($"What is the country of origin for {newName}?");
                        var newNumberOfMembers = PromptForInteger($"How many members does {newName} consist of?");
                        var newWebsite = PromptForString($"What is {newName}'s website address?");
                        var newStyle = PromptForString($"What is {newName}'s style (genre)?");
                        var newContactName = PromptForString($"What is {newName}'s contact name?");
                        var newContactPhoneNumber = PromptForString($"What is {newName}'s phone number? ((xxx)-xxx-xxxx)");

                        var newArtist = new Artist()
                        {
                            Name = newName,
                            CountryOfOrigin = newCountryOfOrigin,
                            NumberOfMembers = newNumberOfMembers,
                            Website = newWebsite,
                            Style = newStyle,
                            IsSigned = true,
                            ContactName = newContactName,
                            ContactPhoneNumber = newContactPhoneNumber
                        };

                        context.Artists.Add(newArtist);
                        context.SaveChanges();

                        Console.WriteLine();
                        Console.WriteLine($"{newName} has been signed to Enigma Records!");
                    }

                    if (userArtistsResponse == "DROP ARTIST")
                    {
                        var nameOfArtistToDrop = PromptForStringUpper("What is the name of the artist you would like to drop?");
                        var artist = Artists.Single(artist => artist.Name.ToUpper() == nameOfArtistToDrop);
                        artist.IsSigned = false;

                        context.SaveChanges();

                        Console.WriteLine();
                        Console.WriteLine($"{nameOfArtistToDrop} has been dropped from Enigma Records.");

                    }

                    if (userArtistsResponse == "RE-SIGN ARTIST")
                    {
                        var nameOfArtistToSignAgain = PromptForStringUpper("What is the name of the dropped artist you would like to Re-Sign?");
                        var artist = Artists.Single(artist => artist.Name.ToUpper() == nameOfArtistToSignAgain);
                        artist.IsSigned = true;

                        context.SaveChanges();

                        Console.WriteLine();
                        Console.WriteLine($"{nameOfArtistToSignAgain} has been re-signed to Enigma Records!");

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

                    var userAlbumResponse = PromptForStringUpper("Which option would you like to choose?");

                    if (userAlbumResponse == "ALL ALBUMS")
                    {
                        var albumsInOrder = Albums.OrderBy(album => album.ReleaseDate);

                        foreach (var album in albumsInOrder.Include(album => album.TheArtistAssociatedWithAlbumObject))
                        {
                            Console.WriteLine();
                            Console.WriteLine(album.TheArtistAssociatedWithAlbumObject.Name);
                            Console.WriteLine(album.Title);
                            Console.WriteLine(album.ReleaseDate);
                            Console.WriteLine();
                        }
                    }

                    if (userAlbumResponse == "ALBUMS BY ARTIST")
                    {
                        var artistAlbums = PromptForStringUpper("Which Artist's albums would you like to view?");
                        var artistAlbumsToView = Albums.Include(album => album.TheArtistAssociatedWithAlbumObject);

                        foreach (var album in artistAlbumsToView.Where(album => album.TheArtistAssociatedWithAlbumObject.Name.ToUpper() == artistAlbums))
                        {
                            Console.WriteLine();
                            Console.WriteLine(album.Title);
                            Console.WriteLine($"Explicit: {album.IsExplicit}");
                            Console.WriteLine(album.ReleaseDate);
                        }
                    }

                    if (userAlbumResponse == "ADD ALBUM")
                    {
                        var artistToAddAlbumTo = PromptForStringUpper("What is the name of the artist you are adding this album to?");
                        var artist = Artists.Single(artist => artist.Name.ToUpper() == artistToAddAlbumTo);
                        var newTitle = PromptForString("What is the name of the album you are adding?");
                        var newIsExplicit = PromptForBool("Is the album explicit? True or False?");
                        var newReleaseDate = PromptForString("What is the albums release date? (YYYY-DD-MM)");

                        var newAlbum = new Album()
                        {
                            Title = newTitle,
                            IsExplicit = newIsExplicit,
                            ReleaseDate = DateTime.Parse(newReleaseDate),
                            ArtistId = artist.Id
                        };

                        Albums.Add(newAlbum);
                        context.SaveChanges();

                        Console.WriteLine($"{newTitle} has been added to {artistToAddAlbumTo}'s discography.");
                    }

                    if (userAlbumResponse == "ADD SONG")
                    {
                        var albumName = PromptForStringUpper("What is the name of the album you would like to add this song to?");
                        var album = Albums.Single(album => album.Title.ToUpper() == albumName);
                        var newTitle = PromptForString($"What is the name of the song you would like to add to {albumName}?");
                        var newTrackNumber = PromptForInteger($"What track number is {newTitle} on the album?");
                        var newDuration = PromptForString($"How long is {newTitle}? (hh:mm:ss)");

                        var checkingTrackNumber = context.Songs.FirstOrDefault(song => song.AlbumId == album.Id && song.TrackNumber == newTrackNumber);

                        if (checkingTrackNumber != null)
                        {
                            Console.WriteLine("Track Number is taken, please try again!");
                        }
                        else
                        {
                            var newSong = new Song()
                            {
                                TrackNumber = newTrackNumber,
                                Title = newTitle,
                                Duration = DateTime.Parse(newDuration),
                                AlbumId = album.Id
                            };

                            Songs.Add(newSong);
                            context.SaveChanges();

                            Console.WriteLine();
                            Console.WriteLine($"{newTitle} has been added to {albumName}.");
                        }
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
