using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DevloperTeamRepository
    {
        private readonly List<DevTeam> _devteamRepository = new List<DevTeam>();
        private DevloperRepository _developerRepository;

        public DevloperTeamRepository(DevloperRepository devloperRepository)
        {
            _developerRepository = devloperRepository;
        }

        public int _count;

        public bool AddDevTeam(DevTeam devTeam)   //create
        {
            if (devTeam == null)
            {
                return false;

            }
            else
            {
                _count++;
                devTeam.TeamID = _count;
                _devteamRepository.Add(devTeam);
                return true;
            }
        }


        public List<DevTeam> GetDevTeams()
        {
            return _devteamRepository;
        }

        public DevTeam GetDeveloper(int id)  //helper method
        {
            foreach (DevTeam teamDeveloper in _devteamRepository)
            {
                if (teamDeveloper.TeamID == id)
                {
                    return teamDeveloper;
                }
            }
            return null;
        }

        public bool UpdateTeamDeveloper(int originalID, DevTeam newDevteamDeveloper) //update
        {
            DevTeam oldTeamID = GetDeveloper(originalID);

            if (oldTeamID != null)
            {
                oldTeamID.TeamName = newDevteamDeveloper.TeamName;
                oldTeamID.TeamID = newDevteamDeveloper.TeamID;
                oldTeamID.Developers = newDevteamDeveloper.Developers;                
                return true;
            }
            else
            {
                return false;
            }

        }


        //update -- add existing developer to team
        public bool AddDeveloperToExistingTeam(int teamID, int developerId)         //return type - do we need it to report back or not
        {
           //Find the Team
           DevTeam devTeam = GetDeveloper(teamID);                                //Call a specific team
           Developer devToAdd = _developerRepository.GetDeveloper(developerId);   ///Call a specific Developer
           int startingCount = devTeam.Developers.Count;
           

            foreach (var dev in devTeam.Developers)
            {
                if (dev.FullName == devToAdd.FullName)
                {
                    return false;
                }
            }
           devTeam.Developers.Add(devToAdd);                                         ///add to teams

            return (startingCount < devTeam.Developers.Count);
        }

        public bool RemoveDeveloperToExistingTeam(int teamID, int developerId)      //return type - do we need it to report back or not
        {
            //Find the Team
            DevTeam devTeam = GetDeveloper(teamID);                                   /// Call a specific team
            Developer devToRemove = _developerRepository.GetDeveloper(developerId);   /// Call a specific Developer
            int startingCount = devTeam.Developers.Count;

            devTeam.Developers.Remove(devToRemove);                                   /// add to teams

            return (startingCount > devTeam.Developers.Count);
        }




        //delete
        public bool DeleteExistingTeamDeveloper(DevTeam movedOnEmployee)   //delete
        {
            bool removeEmployee = _devteamRepository.Remove(movedOnEmployee);
            return removeEmployee;
        }

        public bool RemoveDevTeamFromList(int devTeamId)
        {

            DevTeam teamToRemove = GetDeveloper(devTeamId);

            int currentCount = _devTeams.Count;
            _devteamRepository.Remove(teamToRemove);

            if (currentCount > _devteamRepository.Count)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }

	

        
}
