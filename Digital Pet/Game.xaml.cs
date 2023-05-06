using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;
using System.IO;
using System.Xml.Serialization;
namespace Digital_Pet
{
    
    public partial class Game : Window
    {
        public int i = 0;
        public static Pet Tamagotchi;
        //Declare  public variables
        public Game(Pet tamagotchi) //Get in Pet as a parameter
        {
            Tamagotchi = tamagotchi;//Set the public pet to the pet that was fed in
            InitializeComponent();
            StatsUpdate(); //Update the graphical representations of the attributes of the Pet
            var timer = new System.Timers.Timer(3000);//New timer
            timer.Elapsed += OnTimedEvent;//Runs on OnTimedEvent()
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();


        }
        
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {//Runs with the timer
            Tamagotchi.Damage(); //The pet takes damage every timer interval using a method that is part of the Pet class
            StatsUpdate();//Update stats to reflect this
            i = i + 1;
            if (i == 3)//Change gif from what was set previously every 3rd round. (So each GIF is played enough times to be noticed) 
            {
                auto_gif_change();//GIF changing function
                i = 0;//Reset so it's every 3rd iteration 
            }

        }
        private void swap_gif(string path)
        {//Swaps gif to path provided
            this.Dispatcher.Invoke(() =>//Primary thread must be used to update the GUI
            {
                var gif = new BitmapImage();
                gif.BeginInit();
                gif.UriSource = new Uri(path, UriKind.Relative);
                gif.EndInit();
                WpfAnimatedGif.ImageBehavior.SetAnimatedSource(Test, gif); //Use library to allow for animation of GIF
                Thread.Sleep(500);
            }
            );
        }
        public void auto_gif_change()
        {//Decides path for GIF according to the automatic rules
            //The rules I set were that the attribute with the lowest value gets represented (only when that attribute is below 50)
            int initial_min = Math.Min(Tamagotchi.Get("Boredom"), Tamagotchi.Get("Happiness")); //Calculates minimum of Boredom and Happiness
            int minimum_value = Math.Min(initial_min, Tamagotchi.Get("Hunger"));//Calculates minimum of initial_min and Hunger
            string path =  "Images/GIF/DefaultMonkey.gif";//Default path(if all are over 50)
            if (minimum_value < 50)
            {
                if (initial_min == minimum_value)//Could be boredom or happiness
                {
                    if (Tamagotchi.Get("Boredom") < Tamagotchi.Get("Happiness"))
                    {

                        path = "Images/GIF/BoredMonkey.gif";
                    }
                    else
                    {

                        path = "Images/GIF/DissapointedMonkey.gif";
                    }
                }//Sets path accordingly
                else
                {//Has to be Hunger so sets correct path
                    path = "Images/GIF/DissapointedMonkey.gif";
                } 
            }
            swap_gif(path);//Swap the GIF
        }
        private void StatsUpdate()
        {//Updates all of the Attribute representations to the latest value
            this.Dispatcher.Invoke(() =>//Changing GUI requires Primary Thread
            {
                Health.Value = Tamagotchi.Get("Health");//Get method is built into Pet class. Requires attribute name as a string
                Hunger.Value = Tamagotchi.Get("Hunger");
                Boredom.Value = Tamagotchi.Get("Boredom");
                Happiness.Value = Tamagotchi.Get("Happiness");
                Money.Content = Tamagotchi.Get("Money").ToString();
            }
            );
        }

        private void Feed(object sender, RoutedEventArgs e)
        {//Feeds the pet
            bool feed = Tamagotchi.Feed();//Method built into Pet class
            if (feed == true)//if the feed was successful
            {
                swap_gif("Images/GIF/EatingMonkey.gif");//Update GIF to show success
                StatsUpdate();//Update graphical representation of attribute values
                i = 0;//Set GIF Reset timer to 0
            }
            else {
                MessageBox.Show("Nothing in the pantry. Please go shopping", "Empty Pantry", MessageBoxButton.OK);//Failure so must be out of food
            }
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Play();//Execute Play method in Pet Class
            StatsUpdate();//Update GUI to represent this
            swap_gif("Images/GIF/HappyMonkey.gif");//Swap GIF  to show user that the Activity has occured
            i = 0;
        }

        private void Market(object sender, RoutedEventArgs e)
        {
            Market market = new Market(Tamagotchi);
            market.Show();//Open market for buying food
        }
        public void WriteListToFile(List<int> list, StreamWriter streamWriter)
        {
            foreach (int value in list){//For each value in a list
                streamWriter.Write(value.ToString() + ",");//Writes all values in a list into file
            }
            
            streamWriter.WriteLine();//Write to file
         }
        public void SaveGame(string savename)
        {
            List<int> SaveData = new List<int> { Tamagotchi.Get("Health"), Tamagotchi.Get("Hunger"), Tamagotchi.Get("Boredom"), Tamagotchi.Get("Happiness"), Tamagotchi.Get("Money") };
            //Creat list with attributes in
            List<int> Pantry = Tamagotchi.GetPantry();//Get pantry as a list
            StreamWriter Saver = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + savename);//Get somewhere to save it
            WriteListToFile(SaveData, Saver);//Write lists to file for saving
            WriteListToFile(Pantry, Saver);
            Saver.Close();//Save & Close file
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveGame("Autosave.txt");//Autosave values

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Dialouge Saver = new Dialouge(Tamagotchi);
            Saver.Show();//Run custom Save dialog
        }

        private void Work(object sender, RoutedEventArgs e)
        {
            SaveGame("Autosave.txt");//Game needs to be paused so value saved to an Autosave
            Work minigame = new Work();//Create work class
            minigame.Show();//Show windows
            this.Close();
        }
    }
}
    
