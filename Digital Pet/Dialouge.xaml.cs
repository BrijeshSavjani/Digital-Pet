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
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Digital_Pet;

namespace Digital_Pet
{

    public partial class Dialouge : Window
    {
        public Pet Tamagotchi;
        public Dialouge(Pet pet)
        {
            InitializeComponent();
            Tamagotchi = pet;
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            Game game = new Game(Tamagotchi);
            game.SaveGame(FileName.Text + ".txt");
        }
    }
}
