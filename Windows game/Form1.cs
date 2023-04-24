using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }


        bool goleft, goright, gojump;

        int jumpSpeed = 10;
        int force = 8;
        int speed = 8;
        int backgroundspeed = 15;
        int mouses_picked = 0;
        int keys_picked = 0;
        private void AssetDirection(string direction) //sumasama mga platform door mouse at key kaya ginawa etong function na to
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "mouse" 
                    || x is PictureBox && (string)x.Tag == "door" || x is PictureBox && (string)x.Tag == "key")
                {
                    if (direction == "back")
                    {
                        x.Left-= backgroundspeed;
                    }
                    if(direction == "forward")
                    {
                        x.Left+= backgroundspeed;
                    }
                }

            }

        }


        private void maintime(object sender, EventArgs e)
        {
            //pang pa move ng cat
            if (goleft == true && cat.Left > 10)
            {
                cat.Left -= speed;
            }

            if (goright == true && cat.Left + (cat.Width) < this.ClientSize.Width)

            {
                cat.Left += speed;
            }
            
            if (goleft == true && background.Left<0)
            {
                background.Left += backgroundspeed;
                AssetDirection("forward");
            }
            if (goright == true && background.Left > -1192)
            {
                background.Left -= backgroundspeed;
                AssetDirection("back");
            }
            //jumping cat
            cat.Top += jumpSpeed;

            if (gojump == true && cat.Top > 0)
            {
                jumpSpeed = -12;
                force = -1;

            }
            else
            {
                jumpSpeed = 12;
            }
            //para maging solid ang platform
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (cat.Bounds.IntersectsWith(x.Bounds) && gojump == false)
                    {
                        force = 8;
                        cat.Top = x.Top - cat.Height;
                        jumpSpeed = 0;
                    }

                    x.BringToFront();
                }
                if (x is PictureBox && (string)x.Tag == "mouse")
                {
                    if(cat.Bounds.IntersectsWith(x.Bounds) && x.Visible ==true)
                    {
                        x.Visible = false;
                        mouses_picked += 1;
                        mousespick.Text = "Mouses: " + mouses_picked;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "key")
                {
                    if (cat.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        keys_picked += 1;
                        keyspicked.Text = "Keys: " + keys_picked;
                    }
                }
            }
           
            if (cat.Bounds.IntersectsWith(door.Bounds) && keys_picked == 2 && mouses_picked >= 10 )
            {
                gametime.Stop();
                MessageBox.Show("You've completed the Game, Congrats!" + Environment.NewLine + "Play again? ");

                Form1 f = new Form1();
                f.Show();
                this.Hide();

            }
            if(cat.Top +cat.Height > this.ClientSize.Height)
            {
                gametime.Stop();
                MessageBox.Show("DIE YOU LITTLE CAT!!" + Environment.NewLine + "Play again? ");

                Form1 f = new Form1();
                f.Show();
                this.Hide();
            }


        }

        private void UP(object sender, KeyEventArgs e)
        {
            //para mapastop ang movement ng cat
            if (e.KeyCode == Keys.A)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                goright = false;
            }
            if (gojump == true)
            {
                gojump = false;
            }

           
        }

        private void DOWN(object sender, KeyEventArgs e)
        {
            //hotkeys para ma move ang cat A,S,D,W
            if (e.KeyCode == Keys.A)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.W && gojump == false)
            {
                gojump = true;
            }

           


        }

        private void CLOSE(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
