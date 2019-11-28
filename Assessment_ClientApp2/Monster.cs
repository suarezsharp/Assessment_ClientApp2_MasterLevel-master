using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_ClientApp2
{
    /// <summary>
    /// monster entity class
    /// </summary>
    public class Monster
    {
        public enum EmotionalState
        {
            none,
            happy,
            sad,
            angry,
            bored
        }

        public enum Home
        {
            none,
            Tipa,
            Marr,
            Alfitaria,
            Shella,
            Fum,
            Lynari
        }

        #region FIELDS

        private string _name;
        private int _age;
        private EmotionalState _attitude;
        private Home _tribe;
        private bool _active;
        private DateTime _birthdate;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public EmotionalState Attitude
        {
            get { return _attitude; }
            set { _attitude = value; }
        }

        public Home Tribe
        {
            get { return _tribe; }
            set { _tribe = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public DateTime Birthdate
        {
            get { return _birthdate; }
            set { _birthdate = value;  }
        }

        #endregion

        #region CONSTRUCTORS

        public Monster()
        {

        }

        public Monster(string name, int age, EmotionalState attitude)
        {
            _name = name;
            _age = age;
            _attitude = attitude;
        }

        #endregion
        
        #region METHODS

        public string Greeting()
        {
            string greeting;

            switch (_attitude)
            {
                case EmotionalState.happy:
                    greeting = $"Hello, my name is {_name} and I am having a great day!";
                    break;

                case EmotionalState.sad:
                    greeting = $"{_name} is feeling bad.";
                    break;

                case EmotionalState.angry:
                    greeting = $"I'm {_name}, and stay away from me!";
                    break;

                case EmotionalState.bored:
                    greeting = $"I don't know what to do at {_age} years old.";
                    break;

                default:
                    greeting = $"Hello, my name is {_name}.";
                    break;
            }
                       
            return greeting;
        }

        #endregion
    }
}
