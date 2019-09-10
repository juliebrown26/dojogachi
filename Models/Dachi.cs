using System;

namespace dojodachi
{
    public class Dachi
    {
        public int happiness { get; set; }
        public int fullness { get; set; }
        public int energy { get; set; }
        public int meals { get; set; }
        public string status { get; set; }

        public Dachi()
        {
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
            status = "Welcome to DojoDachi! Interact with your Dachi using the buttons below.";
        }

        public Dachi feed()
        {
            Random rand = new Random();
            int chance = rand.Next(1, 4);
            if (chance == 1)
            {
                this.meals -= 1;
                this.status = $"Your dachi ate some food, but was not satisfied";
                return this;
            }
            else
            {
                this.meals -= 1;
                int amount = rand.Next(5, 11);
                this.fullness += amount;
                this.status = $"Your dachi ate some food and is {amount} fuller!";
                return this;
            }
        }

        public Dachi play()
        {
            Random rand = new Random();
            int chance = rand.Next(1, 4);
            if (chance == 1)
            {
                this.energy -= 5;
                this.status = $"Your dachi is not enjoying this game.";
                return this;
            }
            else
            {
                this.energy -= 5;
                int amount = rand.Next(5, 11);
                this.happiness += amount;
                this.status = $"Your dachi had fun! It is now {amount} happier.";
                return this;
            }
        }

        public Dachi work()
        {
            this.energy -= 5;
            Random rand = new Random();
            int amount = rand.Next(1, 4);
            this.meals += amount;
            this.status = $"Your dachi did a great job and earned {amount} meal(s).";
            return this;
        }

        public Dachi sleep()
        {
            this.energy += 15;
            this.fullness -= 5;
            this.happiness -= 5;
            this.status = $"Your dachi took a nap and gained some energy.";
            return this;
        }
    }
}