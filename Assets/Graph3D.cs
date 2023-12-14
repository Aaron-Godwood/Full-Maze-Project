using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace graph
{
    class Graph3D
    {
        List<List<int>> adjacency = new List<List<int>>();
        List<List<int>> sets = new List<List<int>>();
        List<int> nodesVisited = new List<int>();
        Random rnd = new Random();
        int height;
        int width;

        public Graph3D(int height, int width)
        {
            this.height = height;
            this.width = width;
            for (int i = 0; i < height * width*width; i++)
            {
                sets.Add(new List<int>());
                sets[i].Add(i);
            }
            for (int i = 0; i < width * height*width; i++)
            {
                adjacency.Add(new List<int>());
                for (int j = 0; j < width * height * width; j++)
                {
                    adjacency[i].Add(0);
                }

            }
        }

        public List<List<int>> GetAdjacency()
        {
            return adjacency;
        }

        public void DisplaySets()
        {
            for (int i = 0; i < sets.Count; i++)
            {
                for (int j = 0; j < sets[i].Count; j++)
                {
                    Console.Write(sets[i][j]);
                }
                Console.WriteLine();
            }
        }

        public void DisplayAdjecency()
        {
            for (int i = 0; i < adjacency.Count; i++)
            {
                Console.WriteLine("_---------------");
                for (int j = 0; j < adjacency[i].Count; j++)
                {
                    Console.Write($"{adjacency[i][j]}|");
                }
                Console.WriteLine();
            }
        }

        public void DisplayConnections()
        {
            List<int> connections = new List<int>();
            for (int i = 0; i < adjacency.Count; i++)
            {
                for (int j = 0; j < adjacency[i].Count; j++)
                {
                    if (adjacency[i][j] == 1)
                    {
                        connections.Add(j);
                    }
                }
                Console.Write($"{i}- ");
                for (int j = 0; j < connections.Count; j++)
                {
                    Console.Write($"{connections[j]},");
                }
                connections.Clear();
                Console.WriteLine();
            }
        }

        public void AddEdge(int node1, int node2)
        {
            int temp = 0;
            int temp2 = 0;
            if ((node1 < width * height * width && node2 < width * height * width) && (node1 % width == node2 % width || node1 / width == node2 / width  || node1 / (width*height) == node2 / (width*height)))
            {

                for (int i = 0; i < sets.Count; i++)
                {
                    if (sets[i].Contains(node2))
                    {
                        temp = i;
                    }
                }
                for (int i = 0; i < sets.Count; i++)
                {
                    if (sets[i].Contains(node1))
                    {
                        temp2 = i;
                    }
                }
                if (temp != temp2)
                {
                    adjacency[node1][node2] = 1;
                    adjacency[node2][node1] = 1;
                    for (int i = 0; i < sets[temp].Count; i++)
                    {
                        sets[temp2].Add(sets[temp][i]);
                    }

                    sets.RemoveAt(temp);
                }



            }

        }

        public (int node1, int node2) GetRandomEdge()
        {
            int node1 = rnd.Next(0, width * height*width);
            int node2 = getRandomAdjacentNode(node1);

            return (node1, node2);

        }

        //public void Wilsons()
        //{
        //    int node1 = rnd.Next(0,width * height);
        //    int node2 = rnd.Next(0,width * height);
        //    int currentNode;
        //    Stack<int> currentWalk = new Stack<int>();
        //    while(node1 == node2)
        //    {
        //        node2 = rnd.Next(0,width*height);
        //    }
        //    currentNode = node1;
        //    while(currentNode != node2 )
        //    {
        //        currentWalk.Push(currentNode);
        //        currentNode = getRandomAdjacentNode(currentNode);
        //        if(GetUnvisitedAdjacentNodes(currentNode).Count==0)
        //        {

        //        }

        //    }
        //}

        public void Kruskals()
        {
            //int count = 0;
            while (sets.Count > 1)
            {
                Console.WriteLine(sets.Count);
                (int num1, int num2) = GetRandomEdge();
                Console.WriteLine($"num1 - {num1} num2 - {num2}");
                AddEdge(num1, num2);
                //Console.WriteLine();
                //Console.WriteLine(count++);
                //DisplaySets();
            }
        }

        public void RandomisedDepthFirst()
        {
            Stack<int> stack = new Stack<int>();

            List<int> unvisitedAjacentNodes;
            int currentNode = rnd.Next(0, width * height);
            stack.Push(currentNode);
            nodesVisited.Add(currentNode);
            while (stack.Count > 0)
            {
                //Console.WriteLine("ooooo");
                currentNode = stack.Pop();
                //Console.WriteLine(GetUnvisitedAdjacentNodes(currentNode).Count);
                if (GetUnvisitedAdjacentNodes(currentNode).Count > 0)
                {
                    //Console.WriteLine("oodhhdo");
                    stack.Push(currentNode);
                    unvisitedAjacentNodes = GetUnvisitedAdjacentNodes(currentNode);
                    currentNode = unvisitedAjacentNodes[rnd.Next(0, unvisitedAjacentNodes.Count)];
                    AddEdge(stack.Peek(), currentNode);
                    nodesVisited.Add(currentNode);
                    stack.Push(currentNode);

                }

            }
        }

        public List<int> GetUnvisitedAdjacentNodes(int node)
        {
            List<int> unvistedAdjacentNodes = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                if (!(node % width == 0 && i == 0) && !(node % width == (width-1) && i == 1) && !(node / width == 0 && i == 2) && !(node / width == (width - 1) && i == 3) && !(node / (width*height) == 0 && i == 4) && !(node / (width * height) == (width - 1) && i == 5)) /// fisnihsh this you bastardd a hjfuhdsvnvnvkiholfjakdjgsldkjhjm
                    switch (i)
                    {
                        case 0:
                            if (!nodesVisited.Contains(node - 1))
                            {
                                unvistedAdjacentNodes.Add(node - 1);
                            }


                            break;
                        case 1:
                            if (!nodesVisited.Contains(node + 1))
                            {
                                unvistedAdjacentNodes.Add(node + 1);
                            }
                            break;
                        case 2:
                            if (!nodesVisited.Contains(node - height))
                            {
                                unvistedAdjacentNodes.Add(node - height);
                            }

                            break;
                        case 3:
                            if (!nodesVisited.Contains(node + height))
                            {
                                unvistedAdjacentNodes.Add(node + height);
                            }

                            break;
                        case 4:
                            if (!nodesVisited.Contains(node - width*height))
                            {
                                unvistedAdjacentNodes.Add(node - width * height);
                            }
                            break;
                        case 5:
                            if(!nodesVisited.Contains(node + height*width))
                            {
                                unvistedAdjacentNodes.Add(node + height * width);
                            }
                            break;
                    }


            }
            return unvistedAdjacentNodes;
        }

        public int getRandomAdjacentNode(int node1)
        {
            int node2;
            int direction = rnd.Next(0, 6);
            while ((node1 % width == 0 && direction == 0) || (node1 % width == (width - 1) && direction == 1) || (node1 / width == 0 && direction == 2) || (node1 / width == (width - 1) && direction == 3) || (node1 / (width * height) == 0 && direction == 4) || (node1 / (width * height) == (width - 1) && direction == 5))
                direction = rnd.Next(0, 6);
            switch (direction)
            {
                case 0:
                    node2 = node1 - 1;
                    break;
                case 1:
                    node2 = node1 + 1;
                    break;
                case 2:
                    node2 = node1 - height;
                    break;
                case 3:
                    node2 = node1 + height;
                    break;
                case 4:
                    node2 = node1 - (width * height);
                    break;
                case 5:
                    node2 = node1 + (width * height);
                    break;

                default:
                    node2 = node1 - 1;
                    break;
            }
            return node2;
        }



    }
}
