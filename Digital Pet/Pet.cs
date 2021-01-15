using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Digital_Pet
{
    public class Pet
    {//Pet class
        //Atribute Variables
        private List<int> Pantry;
        private int Health;
        private int Hunger;
        private int Boredom;
        private int Happiness;
        private int Money;
        //Constructor
        public Pet(int[] initial_values, List<int> initial_pantry)
        {//Values can be fresh set or extracted from a fike
            Pantry = initial_pantry;
            Health = initial_values[0];
            Hunger = initial_values[1];
            Boredom = initial_values[2];
            Happiness = initial_values[3];
            Money = initial_values[4];
        }
        public int Get(string attribute)//Returns desired attribute
        {
            switch (attribute)
            {
                case "Health":
                    return Health;
                case "Hunger":
                    return Hunger;
                case "Boredom":
                    return Boredom;
                case "Happiness":
                    return Happiness;
                case "Money":
                    return Money;
            }
            return 101;// >100 = error
        }
        public List<int> GetPantry() { return Pantry; } //Pantry has to have seperate method as it is a different datatype
        public void Damage() {//Damage function. Is repeated so stats deplete
            Hunger = Hunger - 2;
            Boredom = Boredom -1;
            Happiness = Happiness -4;
        }
        public bool Alive()
        {
            if (Health <= 0 | Hunger <= 0 | Boredom <= 0 | Happiness <= 0)
            {
                return false; //If dead return false
            }
            return true;//Else return true
        }
        public bool Feed()//Feed function
        {
            if (Pantry.Count > 0)// If the Pantry has an item in it
            {
                Hunger = Hunger + Pantry[0]; //Feed it to the Pet
                Pantry.RemoveAt(0);//Remove from Pantry
                return true;//Return true to signify it has eaten
            }
            return false;//Else return false because it hasn't eaten
        }
        public void Play()
        {//Play Function
            Boredom = Boredom + 3;//Increases Boredom & Happiness
            Happiness = Happiness + 5;
        }
      

        public void  Pay(int MoneyEarned)
        {//Pay money into balance of pet(Used to buy food)
            Money = Money + MoneyEarned;
        }
        public bool Buy(int Cost,int Hunger_Removed)
        {// Buys food from market place
            //Checks if there's enough money first
            if(Cost <= Money) { Money = Money - Cost; Pantry.Add(Hunger_Removed); return true; }
            return false;//returns flase if there's not enough
        }
    }
}
