using System.Drawing;

namespace minesweeper2
{
    public partial class Form1 : Form
    {
        int[,] matrix = new int[10, 10];
        int[,] matrix2 = new int[10, 10];
        int mouseY = 0;
        int mouseX = 0;
        bool firstClick = false;
        int[] filledRectsX = new int[10000];
        int[] filledRectsY = new int[10000];
        int rectCounter = 0;
        Font betu = new Font(" Arial", 22);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < 8; x++)
            {
                Random rnd = new Random();
                int randX = rnd.Next(9);
                int randY = rnd.Next(9);
                matrix[randX, randY] = 1;
                matrix2[randX, randY] = 9;



            }
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    int bombCounter = 0;
                    if (x < 9)
                    {
                        if (matrix[x + 1, y] == 1) bombCounter++;
                        if (y < 9)
                        {
                            if (matrix[x + 1, y + 1] == 1) bombCounter++;
                        }
                        if (y > 0)
                        {
                            if (matrix[x + 1, y - 1] == 1) bombCounter++;
                        }
                    }
                    if (x > 0)
                    {
                        if (matrix[x - 1, y] == 1) bombCounter++;
                        if (y < 9)
                        {
                            if (matrix[x - 1, y + 1] == 1) bombCounter++;
                        }
                        if (y > 0)
                        {
                            if (matrix[x - 1, y - 1] == 1) bombCounter++;
                        }

                    }
                    if (y < 9)
                    {
                        if (matrix[x, y + 1] == 1) bombCounter++;
                    }
                    if (y > 0)
                    {
                        if (matrix[x, y - 1] == 1) bombCounter++;
                    }
                    if (matrix2[x, y] != 9) matrix2[x, y] = bombCounter;
                }
            }


        }

        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    e.Graphics.DrawRectangle(Pens.Black, j * 50, i * 50, 50, 50);
                    e.Graphics.DrawString(matrix2[i, j].ToString(), betu, Brushes.Red, i * 50, j * 50);


                }
            if (firstClick == true)
            {
                if (matrix2[mouseX, mouseY] == 0)
                {
                    e.Graphics.FillRectangle(Brushes.Gray, mouseX * 50, mouseY * 50, 50, 50);
                }
                else if (matrix2[mouseX, mouseY] != 9)
                {
                    e.Graphics.DrawString(matrix2[mouseX, mouseY].ToString(), betu, Brushes.Red, mouseX * 50, mouseY * 50);
                }
                else if (matrix2[mouseX, mouseY] == 9)
                {
                    //gameover
                    e.Graphics.FillRectangle(Brushes.Red, mouseX * 50, mouseY * 50, 50, 50);
                    Application.Exit();
                }
                filledRectsX[rectCounter] = mouseX;
                filledRectsY[rectCounter] = mouseY;
                rectCounter++;
            }
            for (int a = 0; a < rectCounter; a++)
            {
                if (matrix2[filledRectsX[a], filledRectsY[a]] == 0)
                {
                    e.Graphics.FillRectangle(Brushes.Gray, filledRectsX[a] * 50, filledRectsY[a] * 50, 50, 50);
                }
                else if (matrix2[filledRectsX[a], filledRectsY[a]] != 9)
                {
                    e.Graphics.DrawString(matrix2[filledRectsX[a], filledRectsY[a]].ToString(), betu, Brushes.Red, filledRectsX[a] * 50, filledRectsY[a] * 50);
                }

            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            firstClick = true;
            mouseX = e.X / 50;
            mouseY = e.Y / 50;
            pictureBox1.Refresh();



        }



        

    }
}