using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// spawn random points or simple sphere objects
// generate random numbers between 33, 126 for points x, y, z
// gather their x, y, z add them together and then divide by 3 casting any float values to ints
// cast those ints to chars and save to char array
// use string builder to to then convert that character array to a string

public class GenStars : MonoBehaviour
{
    public GameObject spawnee;
    string starPosData;
    string filePathSPD = "starposdata.txt";

    int countObjs;

    private void Start()
    {
        StartCoroutine(StarDrop());
    }
    #region Generate Stars
    IEnumerator StarDrop()
    {
        Debug.Log("I'm looking for the god damn file!");
        if(!File.Exists(filePathSPD))
        {
            StreamWriter writer = new StreamWriter(filePathSPD, false);
            
            int[,] grid = new int[32, 3];

            int i, j, count;

            int x, y, z;
            x = 0;
            y = 0;
            z = 0;

            // this produces 3 column, 32 row 2 dimensional array
            count = 0;

            for (j = 0; j < 32; j++)
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
                        starPosData = x.ToString() + "," + y.ToString() + "," + z.ToString();

                        writer.WriteLine(starPosData);
                    }

                }
            }
            writer.Close();

            countObjs += 1;

            //Debug.Log("Array length: " + grid.Length);
            //Debug.Log("Value at row 13, col 3: " + grid[12, 2]);
            yield return new WaitForSeconds(0.1F);
            Debug.Log("Generated Star Position Data.");
            GenStarPattern();
        }
        else
        {
            yield return new WaitForSeconds(0.1F);
            Debug.Log("File exists.");
            GenStarPattern();
        }
        
    }
    #endregion

    #region Generate the Star Pattern 
    void GenStarPattern()
    {
        // read in star position data from file
        // string fileDecrypted = DecryptText(filePathSPD);

        if (File.Exists(filePathSPD))
        {
            Debug.Log("File was found.");
            int rowCount = 32;
            int columnCount = 3;
            int i, j;
            int x, y, z, count, total;

            var returnArray = new int[rowCount, columnCount];

            StreamReader sr = File.OpenText(filePathSPD);

            string s;
            string[] split = null;
            count = 0;
            
            x = 0;
            y = 0;
            z = 0;

            char tempChar;

            StringBuilder sb = new StringBuilder();
            // StreamWriter writer = new StreamWriter(filePathSPD, false);

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
                    if(count == 3)
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

