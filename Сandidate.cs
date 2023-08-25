using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Сandidate
    {
        public string Name
        {
            get { return name; } 
            set { name = value; }
        }

        private string name;

        public string SurName
        {
            get { return surname; }
            set { surname = value; }
        }

        private string surname;

        public string RecommendedDepartment
        {
            get { return recommendeddepartment; }
            set { recommendeddepartment = value; }
        }

        private string recommendeddepartment;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string email;

        public bool TestTask
        {
            get { return testtask; }
            set { testtask = value; }
        }

        private bool testtask;

        public bool Interview
        {
            get { return interview; }
            set { interview = value; }
        }

        private bool interview;
        
    }
}
