﻿namespace GruppUppgift_Väderdata
{
    public class Program
    {
        public delegate double MögelDelegat(double temp, double hum);

        static void Main(string[] args)
        {

        }
        public static double CalculateMögel(double temperature, double humidity)
        {
            return temperature + humidity;
        }
    }
}