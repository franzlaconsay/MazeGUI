using System;
using System.Collections;
using System.Linq;

namespace MazeGUI
{
    public class MazeSolver
	{
		private int[] maze;
		private Queue q;
		private int[] backQ; //queue backtrack
		private int[] origin;
		private int[] path;
		private int start;
        private int goal;
		private int i; //steps counter
		private int pathCounter;
		private bool noPath;

		public MazeSolver()
		{
			maze = new int[25];
			q = new Queue();
			backQ = new int[100];
			origin = new int[100];
			path = new int[25];
			start = 0;
			goal = 0;
			maze[start] = 5;
			i = 0;
			pathCounter = 0;
			noPath = false;
		}

		public int Goal
        {
			get { return goal; }
            set 
			{ 
				goal = value;
				maze[goal] = 6; //6 = Goal
			}
        }
		public int Start
		{
			get { return start; }
			set
			{
				start = value;
				maze[start] = 5; //5 = Start
			}
		}

		public void setObstacle(int index)
        {
			maze[index] = 1;
        }

		public void clearObstacle(int index)
		{
			maze[index] = 0;
		}

		public bool isObstacle(int index)
		{
			return maze[index] == 1;
		}

		public int[] findPath()
        {
			calculatePath();
			if(noPath == false)
            {
				backTrack();
            }
			int[] temp = new int[pathCounter];
			Array.Copy(path, temp, pathCounter);
			Array.Reverse(temp);
			Array.Copy(temp, path, pathCounter);
			return temp;
        }

		private void calculatePath()
        {
			int n = start; //n = current cell, origin
			while (n != goal)
			{
				//up
				if (n - 5 >= 0 && maze[n - 5] != 1)
				{
					if (!backQ.Contains(n - 5) || n-5==0)
					{
						q.Enqueue(n - 5);
						backQ[i] = n - 5;
						origin[i] = n;
						i++;
					}
				}
				//down
				if (n + 5 <= 24 && maze[n + 5] != 1)
				{
					if (!backQ.Contains(n + 5))
                    {
						q.Enqueue(n + 5);
						backQ[i] = n + 5;
						origin[i] = n;
						i++;
					}
				}
				//left
				if (n - 1 >= 0 && n % 5 != 0 && maze[n - 1] != 1)
				{
					if (!backQ.Contains(n - 1) || n - 1 == 0)
                    {
						q.Enqueue(n - 1);
						backQ[i] = n - 1;
						origin[i] = n;
						i++;
					}
				}
				//right
				if (n + 1 <= 24 && (n + 1) % 5 != 0 && maze[n + 1] != 1)
				{
					if (!backQ.Contains(n + 1))
                    {
						q.Enqueue(n + 1);
						backQ[i] = n + 1;
						origin[i] = n;
						i++;
					}
				}
				try
				{
					n = (int)q.Dequeue();
				}
				catch (System.Exception)
				{
					noPath = true;
					break;
				}
			}
		}

		private void backTrack()
        {
			int n = goal;
			int x = i - 1; //goal index
			path[0] = goal;
			int p = 1;
            while (n != start)
            {
                if (backQ[x] == n)
                {
					path[p++] = origin[x];
					n = origin[x];
					pathCounter++;
				}
				x--;
            }
			path[p] = start;
			pathCounter++;
		}

		public bool NoPath
		{
			get { return noPath; }
		}

		public void clear()
        {
			q = new Queue();
			backQ = new int[100];
			origin = new int[100];
			path = new int[25];
			maze[start] = 0;
			start = goal;
			maze[start] = 5;
			i = 0;
			pathCounter = 0;
			noPath = false;
		}

		public string printMaze()
		{
			string output = "";
			for (int i = 0; i < maze.Length; i++)
			{
				output += maze[i] + " ";
				if ((i + 1) % 5 == 0)
					output += "\n";
			}
			return output;
		}

		public string printPath()
		{
			string output = "";
			for (int i = 0; i <= pathCounter; i++)
			{
				output += path[i] + "\n";
			}
			return output;
		}

		public string printTrack()
        {
			int l = i;
			string output = "";
			for(int i = 0; i < l; i++)
            {
				output += backQ[i] + " " + origin[i] + "\n";
            }
			return output;
        }
	}
}