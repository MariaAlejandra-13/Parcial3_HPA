using System;
using Newtonsoft.Json;

namespace Parcial3_Problemas1y2
{
    // Clase padre que define los cálculos que debe implementar cada figura.
    abstract class Figura
    {
        public abstract double CalcularArea();
        public abstract double CalcularPerimetro();
    }

    class Rectangulo : Figura
    {
        private readonly double baseRectangulo;
        private readonly double altura;

        public Rectangulo(double baseRectangulo, double altura)
        {
            this.baseRectangulo = baseRectangulo;
            this.altura = altura;
        }

        public override double CalcularArea()
        {
            return baseRectangulo * altura;
        }

        public override double CalcularPerimetro()
        {
            return 2 * (baseRectangulo + altura);
        }
    }

    class Cuadrado : Figura
    {
        private readonly double lado;

        public Cuadrado(double lado)
        {
            this.lado = lado;
        }

        public override double CalcularArea()
        {
            return lado * lado;
        }

        public override double CalcularPerimetro()
        {
            return 4 * lado;
        }
    }

    class Triangulo : Figura
    {
        private readonly double ladoA;
        private readonly double ladoB;
        private readonly double ladoC;

        public Triangulo(double ladoA, double ladoB, double ladoC)
        {
            this.ladoA = ladoA;
            this.ladoB = ladoB;
            this.ladoC = ladoC;
        }

        public override double CalcularArea()
        {
            // Fórmula de Herón a partir de los tres lados.
            double semiperimetro = CalcularPerimetro() / 2;

            return Math.Sqrt(
                semiperimetro *
                (semiperimetro - ladoA) *
                (semiperimetro - ladoB) *
                (semiperimetro - ladoC));
        }

        public override double CalcularPerimetro()
        {
            return ladoA + ladoB + ladoC;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PROBLEMA 1 - FIGURAS GEOMETRICAS");

            Figura[] figuras =
            {
                new Rectangulo(6, 4),
                new Cuadrado(5),
                new Triangulo(3, 4, 5)
            };

            foreach (Figura figura in figuras)
            {
                Console.WriteLine("Figura: " + figura.GetType().Name);
                Console.WriteLine("Area: " + figura.CalcularArea());
                Console.WriteLine("Perimetro: " + figura.CalcularPerimetro());
                Console.WriteLine();
            }

            Console.WriteLine("PROBLEMA 2 - SERIALIZACION A JSON");

            var persona = new
            {
                Nombre = "Cristell",
                Edad = 20,
                Carrera = "Ingenieria en Sistemas"
            };

            string json = JsonConvert.SerializeObject(persona);
            Console.WriteLine(json);
        }
    }
}