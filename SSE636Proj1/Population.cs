using System;
using System.Collections.Generic;

namespace SSE636Proj1
{
    public class Population
    {
        List<Individual> pop;
        int gen;
        int tournamentSize = 5;
        int popSize = 50;
        int elitist = 1;
        double crossChance = 0.5;
        double mutateChance = 0.025;
        int[] target;

        public Population(int type, int[] target)
        {
            pop = new List<Individual>();
            this.target = target;

            switch (type)
            {
                case 1:
                    for (int i = 0; i < 50; i++)
                    {
                        pop.Add(new Individual(target.Length / 3, 1));
                        setFitness(pop[i]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 50; i++)
                    {
                        pop.Add(new Individual(target.Length / 3, 2));
                        setFitness(pop[i]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 50; i++)
                    {
                        pop.Add(new Individual(target.Length, 3));
                        setFitness(pop[i]);
                    }
                    break;
            }

            gen = 1;
        }

        private void setFitness(Individual individual)
        {
            double temp = 0;
            switch (individual.getType())
            {
                case 1:
                    for (int i = 0; i < target.Length; i++)
                    {
                        if (target[i] != individual.getGene(i))
                            temp++;
                    }
                    individual.setFitness((double)(target.Length - temp) / target.Length);
                    break;
                case 2:
                    for (int i = 0; i < target.Length; i++)
                    {
                        if (target[i] != individual.getGene(i))
                            temp += Math.Sqrt((target[i] - individual.getGene(i)) ^ 2);
                    }
                    individual.setFitness((double)target.Length / temp);
                    break;
                case 3:
                    for (int i = 0; i < target.Length; i += 3)
                    {
                        if (target[i] != individual.getGene(i))
                            temp += Math.Sqrt((target[i] - individual.getGene(i)) ^ 2 + (target[i + 1] - individual.getGene(i + 1)) ^ 2 + (target[i + 2] - individual.getGene(i + 2)) ^ 2);
                    }
                    individual.setFitness((double)target.Length / temp);
                    break;
            }
        }

        public void newGeneration()
        {
            gen++;
            List<Individual> temp = new List<Individual>();
            for (int i = 0; i < (pop.Count - elitist); i++)
            {
                temp.Add(crossover());
            }
            temp.Add(getBest());
            pop.Clear();
            pop.AddRange(temp);
            mutate();
        }

        private Individual crossover()
        {
            Individual parent1, parent2;
            List<Individual> tournament = new List<Individual>();
            Random r = new Random();
            List<int> childChrom = new List<int>();
            int temp;

            //Tournament selection
            for (int i = 0; i < tournamentSize; i++)
            {
                temp = r.Next(0, pop.Count);
                while (tournament.Contains(pop[temp]))
                    temp = r.Next(0, pop.Count);
                tournament.Add(pop[temp]);
            }

            parent1 = tournament[0];

            for (int i = 1; i < tournament.Count; i++)
            {
                if (tournament[i].getFitness() > parent1.getFitness())
                    parent1 = tournament[i];
            }

            for (int i = 0; i < tournamentSize; i++)
            {
                temp = r.Next(0, pop.Count);
                while (tournament.Contains(pop[temp]))
                    temp = r.Next(0, pop.Count);
                tournament.Add(pop[temp]);
            }

            parent2 = tournament[tournamentSize];

            for (int i = (tournamentSize + 1); i < tournament.Count; i++)
            {
                if (tournament[i].getFitness() > parent2.getFitness())
                    parent2 = tournament[i];
            }

            //Crossover
            for (int i = 0; i < parent1.getLength(); i++)
            {
                if (r.NextDouble() <= crossChance)
                    childChrom.Add(parent1.getGene(i));
                else
                    childChrom.Add(parent2.getGene(i));
            }

            return new Individual(childChrom, parent1.getType());
        }

        private void mutate()
        {
            Random r = new Random();
            for (int i = 0; i < pop.Count; i++)
            {
                for (int j = 0; j < pop[i].getLength(); j++)
                {
                    if (r.NextDouble() <= mutateChance)
                        switch (pop[i].getType())
                        {
                            case 1:
                                pop[i].setGene(j, r.Next(0, 2));
                                break;
                            case 2:
                                pop[i].setGene(j, r.Next(0, 256));
                                break;
                            case 3:
                                pop[i].setGene(j, r.Next(0, 256));
                                break;
                        }
                }
            }
        }

        public int getGen()
        {
            return gen;
        }

        public Individual getBest()
        {
            Individual best = pop[0];
            for (int i = 1; i < pop.Count; i++)
            {
                if (pop[i].getFitness() > best.getFitness())
                    best = pop[i];
            }
            return best;
        }
    }
}
