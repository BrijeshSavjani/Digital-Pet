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

    public partial class Work : Window
    {
        //Declaring Variables
        public System.Timers.Timer bullet_fire = new System.Timers.Timer(50);
        public System.Timers.Timer alien_movement = new System.Timers.Timer(250);
        public int y = 0;
        public int z = 4;
        public int i = 0;
        public int n = 0;
        public int x = 2;
        int m = 0;
        public int Shot_Ships;
        public Thickness Plane_Margin;
        public Random Shoot = new Random();


        public Work()
        {
            InitializeComponent();
            //Starting the timer responsible for moving the aliens
            alien_movement = new System.Timers.Timer(200);
            //OnTimedEvent called when Timeer interval ticks
            alien_movement.Elapsed += OnTimedEvent;
            alien_movement.AutoReset = true;
            alien_movement.Enabled = true;
            alien_movement.Start();
        }
  
            

        public void GameOver() //Called when Game is Over
        {
            //Stops timers
            alien_movement.Stop();
            bullet_fire.Stop();
            //Alerts User to outcome of game
            MessageBox.Show("Your Score Was" + Shot_Ships.ToString() + "That equates to:" + (Shot_Ships * 10).ToString() + "Coins", "Game", MessageBoxButton.OK);
            //Reads Save Data (Saved in a text file)
            StreamReader SaveData = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Autosave.txt");
            //Creates the Pet using the values
            Game_Saves gs = new Game_Saves();
            Pet Tamagotchi =gs.OpenGame(SaveData);
            //Pay the user based on their score
            Tamagotchi.Pay(Shot_Ships * 10);
            //Returns to game with initial values intact and with the extra money
            Game game = new Game(Tamagotchi);
            game.Show();
            this.Close();

        }
        public void ResetBullet()
        {
            //For when the Hits the edge of the screen.It is reset so it can be fired again 
            Bullet.Visibility = Visibility.Collapsed;
            Bullet.Margin = new Thickness(0, 0, 0, 0);
            this.Bullet.SetValue(Grid.RowProperty, 4);
            z = 4;
            y = 0;
            bullet_fire.Stop();//Stops timer
        }
        private void bullet_travel(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() => //The primary thread must be used to edit the GUI>
            {
                //Increases Margin so the bullet moves up the window
                Bullet.Margin = new Thickness(Plane_Margin.Left, 0, 0, Bullet.Margin.Bottom + 8);
                y = y + 1;
                if (y == 16) //If it gets to the end of a Row (all rows are of height 16 * 8 (128).) then it is swapped to the upper row
                {
                    if (z == 0 & y == 16)//If it's at the top of the top row then Reset the bullet as it has missed.
                    {
                        ResetBullet();
                    }
                    //Code to mobve code up the board (in reverse order i.e the top row is 0)
                    this.Bullet.SetValue(Grid.RowProperty, z - 1);
                    //Resets margin for new row. Keeps Horrizontal Margin
                    Bullet.Margin = new Thickness(Plane_Margin.Left, 0, 0, 0);
                    z = z - 1;
                    y = 0;
  
                }

                foreach (UIElement alien in AlienGrid.Children) //Foreach alien
                {
                    if (alien.Visibility == Visibility.Visible) // if it's not been hit
                    {
                        Point alien_point = alien.TransformToAncestor(WholeBoard).Transform(new Point(0, 0)); // Point of alien relative to the whole board
                        double alien_upper_x = alien_point.X + alien.RenderSize.Width; //Highest X value of the alien at that point in time
                        double alien_upper_y = alien_point.Y + alien.RenderSize.Height;//Highest Y value of the alien at that poinmt in time

                        Point bullet_point = Bullet.TransformToAncestor(WholeBoard).Transform(new Point(0, 0));//Same for bullet
                        double bullet_upper_x = bullet_point.X + Bullet.RenderSize.Width;
                        double bullet_upper_y = bullet_point.Y + Bullet.RenderSize.Height;
                     
                        if (bullet_point.X < alien_upper_x & bullet_upper_x > alien_point.X) //If it's bullet is within the appropiate area to interset the alien
                        {
                            if (bullet_point.Y < alien_upper_y & bullet_upper_y > alien_point.Y) //And is in the appropiate y area to instersect
                            {
                                alien.Visibility = Visibility.Collapsed; //Remove alien
                                Shot_Ships = Shot_Ships + 1; //Increase Score
                                ResetBullet();//Reset the bullet as it has hit it's target
                            }
                        }
                    }
                }

            }
            );

 
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)//Alein movement timer
        {
            this.Dispatcher.Invoke(() => // Uses primary thread for GUI control
            {
                Score.Content = "Score: " + Shot_Ships.ToString();// Update score
                if (Shot_Ships == 20) { GameOver(); } //If use has killed all the aliens then end the gam,e
                if (i < 80) // If it's not at then end of the window
                {
                    Thickness a = AlienGrid.Margin;
                    AlienGrid.Margin = new Thickness(a.Left + x, 0, 0, 0); //Move left by x every interval
                    i = i + 1;
                }

                if (i == 80)
                {
                   ChangeRow(); //If it's at the end of the Row then move it onto the lower row.
                }
            }
            );
        }
        public void ChangeRow()
        {
            this.AlienGrid.SetValue(Grid.RowProperty, n + 1); //Change the row downawards
            alien_movement.Interval = alien_movement.Interval /2; // Increase speed of aliens to make more difficult
            x = x * -1; //Chnage sign on front so it moves in the oppisite direction
            n = n + 1;
            if (n == 4){ GameOver(); } // IF it's on the final row then end the game
            i = 0;//Reset i
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)//Excecuted when a key is pressed
        {
            if (e.Key == Key.Right)//If right arrow is pressed then move to the right by 2
            {
                Plane.Margin = new Thickness(Plane.Margin.Left + 2, 0, 0, 0);
            }
            if (e.Key == Key.Left)
            {
                Plane.Margin = new Thickness(Plane.Margin.Left - 2, 0, 0, 0);//If lft arrow is pressed then move to the left by 2
            }
            if (e.Key == Key.Space)//When the spacebar is pressed
            {
                Bullet.Visibility = Visibility.Visible; // make bullet visible
                bullet_fire.Elapsed += bullet_travel;//Initiaite timers
                bullet_fire.AutoReset = true;
                bullet_fire.Enabled = true;
                bullet_fire.Start();
                Plane_Margin = Plane.Margin;// Set bullet margin to Plane margin so it's inline
                
            }
        }


    }
}
