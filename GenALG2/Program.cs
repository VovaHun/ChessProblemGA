using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GenALG2
{
    class Program
    {
        private static Random rand = new Random();

        // Создание матрицы
        public static void CreatrMatr(ref int[,] population, int col_row, int col_colum) {
            population = new int[col_row, col_colum];
        }

        public static void CreatrVectr(ref int[]vect, int m){
            vect = new int[m];
        }

        public static void InizVectr(ref int[] vect, int m){
            for (int i = 0; i < m; i++) {
                vect[i] = 0;
            }
        }

        public static void CreatrInd(ref int[,] population, int col_row, int col_colum) {
            for (int i = 0; i < col_row; i++)
                for (int j = 0; j < col_colum; j++)
                    population[i,j] = 0;

        }

        // Инициализация матрицы
        public static void InizMatr(ref int[,] population, int col_row, int col_colum){
            for (int i = 0; i < col_row; i++)
                for (int j = 0; j < col_colum; j++)
                    population[i, j] = 0;
        }

           // Создание индивида
        public static void CreatIndiv(ref int[,] individ, int row, int cow) {
            for (int i=0; i<row; i++) {
                int a = 0;
                for (int j=0; j<cow; j++) {
                    if (a !=2){
                        individ[i, j] = rand.Next(0, 2);
                        a += individ[i, j];     
                    }
                    else individ[i, j] = 0;
                }
            }      
        }

        // Формирование популяции
        public static void CreatPopulation(ref int[,] population, int size_of_popul, int gene, ref int[,] individ) {
            CreatrMatr(ref individ,8,8);
            CreatrMatr(ref population, 8 * size_of_popul, 8);

            int number_i = 0;

            for (int i = 0; i < size_of_popul; i++) {
                InizMatr(ref individ, 8, 8);
                CreatIndiv(ref individ, 8, 8);
                Copy(ref individ, ref population, number_i);

                number_i += 8;
            }         
        }

        // Метод капирования индивида в популяцию
        public static void Copy(ref int[,] individ, ref int[,] population, int nuber_i) {
            int y = 0;
            for (int i = nuber_i; i < nuber_i + 8; i++)  {
                for(int j = 0; j<8; j++) {
                    population[i, j] = individ[y, j];              
                }
                y++;      
            }
        }

        // Метод оценки
        public static int func(ref int[,] mas, int row, int number_i) {
            int col = 0;
            int[,] massiv = new int[2, 16];
            int k = 0, k1 = 0;
            
            for (int i = number_i; i < number_i + 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (mas[i, j] == 1) {
                        massiv[0, k] = i;
                        massiv[1, k1] = j;
                        k1++;
                        k++;
                   }                     
                }
            }

          // Сделать вестора координат и работать с ними, так же при кросенговере.

          for(int i=0; i<15; i++) {
                for (int j=i+1; j<16; j++){
                    double a = Math.Sqrt(Math.Pow(massiv[0,j] - massiv[0,i], 2) + Math.Pow(massiv[1,j] - massiv[1,i], 2));
                    if (a >= 2) col++;
                }
             }
            return col;
        }

        //Кроссенговер одинарный
        public static void Crossengover(ref int[,] individ1, ref int[,] individ2, int number_i, ref int[,] population0) {
            int point = rand.Next(1, 14);
            // Создание векторов координат
            int col = 0;
            int[,] massiv1 = new int[2, 16];
            int[,] massiv2 = new int[2, 16];
            int[,] new_massiv1 = new int[2, 16]; 
            int[,] new_massiv2 = new int[2, 16]; 

            int k = 0, k1 = 0;
            int z = 0, z1 = 0;

            //Инициализация векторов

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++){
                    if (individ1[i,j]==1) {
                        massiv1[0, k] = i;
                        massiv1[1, k1] = j;
                        k1++;
                        k++;
                    }
                    if (individ2[i, j] == 1) {
                        massiv2[0, z] = i;
                        massiv2[1, z1] = j;
                        z++;
                        z1++;
                    }
                }
            }

            // Кросенговер

            for(int i=0; i<2; i++){
                for(int j=0; j<point; j++){
                    new_massiv1[i, j] = massiv1[i, j];
                    new_massiv2[i, j] = massiv2[i, j];
                }
            }


            for (int i = 0; i < 2; i++) {
                for (int j = point; j < 16; j++) {
                    new_massiv1[i, j] = massiv2[i, j];
                    new_massiv2[i, j] = massiv1[i, j];
                }
            }
            Mutation(ref new_massiv1);
            Mutation(ref new_massiv2);

            InizMatr(ref individ1, 8, 8);
            InizMatr(ref individ2, 8, 8);

            //формирование матриц индивидов
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    for(int q = 0; q<16; q++) {
                        if (i == new_massiv1[0, q] & j == new_massiv1[1, q]){
                            individ1[i, j] = 1;
                        }
                       
                        if (i == new_massiv2[0, q] & j == new_massiv2[1, q]) {
                            individ2[i, j] = 1;
                        }                  
                    }   
                }
            }

            Copy(ref individ1,ref population0, number_i);
            Copy(ref individ2, ref population0, number_i+8);
        }

        // Метод копирования одной матрицы в другую
        public static void CopyMatrInMatr(ref int[,]popul1, ref int[,]popul2, int size, int m) {
            for(int i=0; i < size * 8; i++){
                for (int j=0; j<m; j++) {
                    popul1[i, j] = popul2[i, j];
                }
            }
        }

        // Ранжирование 
       public static void Sortirovka(ref int[] A,int size,ref int[] result ){
            for(int i=0; i<size-1; i++){
                for(int j =i+1; j<size; j++) {
                    if(result[A[i]] < result[A[j]]) {
                        int fer = A[i];
                        A[i] = A[j];
                        A[j] = fer;
                    }
                }
            }
        } 

        //Сортировка матрицы по вектору

        public static void SortForMatr(ref int[] A, ref int[,]popul,int size) {
            int[,] mas = new int[size*8,8];
            int a = 0;
            int b = 0;
            for (int z =0; z<size*8; z += 8) {
                int ii = A[a] * 8;
                int iii = ii + 8;
                for (int i = ii; i < iii; i++) {
                    for (int j = 0; j < 8; j++) {
                        mas[b, j] = popul[i, j];
                    }
                    b++;
                }
                a++;   
            }

            for(int i = 0; i < size * 8; i++) {
                for (int j=0; j<8; j++) {
                    popul[i, j] = mas[i, j];
                }
            }       
        }

        //Мутация
        public static void Mutation(ref int[,] mas) {
            int chis = rand.Next(0, 10);
            if (chis == 2)
            {
                int gen = rand.Next(0, 16);
                mas[1, gen] = rand.Next(0, 8);
            }

            if (chis == 4)
            {
                int gen = rand.Next(0, 16);
                int neg = rand.Next(0, 16);
                mas[1, gen] = rand.Next(0, 8);
                mas[1, neg] = rand.Next(0, 8);
            }
        }


        public static void CopyVectr(ref int[] vectr, ref int[] new_vectr, int size)
        {
            for (int i = 0; i < size; i++) vectr[i] = new_vectr[i];
        }


        public static void PrintPopul(ref int[,] popul,int size)
        {
            for(int i = 0; i < size * 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Console.Write(popul[i, j]);
                }
                Console.Write("\n");
            }
        }

        public static void PrintResult(ref int[,] popul, int size, int b, int cycle)
        {
            int ii = b * 8;
            int iii = ii + 8;

            for (int i = ii; i<iii; i++) {
                for (int j=0; j<8; j++){
                    if (popul[i,j] == 1)Console.Write("#");
                    if (popul[i, j] == 0) Console.Write(0);
                }
                Console.Write("\n");
            }

            Console.WriteLine("Решение найденно за " + cycle + " итераций");
        }

        static void Main(string[] args)
        {
            int size_of_popul = 200;
            int gene = 16;
            int row = 8;
            int[,] individ = null;
            int[,] population = null;
            
            int[,] population0 = null;
            int[] vectr_result = null;
            int[] new_vectr_result = null;
          
            int[,] min_indiv1 = null;
            int[,] min_indiv2 = null;

            //Вектор для сортировки
            int[] A = null;

            // Создание вектора результата для старого поколения
            CreatrVectr(ref vectr_result, size_of_popul);
            InizVectr(ref vectr_result, size_of_popul);

            // Создание вектора результата для нового поколения
            CreatrVectr(ref new_vectr_result, size_of_popul);
            InizVectr(ref new_vectr_result, size_of_popul);

            CreatPopulation(ref population, size_of_popul, gene, ref individ);

            // Вектор для ранжирования
            CreatrVectr(ref A, size_of_popul);
            InizVectr(ref A, size_of_popul);

            

            int sr = 0;
            int a = 0;
            for (int i = 0; i <size_of_popul*8; i+=8) { 
                vectr_result[a] = func(ref population,row, i);
                //Console.WriteLine("Финотип индивида " + vectr_result[a]);
                
                if( vectr_result[a] == 120)
                {
                    break;
                }

                sr += vectr_result[a];
                a++;
            }
            sr /= size_of_popul;
            //Console.WriteLine("Средний фенотип по поколению " + sr + "\n");

            // Создание и инициализация вторичных матриц

            CreatrMatr(ref population0, size_of_popul * 8, 8);
            InizMatr(ref population0, size_of_popul * 8, 8);

            CreatrMatr(ref min_indiv1, 8, 8);
            CreatrMatr(ref min_indiv2, 8, 8);

            //PrintPopul(ref population, size_of_popul);

            for (int i = 0; i < size_of_popul; i++)
            {
                A[i] = i;
            }

            Sortirovka(ref A, size_of_popul, ref vectr_result);

            int flag = 0;
            // Цикл по количеству итераций
            int cycle = 0;
            while (cycle < 200) {


                SortForMatr(ref A, ref population, size_of_popul);
                int k1 = 0;
                for (int i = 0; i < size_of_popul * 8; i += 16)
                {
                    InizMatr(ref min_indiv1, 8, 8);
                    InizMatr(ref min_indiv2, 8, 8);

                    int x = 0;

                    for (int k = k1; k < k1 + 8; k++)
                    {

                        for (int j = 0; j < 8; j++)
                        {
                            min_indiv1[x, j] = population[k, j];
                            min_indiv2[x, j] = population[k + 8, j];
                        }
                        x++;
                    }
                    Crossengover(ref min_indiv1, ref min_indiv2, i, ref population0);
                    k1 += 8;
                }


                int b = 0;
                sr = 0;
                for (int i = 0; i < size_of_popul * 8; i += 8)
                {

                    new_vectr_result[b] = func(ref population0, row, i);

                    //Console.WriteLine("Фенотип индивида " + new_vectr_result[b]);

                    if (new_vectr_result[b] == 120)
                    {
                        PrintResult(ref population0, size_of_popul, b,cycle);
                        flag = 1; 
                    }
                    if (flag == 1) break;

                    sr += new_vectr_result[b];
                    b++;
                }

                if (flag == 1) cycle = 200;

                sr /= size_of_popul;
               
                CopyVectr(ref vectr_result,ref new_vectr_result, size_of_popul);
                Sortirovka(ref A, size_of_popul, ref vectr_result);

                //Console.WriteLine("Средний фенотип по поколению " + sr + "\n");

                CopyMatrInMatr(ref population, ref population0, size_of_popul, row);

                cycle++;

               
            }



            //PrintPopul(ref population, size_of_popul);


            Console.ReadKey();
        }
    }
}
