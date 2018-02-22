using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG10ObjectsAndClasses
{
    /// <summary>
    /// Class for a pet
    /// </summary>
    class Pet
    {

        /// <summary>
        /// Name
        /// </summary>
        private string m_sName;

        /// <summary>
        /// Hunger status
        /// </summary>
        private int m_iHunger;

        /// <summary>
        /// Stress status
        /// </summary>
        private int m_iStress;

        /// <summary>
        /// Dirtiness status
        /// </summary>
        private int m_iDirtiness;

        /// <summary>
        /// Pet status
        /// </summary>
        public enum PetDetailedStatus
        {
            FINE,
            hungry,
            DIRTY,
            STRESSED
        }

        /// <summary>
        /// Pet living status
        /// </summary>
        public enum PetLivingStatus
        {
            FINE,
            ALIVE,
            TIRED,
            DEAD
        }

        /// <summary>
        /// Constructor of a pet
        /// </summary>
        /// <param name="name">name</param>
        public Pet(string name)
        {
            m_sName = name;
            m_iStress = 5;
            m_iDirtiness = 5;
            m_iHunger = 5;
        }

        /// <summary>
        /// Getter for the name of the pet
        /// </summary>
        /// <returns>name</returns>
        public string GetName()
        {
            return m_sName;
        }

        /// <summary>
        /// Pet will eat when it is feed
        /// </summary>
        public void Eat()
        {
            // if pet is not hungry, water will be dirty
            if (m_iHunger <= 0)
            {
                m_iDirtiness += 4;
                m_iStress += 1;

            }  else if (15 < m_iStress) {
                // if pet felt too much stress, it cannot eat well
                m_iHunger -= 3;
                m_iDirtiness += 3;
                m_iStress -= 1;
            }
            else
            {
                m_iHunger -= 5;
                m_iDirtiness += 2;
                m_iStress -= 2;
            }

            if (m_iHunger < 0)
            {
                m_iHunger = 0;
            }

            if (m_iStress < 0)
            {
                m_iStress = 0;
            }


            Console.WriteLine(m_sName + " has eaten!");
        }

        /// <summary>
        /// Pet will be cleaned when the water of aqualium is changed
        /// </summary>
        public void Cleaned()
        {
            m_iHunger += 4;
            m_iDirtiness -= 15;
            m_iStress += 5;

            if (m_iDirtiness < 0)
            {
                m_iDirtiness = 0;
            }

            Console.WriteLine(m_sName + " has been cleen:D");
        }

        /// <summary>
        /// Pet will be relax when it feels freedom
        /// </summary>
        public void Relax()
        {
            m_iStress -= 5;
            m_iHunger += 5;
            m_iDirtiness += 1;

            if (m_iStress < 0)
            {
                m_iStress = 0;
            }

            Console.WriteLine(m_sName + " had relax time!");
        }

        /// <summary>
        /// Pet will do deep breathe when the aqualium is added plants
        /// </summary>
        public void DeepBreathe()
        {
            m_iHunger += 3;
            m_iDirtiness += 1;
            m_iStress -= 4;

            if (m_iStress < 0)
            {
                m_iStress = 0;
            }

            Console.WriteLine(m_sName + " released stress :D");
        }

        /// <summary>
        /// Pet will feel stress when it is seen by human
        /// </summary>
        public void SeenByHuman()
        {
            m_iStress += 5;
            m_iHunger += 4;

            Console.WriteLine(m_sName + " has been swimming!");
        }

        /// <summary>
        /// Return all of the condition of the pet.
        /// </summary>
        /// <returns>PetDetailedStatus</returns>
        public List<PetDetailedStatus> GetDetailedStatus()
        {
            List<PetDetailedStatus> status = new List<PetDetailedStatus>();
            if (14 < m_iHunger)
            {
                status.Add(PetDetailedStatus.hungry);
            }

            if (14 < m_iStress)
            {
                status.Add(PetDetailedStatus.STRESSED);
            }

            if (14 < m_iDirtiness)
            {
                status.Add(PetDetailedStatus.DIRTY);
            }

            if (status.Count() == 0)
            {
                status.Add(PetDetailedStatus.FINE);
            }

            return status;
        }

        /// <summary>
        /// Evaluate the condition and return living status of the pet
        /// </summary>
        /// <returns>PetLivingStatus</returns>
        public PetLivingStatus GetLivingStatus()
        {

            if (20 < m_iHunger || 20 < m_iStress || 20 < m_iDirtiness)
            {
                return PetLivingStatus.DEAD;
            }

            if (15 < m_iHunger || 15 < m_iStress || 15 < m_iDirtiness)
            {
                return PetLivingStatus.TIRED;
            }

            if (m_iHunger < 5 && m_iStress < 5 &&  m_iDirtiness < 5)
            {
                return PetLivingStatus.FINE;
            }

            return PetLivingStatus.ALIVE;
        }

        /// <summary>
        /// Evaluate living status and return the ascii art of the pet
        /// </summary>
        /// <returns></returns>
        public string[] GetImage()
        {
            switch (GetLivingStatus())
            {
                case Pet.PetLivingStatus.FINE:
                    return GetFineImage();

                case Pet.PetLivingStatus.ALIVE:
                    return GetNomalImage();

                case Pet.PetLivingStatus.TIRED:
                    return GetTiredImage();

                case Pet.PetLivingStatus.DEAD:
                    return GetDeadImage();

                default:
                    throw new SystemException();
            }
        }

        /// <summary>
        /// Get fish ascii art with nomal face
        /// </summary>
        /// <returns>an array that is filled by ascii art of the fish</returns>
        public string[] GetNomalImage()
        {
            string[] fishImage =
            {
                "      /\\       ",
                "   _ /./        ",
                ",-'    `-:.,-' /",
                "> O )<)    _(   ",
                "`-._  _.:' `-.\\",
                "    `` \\;      "
            };

            return fishImage;
        }

        /// <summary>
        /// Get fish ascii art with happy face
        /// </summary>
        /// <returns>an array that is filled by ascii art of the fish</returns>
        public string[] GetFineImage()
        {
            string[] fishImage =
            {
                "      /\\       ",
                "   _ /./        ",
                ",-'    `-:.,-' /",
                "> ^ )<)    _(   ",
                "`-._  _.:' `-.\\",
                "    `` \\;      "
            };

            return fishImage;
        }

        /// <summary>
        /// Get fish ascii art with tired face
        /// </summary>
        /// <returns>an array that is filled by ascii art of the fish</returns>
        public string[] GetTiredImage()
        {
            string[] fishImage =
            {
                "      /\\       ",
                "   _ /./        ",
                ",-'    `-:.,-' /",
                "> + )<)    _(   ",
                "`-._  _.:' `-.\\",
                "    `` \\;      "
            };

            return fishImage;
        }

        /// <summary>
        /// Get fish ascii art with dead face
        /// </summary>
        /// <returns>an array that is filled by ascii art of the fish</returns>
        public string[] GetDeadImage()
        {
            string[] fishImage =
            {
                "   _ /        ",
                ",-'    `-:.,-' /",
                "> - )<)    _(   ",
                "`-._  _.:' `-.\\",
                "    \\;      "
            };

            return fishImage;
        }

    }
}
