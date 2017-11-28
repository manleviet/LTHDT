using System;

namespace virus
{
    class Patient
    {
        Virus[] virusPop;
	    int numVirusCells;
	    float immunity;	// degree of immunity, in %

        public Patient(float initImmunity, int initNumVirusCells) 
        { 
	        float resistance;
	
	        immunity = initImmunity;
            numVirusCells = initNumVirusCells;
            virusPop = new Virus[numVirusCells];

            Random r = new Random();
	        for (int i = 0; i < initNumVirusCells; i++) {
		        //randomly generate resistance, between 0.0 and 1.0
                resistance = (float) r.NextDouble();
		
		        virusPop[i] = new Virus(resistance);
	        }
        }

        ~Patient()
        {
            virusPop = null;
        }

        public void TakeDrug()
        {
            if (immunity == 1) return;

            immunity = immunity + 0.1f;
        }

        public void SimulateStep()
        {
            Virus virus;
            bool survived = false;

            for (int i = 0; i < numVirusCells; i++){
                virus = virusPop[i];

                survived = virus.Survive(immunity);

                if (!survived) {
                    // delete virus;
                    // delete virus i
                    for (int k = i, j = k+1; j < numVirusCells; j++, k++)
                    {
                        virusPop[k] = virusPop[j];
                    }

                    numVirusCells--;
                }
            }
        }

        public int getNumVirusCells()
        {
            return numVirusCells;
        }
    }
}
