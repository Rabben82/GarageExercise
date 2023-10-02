﻿namespace GarageExercise.Entities;

public class Motorcycle : Vehicle
{
    public Motorcycle(string model, string registrationNumber, string color, int numberOfWheels, int productionYear) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        base.InstanceCount++;
    }
}