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
using System.IO;

namespace Digital_Pet
{ 
    public partial class Game_Saves : Window
    {
        //Declare public variables
        public int m = 0;

        public Game_Saves()
        {
            InitializeComponent();
            //Open and read information about root directory
            DirectoryInfo root_directory_info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            //Filter for text files
            FileInfo[] files = root_directory_info. GetFiles("*.txt");
            //Add the name of every file to the listbox so the user can choose
            List<string> filenames = new List<string>();
            foreach(FileInfo file in files)
            {
                filenames.Add(file.Name);
            }
            SavesAvailaible.ItemsSource = filenames;
        }
      
        public Pet OpenGame(StreamReader SaveData)
        {
            //Array for Initial Values
            int[] InitialValues = new int[5];
            //Array for Initial Pantry
            List<int> Pantry = new List<int>();
            //Open and divide file
            string lines = SaveData.ReadToEnd();
            string line1 = lines.Split('\n')[0];
            string line2 = lines.Split('\n')[1];
            //Adds each attribute to the initial value array
            while (m <= 4)
            {
                 string [] values = line1.Split(',');
                 InitialValues[m] = int.Parse(values[m]);
                 m = m + 1;
            }
            //Adds each item to the temporary Pantry variable
            foreach (string value in line2.Split(','))
            {
                //Checks Variable isn't empty or a new line
                if (value != "" & value != "\r") { Pantry.Add(int.Parse(value)); }
            }
            //Creates the pet and returns it
            return new Pet(InitialValues, Pantry);
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //Read file
            StreamReader chosen_save = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/" + SavesAvailaible.SelectedItem);
            //Create Pet
            Pet pet = OpenGame(chosen_save);
            //Create & Open New window
            Game game = new Game(pet);
            game.Show();
            this.Close();
        }
    }
}
