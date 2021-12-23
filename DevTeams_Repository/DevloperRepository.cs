using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{


    public class DevloperRepository
    {
        private readonly List<Developer> _developerRepo = new List<Developer>();
        private int _count;

        public bool AddContentToDirectory(Developer newDev)   //create
        {
            if (newDev == null)
            {
                return false;

            }

            foreach (var dev in _developerRepo)
            {
                if (dev.FirstName == newDev.FirstName && dev.LastName == newDev.LastName)
                {
                    return false;
                }
            }
                _count++;
                newDev.ID = _count;
                _developerRepo.Add(newDev);
                return true;
            
        }

        public List<Developer> GetDeveloper()           //read
        {
            return _developerRepo;
        }

        public Developer GetDeveloper(int id)  //helper method
        {
            foreach (Developer devloper in _developerRepo)
            {
                if (devloper.ID == id)
                {
                    return devloper;
                }
            }
            return null;
        }

        public bool UpdateDeveloper(int originalID, Developer newDeveloper) //update
        {
            Developer oldDeveloper = GetDeveloper(originalID);

            if(oldDeveloper != null)
            {
                oldDeveloper.ID = newDeveloper.ID;
                oldDeveloper.FirstName = newDeveloper.FirstName;
                oldDeveloper.LastName = newDeveloper.LastName;
                oldDeveloper.HasPluralSight = newDeveloper.HasPluralSight;
                return true;
            }
            else
            {
                return false;
            }



        }
        

        public bool DeleteExistingDeveloper(Developer movedOnEmployee)   //delete
        {
            bool removeEmployee = _developerRepo.Remove(movedOnEmployee);
            return removeEmployee;
        }


        public List<Developer> GetDevThatHasPluralSight ()
        {
            List<Developer> needsLiscense = new List<Developer>();
            foreach (Developer developer in _developerRepo)
            {
                if (developer.HasPluralSight == false)
                {
                    needsLiscense.Add(developer);
                }

            }
            return needsLiscense;

            //  return _developerContext.Where(m => !m.HasPluralSight).ToList();  // alternative in lambda form
        }
        

    }
}
