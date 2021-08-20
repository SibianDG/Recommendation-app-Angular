using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using PuppeteerSharp;

namespace Api.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;


        public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                Item batmanBegins = new Movie("Batman Begins", "Batman", null, null);
                Item theDarkKnight = new Movie("The Dark Knight", "Batman2", null, null);
                Item theDarkKnightRises = new Movie("The Dark Knight Rises", "Batman3", null, null);
                Item book = new Book("Book1", "Book1Summary", "./book1.png", "book1.com", 361);
                Item serie = new Serie("Black Mirror", "BM", null, null);
                List<Item> items = new List<Item>() {batmanBegins, theDarkKnight, theDarkKnightRises, book, serie};
                List<Item> movies = new List<Item>() {batmanBegins, theDarkKnight, theDarkKnightRises};
                List<Item> series = new List<Item>() {serie};

                _dbContext.Items.AddRange(items);

                string[] keywords = { "amazing", "sad", "lonely" };
                string[] keywords2 = { "damn", "nice" };
                string[] keywords3 = { "serie" };

                Recommendation recommendation1 = new Recommendation(keywords, movies);
                recommendation1.Rating = 0.88;
                Recommendation recommendation2 = new Recommendation(keywords2, items);
                Recommendation recommendation3 = new Recommendation(keywords3, series);
                recommendation3.Rating = .95;

                _dbContext.Recommendations.Add(recommendation1);
                _dbContext.Recommendations.Add(recommendation2);
                _dbContext.Recommendations.Add(recommendation3);
                
                 #region imdb 
                // List<Item> list = new List<Item>();
                //
                //     list.Add(new Movie("Megamind", "Megamind", null, "https://www.imdb.com/title/tt1001526/", 95));
                //     list.Add(new Movie("Slumdog Millionaire", "Slumdog Millionaire", null, "https://www.imdb.com/title/tt1010048/", 120));
                //     list.Add(new Movie("Fast & Furious", "Fast & Furious", null, "https://www.imdb.com/title/tt1013752/", 107));
                //     list.Add(new Movie("Alice in Wonderland", "Alice in Wonderland", null, "https://www.imdb.com/title/tt1014759/", 108));
                //     list.Add(new Movie("JFK", "JFK", null, "https://www.imdb.com/title/tt0102138/", 189));
                //     list.Add(new Movie("Argo", "Argo", null, "https://www.imdb.com/title/tt1024648/", 120));
                //     list.Add(new Movie("Daens", "Daens", null, "https://www.imdb.com/title/tt0104046/", 138));
                //     list.Add(new Movie("Up", "Up", null, "https://www.imdb.com/title/tt1049413/", 96));
                //     list.Add(new Movie("Reservoir Dogs", "Reservoir Dogs", null, "https://www.imdb.com/title/tt0105236/", 99));
                //     list.Add(new Movie("Jurassic Park", "Jurassic Park", null, "https://www.imdb.com/title/tt0107290/", 127));
                //     list.Add(new Movie("Skyfall", "Skyfall", null, "https://www.imdb.com/title/tt1074638/", 143));
                //     list.Add(new Movie("De helaasheid der dingen", "De helaasheid der dingen", null, "https://www.imdb.com/title/tt1075110/", 108));
                //     list.Add(new Movie("Night at the Museum: Battle of the Smithsonian", "Night at the Museum: Battle of the Smithsonian", null, "https://www.imdb.com/title/tt1078912/", 105));
                //     list.Add(new Movie("Ice Age: Dawn of the Dinosaurs", "Ice Age: Dawn of the Dinosaurs", null, "https://www.imdb.com/title/tt1080016/", 94));
                //     list.Add(new Movie("Stalingrad", "Stalingrad", null, "https://www.imdb.com/title/tt0108211/", 134));
                //     list.Add(new Serie("Friends", "Friends", null, null));
                //     list.Add(new Movie("Lone Survivor", "Lone Survivor", null, "https://www.imdb.com/title/tt1091191/", 121));
                //     list.Add(new Movie("Forrest Gump", "Forrest Gump", null, "https://www.imdb.com/title/tt0109830/", 142));
                //     list.Add(new Serie("Black-out", "Black-out", null, null));
                //     list.Add(new Movie("The Lion King", "The Lion King", null, "https://www.imdb.com/title/tt0110357/", 88));
                //     list.Add(new Movie("Tron", "Tron", null, "https://www.imdb.com/title/tt1104001/", 125));
                //     list.Add(new Movie("Police Academy: Mission to Moscow", "Police Academy: Mission to Moscow", null, "https://www.imdb.com/title/tt0110857/", 83));
                //     list.Add(new Movie("Paddington", "Paddington", null, "https://www.imdb.com/title/tt1109624/", 95));
                //     list.Add(new Movie("Paul Blart: Mall Cop", "Paul Blart: Mall Cop", null, "https://www.imdb.com/title/tt1114740/", 91));
                //     list.Add(new Movie("The Hangover", "The Hangover", null, "https://www.imdb.com/title/tt1119646/", 100));
                //     list.Add(new Movie("Apollo 13", "Apollo 13", null, "https://www.imdb.com/title/tt0112384/", 140));
                //     list.Add(new Movie("Die Hard: With a Vengeance", "Die Hard: With a Vengeance", null, "https://www.imdb.com/title/tt0112864/", 128));
                //     list.Add(new Movie("GoldenEye", "GoldenEye", null, "https://www.imdb.com/title/tt0113189/", 130));
                //     list.Add(new Movie("Toy Story", "Toy Story", null, "https://www.imdb.com/title/tt0114709/", 81));
                //     list.Add(new Movie("The Karate Kid", "The Karate Kid", null, "https://www.imdb.com/title/tt1155076/", 140));
                //     list.Add(new Serie("Kamp Waes", "Kamp Waes", null, null));
                //     list.Add(new Movie("Independence Day", "Independence Day", null, "https://www.imdb.com/title/tt0116629/", 145));
                //     list.Add(new Movie("Mission: Impossible", "Mission: Impossible", null, "https://www.imdb.com/title/tt0117060/", 110));
                //     list.Add(new Movie("Moon", "Moon", null, "https://www.imdb.com/title/tt1182345/", 97));
                //     list.Add(new Serie("Midsomer Murders", "Midsomer Murders", null, null));
                //     list.Add(new Movie("Bean", "Bean", null, "https://www.imdb.com/title/tt0118689/", 89));
                //     list.Add(new Movie("2012", "2012", null, "https://www.imdb.com/title/tt1190080/", 158));
                //     list.Add(new Movie("The Bourne Legacy", "The Bourne Legacy", null, "https://www.imdb.com/title/tt1194173/", 135));
                //     list.Add(new Serie("The Mentalist", "The Mentalist", null, null));
                //     list.Add(new Movie("Titanic", "Titanic", null, "https://www.imdb.com/title/tt0120338/", 194));
                //     list.Add(new Movie("Tomorrow Never Dies", "Tomorrow Never Dies", null, "https://www.imdb.com/title/tt0120347/", 119));
                //     list.Add(new Movie("Toy Story 2", "Toy Story 2", null, "https://www.imdb.com/title/tt0120363/", 92));
                //     list.Add(new Movie("Jack Ryan: Shadow Recruit", "Jack Ryan: Shadow Recruit", null, "https://www.imdb.com/title/tt1205537/", 105));
                //     list.Add(new Movie("Fantastic Four", "Fantastic Four", null, "https://www.imdb.com/title/tt0120667/", 106));
                //     list.Add(new Movie("Mission: Impossible II", "Mission: Impossible II", null, "https://www.imdb.com/title/tt0120755/", 123));
                //     list.Add(new Movie("Saving Private Ryan", "Saving Private Ryan", null, "https://www.imdb.com/title/tt0120815/", 169));
                //     list.Add(new Movie("Doctor Strange", "Doctor Strange", null, "https://www.imdb.com/title/tt1211837/", 115));
                //     list.Add(new Movie("Escape Plan", "Escape Plan", null, "https://www.imdb.com/title/tt1211956/", 115));
                //     list.Add(new Movie("Cars 2", "Cars 2", null, "https://www.imdb.com/title/tt1216475/", 106));
                //     list.Add(new Serie("Castle", "Castle", null, null));
                //     list.Add(new Movie("Iron Man 2", "Iron Man 2", null, "https://www.imdb.com/title/tt1228705/", 124));
                //     list.Add(new Movie("Mission: Impossible - Ghost Protocol", "Mission: Impossible - Ghost Protocol", null, "https://www.imdb.com/title/tt1229238/", 132));
                //     list.Add(new Movie("Alvin and the Chipmunks: The Squeakquel", "Alvin and the Chipmunks: The Squeakquel", null, "https://www.imdb.com/title/tt1231580/", 88));
                //     list.Add(new Movie("RoboCop", "RoboCop", null, "https://www.imdb.com/title/tt1234721/", 117));
                //     list.Add(new Movie("2 Guns", "2 Guns", null, "https://www.imdb.com/title/tt1272878/", 109));
                //     list.Add(new Movie("Looper", "Looper", null, "https://www.imdb.com/title/tt1276104/", 113));
                //     list.Add(new Movie("Madagascar 3: Europe's Most Wanted", "Madagascar 3: Europe's Most Wanted", null, "https://www.imdb.com/title/tt1277953/", 95));
                //     list.Add(new Movie("The Social Network", "The Social Network", null, "https://www.imdb.com/title/tt1285016/", 120));
                //     list.Add(new Movie("Ghostbusters", "Ghostbusters", null, "https://www.imdb.com/title/tt1289401/", 117));
                //     list.Add(new Serie("Code van Coppens", "Code van Coppens", null, null));
                //     list.Add(new Movie("Iron Man Three", "Iron Man Three", null, "https://www.imdb.com/title/tt1300854/", 130));
                //     list.Add(new Movie("Kung Fu Panda 2", "Kung Fu Panda 2", null, "https://www.imdb.com/title/tt1302011/", 90));
                //     list.Add(new Movie("Despicable Me", "Despicable Me", null, "https://www.imdb.com/title/tt1323594/", 95));
                //     list.Add(new Movie("The Matrix", "The Matrix", null, "https://www.imdb.com/title/tt0133093/", 136));
                //     list.Add(new Movie("Tinker Tailor Soldier Spy", "Tinker Tailor Soldier Spy", null, "https://www.imdb.com/title/tt1340800/", 122));
                //     list.Add(new Movie("The Dark Knight Rises", "The Dark Knight Rises", null, "https://www.imdb.com/title/tt1345836/", 164));
                //     list.Add(new Movie("Upside Down", "Upside Down", null, "https://www.imdb.com/title/tt1374992/", 109));
                //     list.Add(new Movie("Inception", "Inception", null, "https://www.imdb.com/title/tt1375666/", 148));
                //     list.Add(new Movie("Total Recall", "Total Recall", null, "https://www.imdb.com/title/tt1386703/", 118));
                //     list.Add(new Movie("Taken 2", "Taken 2", null, "https://www.imdb.com/title/tt1397280/", 92));
                //     list.Add(new Serie("Buiten de Zone", "Buiten de Zone", null, null));
                //     list.Add(new Movie("Happy Feet Two", "Happy Feet Two", null, "https://www.imdb.com/title/tt1402488/", 117));
                //     list.Add(new Movie("The World Is Not Enough", "The World Is Not Enough", null, "https://www.imdb.com/title/tt0143145/", 128));
                //     list.Add(new Movie("Rio", "Rio", null, "https://www.imdb.com/title/tt1436562/", 96));
                //     list.Add(new Movie("Gravity", "Gravity", null, "https://www.imdb.com/title/tt1454468/", 91));
                //     list.Add(new Movie("Baywatch", "Baywatch", null, "https://www.imdb.com/title/tt1469304/", 116));
                //     list.Add(new Serie("Sherlock", "Sherlock", null, null));
                //     list.Add(new Movie("Oblivion", "Oblivion", null, "https://www.imdb.com/title/tt1483013/", 124));
                //     list.Add(new Movie("The Greatest Showman", "The Greatest Showman", null, "https://www.imdb.com/title/tt1485796/", 105));
                //     list.Add(new Serie("Dag Sinterklaas", "Dag Sinterklaas", null, null));
                //     list.Add(new Serie("F.C. De Kampioenen", "F.C. De Kampioenen", null, null));
                //     list.Add(new Movie("Fantastic Four", "Fantastic Four", null, "https://www.imdb.com/title/tt1502712/", 100));
                //     list.Add(new Movie("A Star Is Born", "A Star Is Born", null, "https://www.imdb.com/title/tt1517451/", 136));
                //     list.Add(new Serie("The Walking Dead", "The Walking Dead", null, null));
                //     list.Add(new Movie("Melancholia", "Melancholia", null, "https://www.imdb.com/title/tt1527186/", 135));
                //     list.Add(new Movie("Elysium", "Elysium", null, "https://www.imdb.com/title/tt1535108/", 109));
                //     list.Add(new Serie("Samson en Gert", "Samson en Gert", null, null));
                //     list.Add(new Movie("The Girl with the Dragon Tattoo", "The Girl with the Dragon Tattoo", null, "https://www.imdb.com/title/tt1568346/", 158));
                //     list.Add(new Movie("Lockout", "Lockout", null, "https://www.imdb.com/title/tt1592525/", 95));
                //     list.Add(new Movie("Fast Five", "Fast Five", null, "https://www.imdb.com/title/tt1596343/", 130));
                //     list.Add(new Serie("Hawaii Five-0", "Hawaii Five-0", null, null));
                //     list.Add(new Movie("Alvin and the Chipmunks: Chipwrecked", "Alvin and the Chipmunks: Chipwrecked", null, "https://www.imdb.com/title/tt1615918/", 87));
                //     list.Add(new Serie("Top Gear", "Top Gear", null, null));
                //     list.Add(new Movie("Independence Day: Resurgence", "Independence Day: Resurgence", null, "https://www.imdb.com/title/tt1628841/", 120));
                //     list.Add(new Movie("Edge of Tomorrow", "Edge of Tomorrow", null, "https://www.imdb.com/title/tt1631867/", 113));
                //     list.Add(new Movie("Johnny English Reborn", "Johnny English Reborn", null, "https://www.imdb.com/title/tt1634122/", 101));
                //     list.Add(new Movie("Ted", "Ted", null, "https://www.imdb.com/title/tt1637725/", 106));
                //     list.Add(new Movie("The Christmas Bunny", "The Christmas Bunny", null, "https://www.imdb.com/title/tt1640714/", 98));
                //     list.Add(new Movie("The Dictator", "The Dictator", null, "https://www.imdb.com/title/tt1645170/", 83));
                //     list.Add(new Movie("New Kids Turbo", "New Kids Turbo", null, "https://www.imdb.com/title/tt1648112/", 84));
                //     list.Add(new Movie("Stuart Little", "Stuart Little", null, "https://www.imdb.com/title/tt0164912/", 84));
                //     list.Add(new Movie("The Revenant", "The Revenant", null, "https://www.imdb.com/title/tt1663202/", 156));
                //     list.Add(new Movie("Ice Age: Continental Drift", "Ice Age: Continental Drift", null, "https://www.imdb.com/title/tt1667889/", 88));
                //     list.Add(new Movie("Now You See Me", "Now You See Me", null, "https://www.imdb.com/title/tt1670345/", 115));
                //     list.Add(new Serie("Mag ik u kussen?", "Mag ik u kussen?", null, null));
                //     list.Add(new Movie("Intouchables", "Intouchables", null, "https://www.imdb.com/title/tt1675434/", 112));
                //     list.Add(new Movie("Despicable Me 2", "Despicable Me 2", null, "https://www.imdb.com/title/tt1690953/", 98));
                //     list.Add(new Movie("Gladiator", "Gladiator", null, "https://www.imdb.com/title/tt0172495/", 155));
                //     list.Add(new Movie("A Dog's Purpose", "A Dog's Purpose", null, "https://www.imdb.com/title/tt1753383/", 100));
                //     list.Add(new Movie("Code 37", "Code 37", null, "https://www.imdb.com/title/tt1757710/", 96));
                //     list.Add(new Movie("Apollo 18", "Apollo 18", null, "https://www.imdb.com/title/tt1772240/", 86));
                //     list.Add(new Serie("Tegen de sterren op", "Tegen de sterren op", null, null));
                //     list.Add(new Movie("Groenten uit Balen", "Groenten uit Balen", null, "https://www.imdb.com/title/tt1784458/", 110));
                //     list.Add(new Serie("The Adventures of Tintin", "The Adventures of Tintin", null, null));
                //     list.Add(new Serie("Homeland", "Homeland", null, null));
                //     list.Add(new Serie("Ed, Edd n Eddy", "Ed, Edd n Eddy", null, null));
                //     list.Add(new Movie("Captain America: The Winter Soldier", "Captain America: The Winter Soldier", null, "https://www.imdb.com/title/tt1843866/", 136));
                //     list.Add(new Movie("End of Watch", "End of Watch", null, "https://www.imdb.com/title/tt1855199/", 109));
                //     list.Add(new Movie("Furious 6", "Furious 6", null, "https://www.imdb.com/title/tt1905041/", 130));
                //     list.Add(new Movie("The Hitman's Bodyguard", "The Hitman's Bodyguard", null, "https://www.imdb.com/title/tt1959563/", 118));
                //     list.Add(new Movie("Monsters, Inc.", "Monsters, Inc.", null, "https://www.imdb.com/title/tt0198781/", 92));
                //     list.Add(new Movie("Man of Tai Chi", "Man of Tai Chi", null, "https://www.imdb.com/title/tt2016940/", 105));
                //     list.Add(new Movie("12 Years a Slave", "12 Years a Slave", null, "https://www.imdb.com/title/tt2024544/", 134));
                //     list.Add(new Movie("Michael Kohlhaas", "Michael Kohlhaas", null, "https://www.imdb.com/title/tt2054790/", 122));
                //     list.Add(new Movie("Eye in the Sky", "Eye in the Sky", null, "https://www.imdb.com/title/tt2057392/", 102));
                //     list.Add(new Movie("The Imitation Game", "The Imitation Game", null, "https://www.imdb.com/title/tt2084970/", 114));
                //     list.Add(new Serie("Black Mirror", "Black Mirror", null, null));
                //     list.Add(new Movie("Memento", "Memento", null, "https://www.imdb.com/title/tt0209144/", 113));
                //     list.Add(new Movie("Assassin's Creed", "Assassin's Creed", null, "https://www.imdb.com/title/tt2094766/", 115));
                //     list.Add(new Movie("Jagten", "Jagten", null, "https://www.imdb.com/title/tt2106476/", 115));
                //     list.Add(new Movie("Pearl Harbor", "Pearl Harbor", null, "https://www.imdb.com/title/tt0213149/", 183));
                //     list.Add(new Movie("American Sniper", "American Sniper", null, "https://www.imdb.com/title/tt2179136/", 133));
                //     list.Add(new Serie("Elementary", "Elementary", null, null));
                //     list.Add(new Serie("Arrow", "Arrow", null, null));
                //     list.Add(new Movie("The Mountain Between Us", "The Mountain Between Us", null, "https://www.imdb.com/title/tt2226597/", 112));
                //     list.Add(new Serie("Hannibal", "Hannibal", null, null));
                //     list.Add(new Movie("Kung Fu Panda 3", "Kung Fu Panda 3", null, "https://www.imdb.com/title/tt2267968/", 95));
                //     list.Add(new Movie("Frozen", "Frozen", null, "https://www.imdb.com/title/tt2294629/", 102));
                //     list.Add(new Movie("Olympus Has Fallen", "Olympus Has Fallen", null, "https://www.imdb.com/title/tt2302755/", 119));
                //     list.Add(new Serie("Line of Duty", "Line of Duty", null, null));
                //     list.Add(new Movie("The Fast and the Furious", "The Fast and the Furious", null, "https://www.imdb.com/title/tt0232500/", 106));
                //     list.Add(new Movie("White House Down", "White House Down", null, "https://www.imdb.com/title/tt2334879/", 131));
                //     list.Add(new Serie("Foute Vrienden", "Foute Vrienden", null, null));
                //     list.Add(new Serie("True Detective", "True Detective", null, null));
                //     list.Add(new Movie("Jobs", "Jobs", null, "https://www.imdb.com/title/tt2357129/", 128));
                //     list.Add(new Serie("De mol", "De mol", null, null));
                //     list.Add(new Movie("Spectre", "Spectre", null, "https://www.imdb.com/title/tt2379713/", 148));
                //     list.Add(new Movie("Mission: Impossible - Rogue Nation", "Mission: Impossible - Rogue Nation", null, "https://www.imdb.com/title/tt2381249/", 131));
                //     list.Add(new Serie("Code Lyoko Evolution", "Code Lyoko Evolution", null, null));
                //     list.Add(new Movie("Atomic Blonde", "Atomic Blonde", null, "https://www.imdb.com/title/tt2406566/", 115));
                //     list.Add(new Movie("Stuart Little 2", "Stuart Little 2", null, "https://www.imdb.com/title/tt0243585/", 77));
                //     list.Add(new Movie("Taken 3", "Taken 3", null, "https://www.imdb.com/title/tt2446042/", 108));
                //     list.Add(new Movie("Die Another Day", "Die Another Day", null, "https://www.imdb.com/title/tt0246460/", 133));
                //     list.Add(new Serie("Salamander", "Salamander", null, null));
                //     list.Add(new Movie("S.W.A.T.", "S.W.A.T.", null, "https://www.imdb.com/title/tt0257076/", 117));
                //     list.Add(new Movie("The Bourne Identity", "The Bourne Identity", null, "https://www.imdb.com/title/tt0258463/", 119));
                //     list.Add(new Movie("Marina", "Marina", null, "https://www.imdb.com/title/tt2614860/", 118));
                //     list.Add(new Movie("Finding Nemo", "Finding Nemo", null, "https://www.imdb.com/title/tt0266543/", 100));
                //     list.Add(new Movie("F.C. De Kampioenen: Kampioen zijn blijft plezant", "F.C. De Kampioenen: Kampioen zijn blijft plezant", null, "https://www.imdb.com/title/tt2671776/", 120));
                //     list.Add(new Movie("Hitman: Agent 47", "Hitman: Agent 47", null, "https://www.imdb.com/title/tt2679042/", 96));
                //     list.Add(new Movie("Ice Age", "Ice Age", null, "https://www.imdb.com/title/tt0268380/", 81));
                //     list.Add(new Movie("A Beautiful Mind", "A Beautiful Mind", null, "https://www.imdb.com/title/tt0268978/", 135));
                //     list.Add(new Serie("The Blacklist", "The Blacklist", null, null));
                //     list.Add(new Movie("Lilo & Stitch", "Lilo & Stitch", null, "https://www.imdb.com/title/tt0275847/", 85));
                //     list.Add(new Movie("Insomnia", "Insomnia", null, "https://www.imdb.com/title/tt0278504/", 118));
                //     list.Add(new Movie("Kingsman: The Secret Service", "Kingsman: The Secret Service", null, "https://www.imdb.com/title/tt2802144/", 129));
                //     list.Add(new Movie("Fast & Furious 7", "Fast & Furious 7", null, "https://www.imdb.com/title/tt2820852/", 137));
                //     list.Add(new Serie("24", "24", null, null));
                //     list.Add(new Serie("Rick and Morty", "Rick and Morty", null, null));
                //     list.Add(new Movie("Lucy", "Lucy", null, "https://www.imdb.com/title/tt2872732/", 89));
                //     list.Add(new Movie("The Recruit", "The Recruit", null, "https://www.imdb.com/title/tt0292506/", 115));
                //     list.Add(new Movie("Ad Astra", "Ad Astra", null, "https://www.imdb.com/title/tt2935510/", 123));
                //     list.Add(new Movie("The Transporter", "The Transporter", null, "https://www.imdb.com/title/tt0293662/", 92));
                //     list.Add(new Serie("Chris & Co", "Chris & Co", null, null));
                //     list.Add(new Serie("W817", "W817", null, null));
                //     list.Add(new Serie("Man bijt hond", "Man bijt hond", null, null));
                //     list.Add(new Movie("Image", "Image", null, "https://www.imdb.com/title/tt3089922/", 87));
                //     list.Add(new Movie("Love Actually", "Love Actually", null, "https://www.imdb.com/title/tt0314331/", 135));
                //     list.Add(new Movie("Cars", "Cars", null, "https://www.imdb.com/title/tt0317219/", 117));
                //     list.Add(new Movie("The Incredibles", "The Incredibles", null, "https://www.imdb.com/title/tt0317705/", 115));
                //     list.Add(new Movie("Mission: Impossible III", "Mission: Impossible III", null, "https://www.imdb.com/title/tt0317919/", 126));
                //     list.Add(new Movie("Fantastic Beasts and Where to Find Them", "Fantastic Beasts and Where to Find Them", null, "https://www.imdb.com/title/tt3183660/", 132));
                //     list.Add(new Movie("Sahara", "Sahara", null, "https://www.imdb.com/title/tt0318649/", 124));
                //     list.Add(new Movie("The Day After Tomorrow", "The Day After Tomorrow", null, "https://www.imdb.com/title/tt0319262/", 124));
                //     list.Add(new Movie("2 Fast 2 Furious", "2 Fast 2 Furious", null, "https://www.imdb.com/title/tt0322259/", 107));
                //     list.Add(new Movie("Survivor", "Survivor", null, "https://www.imdb.com/title/tt3247714/", 96));
                //     list.Add(new Serie("Tomtesterom", "Tomtesterom", null, null));
                //     list.Add(new Movie("xXx: State of the Union", "xXx: State of the Union", null, "https://www.imdb.com/title/tt0329774/", 101));
                //     list.Add(new Movie("London Has Fallen", "London Has Fallen", null, "https://www.imdb.com/title/tt3300542/", 99));
                //     list.Add(new Movie("Deep Web", "Deep Web", null, "https://www.imdb.com/title/tt3312868/", 90));
                //     list.Add(new Serie("Eigen Kweek", "Eigen Kweek", null, null));
                //     list.Add(new Movie("Live Free or Die Hard", "Live Free or Die Hard", null, "https://www.imdb.com/title/tt0337978/", 128));
                //     list.Add(new Movie("The Polar Express", "The Polar Express", null, "https://www.imdb.com/title/tt0338348/", 100));
                //     list.Add(new Movie("I, Robot", "I, Robot", null, "https://www.imdb.com/title/tt0343818/", 115));
                //     list.Add(new Serie("Forever", "Forever", null, null));
                //     list.Add(new Movie("Madagascar", "Madagascar", null, "https://www.imdb.com/title/tt0351283/", 86));
                //     list.Add(new Serie("Wauters vs Waes", "Wauters vs Waes", null, null));
                //     list.Add(new Serie("Blokken", "Blokken", null, null));
                //     list.Add(new Serie("Beau Séjour", "Beau Séjour", null, null));
                //     list.Add(new Serie("Stalker", "Stalker", null, null));
                //     list.Add(new Serie("Cordon", "Cordon", null, null));
                //     list.Add(new Movie("Mr. & Mrs. Smith", "Mr. & Mrs. Smith", null, "https://www.imdb.com/title/tt0356910/", 120));
                //     list.Add(new Movie("Incredibles 2", "Incredibles 2", null, "https://www.imdb.com/title/tt3606756/", 118));
                //     list.Add(new Movie("Inglourious Basterds", "Inglourious Basterds", null, "https://www.imdb.com/title/tt0361748/", 153));
                //     list.Add(new Serie("Spring", "Spring", null, null));
                //     list.Add(new Movie("The Martian", "The Martian", null, "https://www.imdb.com/title/tt3659388/", 144));
                //     list.Add(new Movie("Happy Feet", "Happy Feet", null, "https://www.imdb.com/title/tt0366548/", 108));
                //     list.Add(new Serie("De pappenheimers", "De pappenheimers", null, null));
                //     list.Add(new Movie("Charlie and the Chocolate Factory", "Charlie and the Chocolate Factory", null, "https://www.imdb.com/title/tt0367594/", 115));
                //     list.Add(new Movie("Indiana Jones and the Kingdom of the Crystal Skull", "Indiana Jones and the Kingdom of the Crystal Skull", null, "https://www.imdb.com/title/tt0367882/", 122));
                //     list.Add(new Movie("National Treasure", "National Treasure", null, "https://www.imdb.com/title/tt0368891/", 131));
                //     list.Add(new Movie("Iron Man", "Iron Man", null, "https://www.imdb.com/title/tt0371746/", 126));
                //     list.Add(new Movie("The Bourne Supremacy", "The Bourne Supremacy", null, "https://www.imdb.com/title/tt0372183/", 108));
                //     list.Add(new Movie("Batman Begins", "Batman Begins", null, "https://www.imdb.com/title/tt0372784/", 140));
                //     list.Add(new Movie("Snowden", "Snowden", null, "https://www.imdb.com/title/tt3774114/", 134));
                //     list.Add(new Movie("Casino Royale", "Casino Royale", null, "https://www.imdb.com/title/tt0381061/", 144));
                //     list.Add(new Serie("Professor T.", "Professor T.", null, null));
                //     list.Add(new Movie("The Da Vinci Code", "The Da Vinci Code", null, "https://www.imdb.com/title/tt0382625/", 149));
                //     list.Add(new Movie("Ratatouille", "Ratatouille", null, "https://www.imdb.com/title/tt0382932/", 111));
                //     list.Add(new Movie("Stealth", "Stealth", null, "https://www.imdb.com/title/tt0382992/", 121));
                //     list.Add(new Movie("Transporter 2", "Transporter 2", null, "https://www.imdb.com/title/tt0388482/", 87));
                //     list.Add(new Movie("Bee Movie", "Bee Movie", null, "https://www.imdb.com/title/tt0389790/", 91));
                //     list.Add(new Movie("The Space Between Us", "The Space Between Us", null, "https://www.imdb.com/title/tt3922818/", 120));
                //     list.Add(new Movie("The Pacifier", "The Pacifier", null, "https://www.imdb.com/title/tt0395699/", 95));
                //     list.Add(new Movie("Bolt", "Bolt", null, "https://www.imdb.com/title/tt0397892/", 96));
                //     list.Add(new Movie("F.C. De Kampioenen 2: Jubilee general", "F.C. De Kampioenen 2: Jubilee general", null, "https://www.imdb.com/title/tt4002200/", 96));
                //     list.Add(new Movie("Open Season", "Open Season", null, "https://www.imdb.com/title/tt0400717/", 86));
                //     list.Add(new Movie("Black", "Black", null, "https://www.imdb.com/title/tt4008758/", 95));
                //     list.Add(new Serie("De Slimste Mens ter Wereld", "De Slimste Mens ter Wereld", null, null));
                //     list.Add(new Movie("Citizenfour", "Citizenfour", null, "https://www.imdb.com/title/tt4044364/", 114));
                //     list.Add(new Movie("The Guardian", "The Guardian", null, "https://www.imdb.com/title/tt0406816/", 139));
                //     list.Add(new Movie("Cowboys & Aliens", "Cowboys & Aliens", null, "https://www.imdb.com/title/tt0409847/", 119));
                //     list.Add(new Serie("Ten oorlog", "Ten oorlog", null, null));
                //     list.Add(new Movie("Evan Almighty", "Evan Almighty", null, "https://www.imdb.com/title/tt0413099/", 96));
                //     list.Add(new Serie("Mr. Robot", "Mr. Robot", null, null));
                //     list.Add(new Movie("13 Hours", "13 Hours", null, "https://www.imdb.com/title/tt4172430/", 144));
                //     list.Add(new Serie("Code Lyoko", "Code Lyoko", null, null));
                //     list.Add(new Movie("Transformers", "Transformers", null, "https://www.imdb.com/title/tt0418279/", 144));
                //     list.Add(new Movie("Jason Bourne", "Jason Bourne", null, "https://www.imdb.com/title/tt4196776/", 123));
                //     list.Add(new Movie("Get Smart", "Get Smart", null, "https://www.imdb.com/title/tt0425061/", 110));
                //     list.Add(new Serie("De Biker Boys", "De Biker Boys", null, null));
                //     list.Add(new Movie("Real Steel", "Real Steel", null, "https://www.imdb.com/title/tt0433035/", 127));
                //     list.Add(new Movie("G-Force", "G-Force", null, "https://www.imdb.com/title/tt0436339/", 88));
                //     list.Add(new Movie("Ice Age: The Meltdown", "Ice Age: The Meltdown", null, "https://www.imdb.com/title/tt0438097/", 91));
                //     list.Add(new Movie("The Bourne Ultimatum", "The Bourne Ultimatum", null, "https://www.imdb.com/title/tt0440963/", 115));
                //     list.Add(new Movie("Kung Fu Panda", "Kung Fu Panda", null, "https://www.imdb.com/title/tt0441773/", 92));
                //     list.Add(new Serie("Quantico", "Quantico", null, null));
                //     list.Add(new Movie("The Sentinel", "The Sentinel", null, "https://www.imdb.com/title/tt0443632/", 108));
                //     list.Add(new Movie("Paddington 2", "Paddington 2", null, "https://www.imdb.com/title/tt4468740/", 103));
                //     list.Add(new Movie("Knowing", "Knowing", null, "https://www.imdb.com/title/tt0448011/", 121));
                //     list.Add(new Movie("Horton Hears a Who!", "Horton Hears a Who!", null, "https://www.imdb.com/title/tt0451079/", 86));
                //     list.Add(new Movie("Mr. Bean's Holiday", "Mr. Bean's Holiday", null, "https://www.imdb.com/title/tt0453451/", 90));
                //     list.Add(new Movie("Deja Vu", "Deja Vu", null, "https://www.imdb.com/title/tt0453467/", 126));
                //     list.Add(new Movie("The Pursuit of Happyness", "The Pursuit of Happyness", null, "https://www.imdb.com/title/tt0454921/", 117));
                //     list.Add(new Movie("Darkest Hour", "Darkest Hour", null, "https://www.imdb.com/title/tt4555426/", 125));
                //     list.Add(new Serie("Wie is de Mol?", "Wie is de Mol?", null, null));
                //     list.Add(new Serie("Quantum Break", "Quantum Break", null, null));
                //     list.Add(new Movie("The Fast and the Furious: Tokyo Drift", "The Fast and the Furious: Tokyo Drift", null, "https://www.imdb.com/title/tt0463985/", 104));
                //     list.Add(new Serie("Het geslacht De Pauw", "Het geslacht De Pauw", null, null));
                //     list.Add(new Movie("The Dark Knight", "The Dark Knight", null, "https://www.imdb.com/title/tt0468569/", 152));
                //     list.Add(new Movie("Night at the Museum", "Night at the Museum", null, "https://www.imdb.com/title/tt0477347/", 108));
                //     list.Add(new Movie("The Tree of Life", "The Tree of Life", null, "https://www.imdb.com/title/tt0478304/", 139));
                //     list.Add(new Movie("Madagascar: Escape 2 Africa", "Madagascar: Escape 2 Africa", null, "https://www.imdb.com/title/tt0479952/", 89));
                //     list.Add(new Movie("I Am Legend", "I Am Legend", null, "https://www.imdb.com/title/tt0480249/", 101));
                //     list.Add(new Movie("The Prestige", "The Prestige", null, "https://www.imdb.com/title/tt0482571/", 130));
                //     list.Add(new Movie("4: Rise of the Silver Surfer", "4: Rise of the Silver Surfer", null, "https://www.imdb.com/title/tt0486576/", 92));
                //     list.Add(new Serie("The IT Crowd", "The IT Crowd", null, null));
                //     list.Add(new Movie("Jumper", "Jumper", null, "https://www.imdb.com/title/tt0489099/", 88));
                //     list.Add(new Movie("Wanted", "Wanted", null, "https://www.imdb.com/title/tt0493464/", 110));
                //     list.Add(new Movie("Ocean's Thirteen", "Ocean's Thirteen", null, "https://www.imdb.com/title/tt0496806/", 122));
                //     list.Add(new Movie("Avatar", "Avatar", null, "https://www.imdb.com/title/tt0499549/", 162));
                //     list.Add(new Movie("Dunkirk", "Dunkirk", null, "https://www.imdb.com/title/tt5013056/", 106));
                //     list.Add(new Movie("Demain tout commence", "Demain tout commence", null, "https://www.imdb.com/title/tt5078204/", 118));
                //     list.Add(new Serie("De ideale wereld", "De ideale wereld", null, null));
                //     list.Add(new Movie("Operation Finale", "Operation Finale", null, "https://www.imdb.com/title/tt5208252/", 122));
                //     list.Add(new Movie("Sniper Special Ops", "Sniper Special Ops", null, "https://www.imdb.com/title/tt5344794/", 86));
                //     list.Add(new Movie("De Premier", "De Premier", null, "https://www.imdb.com/title/tt5357556/", 102));
                //     list.Add(new Movie("Deadpool 2", "Deadpool 2", null, "https://www.imdb.com/title/tt5463162/", 119));
                //     list.Add(new Movie("Follow: Love Life Ghent", "Follow: Love Life Ghent", null, "https://www.imdb.com/title/tt5467530/", 120));
                //     list.Add(new Serie("Dagelijkse kost", "Dagelijkse kost", null, null));
                //     list.Add(new Serie("APB", "APB", null, null));
                //     list.Add(new Movie("The Shape of Water", "The Shape of Water", null, "https://www.imdb.com/title/tt5580390/", 123));
                //     list.Add(new Movie("Dr. No", "Dr. No", null, "https://www.imdb.com/title/tt0055928/", 110));
                //     list.Add(new Serie("Genius", "Genius", null, null));
                //     list.Add(new Movie("Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb", "Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb", null, "https://www.imdb.com/title/tt0057012/", 95));
                //     list.Add(new Movie("From Russia with Love", "From Russia with Love", null, "https://www.imdb.com/title/tt0057076/", 115));
                //     list.Add(new Movie("Goldfinger", "Goldfinger", null, "https://www.imdb.com/title/tt0058150/", 110));
                //     list.Add(new Movie("Thunderball", "Thunderball", null, "https://www.imdb.com/title/tt0059800/", 130));
                //     list.Add(new Serie("Greyzone", "Greyzone", null, null));
                //     list.Add(new Serie("De Dag", "De Dag", null, null));
                //     list.Add(new Movie("Angel Has Fallen", "Angel Has Fallen", null, "https://www.imdb.com/title/tt6189022/", 121));
                //     list.Add(new Movie("You Only Live Twice", "You Only Live Twice", null, "https://www.imdb.com/title/tt0062512/", 117));
                //     list.Add(new Movie("Follow IV", "Follow IV", null, "https://www.imdb.com/title/tt6260592/", null));
                //     list.Add(new Movie("2001: A Space Odyssey", "2001: A Space Odyssey", null, "https://www.imdb.com/title/tt0062622/", 149));
                //     list.Add(new Movie("Patser", "Patser", null, "https://www.imdb.com/title/tt6438030/", 125));
                //     list.Add(new Serie("'t is gebeurd", "'t is gebeurd", null, null));
                //     list.Add(new Serie("La casa de papel", "La casa de papel", null, null));
                //     list.Add(new Movie("On Her Majesty's Secret Service", "On Her Majesty's Secret Service", null, "https://www.imdb.com/title/tt0064757/", 142));
                //     list.Add(new Serie("Wissel van de macht", "Wissel van de macht", null, null));
                //     list.Add(new Serie("Reizen Waes", "Reizen Waes", null, null));
                //     list.Add(new Movie("Diamonds Are Forever", "Diamonds Are Forever", null, "https://www.imdb.com/title/tt0066995/", 120));
                //     list.Add(new Movie("Tenet", "Tenet", null, "https://www.imdb.com/title/tt6723592/", 150));
                //     list.Add(new Movie("I Am Heath Ledger", "I Am Heath Ledger", null, "https://www.imdb.com/title/tt6739646/", 90));
                //     list.Add(new Serie("The Putin Interviews", "The Putin Interviews", null, null));
                //     list.Add(new Movie("The Godfather", "The Godfather", null, "https://www.imdb.com/title/tt0068646/", 175));
                //     list.Add(new Serie("Alex Rider", "Alex Rider", null, null));
                //     list.Add(new Movie("Live and Let Die", "Live and Let Die", null, "https://www.imdb.com/title/tt0070328/", 121));
                //     list.Add(new Movie("The Man with the Golden Gun", "The Man with the Golden Gun", null, "https://www.imdb.com/title/tt0071807/", 125));
                //     list.Add(new Serie("Undercover", "Undercover", null, null));
                //     list.Add(new Movie("Joker", "Joker", null, "https://www.imdb.com/title/tt7286456/", 122));
                //     list.Add(new Serie("Bodyguard", "Bodyguard", null, null));
                //     list.Add(new Movie("Niet Schieten", "Niet Schieten", null, "https://www.imdb.com/title/tt7534314/", 139));
                //     list.Add(new Serie("De Twaalf", "De Twaalf", null, null));
                //     list.Add(new Movie("The Spy Who Loved Me", "The Spy Who Loved Me", null, "https://www.imdb.com/title/tt0076752/", 125));
                //     list.Add(new Movie("Man of Steel", "Man of Steel", null, "https://www.imdb.com/title/tt0770828/", 143));
                //     list.Add(new Serie("Dexter", "Dexter", null, null));
                //     list.Add(new Serie("Manhunt", "Manhunt", null, null));
                //     list.Add(new Movie("Moonraker", "Moonraker", null, "https://www.imdb.com/title/tt0079574/", 126));
                //     list.Add(new Movie("Bangkok Dangerous", "Bangkok Dangerous", null, "https://www.imdb.com/title/tt0814022/", 99));
                //     list.Add(new Movie("Interstellar", "Interstellar", null, "https://www.imdb.com/title/tt0816692/", 169));
                //     list.Add(new Movie("World War Z", "World War Z", null, "https://www.imdb.com/title/tt0816711/", 116));
                //     list.Add(new Movie("Shooter", "Shooter", null, "https://www.imdb.com/title/tt0822854/", 124));
                //     list.Add(new Movie("For Your Eyes Only", "For Your Eyes Only", null, "https://www.imdb.com/title/tt0082398/", 127));
                //     list.Add(new Movie("The Bucket List", "The Bucket List", null, "https://www.imdb.com/title/tt0825232/", 97));
                //     list.Add(new Serie("Vrede op Aarde", "Vrede op Aarde", null, null));
                //     list.Add(new Movie("Dracula Untold", "Dracula Untold", null, "https://www.imdb.com/title/tt0829150/", 92));
                //     list.Add(new Movie("Raiders of the Lost Ark", "Raiders of the Lost Ark", null, "https://www.imdb.com/title/tt0082971/", 115));
                //     list.Add(new Movie("Quantum of Solace", "Quantum of Solace", null, "https://www.imdb.com/title/tt0830515/", 106));
                //     list.Add(new Movie("Cloudy with a Chance of Meatballs", "Cloudy with a Chance of Meatballs", null, "https://www.imdb.com/title/tt0844471/", 90));
                //     list.Add(new Movie("1917", "1917", null, "https://www.imdb.com/title/tt8579674/", 119));
                //     list.Add(new Movie("Octopussy", "Octopussy", null, "https://www.imdb.com/title/tt0086034/", 131));
                //     list.Add(new Movie("Indiana Jones and the Temple of Doom", "Indiana Jones and the Temple of Doom", null, "https://www.imdb.com/title/tt0087469/", 118));
                //     list.Add(new Movie("The Karate Kid", "The Karate Kid", null, "https://www.imdb.com/title/tt0087538/", 126));
                //     list.Add(new Movie("Police Academy", "Police Academy", null, "https://www.imdb.com/title/tt0087928/", 96));
                //     list.Add(new Movie("Undercover in the Alt-Right", "Undercover in the Alt-Right", null, "https://www.imdb.com/title/tt8836342/", 57));
                //     list.Add(new Movie("Back to the Future", "Back to the Future", null, "https://www.imdb.com/title/tt0088763/", 116));
                //     list.Add(new Movie("How to Train Your Dragon", "How to Train Your Dragon", null, "https://www.imdb.com/title/tt0892769/", 98));
                //     list.Add(new Movie("Monsters vs. Aliens", "Monsters vs. Aliens", null, "https://www.imdb.com/title/tt0892782/", 94));
                //     list.Add(new Movie("Extraction", "Extraction", null, "https://www.imdb.com/title/tt8936646/", 116));
                //     list.Add(new Movie("Police Academy 2: Their First Assignment", "Police Academy 2: Their First Assignment", null, "https://www.imdb.com/title/tt0089822/", 87));
                //     list.Add(new Movie("A View to a Kill", "A View to a Kill", null, "https://www.imdb.com/title/tt0090264/", 131));
                //     list.Add(new Movie("WALL·E", "WALL·E", null, "https://www.imdb.com/title/tt0910970/", 98));
                //     list.Add(new Movie("The Boy in the Striped Pyjamas", "The Boy in the Striped Pyjamas", null, "https://www.imdb.com/title/tt0914798/", 94));
                //     list.Add(new Movie("Police Academy 3: Back in Training", "Police Academy 3: Back in Training", null, "https://www.imdb.com/title/tt0091777/", 83));
                //     list.Add(new Serie("Flikken Maastricht", "Flikken Maastricht", null, null));
                //     list.Add(new Movie("Good Morning, Vietnam", "Good Morning, Vietnam", null, "https://www.imdb.com/title/tt0093105/", 121));
                //     list.Add(new Movie("The Living Daylights", "The Living Daylights", null, "https://www.imdb.com/title/tt0093428/", 130));
                //     list.Add(new Movie("Taken", "Taken", null, "https://www.imdb.com/title/tt0936501/", 90));
                //     list.Add(new Movie("Police Academy 4: Citizens on Patrol", "Police Academy 4: Citizens on Patrol", null, "https://www.imdb.com/title/tt0093756/", 88));
                //     list.Add(new Movie("Source Code", "Source Code", null, "https://www.imdb.com/title/tt0945513/", 93));
                //     list.Add(new Movie("Green Zone", "Green Zone", null, "https://www.imdb.com/title/tt0947810/", 115));
                //     list.Add(new Movie("Aanrijding in Moscou", "Aanrijding in Moscou", null, "https://www.imdb.com/title/tt0948530/", 102));
                //     list.Add(new Movie("Alvin and the Chipmunks", "Alvin and the Chipmunks", null, "https://www.imdb.com/title/tt0952640/", 92));
                //     list.Add(new Movie("Police Academy 5: Assignment: Miami Beach", "Police Academy 5: Assignment: Miami Beach", null, "https://www.imdb.com/title/tt0095882/", 90));
                //     list.Add(new Serie("Blackadder Goes Forth", "Blackadder Goes Forth", null, null));
                //     list.Add(new Serie("Mr. Bean", "Mr. Bean", null, null));
                //     list.Add(new Movie("Back to the Future Part II", "Back to the Future Part II", null, "https://www.imdb.com/title/tt0096874/", 108));
                //     list.Add(new Movie("Indiana Jones and the Last Crusade", "Indiana Jones and the Last Crusade", null, "https://www.imdb.com/title/tt0097576/", 127));
                //     list.Add(new Movie("Licence to Kill", "Licence to Kill", null, "https://www.imdb.com/title/tt0097742/", 133));
                //     list.Add(new Movie("Police Academy 6: City Under Siege", "Police Academy 6: City Under Siege", null, "https://www.imdb.com/title/tt0098105/", 84));
                //     list.Add(new Movie("The Adventures of Tintin", "The Adventures of Tintin", null, "https://www.imdb.com/title/tt0983193/", 107));
                //     list.Add(new Movie("Valkyrie", "Valkyrie", null, "https://www.imdb.com/title/tt0985699/", 121));
                //     list.Add(new Serie("Willy's en marjetten", "Willy's en marjetten", null, null));
                //     list.Add(new Movie("Sherlock Holmes", "Sherlock Holmes", null, "https://www.imdb.com/title/tt0988045/", 128));
                //     list.Add(new Movie("Back to the Future Part III", "Back to the Future Part III", null, "https://www.imdb.com/title/tt0099088/", 118));
                //     list.Add(new Movie("The Wolf of Wall Street", "The Wolf of Wall Street", null, "https://www.imdb.com/title/tt0993846/", 180));
                //     list.Add(new Movie("Home Alone", "Home Alone", null, "https://www.imdb.com/title/tt0099785/", 103));
                //     
                 #endregion imdb
                
                //_dbContext.Items.AddRange(list);
                #region books
                var options = new LaunchOptions { Headless = true };
            
                Console.WriteLine("Downloading chromium");
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

                string[] keywordsBooks1 = { "sibian", "test"};
                var bookLinks1 = new List<string> {"https://www.standaardboekhandel.be/p/stemmen-aan-het-front-9789026150982", "https://www.standaardboekhandel.be/p/dingen-die-je-alleen-ziet-als-je-er-tijd-voor-neemt-9789022581124"};
                await CreateBookRecommendation(keywordsBooks1, bookLinks1, options, _dbContext);
                #endregion
                
                #region users
/*----------------------------------USERS--------------------------------------------------------------------------------------------------------------------------*/
                Customer customer = new Customer { Email = "sibian.dg@student.hogent.be", FirstName = "Sibian", LastName = "DG" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "P@ssword1111");
                Customer student = new Customer { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                await CreateUser(student.Email, "P@ssword1111");
                #endregion
                
                _dbContext.SaveChanges();
            }
        }
        
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
        
        private async Task CreateBookRecommendation(string[] keywords, List<string> links, LaunchOptions options, ApplicationDbContext context)
        {
            List<Item> books = new List<Item>();


            foreach (string link in links)
            {
                Console.WriteLine("Navigating to "+link);

                using (var browser = await Puppeteer.LaunchAsync(options))
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync(link);
                    var title = await page.QuerySelectorAsync(".c-product-detail__title--badge").EvaluateFunctionAsync<string>("_ => _.innerText");
                    var summary = await page.QuerySelectorAsync(".c-product-detail__description")
                        .EvaluateFunctionAsync<string>("_ => _.lastElementChild.innerHTML");
                    summary = summary.Trim();
                    summary = summary.Replace("<p>", " ");
                    summary = summary.Replace("</p>", " ");
                    summary = summary.Replace("<br>", "\n");

                    
                    var pageList = await page.EvaluateFunctionAsync(@"(page) => {
                    const anchors = Array.from(document.querySelectorAll(page));
                    return anchors.map(anchor => anchor.innerText)}", ".c-list--semantic.c-product-specs > li:nth-child(1)");
                    string pageSize = (string)pageList[1];
                    pageSize = pageSize.Split(':')[1];
                    Regex.Replace(pageSize, @"\s+", "");

                    var imageList =  await page.EvaluateFunctionAsync(
                        @"(x) => Array.from(document.querySelectorAll(x)).map(a => a.href)",  ".c-product__slider a[data-lightbox=\"products\"]");
                    //imageList contains front en back. TODO: implement back into application :)
                    var front = imageList[0];
                    books.Add(new Book(title, summary, (string)front, link, int.Parse(pageSize)));
                    
                    _dbContext.Recommendations.Add(new Recommendation(keywords, books));


                }
            }


        }
    }
}