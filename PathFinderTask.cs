/*
 *  Робот изначально находится в точке checkpoints[0]. Вернуть нужно порядок посещения чекпоинтов. Например, если функция возвращает массив {0,2,1},
 *  это означает, что робот сначала поедет в чекпоинт с индексом 2, а из него в чекпоинт с индексом 1 и на этом закончит свой путь.
 *  Реализуйте следующую оптимизацию (отсечение перебора): прекращайте перебор, если текущая длина пути уже больше, чем минимальный путь, найденный ранее.
 * */

using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        private static double resultLenght = 0;
        private static int[] bestOrder; //результирующий массив содержащий порядок прохождения чекпоинтов
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)=> MakeTrivialPermutation(checkpoints.Length, checkpoints);    
        
        private static int[] MakeTrivialPermutation(int size, Point[] checkpoints)
        {
            bestOrder = new int[size]; 
            for (int i = 0; i < bestOrder.Length; i++) 
                bestOrder[i] = i;//просто заполняем массив первоначальными данными
            resultLenght = checkpoints.GetPathLength(bestOrder); // для получения некой начальной длинны прохождания
            MakePermutation(new int[size], 1, checkpoints);
            return bestOrder;
        }
        /*
         * Текущая логика метода MakePermutation: через циклы методом рекурсии создаем вариант маршрута, оцениваем его длинну во все время создания ветвлений, 
         * если длинна текущего варианта выходит длинее чем вариант который хранится в результирующем массиве - просто прекращаем дальнейший вызов рекурсии.
         * база рекурсии: когда вариант полностью собран (permutation.Length == position) оцениваем длинну маршрута с маршрутом хранящимся в результирующем массиве
         * если длинна текущего маршрута короче тогда сохраняем его в результирующий массив. Результирующий массив представляет собой массив с порядком прохождения 
         * чекпоинтов (описание есть в самом начале)
         * */
        private static void MakePermutation(int[] permutation, int position , Point[] checkpoints)
        {
                double permutationLenght = checkpoints.GetPathLength(permutation.Take(position).ToArray());  //длинна текущего варианта маршрута (найден методом перебора)          
                if (permutation.Length == position && permutationLenght < resultLenght) //если длинна текущего варианта меньше чем длинна маршрута содержащегося в результирующим массиве
                {
                    bestOrder = permutation.ToArray(); //тогда сохраняем текущий маршрут как самый короткий в результирующем массиве
                    resultLenght = permutationLenght; // сохраняем длинну найденного маршрута 
                    return; //выходим из ветки рекурсии
                }
                /*
                 * Логика рекурсии: нулевой элемент маршрута не изменяется. Цикл начинается с 1 позиции, в массив с текущими вариантом в цикле начинают заполнятся элементы
                 * При этом для нормальной работы рекурсии необходимо этот массив передавать при вызове метода, т.к. в каждой рекурсии поочередно изменяются элементы массива
                 * */
            for (int i = 1; i < permutation.Length; i++) //цикл для создания рекурсии
            {
                bool found = false; //флаг для выхода из обоих циклов
                for (int j = 1; j < position; j++)
                    if (permutation[j] == i) //если в текущем массиве уже есть значение
                    {
                        found = true; //выходим из первого цикла
                        break;
                    }
                if (found) continue; //выходим из первого цикла
                permutation[position] = i; //иначе записать значние в массив
                if (permutationLenght < resultLenght) //если длинна текущего маршрута пока что короче или равна результирующему вызов рекурсии. немного недоработан т.к. здесь не оценивается длинна с новым добавленным значением, переоценка произойдет после рекурсии 
                    MakePermutation(permutation, position + 1 , checkpoints);
            }
        }
    }
}