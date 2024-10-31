using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Raytracer.Common.Tests;

public class MatrixScenarios
{
    [Fact]
    public void Constructing_and_inspecting_a_4x4_matrix()
    {
        var matrix = new Matrix<double>(new[,]
            { { 1, 2, 3, 4 }, { 5.5, 6.5, 7.5, 8.5 }, { 9, 10, 11, 12 }, { 13.5, 14.5, 15.5, 16.5 } });
        using var assertionScope = new AssertionScope();
        matrix[0, 0].Should().Be(1);
        matrix[0, 3].Should().Be(4);
        matrix[1, 0].Should().Be(5.5);
        matrix[1, 2].Should().Be(7.5);
        matrix[2, 2].Should().Be(11);
        matrix[3, 0].Should().Be(13.5);
        matrix[3, 2].Should().Be(15.5);
    }
    
    [Fact]
    public void Constructing_and_inspecting_a_2x2_matrix()
    {
        var matrix = new Matrix<int>(new[,]
            { { -3, 5 }, {1, -2} });
        using var assertionScope = new AssertionScope();
        matrix[0, 0].Should().Be(-3);
        matrix[0, 1].Should().Be(5);
        matrix[1, 0].Should().Be(1);
        matrix[1, 1].Should().Be(-2);
    }
    
    [Fact]
    public void Constructing_and_inspecting_a_3x3_matrix()
    {
        var matrix = new Matrix<int>(new[,]
            { { -3, 5, 0 }, {1, -2, -7}, {0, 1, 1} });
        using var assertionScope = new AssertionScope();
        matrix[0, 0].Should().Be(-3);
        matrix[1, 1].Should().Be(-2);
        matrix[2, 2].Should().Be(1);
    }
    
    [Fact]
    public void Matrix_equality_with_identical_matrices()
    {
        var matrixA = new Matrix<int>(new[,]
            { { 1,2,3,4 }, {5,6,7,8}, {9,8,7,6}, {5,4,3,2}});
        var matrixB = new Matrix<int>(new[,]
            { { 1,2,3,4 }, {5,6,7,8}, {9,8,7,6}, {5,4,3,2}});
        using var assertionScope = new AssertionScope();
        var areEqual = matrixA == matrixB;
        areEqual.Should().BeTrue();
    }
    
    [Fact]
    public void Matrix_equality_with_non_identical_matrices()
    {
        var matrixA = new Matrix<int>(new[,]
            { { 1,2,3,4 }, {5,6,7,8}, {9,8,7,6}, {5,4,3,2}});
        var matrixB = new Matrix<int>(new[,]
            { { 2,3,4,5 }, {6,7,8,9}, {8,7,6,5}, {4,3,2,1}});
        using var assertionScope = new AssertionScope();
        var areEqual = matrixA == matrixB;
        areEqual.Should().BeFalse();
    }
    
    [Fact]
    public void Multiplying_two_matrices()
    {
        var matrixA = new Matrix<int>(new[,]
            { { 1,2,3,4 }, {5,6,7,8}, {9,8,7,6}, {5,4,3,2}});
        var matrixB = new Matrix<int>(new[,]
            { { -2, 1, 2, 3 }, {3, 2, 1, -1}, {4, 3, 6 ,5}, {1,2,7,8}});
        using var assertionScope = new AssertionScope();
        var expectedResult = new Matrix<int>(new[,]
            { { 20,22,50, 48 }, {44, 54 ,114, 108}, {40, 58, 110 ,102}, {16,26,46,42}});
        ((matrixA * matrixB) == expectedResult).Should().BeTrue();
    }

    [Fact]
    public void Multiplying_a_matrix_with_a_tuple()
    {
        var matrix = new Matrix<int>(new[,]
            { {1,2,3,4}, {2,4,4,2}, {8,6,4,1}, {0,0,0,1}});
        var tuple = new Tuple(1, 2, 3, 1);

        var actualTuple = (matrix * tuple).ToTuple();
        var expectedTuple = new Tuple(18, 24, 33, 1);
        (actualTuple).Should().Be(expectedTuple);
    }
    
    [Fact]
    public void Multiplying_a_matrix_with_a_identity_matrix()
    {
        var matrix = new Matrix<int>(new[,]
            { {0, 1, 2, 4}, {1,2,4,8}, {2,4,8,16}, {4,8,16,32}});
        var identityMatrix = matrix.IdentityMatrix();

        var actualTuple = (matrix * identityMatrix);
        (actualTuple == matrix).Should().Be(true);
    }
    
    [Fact]
    public void Multiplying_a_tuple_with_a_identity_matrix()
    {
        var identityMatrix = Matrix<int>.IdentityMatrix(4);
        var tuple = new Tuple(1, 2, 3, 4);
        var actualTuple = (identityMatrix * tuple).ToTuple();
        actualTuple.Should().Be(tuple);
    }
    
    [Fact]
    public void Transposing_a_matrix()
    {
        var matrix = new Matrix<int>(new[,] {{0, 9, 3, 0}, {9, 8, 0, 8}, {1, 8, 5,3}, {0, 0, 5, 8}});
        var actualResult = matrix.Transpose();
        var expectedResult = new Matrix<int>(new[,] { { 0, 9, 1, 0 }, { 9, 8, 8, 0 }, { 3, 0, 5, 5 }, { 0, 8, 3, 8 } });
        (actualResult == expectedResult).Should().BeTrue();
    }

    [Fact]
    public void Calculating_the_determinant_of_a_2x2_matrix()
    {
        var matrix = new Matrix<int>(new int[,] { { 1, 5 }, { -3, 2 } });
        matrix.Determinant().Should().Be(17);
    }
    
    [Fact]
    public void Submatrix_of_3x3_should_be_2x2()
    {
        var matrix = new Matrix<int>(new int[,] { {1, 5, 0}, {-3, 2, 7}, {0, 6,-3} });
        var res = matrix.GetSubmatrix(0, 2) == new Matrix<int>(new int[,] {{-3, 2}, {0, 6}});
        res.Should().BeTrue();
    }
    
    [Fact]
    public void Submatrix_of_4x4_should_be_3x3()
    {
        var matrix = new Matrix<int>(new int[,] {{ -6,1,1,6}, {-8, 5, 8, 6}, {-1, 0, 8, 2}, {-7, 1, -1, 1}} );
        var res = matrix.GetSubmatrix(2, 1) == new Matrix<int>(new int[,] {{-6, 1, 6}, {-8,8,6}, {-7,-1,1}});
        res.Should().BeTrue();
    }
    
    [Fact]
    public void Calculating_the_minor_of_a_3x3_matrix()
    {
        var matrix = new Matrix<int>(new int[,] {{3, 5, 0}, {2, -1, -7}, {6, -1, 5}});
        var b = matrix.GetSubmatrix(1, 0);

        var det = b.Determinant();
        det.Should().Be(25);
        var minor = matrix.GetMinor(1, 0);
        minor.Should().Be(25);
    }
    
    [Fact]
    public void Computing_cofactors()
    {
        var matrix = new Matrix<int>(new int[,] {{3, 5, 0}, {2, -1, -7}, {6, -1, 5}});
        matrix.GetMinor(0, 0).Should().Be(-12);
        matrix.GetCofactor(0, 0).Should().Be(-12);
        matrix.GetMinor(1, 0).Should().Be(25);
        matrix.GetCofactor(1,0).Should().Be(-25);
        
    }
    
    [Fact]
    public void Calculate_the_determinant_of_a_3x3_matrix()
    {
        var matrix = new Matrix<int>(new[,] {{1,2,6}, {-5, 8, -4}, {2, 6, 4}});
        matrix.GetCofactor(0, 0).Should().Be(56);
        matrix.GetCofactor(0, 1).Should().Be(12);
        matrix.Determinant().Should().Be(-196);
        
    }
    
    [Fact]
    public void Calculate_the_determinant_of_a_4x4_matrix()
    {
        var matrix = new Matrix<int>(new[,]
            { { -2, -8, 3, 5 }, { -3, 1, 7, 3 }, { 1, 2, -9, 6 }, { -6, 7, 7, -9 } });
        matrix.GetCofactor(0, 0).Should().Be(690);
        matrix.GetCofactor(0, 2).Should().Be(210);
        matrix.GetCofactor(0, 3).Should().Be(51);
        matrix.Determinant().Should().Be(-4071);
    }
}

public class Matrix<T> where T : INumber<T>
{
    public static Matrix<int> IdentityMatrix(int rows)
    {
        var identity = new int[rows, rows];
        for (var i = 0; i < rows; i++)
        {
            identity[i, i] = 1;
        }

        return new Matrix<int>(identity);
    } 
    
    public Matrix<int> IdentityMatrix()
    {
        var identity = new int[Rows, Rows];
        for (var i = 0; i < Rows; i++)
        {
            identity[i, i] = 1;
        }

        return new Matrix<int>(identity);
    } 
    
    private readonly T[,] _data;

    public Matrix(int rows, int cols)
    {
        _data = new T[rows, cols];
    }
    public static bool operator ==(Matrix<T> a, Matrix<T> b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            return false;

        for (int i = 0; i < a.Rows; i++)
        for (int j = 0; j < a.Cols; j++)
            if (!EqualityComparer<T>.Default.Equals(a[i, j], b[i, j]))
                return false;
        return true;
    }
    
    public static bool operator !=(Matrix<T> a, Matrix<T> b)
    {
        return !(a == b);
    }
    
    public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
    {
        if (a.Cols != b.Rows) throw new InvalidArrayMultiplicationException();
        //  Could consider translating the b matrix
        var result = new T[a.Cols, b.Rows];
        // nu [0,0] = A0 row dot b column0
        // nu [0,1] = A0 row dot b column 1
        for (int i = 0; i < a.Rows; i++)
        {
            for (int k = 0; k < b.Cols; k++)
            {
                T sum = default!;
                for (int j = 0; j < b.Cols; j++)
                {
                    sum = sum + a[i, j] * b[j, k];
                }
                result[i, k] = sum;
            }
        }

        return new Matrix<T>(result);
    }
    
    public static Matrix<T> operator *(Matrix<T> a, Tuple t)
    {
        var result = new T[a.Rows, 1];

        for (int i = 0; i < a.Rows; i++)
        {
            for (int k = 0; k < 1; k++)
            {
                T sum = default(T);
                for (int j = 0; j < 4; j++)
                {
                    T part = default(T);
                    switch (j)
                    {
                        case 0:
                            part = (T)Convert.ChangeType(t.X, typeof(T));
                            break;
                        case 1:
                            part = (T)Convert.ChangeType(t.Y, typeof(T));
                            break;
                        case 2:
                            part = (T)Convert.ChangeType(t.Z, typeof(T));
                            break;
                        case 3:
                            part = (T)Convert.ChangeType(t.W, typeof(T));
                            break;
                        default:
                            part = default(T);
                            break;
                    }
                    sum = sum + a[i, j] * part;
                }
                result[i, k] = sum;
            }
        }

        return new Matrix<T>(result);
    }

    public Tuple ToTuple()
    {
        double x = 0;
        double y = 0;
        double z = 0;
        double w = 0;
        for (int i = 0; i < Rows; i++)
        {
           
            switch (i)
            {
                case 0:
                    x = (double)Convert.ChangeType(_data[0,0], typeof(double));
                    break;
                case 1:
                    y = (double)Convert.ChangeType(_data[1,0], typeof(double));

                    break;
                case 2:
                    z = (double)Convert.ChangeType(_data[2,0], typeof(double));

                    break;
                case 3:
                    w = (double)Convert.ChangeType(_data[3,0], typeof(double));
                    break;
            }
        }

        return new Tuple(x, y, z, w);
    }
    
    public Matrix(T[,] data )
    {
        //  Not ideal for reference types.
        _data = data;
    }
    
    public T this[int row, int col]
    {
        get => _data[row, col];
        set => _data[row, col] = value;
    }
    
    public int Rows => _data.GetLength(0);
    public int Cols => _data.GetLength(1);

    public Matrix<T> Transpose()
    {
        var matrix = new Matrix<T>(Cols, Rows);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                matrix[j, i] = _data[i, j];
            }
        }

        return matrix;
    }

    public T Determinant()
    {
        if (Rows == 2 && Cols == 2)
        {
            return _data[0,0] * _data[1,1] - _data[0,1] * _data[1,0];
        }

        T det = default;
        for (int i = 0; i < Cols; i++)
        {
            det = det + _data[0, i] * GetCofactor(0, i);
        }

        return det;
    }
    
    public Matrix<T> GetSubmatrix(int x, int y)
    {
        if (x < 0 || x >= Cols) throw new Exception();
        if (y < 0 || y >= Rows) throw new Exception();
        var rowCount = Rows - 1;
        var columnCount = Cols - 1;

        var matrix = new Matrix<T>(rowCount, columnCount);
        for (int i = 0; i < Rows; i++)
        {
            if(i == x) continue;
            var xCoordinate = i > x ? i - 1 : i;
            for (int j = 0; j < Cols; j++)
            {
                if(j == y) continue;
                var yCoordinate = j > y ? j - 1 : j;
                matrix[xCoordinate, yCoordinate] = _data[i, j];
            }
        }

        return matrix;
    }

    public T GetMinor(int col, int row)
    {
        return GetSubmatrix(col, row).Determinant();
    }

    public T GetCofactor(int col, int row)
    {
        var sign = (col + row) % 2 == 0 ? 1 : -1;
        return MultiplyBySign(GetMinor(col, row), sign);
    }
    //  WHY DID I HAVE TO DO IT WITH GENERICS FUCK MY LIFE.
    private T MultiplyBySign(T value, int sign)
    {
        if (sign == 1)
        {
            return value;
        }
        else
        {
            return (dynamic)value * -1; // Using dynamic for runtime resolution
        }
    }
    
}

public class InvalidArrayMultiplicationException : Exception {

}