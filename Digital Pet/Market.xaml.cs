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

namespace Digital_Pet
{
    /// <summary>
    /// Interaction logic for Market.xaml
    /// </summary>
    public partial class Market : Window
    {
        public Pet Tamagotchi; //Public Pet
        public Market(Pet pet)
        {
            InitializeComponent();
            Tamagotchi = pet; //Set the pet that came in as a parameter as the public pet
        }

        private void Meal_Click(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Buy(32, 32); //Using buy method that's in the Pet class
        }

        private void Burger_Click(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Buy(20, 18); //Using buy method that's in the Pet class
        }

        private void Pizza_Click(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Buy(20, 18);//Using buy method that's in the Pet class
        }

        private void Drink_Click(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Buy(10, 5);//Using buy method that's in the Pet class
        }

        private void Noodles_Click(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Buy(20, 18);//Using buy method that's in the Pet class
        }

        private void Apple_Click(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Buy(2, 1);//Using buy method that's in the Pet class
        }
    }
}
