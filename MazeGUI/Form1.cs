using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeGUI
{
    public partial class Form1 : Form
    {
        Panel[] mazeBox;
        MazeSolver mazeSolver;
        const string GoalColor = "#343A40", PathColor = "#28A745", WallColor = "#DC3545";

        public Form1()
        {
            InitializeComponent();
            mazeSolver = new MazeSolver();
            mazeBox = new Panel[25];
            mazeBox[0] = maze0;
            mazeBox[1] = maze1;
            mazeBox[2] = maze2;
            mazeBox[3] = maze3;
            mazeBox[4] = maze4;
            mazeBox[5] = maze5;
            mazeBox[6] = maze6;
            mazeBox[7] = maze7;
            mazeBox[8] = maze8;
            mazeBox[9] = maze9;
            mazeBox[10] = maze10;
            mazeBox[11] = maze11;
            mazeBox[12] = maze12;
            mazeBox[13] = maze13;
            mazeBox[14] = maze14;
            mazeBox[15] = maze15;
            mazeBox[16] = maze16;
            mazeBox[17] = maze17;
            mazeBox[18] = maze18;
            mazeBox[19] = maze19;
            mazeBox[20] = maze20;
            mazeBox[21] = maze21;
            mazeBox[22] = maze22;
            mazeBox[23] = maze23;
            mazeBox[24] = maze24;
            mazeBox[0].Paint += paintCircle;
            mazeBox[0].Enabled = false;
        }

        private void maze_Click(object sender, EventArgs e)
        {
            int panelIndex = getPanelIndex(sender);
            if (radioWall.Checked)
            {
                //place walls
                if (((Panel)sender).BackColor == SystemColors.Control)
                {
                    ((Panel)sender).BackColor = ColorTranslator.FromHtml(WallColor);
                    mazeSolver.setObstacle(getPanelIndex(sender));
                }
                else
                {
                    ((Panel)sender).BackColor = SystemColors.Control;
                    mazeSolver.clearObstacle(getPanelIndex(sender));
                }
            }
            else
            {
                //find path
                //sender = clicked panel
                mazeSolver.Goal = panelIndex;
                int[] path = mazeSolver.findPath();
                if (mazeSolver.NoPath == true)
                {
                    pathLabel.Text = "No Path Available";
                    mazeSolver.clearObstacle(mazeSolver.Goal);
                    mazeSolver.Goal = mazeSolver.Start;
                }
                else
                {
                    mazeBox[mazeSolver.Start].Enabled = true; ;
                    mazeBox[mazeSolver.Goal].Enabled = false; ;
                    mazeBox[mazeSolver.Start].Paint -= paintCircle;
                    mazeBox[mazeSolver.Goal].Paint += paintCircle;
                    mazeSolver.clearObstacle(mazeSolver.Start);
                    //remove path highlight
                    for (int i = 0; i < 25; i++)
                    {
                        if (!mazeSolver.isObstacle(i))
                            mazeBox[i].BackColor = SystemColors.Control;
                    }
                    //new path hightlinght
                    for (int i = 0; i < path.Length; i++)
                    {
                        mazeBox[path[i]].BackColor = ColorTranslator.FromHtml(PathColor);
                    }
                    Refresh();
                    pathLabel.Text = "(" + mazeSolver.Start / 5 + "," + mazeSolver.Start % 5 +
                        ") → (" + mazeSolver.Goal / 5 + "," + mazeSolver.Goal % 5 + ")";
                }
                mazeSolver.clear();
            }
        }

        private int getPanelIndex(object panel)
        {
            string panelName = ((Panel)panel).Name;
            string[] mazeIndex = panelName.Split(new string[] { "maze" }, StringSplitOptions.None);
            return Convert.ToInt32(mazeIndex[1]);
        }

        private void radioPath_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 25; i++)
            {
                if (mazeSolver.isObstacle(i))
                {
                    mazeBox[i].Enabled = false;
                }
            }
        }

        private void radioWall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 25; i++)
            {
                if (mazeSolver.isObstacle(i))
                {
                    mazeBox[i].Enabled = true;
                }
            }
            //remove path highlight
            for (int i = 0; i < 25; i++)
            {
                if (mazeBox[i].BackColor == ColorTranslator.FromHtml(PathColor))
                    mazeBox[i].BackColor = SystemColors.Control;
            }
        }

        private void paintCircle(object sender, PaintEventArgs e)
        {
            //paint goal circle
            SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml(GoalColor));
            e.Graphics.FillEllipse(brush, 10, 10, 50, 50);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            mazeBox[mazeSolver.Start].Paint -= paintCircle;
            mazeBox[0].Paint += paintCircle;
            Refresh();
            //mazeBox[mazeSolver.Start].Enabled = true;
            mazeBox[0].Enabled = false; ;
            for (int i = 0; i < 25; i++)
            {
                mazeBox[i].BackColor = SystemColors.Control;
                mazeBox[i].Enabled = true;
            }
            mazeBox[0].Enabled = false;
            pathLabel.Text = "(0,0)";
            mazeSolver = new MazeSolver();
            radioWall.Checked = true;
        }
    }
}