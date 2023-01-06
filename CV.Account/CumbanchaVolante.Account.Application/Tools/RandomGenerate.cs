
using CV.MsAccount.Application.Interfaces;
using System.Text;

namespace CV.MsAccount.Application.Tools
{
    public class RandomGenerate: IRandomGenerate
    {
        private string[] wordsforAlias;
        private Random random;

        public RandomGenerate()
        {
            this.random = new Random();
            this.wordsforAlias = new string[] { "Rufus", "Arena", "Arbol", "Pasto", "Cuervo", "Katana", "Iris", "Mouse", "Tigre", "Leon", "Tesoro", "Polo", 
                                      "Vino", "Beer", "Vodka", "Panda", "Ninja", "Sable", "Moon", "Marte", "Album", "Moda", "Anana", "Hielo", "Bruma", "Gallo",
                                         "Prince", "Rojo", "Sadie", "Rick","Rey", "Talla", "Bear", "Pera", "Purple", "Piedra", "Lodo", "Carpa", "Rain", "Jet", "Yate",
                "Koala", "Metal", "Templo", "Fuego", "Aire", "Tierra", "Agua", "Morty", "Bart" };
        }

        public string GenerateAlias()
        {
            int[] indices = generarNumerosAleatoriosNoRepetidos(4, 0, wordsforAlias.Length);

            string alias = String.Format("{0}", wordsforAlias[indices[0]]);
            for (int i = 1; i < 4; i++)
            {
                alias += String.Format(".{0}", wordsforAlias[indices[i]]);
            }
            return alias;
        }
        public string GenerateCbu()
        {
            var cbu = new StringBuilder();
            cbu.Append(ArrayToString(generarNumerosAleatoriosNoRepetidos(7, 0, 9)));
            cbu.Append(ArrayToString(generarNumerosAleatoriosNoRepetidos(7, 0, 9)));
            cbu.Append(ArrayToString(generarNumerosAleatoriosNoRepetidos(8, 0, 9)));
            return cbu.ToString();
        }
        private StringBuilder ArrayToString(int [] array)
        {
            var result = new StringBuilder();
            foreach (var item in array)
            {
                result.Append(item.ToString());
            }
            return result;
        }
        private int generarNumeroAleatorio(int min, int max)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }
            return this.random.Next(min, max + 1);
        }
        private int[] generarNumerosAleatoriosNoRepetidos(int longitud, int min, int max)
        {

            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }

            if (longitud <= 0 || (max - min) < longitud - 1)
            {
                return null;
            }

            int[] numeros = new int[longitud];

            bool repetido;
            int numero;
            int indice = 0;

            while (indice < numeros.Length)
            {

                repetido = false;

                numero = generarNumeroAleatorio(min, max);

                for (int i = 0; i < indice; i++)
                {
                    if (numeros[i] == numero)
                    {
                        repetido = true;
                    }
                }

                if (!repetido)
                {
                    numeros[indice] = numero;
                    indice++;
                }
            }
            return numeros;
        }


    }
}
