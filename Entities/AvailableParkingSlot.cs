﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageExercise.Entities
{
    internal class AvailableParkingSlot : Vehicle
    {
        private static int instanceCounter;
        public AvailableParkingSlot(string model, string registrationNumber, string color, int numberOfWheels, int productionYear) : base(model, registrationNumber, color, numberOfWheels, productionYear)
        {
            instanceCounter++;
            base.InstanceCount = instanceCounter;
        }

        public override string ToString()
        {
            return $"Available Parking Slot";
        }
    }
}