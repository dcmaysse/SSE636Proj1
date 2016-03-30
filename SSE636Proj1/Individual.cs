using System;
using System.Collections.Generic;

namespace SSE636Proj1
{
    public class Individual
    {
        private List<int> chromosome;
        private int type;
        private double fitness;

        public Individual(int length, int type)
        {
            Random r = new Random();
            chromosome = new List<int>();
            this.type = type;

            switch (type)
            {
                case 1:
                    for (int i = 0; i < length; i++)
                        chromosome.Add(r.Next(0, 2));
                    break;
                case 2:
                    for (int i = 0; i < length; i++)
                        chromosome.Add(r.Next(0, 256));
                    break;
                case 3:
                    for (int i = 0; i < length; i++)
                        chromosome.Add(r.Next(0, 256));
                    break;
            }
        }

        public Individual(List<int> chromosome, int type)
        {

        }

        public int getGene(int pos)
        {
            return chromosome[pos];
        }

        public void setGene(int pos, int gene)
        {
            chromosome[pos] = gene;
        }

        public void setFitness(double fitness)
        {
            this.fitness = fitness;
        }

        public double getFitness()
        {
            return fitness;
        }

        public int getType()
        {
            return type;
        }

        public int getLength()
        {
            return chromosome.Count;
        }
    }
}