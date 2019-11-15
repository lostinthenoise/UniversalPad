using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenStarCluster : MonoBehaviour
{
    public GameObject spawnee;
    string starClusterPos;
    string scfilename = "starclusterposdata.txt";
    int countObjs;

    private void Start()
    {
        StartCoroutine(StarClusterDrop());
    }

    #region Generate Stars Clusters
    IEnumerator StarClusterDrop()
    {

        if (!File.Exists(scfilename))
        {
            // Debug.Log("-----------STAR CLUSTER DATA------------");
            StreamWriter writer = new StreamWriter(scfilename, false);

            int[,] grid = new int[16, 3];

            int i, j, count;

            int x, y, z;
            x = 0;
            y = 0;
            z = 0;

            // this produces 3 column, 32 row 2 dimensional array
            count = 0;

            for (j = 0; j < 16; j++)
            {
                for (i = 0; i < 3; i++)
                {
                    count++;
                    if (i == 0)
                    {
                        x = grid[j, i] = Random.Range(33, 126);
                        // Console.WriteLine("X: " + x);
                    }
                    if (i == 1)
                    {
                        y = grid[j, i] = Random.Range(33, 126);
                        // Console.WriteLine("Y: " + y);
                    }
                    if (i == 2)
                    {
                        z = grid[j, i] = Random.Range(33, 126);
                        // Console.WriteLine("Z: " + z);
                    }

                    // Debug.Log(grid[j, i] + " ");

                    if (count == 3)
                    {
                        // Debug.Log("");
                        count = 0;
                        Instantiate(spawnee, new Vector3(x, y, z), Quaternion.identity);
                        yield return new WaitForSeconds(0.01F);
                        countObjs += 1;
                        // Debug.Log("Object number: " + countObjs);
                        starClusterPos = x.ToString() + "," + y.ToString() + "," + z.ToString();
                        writer.WriteLine(starClusterPos);
                    }

                }
            }
            writer.Close();

            countObjs += 1;

            //Debug.Log("Array length: " + grid.Length);
            //Debug.Log("Value at row 13, col 3: " + grid[12, 2]);

            GenClusterPattern();
        }
        else
        {
            // Debug.Log("File exists.");
            GenClusterPattern();
        }

    }
    #endregion

    #region Generate the Star Cluster Pattern
    void GenClusterPattern()
    {
        // read in star position data from file

        if (File.Exists(scfilename))
        {

            int rowCount = 16;
            int columnCount = 3;
            int i, j;
            int x, y, z, count, total;

            var returnArray = new int[rowCount, columnCount];

            StreamReader sr = File.OpenText(scfilename);

            string s;
            string[] split = null;
            count = 0;

            x = 0;
            y = 0;
            z = 0;

            char tempChar;

            StringBuilder sb = new StringBuilder();

            for (i = 0; (s = sr.ReadLine()) != null; i++)
            {
                split = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (j = 0; j < columnCount; j++)
                {
                    count++;
                    if (j == 0)
                    {
                        x = returnArray[i, j] = int.Parse(split[j]);
                    }
                    if (j == 1)
                    {
                        y = returnArray[i, j] = int.Parse(split[j]);
                    }
                    if (j == 2)
                    {
                        z = returnArray[i, j] = int.Parse(split[j]);
                    }

                    // Debug.Log(returnArray[i, j]);
                    if (count == 3)
                    {
                        count = 0;
                        total = (x + y + z) / 3;
                        tempChar = (char)total;
                        sb.Append(tempChar);
                        // Debug.Log("Total: " + total);
                    }
                }

            }
            Debug.Log(sb.ToString().TrimEnd('\n'));
           
            sr.Close();
        }
        else
        {
            Debug.Log("Hmmmm...something smelly happened.");
        }
    }

    #endregion

}
