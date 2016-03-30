using System;

public class Population
{
    List<Individual> pop;
    int gen;

	public Population(int length, int type)
	{
        pop = new List<Individual>();

        switch (type)
        {
            case 1:
                for (int i = 0; i < 50; i++)
                    pop.Add(new Individual(length / 3, 1));
            case 2:
                for (int i = 0; i < 50; i++)
                    pop.Add(new Individual(length, 1));
        }

        gen = 1;
	}


}
