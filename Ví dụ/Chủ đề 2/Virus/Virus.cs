using System;

namespace virus
{
    class Virus
    {
        float reproductionRate; // rate of reproduction, in %
        float resistance; // resistance against drugs, in %
        const float defaultReproductionRate = 0.1f;

        public Virus(float newResistance)
        {
            reproductionRate = defaultReproductionRate;
            resistance = newResistance;
        }

        public Virus(float newReproductionRate, float newResistance)
        {
            reproductionRate = newReproductionRate;
            resistance = newResistance;
        }

        // If this virus cell reproduces, 
        // returns a new offspring with identical genetic info. 
        // Otherwise, returns NULL. 
        public Virus Reproduce(float immunity)
        {
            Random r = new Random();
            float prob = (float) r.NextDouble(); // generate number between 0 and 1

	        // If the patient's immunity is too strong, it cannot reproduce
	        if (immunity > prob)
		        return null;

	        // Does the virus reproduce this time? 
	        if (prob > reproductionRate)
		        return null;	
	        // No! 
	        return new Virus(reproductionRate, resistance);
        }

        // Returns true if this virus cell survives, given the patient's immunity
        public bool Survive(float immunity)
        {
	        // If the patient's immunity is too strong, then this cell cannot survive 
	        if (immunity > resistance)
		        return false;
	        return true;
        }
    }
}
