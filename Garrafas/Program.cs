using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garrafas
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IGarrafa> garrafas = new List<IGarrafa>();

            var retangulo = new FabricaGarrafa();
            garrafas.Add(retangulo.Create(GarrafaType.Quadrado,2, 2));
            garrafas.Add(retangulo.Create(GarrafaType.Retangulo, 1, 2));
            garrafas.Add(retangulo.Create(GarrafaType.Retangulo, 3, 2));
            garrafas.Add(retangulo.Create(GarrafaType.Retangulo, 4, 2));
            garrafas.Add(retangulo.Create(GarrafaType.Retangulo, 5, 2));
            //Cenarios de validações com erro
            //garrafas.Add(retangulo.Create(2, 2));

            int areas = 0;

            try
            {
                foreach (var item in garrafas)
                {
                    if (!item.IsValid())
                    {
                        throw new Exception("Teve um erro na validação das areas");
                    }

                    var area = item.CalcularArea();
                    Console.WriteLine($"Garrafa do tipo: {item.GetType().Name}, tem area: {area}");
                    areas += area;
                }

                Console.WriteLine($"Areas das Garrafas: {areas}");
            }
            catch
            {
                Console.WriteLine($"Areas das Garrafas até o erro: {areas}");
            }

            Console.ReadLine();
        }

        public class FabricaGarrafa : IFabricaGarrafa
        {
            public IGarrafa Create(GarrafaType garrafa, int altura, int largura)
            {
                if (GarrafaType.Quadrado == garrafa)
                {
                    return new GarrafaQuadrado(altura, largura);
                }

                if (GarrafaType.Retangulo == garrafa)
                {
                    return new GarrafaRetangular(altura, largura);
                }

                throw new Exception($"Tipo desconhecido [{garrafa}]");
            }
        }

        public class GarrafaRetangular : Garrafa
        {
            public GarrafaRetangular(int altura, int largura) : base(altura, largura)
            {
            }

            public override int CalcularArea()
            {
                return Altura * Largura;
            }

            public override bool ValidarArea()
            {
                return Largura != Altura;
            }
        }

        public class GarrafaQuadrado : Garrafa
        {
            public GarrafaQuadrado(int altura, int largura) : base(altura, largura)
            {
            }

            public override int CalcularArea()
            {
                return Altura * Largura;
            }

            public override bool ValidarArea()
            {
                return Largura == Altura;
            }
        }

        public abstract class Garrafa : IGarrafa
        {
            public Garrafa(int altura, int largura)
            {
                Altura = altura;
                Largura = largura;
            }

            public int Altura { get; }
            public int Largura { get; }

            public abstract int CalcularArea();
            public abstract bool ValidarArea();

            public bool IsValid()
            {
                if (Altura <= 0 || Largura <= 0) return false;
                if (!ValidarArea()) return false;

                return true;
            }
        }

        public interface IGarrafa
        {
            int Altura { get; }
            int Largura { get; }
            int CalcularArea();
            bool IsValid();
        }

        public interface IFabricaGarrafa
        {
            IGarrafa Create(GarrafaType garrafa, int altura, int largura);
        }

        public enum GarrafaType
        {
            Quadrado,
            Retangulo
        }
    }
}
