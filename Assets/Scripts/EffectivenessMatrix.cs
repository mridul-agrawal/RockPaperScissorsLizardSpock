/// <summary>
/// This Class is used to determine which object is more effective on which object using a 2D Array. 
/// </summary>
public class EffectivenessMatrix 
{
    static int[][] matrix =
    {
        //Has to be same order as ObjectType class
        //                      Roc   Pap    Sic    Liz    Spo    
        /*Rock*/     new int[] {0,    -1,     1,     1,    -1 },
        /*Paper*/    new int[] {1,     0,    -1,    -1,     1 },
        /*Scissors*/ new int[] {-1,    1,     0,     1,    -1 },
        /*Lizard*/   new int[] {-1,    1,    -1,     0,     1 },
        /*Spock*/    new int[] {1,    -1,     1,    -1,     0 },
    };

    // A method which takes 2 ObjectTypes and returns the result as an integer using the effectiveness matrix.
    public static int GetResult(ObjectType playerType, ObjectType cpuType)
    {
        int row = (int)playerType - 1;
        int col = (int)cpuType - 1;

        return matrix[row][col];
    }

}
