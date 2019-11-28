using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment_ClientApp2
{

    // **************************************************
    //
    // Assessment: Client App 2.0
    // Author: Alex Suarez
    // Dated: 11/25/19
    // Level (Novice, Apprentice, or Master): 
    //
    // **************************************************    

    class Program
    {
        /// <summary>
        /// Main method - app starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //
            // initialize monster list from method
            //
            List<Monster> monsters = ReadFromDataFile();

            //
            // read monsters from data file
            //
            //List<Monster> monsters = ReadFromDataFile();

            //
            // application flow
            //
            DisplayWelcomeScreen();
            DisplayMenuScreen(monsters);
            DisplayClosingScreen();
        }

        #region UTILITY METHODS

        /// <summary>
        /// initialize a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>
        static List<Monster> InitializeMonsterList()
        {
            //
            // create a list of monsters
            // note: list and object initializers used
            //
            List<Monster> monsters = new List<Monster>()
            {

                new Monster()
                {
                    Name = "Sid",
                    Age = 145,
                    Attitude = Monster.EmotionalState.happy,
                    Tribe = Monster.Home.Tipa,
                    Active = true
                },

                new Monster()
                {
                    Name = "Lucy",
                    Age = 125,
                    Attitude = Monster.EmotionalState.bored,
                    Tribe = Monster.Home.Lynari,
                    Active = false
                },

                new Monster()
                {
                    Name = "Bill",
                    Age = 934,
                    Attitude = Monster.EmotionalState.sad,
                    Tribe = Monster.Home.Alfitaria,
                    Active = true
                },

                new Monster()
                {
                    Name = "Joe",
                    Age = 830,
                    Attitude = Monster.EmotionalState.angry,
                    Tribe = Monster.Home.Shella,
                    Active = false
                },

                new Monster()
                {
                    Name = "Candice",
                    Age = 231,
                    Attitude = Monster.EmotionalState.happy,
                    Tribe = Monster.Home.Fum,
                    Active = true
                }

            };

            return monsters;
        }

        #endregion

        #region SCREEN DISPLAY METHODS

        /// <summary>
        /// SCREEN: display and process menu options
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) List All Monsters");
                Console.WriteLine("\tb) View Monster Detail");
                Console.WriteLine("\tc) Add Monster");
                Console.WriteLine("\td) Delete Monster");
                Console.WriteLine("\te) Update Monster");
                Console.WriteLine("\tf) Save Monsters");
                Console.WriteLine("\tg) Filter Monster List");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllMonsters(monsters);
                        break;

                    case "b":
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case "c":
                        DisplayAddMonster(monsters);
                        break;

                    case "d":
                        DisplayDeleteMonster(monsters);
                        break;

                    case "e":
                        DisplayUpdateMonster(monsters);
                        break;

                    case "f":
                        DisplayWriteToDataFile(monsters);
                        break;

                    case "g":
                        DisplayFilterSelect(monsters);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        /// <summary>
        /// SCREEN: list all monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");

            MonsterTable(monsters);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: monster detail
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            MonsterInfo(selectedMonster);
            Console.WriteLine("\t*********************");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: add a monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster newMonster = new Monster();

            DisplayScreenHeader("Add Monster");

            //
            // add monster object property values
            //
            Console.Write("\tName: ");
            newMonster.Name = Console.ReadLine();
            Console.Write("\tAge: ");
            int.TryParse(Console.ReadLine(), out int age);
            newMonster.Age = age;
            Console.Write("\tAttitude: [Happy, Sad, Angry, Bored]");
            Enum.TryParse(Console.ReadLine().ToLower(), out Monster.EmotionalState attitude);
            newMonster.Attitude = attitude;
            Console.Write("\tTribe: [Tipa, Marr, Alfitaria, Shella, Fum, Lynari]");
            Enum.TryParse(Console.ReadLine(), out Monster.Home tribe);
            newMonster.Tribe = tribe;
            Console.Write("\tActivity: [True, False]");
            bool.TryParse(Console.ReadLine(), out bool activity);
            newMonster.Active = activity;
            Console.WriteLine("\tBirthdate: [MM/DD]");
            DateTime.TryParse(Console.ReadLine(), out DateTime birthdate);
            newMonster.Birthdate = birthdate;

            //
            // echo new monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(newMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        /// <summary>
        /// SCREEN: delete monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                monsters.Remove(selectedMonster);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{monsterName} not found");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: update monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        /// 
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            bool validResponse = false;
            Monster selectedMonster = null;

            do
            {
                DisplayScreenHeader("Update Monster");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.Write("\tEnter name:");
                string monsterName = Console.ReadLine();

                //
                // get monster object
                //

                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong name choice
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct name.");
                    DisplayContinuePrompt();
                }

                //
                // update monster
                //

            } while (!validResponse);


            //
            // update monster properties
            //
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
            Console.WriteLine();
            Console.Write($"\tCurrent Name: {selectedMonster.Name} New Name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedMonster.Name = userResponse;
            }

            Console.Write($"\tCurrent Age: {selectedMonster.Age} New Age: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                int.TryParse(userResponse, out int age);
                selectedMonster.Age = age;
            }

            Console.Write($"\tCurrent Attitude: {selectedMonster.Attitude} New Attitude: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                Enum.TryParse(userResponse, out Monster.EmotionalState attitude);
                selectedMonster.Attitude = attitude;
            }

            Console.Write($"\tCurrent Tribe: {selectedMonster.Tribe} New Tribe: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                Enum.TryParse(userResponse, out Monster.Home tribe);
                selectedMonster.Tribe = tribe;
            }

            Console.Write($"\tCurrent Activity: {selectedMonster.Active} New Activity: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                bool.TryParse(userResponse, out bool activity);
                selectedMonster.Active = activity;
            }

            Console.Write($"\tCurrent Birthdate: {selectedMonster.Birthdate.Month}/{selectedMonster.Birthdate.Day} New Birthdate: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                DateTime.TryParse(userResponse, out DateTime birthdate);
                selectedMonster.Birthdate = birthdate;
            }

            //
            // echo updated monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(selectedMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: write list of monsters to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayWriteToDataFile(List<Monster> monsters)
        {
            DisplayScreenHeader("Write to Data File");

            //
            // prompt the user to continue
            //
            Console.WriteLine("\tThe application is ready to write to the data file.");
            Console.Write("\tEnter 'y' to continue or 'n' to cancel.");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayContinuePrompt();
                WriteToDataFile(monsters);
                //
                // TODO process I/O exceptions
                //

                Console.WriteLine();
                Console.WriteLine("\tList written to data the file.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tList not written to the data file.");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterSelect(List<Monster> monsters)
        {
            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Filter Type");
                Console.WriteLine("\ta) Name");
                Console.WriteLine("\tb) Age");
                Console.WriteLine("\tc) Attitude");
                Console.WriteLine("\td) Tribe");
                Console.WriteLine("\te) Activity");
                Console.WriteLine("\tf) Birthdate");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        DisplayFilteredMonsterList(monsters, 0);
                        break;

                    case "b":
                        DisplayFilteredMonsterList(monsters, 1);
                        break;

                    case "c":
                        DisplayFilteredMonsterList(monsters, 2);
                        break;

                    case "d":
                        DisplayFilteredMonsterList(monsters, 3);
                        break;

                    case "e":
                        DisplayFilteredMonsterList(monsters, 4);
                        break;

                    case "f":
                        DisplayFilteredMonsterList(monsters, 5);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);
        }

        static void DisplayFilteredMonsterList(List<Monster> monsters, int filterType)
        {
            List<Monster> filteredMonsters = new List<Monster>();
            DisplayScreenHeader("Filtered Monster List");
            switch (filterType)
            {
                case 0:
                    Console.Write($"Desired Name:");
                    string filterName = Console.ReadLine();
                    foreach (Monster monster in monsters)
                    {
                        if (monster.Name.ToUpper() == filterName.ToUpper()) filteredMonsters.Add(monster);

                    }
                    break;
                case 1:
                    Console.Write($"Desired Age:");
                    int.TryParse(Console.ReadLine(), out int filterAge);
                    foreach (Monster monster in monsters)
                    {
                        if (monster.Age == filterAge) filteredMonsters.Add(monster);
                    }
                    break;
                case 2:
                    Console.Write($"Desired Attitude:");
                    Enum.TryParse<Monster.EmotionalState>(Console.ReadLine().ToLower(),
                        out Monster.EmotionalState filterAttitude);
                    foreach (Monster monster in monsters)
                    {
                        if (monster.Attitude == filterAttitude) filteredMonsters.Add(monster);
                    }
                    break;
                case 3:
                    Console.Write($"Desired Tribe:");
                    Enum.TryParse<Monster.Home>(ToTitleCase(Console.ReadLine()),
                        out Monster.Home filterTribe);
                    foreach (Monster monster in monsters)
                    {
                        if (monster.Tribe == filterTribe) filteredMonsters.Add(monster);
                    }
                    break;
                case 4:
                    Console.Write($"Desired Activity:");
                    bool.TryParse(Console.ReadLine(), out bool filterActivity);
                    foreach (Monster monster in monsters)
                    {
                        if (monster.Active == filterActivity) filteredMonsters.Add(monster);
                    }
                    break;
                case 5:
                    Console.Write($"Desired Birthdate [MM/DD]: ");
                    DateTime.TryParse(Console.ReadLine(), out DateTime filterDate);
                    foreach (Monster monster in monsters)
                    {
                        if (monster.Birthdate.Month == filterDate.Month
                            && monster.Birthdate.Day == filterDate.Day) filteredMonsters.Add(monster);
                    }
                    break;
                default:
                    Console.WriteLine("How did you even manage to do this.");
                    break;
            }
            DisplayAllMonsters(filteredMonsters);
        }

        #endregion

        #region FILE I/O METHODS

        /// <summary>
        /// write monster list to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void WriteToDataFile(List<Monster> monsters)
        {
            string[] monstersString = new string[monsters.Count];

            //
            // create array of monster strings
            //
            for (int index = 0; index < monsters.Count; index++)
            {
                string monsterString =
                    monsters[index].Name + "," +
                    monsters[index].Age + "," +
                    monsters[index].Attitude + "," +
                    monsters[index].Tribe + "," +
                    monsters[index].Active + "," +
                    monsters[index].Birthdate;

                monstersString[index] = monsterString;
            }

            File.WriteAllLines("Data\\Data.txt", monstersString);
        }

        /// <summary>
        /// read monsters from data file and return a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>        
        static List<Monster> ReadFromDataFile()
        {
            List<Monster> monsters = new List<Monster>();

            //
            // read all lines in the file
            //
            string[] monstersString = File.ReadAllLines("Data\\Data.txt");

            //
            // create monster objects and add to the list
            //
            foreach (string monsterString in monstersString)
            {
                //
                // get individual properties
                //
                string[] monsterProperties = monsterString.Split(',');

                //
                // create monster
                //
                Monster newMonster = new Monster();

                //
                // update monster property values
                //
                newMonster.Name = monsterProperties[0];

                int.TryParse(monsterProperties[1], out int age);
                newMonster.Age = age;

                Enum.TryParse(monsterProperties[2], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                Enum.TryParse(monsterProperties[3], out Monster.Home home);
                newMonster.Tribe = home;

                bool.TryParse(monsterProperties[4], out bool active);
                newMonster.Active = active;

                DateTime.TryParse(monsterProperties[5], out DateTime birthdate);
                newMonster.Birthdate = birthdate;

                //
                // add new monster to list
                //
                monsters.Add(newMonster);
            }

            return monsters;
        }

        #endregion

        #region CONSOLE HELPER METHODS

        /// <summary>
        /// display all monster properties
        /// </summary>
        /// <param name="monster">monster object</param>
        static void MonsterInfo(Monster monster)
        {
            if (monster != null)
            {
                Console.WriteLine($"\tName: {monster.Name}");
                Console.WriteLine($"\tAge: {monster.Age}");
                Console.WriteLine($"\tAttitude: {monster.Attitude}");
                Console.WriteLine($"\tTribe: {monster.Tribe}");
                Console.WriteLine($"\tActive: {monster.Active}");
                Console.WriteLine($"\tBirthdate: {monster.Birthdate.Month}/{monster.Birthdate.Day}");
                Console.WriteLine("\t" + monster.Greeting());
            }

        }

        static void MonsterTable(List<Monster> monsters)
        {
            string asteriskBar = new string('*', 60);
            Console.WriteLine("{0,10} {1,5} {2,10} {3,12} {4,7} {5,7}", "Name", "Age", "Attitude", "Tribe", "Active", "Birthdate");
            Console.WriteLine(asteriskBar);
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("{0,10} {1,5} {2,10} {3,12} {4,7} {5,7}",
                    monster.Name, monster.Age, monster.Attitude, monster.Tribe, monster.Active, monster.Birthdate.Month + "/" + monster.Birthdate.Day);
            }
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Monster Tracker");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Monster Tracker!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// Turns a one-word string into title case.
        /// </summary>
        static string ToTitleCase(string string1)
        {
            StringBuilder sb = new StringBuilder();
            char[] charArray1 = string1.ToCharArray();
            for (int i = 0; i < string1.Length; i++)
            {
                if (i == 0) sb.Append(charArray1[i].ToString().ToUpper());
                if (i != 0) sb.Append(charArray1[i].ToString().ToLower());
            }
            string string2 = sb.ToString();
            return string2;
        }


        #endregion
    }
}
