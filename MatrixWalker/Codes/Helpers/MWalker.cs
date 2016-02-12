using MatrixWalker.Codes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatrixWalker.Codes.Helpers
{
    public static class MWalker
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private static bool ValidateParameters<T>(List<List<T>> matrix, int row, int column)
        {
            if (row > (matrix.Count - 1) || row < 0)
                throw new RowIndexOutOfRangeException();

            if (column > matrix.ElementAt(0).Count - 1 || column < 0)
                throw new ColumnIndexOutOfRangeException();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static T GetValue<T>(List<List<T>> matrix, int row, int column)
        {
            ValidateParameters(matrix, row, column);

            /*
             * matrix[x] = List<T> -> first level is the row index
             * matrix[x][y] = T -> that returns the value of the x'th row and y'th column
             */

            T value = (matrix.ElementAt(row)).ElementAt(column);
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public static void UpdateValue<T>(ref List<List<T>> matrix, int row, int column, T value)
        {
            ValidateParameters(matrix, row, column);
            //matrix[row][column] = value;
            List<T> relatedRow = matrix.ElementAt(row);
            relatedRow[column] = value;
        }

        public static decimal CalculateAreaTotalValueForSquare(List<List<decimal>> matrix, int rowStartIndex, int columnStartIndex, int edgeWidth)
        {
            ValidateParameters(matrix, rowStartIndex, columnStartIndex);
            int rowEndIndex = rowStartIndex + edgeWidth;
            int columnEndIndex = columnStartIndex + edgeWidth;
            //ValidateParameters(matrix, rowEndIndex, columnEndIndex);
            decimal totalRangeValue = 0;

            for (int i = rowStartIndex; i < rowEndIndex; i++)
            {
                for (int j = columnStartIndex; j < columnEndIndex; j++)
                {
                    try
                    {
                        ValidateParameters(matrix, i, j);
                        decimal value = GetValue(matrix, i, j);
                        totalRangeValue += value;
                    }
                    catch (RowIndexOutOfRangeException)
                    {
                        continue;
                    }
                    catch (ColumnIndexOutOfRangeException)
                    {
                        continue;
                    }
                }
            }

            return totalRangeValue;
        }

        public static decimal CalculateAreaTotalValueForCircle(List<List<decimal>> matrix, int centerRowIndex, int centerColumnIndex, int radius)
        {
            ValidateParameters(matrix, centerRowIndex, centerColumnIndex);
            int rowStartIndex = centerRowIndex - radius;
            if (rowStartIndex < 0)
                rowStartIndex = 0;

            int rowEndIndex = centerRowIndex + radius;

            int columnStartIndex = centerColumnIndex - radius;
            if (columnStartIndex < 0)
                columnStartIndex = 0;

            int columnEndIndex = centerColumnIndex + radius;

            decimal totalRangeValue = 0;

            for (int i = rowStartIndex; i <= rowEndIndex; i++)
            {
                for (int j = columnStartIndex; j <= columnEndIndex; j++)
                {
                    int rowColumnMultiplication = Convert.ToInt32(Math.Floor(Math.Pow(Math.Abs(i - centerRowIndex), 2) + Math.Pow(Math.Abs(j - centerColumnIndex), 2)));

                    decimal hypotenuse = Convert.ToDecimal(Math.Sqrt((double)rowColumnMultiplication));

                    if (hypotenuse <= radius)
                    {
                        try
                        {
                            ValidateParameters(matrix, i, j);
                            decimal value = GetValue(matrix, i, j);
                            totalRangeValue += value;
                        }
                        catch (RowIndexOutOfRangeException)
                        {
                            continue;
                        }
                        catch (ColumnIndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                }
            }

            return totalRangeValue;
        }

        public static void UpdateAllValuesInTheCircularArea<T>(ref List<List<T>> matrix, int centerRowIndex, int centerColumnIndex, int radius, T newValue)
        {
            ValidateParameters<T>(matrix, centerRowIndex, centerColumnIndex);
            int rowStartIndex = centerRowIndex - radius;
            if (rowStartIndex < 0)
                rowStartIndex = 0;

            int rowEndIndex = centerRowIndex + radius;

            int columnStartIndex = centerColumnIndex - radius;
            if (columnStartIndex < 0)
                columnStartIndex = 0;

            int columnEndIndex = centerColumnIndex + radius;

            for (int i = rowStartIndex; i <= rowEndIndex; i++)
            {
                for (int j = columnStartIndex; j <= columnEndIndex; j++)
                {
                    int rowColumnMultiplication = Convert.ToInt32(Math.Floor(Math.Pow(Math.Abs(i - centerRowIndex), 2) + Math.Pow(Math.Abs(j - centerColumnIndex), 2)));

                    decimal hypotenuse = Convert.ToDecimal(Math.Sqrt((double)rowColumnMultiplication));

                    if (hypotenuse <= radius)
                    {
                        try
                        {
                            ValidateParameters<T>(matrix, i, j);

                            T value = GetValue<T>(matrix, i, j);

                            int intValue = Convert.ToInt32(value);

                            if (intValue == -1)
                                UpdateValue<T>(ref matrix, i, j, newValue);
                            else
                            {

                            }
                        }
                        catch (RowIndexOutOfRangeException)
                        {
                            continue;
                        }
                        catch (ColumnIndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                }
            }
        }


        public static void FindAndReplaceValues<T>(ref List<List<T>> matrix, T oldValue, T newValue, int rowStartIndex, int columnStartIndex, int rowEndIndex, int columnEndIndex)
        {
            for (int i = rowStartIndex; i <= rowEndIndex; i++)
            {
                for (int j = columnStartIndex; j <= columnEndIndex; j++)
                {
                    try
                    {
                        T value = GetValue<T>(matrix, i, j);

                        if (value.Equals(oldValue))
                        {
                            ValidateParameters(matrix, i, j);
                            UpdateValue<T>(ref matrix, i, j, newValue);
                        }
                    }
                    catch (RowIndexOutOfRangeException)
                    {
                        continue;
                    }
                    catch (ColumnIndexOutOfRangeException)
                    {
                        continue;
                    }
                }
            }
        }

        public static string PrintMatrix<T>(List<List<T>> matrix)
        {
            string matrixString = string.Empty;

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    matrixString += GetIdCharacter<T>(GetValue<T>(matrix, i, j)) + "\t";
                }
                matrixString += Environment.NewLine;
            }

            return matrixString;
        }

        private static char GetIdCharacter<T>(T id)
        {
            int intId = Convert.ToInt32(id);

            if (intId == -1)
                return '-';

            return (char)(int)(intId + 'A' + 1);
        }


    }
}
