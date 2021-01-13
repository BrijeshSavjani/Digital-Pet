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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Digital_Pet
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close(); //Close program
        }

        private void Game_Save(object sender, RoutedEventArgs e)
        {
            Game_Saves game_Saves = new Game_Saves();
            game_Saves.Show();
            this.Close();
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            int[] initial_values = new int[] { 100, 100, 100, 100,100 }; //InitialValues (No Damage as new game)
            List<int> Pantry = new List<int> (); //Pantry is empty
            Pet Tamagotchi = new Pet(initial_values,Pantry);//Create Pet 
            Game game = new Game(Tamagotchi);//Feed into Game ti be used (done so Game remains same regardless if it's save or new game)
            game.Show();//Show Game
            this.Close();//Closes this startup window
            
            
        }
    }
}
