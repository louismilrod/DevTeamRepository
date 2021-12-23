using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
using System.Threading.Tasks;
using DevTeams_Repository;
using System.Threading;

namespace DevTeamsUI
{
    public class Program_UI
    {
        private readonly DevloperRepository _devRepository = new DevloperRepository();

        private readonly DevloperTeamRepository _devTeamRepo;
        public Program_UI()
        {
            _devTeamRepo = new DevloperTeamRepository(_devRepository);
        }

        public void Run()
        {
            Seed();
            RunApplication();
        }

        public void RunApplication()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welcome to dev teams\n" +
                    "1. Add A Developer \n" +
                    "2. View All Existing Developers \n" +
                    "3. View a single Developer\n" +
                    "4. See what developer have plural sight\n" +
                    "5. Delete An Existing Developer\n" +
                    "6. View Dev Teams \n" +
                    "7. Create Dev Teams\n" +
                    "8. Add A Developer To A Team \n" +
                    "9. Remove Dev Team \n" +
                    "10. Exit");

                string userInput = ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddADeveloper();
                        break;
                    case "2":
                        ViewAllExistingDevelopers();
                        break;
                    case "3":
                        ViewASingleDeveloper();
                        break;
                    case "4":
                        GetDevThatHasPluralSight();
                        break;
                    case "5":
                        DeleteAnExistingDeveloper();
                        break;
                    case "6":
                        ViewDevTeams();
                        break;
                    case "7":
                        CreateDevTeam();
                        break;
                    case "8":
                        AddDeveloperToDevTeam();
                        break;
                    case "9":
                        RemoveDevTeam();
                        break;
                    case "10":
                        isRunning = false;
                        break;
                    default:
                        WriteLine("INVALID SELCTION");
                        WaitForKey();
                        break;
                }
                Clear();

            }
        }

        private void AddADeveloper()
        {
            Clear();
            WriteLine("Write the first name of the developer you wish to add?");
            Developer newDeveloper = new Developer();
            newDeveloper.FirstName = ReadLine();
            WriteLine("Please write the last name of the devloper you wish to add");
            newDeveloper.LastName = ReadLine();

            WriteLine("Does this developer have plural sight? \n" +
                "y/n?");
            string userInput = ReadLine();
            if (userInput == "Y".ToLower())
            {
                newDeveloper.HasPluralSight = true;
            }
            if (userInput == "N".ToLower())
            {
                newDeveloper.HasPluralSight = false;
            }
            else
            {
                WriteLine("Please enter a valid selection");
            }
            _devRepository.AddContentToDirectory(newDeveloper);
        }
        private void ViewAllExistingDevelopers()
        {
            Clear();
            List<Developer> developers = _devRepository.GetDeveloper();
            foreach (Developer developer in developers)
            {
                WriteLine($"Developers names is: {developer.FullName} \n" +
                    $"Developer has plural sight: {developer.HasPluralSight}\n" +
                    $"The developers ID is: {developer.ID}\n" +
                    $" ");
            }
            ReadKey();
        }
        private void ViewASingleDeveloper()
        {
            Console.Clear();
            var dev = _devRepository.GetDeveloper(1);
            WriteLine(dev.ToString());
            ReadKey();
        }
        private void GetDevThatHasPluralSight()
        {
            Clear();
            WriteLine("Here is a list of all the developers plural sight status: \n" +
                "");
            List<Developer> developers = _devRepository.GetDeveloper();
            foreach (Developer developer in developers)
            {
                WriteLine($"Developer ID: {developer.ID}\n" +
                    $"Developers names is: {developer.FullName} \n" +
                    $"Developers plural sight status is: {developer.HasPluralSight}\n" +
                    " ");
            }
            ReadKey();

        }
        private void DeleteAnExistingDeveloper()
        {
            Clear();
            ViewAllExistingDevelopersButSleep();
            Console.WriteLine("Please select the developer you wish to remove by ID");
            Developer item = _devRepository.GetDeveloper(int.Parse(ReadLine()));
            if (item != null)
            {
                var success = _devRepository.DeleteExistingDeveloper(item);
                if (success)
                {
                    Console.WriteLine("SUCCESS");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }
            }
        }
        private void ViewDevTeams()
        {
            //Clear();
            List<DevTeam> devTeams = _devTeamRepo.GetDevTeams();
            foreach (DevTeam devteam in devTeams)
            {
                foreach (var developer in devteam.Developers)
                {
                    WriteLine($"{developer.FullName}");
                }
                WriteLine($"Are on team {devteam.TeamName}\n" +
                    $"The team ID is: { devteam.TeamID}\n" +
                    $" ");
            }

            ReadKey();
        }

        private void CreateDevTeam()
        {
            bool isLooping = true;
            if (isLooping)
            {
                Clear();
                DevTeam newDevTeam = new DevTeam();
                WriteLine("Enter the name of the new development team: \n");
                string devTeamName = ReadLine();
                newDevTeam.TeamName = devTeamName;

                bool devTeamCreation = _devTeamRepo.AddDevTeam(newDevTeam);

                if (devTeamCreation == true)
                {
                    WriteLine("Team created!");
                    ReadKey();
                }
                else
                {
                    WriteLine("Team Creation Failed");
                    Thread.Sleep(500);
                    isLooping = false;
                }

            }
        }

        private void AddDeveloperToDevTeam()
        {
            List<Developer> newDeveloper = _devRepository.GetDeveloper();
            List<DevTeam> newTeam = _devTeamRepo.GetDevTeams();
            //newTeam.TeamID = 

            Clear();
            ViewAllExistingDevelopersButSleep();
            WriteLine("\n" +
                "Please select the developer by ID");
            int userInput = Convert.ToInt32(ReadLine());

            Developer chosenDevloper = _devRepository.GetDeveloper(userInput);
            ViewDevTeams();
            WriteLine("\n Please select a team to add to:");
            int userInputTwo = Convert.ToInt32(ReadLine());
            DevTeam chosenTeam = _devTeamRepo.GetDeveloper(userInputTwo);  ///how to set int as _count??

            var success = _devTeamRepo.AddDeveloperToExistingTeam(userInputTwo, chosenDevloper.ID);
            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAIL");
            }

        }
        private void RemoveDevTeam()
        {
            List<DevTeam> removeDevTeam = _devTeamRepo.GetDevTeams();

            Console.Clear();

            
            ViewDevTeams();

            Console.WriteLine("Enter the ID of the team you wish to remove. \n");
            string teamRemoval = Console.ReadLine();

            int convertedTeamRemoval = Convert.ToInt32(teamRemoval);

            _devTeamRepo.RemoveDevTeamFromList(convertedTeamRemoval);

        }

        private void WaitForKey()
            {
                ReadKey();
            }

        private void Seed()
            {
                Developer louis = new Developer();
                louis.FirstName = "Louis";
                louis.LastName = "Milrod";
                louis.HasPluralSight = false;
                _devRepository.AddContentToDirectory(louis);

                Developer victoria = new Developer();
                victoria.FirstName = "Victoria";
                victoria.LastName = "Gundaker";
                victoria.HasPluralSight = true;
                _devRepository.AddContentToDirectory(victoria);

                DevTeam teamHouse = new DevTeam();
                teamHouse.TeamName = "Team House";
                teamHouse.Developers.Add(louis);
                teamHouse.Developers.Add(victoria);
                _devTeamRepo.AddDevTeam(teamHouse);
            }


        private void ViewAllExistingDevelopersButSleep()
            {
                Clear();
                List<Developer> developers = _devRepository.GetDeveloper();
                foreach (Developer developer in developers)
                {
                    WriteLine($"Developers names is: {developer.FullName} \n" +
                        $"Developer has plural sight: {developer.HasPluralSight}\n" +
                        $"The developers ID is: {developer.ID}\n" +
                        $" ");
                }
                Thread.Sleep(250);
            }


        //asks for team, team id, should be a method that takes in devid and and lists them taht way
        //hould be able to see a list of existing developers to choose from and add to existing teams
        //ViewAllExistingDevelopers()  UpdateTeamDeveloper()






    }
     
    
}
