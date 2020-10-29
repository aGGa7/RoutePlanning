/*
 *  ����� ���������� ��������� � ����� checkpoints[0]. ������� ����� ������� ��������� ����������. ��������, ���� ������� ���������� ������ {0,2,1},
 *  ��� ��������, ��� ����� ������� ������ � �������� � �������� 2, � �� ���� � �������� � �������� 1 � �� ���� �������� ���� ����.
 *  ���������� ��������� ����������� (��������� ��������): ����������� �������, ���� ������� ����� ���� ��� ������, ��� ����������� ����, ��������� �����.
 * */

using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        private static double resultLenght = 0;
        private static int[] bestOrder; //�������������� ������ ���������� ������� ����������� ����������
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)=> MakeTrivialPermutation(checkpoints.Length, checkpoints);    
        
        private static int[] MakeTrivialPermutation(int size, Point[] checkpoints)
        {
            bestOrder = new int[size]; 
            for (int i = 0; i < bestOrder.Length; i++) 
                bestOrder[i] = i;//������ ��������� ������ ��������������� �������
            resultLenght = checkpoints.GetPathLength(bestOrder); // ��� ��������� ����� ��������� ������ �����������
            MakePermutation(new int[size], 1, checkpoints);
            return bestOrder;
        }
        /*
         * ������� ������ ������ MakePermutation: ����� ����� ������� �������� ������� ������� ��������, ��������� ��� ������ �� ��� ����� �������� ���������, 
         * ���� ������ �������� �������� ������� ������ ��� ������� ������� �������� � �������������� ������� - ������ ���������� ���������� ����� ��������.
         * ���� ��������: ����� ������� ��������� ������ (permutation.Length == position) ��������� ������ �������� � ��������� ���������� � �������������� �������
         * ���� ������ �������� �������� ������ ����� ��������� ��� � �������������� ������. �������������� ������ ������������ ����� ������ � �������� ����������� 
         * ���������� (�������� ���� � ����� ������)
         * */
        private static void MakePermutation(int[] permutation, int position , Point[] checkpoints)
        {
                double permutationLenght = checkpoints.GetPathLength(permutation.Take(position).ToArray());  //������ �������� �������� �������� (������ ������� ��������)          
                if (permutation.Length == position && permutationLenght < resultLenght) //���� ������ �������� �������� ������ ��� ������ �������� ������������� � �������������� �������
                {
                    bestOrder = permutation.ToArray(); //����� ��������� ������� ������� ��� ����� �������� � �������������� �������
                    resultLenght = permutationLenght; // ��������� ������ ���������� �������� 
                    return; //������� �� ����� ��������
                }
                /*
                 * ������ ��������: ������� ������� �������� �� ����������. ���� ���������� � 1 �������, � ������ � �������� ��������� � ����� �������� ���������� ��������
                 * ��� ���� ��� ���������� ������ �������� ���������� ���� ������ ���������� ��� ������ ������, �.�. � ������ �������� ���������� ���������� �������� �������
                 * */
            for (int i = 1; i < permutation.Length; i++) //���� ��� �������� ��������
            {
                bool found = false; //���� ��� ������ �� ����� ������
                for (int j = 1; j < position; j++)
                    if (permutation[j] == i) //���� � ������� ������� ��� ���� ��������
                    {
                        found = true; //������� �� ������� �����
                        break;
                    }
                if (found) continue; //������� �� ������� �����
                permutation[position] = i; //����� �������� ������� � ������
                if (permutationLenght < resultLenght) //���� ������ �������� �������� ���� ��� ������ ��� ����� ��������������� ����� ��������. ������� ����������� �.�. ����� �� ����������� ������ � ����� ����������� ���������, ���������� ���������� ����� �������� 
                    MakePermutation(permutation, position + 1 , checkpoints);
            }
        }
    }
}