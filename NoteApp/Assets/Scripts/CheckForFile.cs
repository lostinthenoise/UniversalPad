using System.IO;
using System.Text;
using UnityEngine;

public class CheckForFile : MonoBehaviour
{
	string kf = ".mtkf.txt";
    string m4 = ".mm4.txt";
    char[] daCharKeyArray = new char[32];
    char[] daM4CharKeyArray = new char[16];
    float min = 33.0f;
    float max = 126.0f;

    // Start is called before the first frame update
    void Start()
    {
		MakeDaKey();
    }

	#region Make Key File
	public void MakeDaKey()
	{
		if (!File.Exists(kf) || !File.Exists(m4))
		{
			string filePath = @kf;
			StringBuilder sKeyBuilder = new StringBuilder(32);
			// 32 random string for key file
			for (int i = 0; i < 32; i++)
			{
				daCharKeyArray[i] = (char)Random.Range((int)min, (int)max);
				// Debug.Log(daCharKeyArray[i]);
				sKeyBuilder.Append(daCharKeyArray[i]);
			}

			string sResult = sKeyBuilder.ToString();

			// Debug.Log("sResult: " + sResult);
			// Debug.Log("sResult length: " + sResult.Length);

			string content = sResult;

			StreamWriter writer = new StreamWriter(filePath, true);
			writer.WriteLine(content);
			writer.Close();

            string fileM4Path = @m4;
            StringBuilder sM4KeyBuilder = new StringBuilder(16);
            // 16 random string for key file
            for (int i = 0; i < 16; i++)
            {
                daM4CharKeyArray[i] = (char)Random.Range((int)min, (int)max);
                // Debug.Log(daM4CharKeyArray[i]);
                sM4KeyBuilder.Append(daM4CharKeyArray[i]);
            }
            string daContent = sM4KeyBuilder.ToString();

            //Debug.Log("IV file: " + daContent);
            //Debug.Log("IV file length: " + daContent.Length);

            StreamWriter writerM4 = new StreamWriter(fileM4Path, true);
            writerM4.WriteLine(daContent);
            writerM4.Close();
        }
	}
	#endregion
}
