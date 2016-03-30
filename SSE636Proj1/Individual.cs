using System;

public class Individual
{
    private List<int> chromosome;
	public Individual(int length, int type)
	{
        Random r = new Random();
        chromosome = new List<int>();
        switch (type)
        {
            case 1:
                for (int i = 0; i < length; i++)
                {
                    chromosome.Add(r.Next(0, 2));
                }
            case 2:
                for (int i = 0; i < length; i++)
                {
                    chromosome.Add(r.Next(0, 2));
                }

        }

	}

    public int getGene(int pos)
    {
        return chromosome[pos];
    }

    public void setGene(int pos, int gene)
    {
        chromosome[pos] = gene;
    }
}
