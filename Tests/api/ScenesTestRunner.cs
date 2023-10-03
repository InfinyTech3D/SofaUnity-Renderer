using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;


public class ScenesTestRunner : MonoBehaviour
{
    // String to store all logs from tested scenes
    private string m_logs;
    private Queue m_logsQueue = new Queue();
    private int m_maxstack = 5000;

    // Use EditorBuildSettings Scene
    private List<EditorBuildSettingsScene> m_editorBuildSettingsScenes = null;

    // Internal data to store the number of scene to test
    public int m_nbrTestedScenes = 0;
    public bool WriteRefMode = false;
    public bool m_testEndoscopy = true;
    public bool m_testFluoroscopy = true;
    public bool m_testUltrasound = true;
    public bool m_testHaptic = true;

    // Start is called before the first frame update
    void Start()
    {
        // Add log catching
        Application.logMessageReceived += HandleLog;

        // Create list of scene to add to build settings
        m_editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();

        // 1. Add basic examples
        string folderExamples = Application.dataPath + "/SofaUnity/Scenes/Examples/";
        m_nbrTestedScenes += AddUnitySceneFromFolder(folderExamples);

        // 2. Add Demos Interaction
        string folderInteraction = Application.dataPath + "/SofaUnity/Scenes/Demos/Interaction/";
        m_nbrTestedScenes += AddUnitySceneFromFolder(folderInteraction);

        if (m_testEndoscopy)
        {
            // 3. Add Demos Capsule Endoscopy
            string folderCapsuleEndoscopy = Application.dataPath + "/SofaUnity/Scenes/Demos/Endoscopy/Virtual Capsule Endoscopy/Scenes/";
            m_nbrTestedScenes += AddUnitySceneFromFolder(folderCapsuleEndoscopy);

            // 4. Add Demos BeamAdapter Endoscopy
            string folderBeamAdapter = Application.dataPath + "/SofaUnity/Scenes/Demos/Endoscopy/BeamAdapter/";
            m_nbrTestedScenes += AddUnitySceneFromFolder(folderBeamAdapter);
        }

        if (m_testFluoroscopy)
        {
            // 5. Add Demos Fluoroscopy
            string folderFluoro = Application.dataPath + "/SofaUnity/Scenes/Demos/Imaging/Fluoroscopy/Scenes/";
            m_nbrTestedScenes += AddUnitySceneFromFolder(folderFluoro);
        }

        if (m_testUltrasound)
        {
            // 6. Add Demos Ultrasound
            string folderUltrasound = Application.dataPath + "/SofaUnity/Scenes/Demos/Imaging/Ultrasound/Scenes/";
            m_nbrTestedScenes += AddUnitySceneFromFolder(folderUltrasound);
        }

        if (m_testHaptic)
        {
            // 7. Add Demos Haptics
            string folderHaptics = Application.dataPath + "/SofaUnity/Scenes/Demos/Haptic/";
            m_nbrTestedScenes += AddUnitySceneFromFolder(folderHaptics);
        }

        EditorBuildSettings.scenes = m_editorBuildSettingsScenes.ToArray();
        Debug.Log("Nbr scene in EditorBuildSettings: " + EditorBuildSettings.scenes.Length);
        Debug.Log("Nbr SceneManager.sceneCountInBuildSettings: " + SceneManager.sceneCountInBuildSettings);
        Debug.Log("Nbr scene counted: " + m_nbrTestedScenes);
    }


    int AddUnitySceneFromFolder(string folderPath)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(folderPath);

        FileInfo[] info = dirInfo.GetFiles("*.unity");
        int cptScene = 0;
        foreach (FileInfo f in info)
        {
            string relativepath = f.FullName.Substring(Application.dataPath.Length);
            relativepath = "Assets/" + relativepath.Replace("\\", "/");
            Debug.Log(f.Name + " = " + relativepath);

            m_editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(relativepath, true));
            cptScene++;
        }

        return cptScene;
    }


    void OnDisable()
    {
        Debug.Log("#########  ScenesTestRunner OnDisable  #########");
        Application.logMessageReceived -= HandleLog;
    }

    int m_cptInternal = 0;
    int m_testedLevel = 0; // Last being the scene checker in build settings
    string m_sceneName = "";

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (m_testedLevel >= SceneManager.sceneCountInBuildSettings - 1)
        if (m_testedLevel >= 2)
            return;

        if (m_cptInternal == 50)
        {
            // open scene to test
            openTestScene(m_testedLevel);
        }

        if (m_cptInternal == 300)
        {
            // write scene logs
            writeLogs(m_testedLevel);

            // take screenshot
            takeScreenShot(m_sceneName);

            // close scene
            closeTestScene(m_testedLevel);

            m_testedLevel++;

            if (WriteRefMode)
            {
                m_cptInternal = 0;
                m_sceneName = "";
            }
        }

        if (!WriteRefMode && m_cptInternal == 310)
        {
            compareScreenshots(m_sceneName);
            m_cptInternal = 0;
            m_sceneName = "";
        }

        m_cptInternal++;
    }

    
    void openTestScene(int level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        Scene my_scene = SceneManager.GetSceneByBuildIndex(level);
        m_sceneName = my_scene.name;
        Debug.Log("#########  Load Level: " + level + " -> " + m_sceneName + "  #########");
    }


    bool closeTestScene(int level)
    {
        Scene my_scene = SceneManager.GetSceneByBuildIndex(level);
        Debug.Log("#########  Close Level: " + level + " -> " + my_scene.name + "  #########");
        bool res = SceneManager.UnloadScene(level);
        return res;
    }


    void takeScreenShot(string sceneName)
    {
        string folderPath;
        if (WriteRefMode)
            folderPath = Directory.GetCurrentDirectory() + "/Assets/SofaUnity/Tests/references/";
        else
            folderPath = Directory.GetCurrentDirectory() + "/Assets/SofaUnity/Tests/logs/";

        string screenshotName = "Snap_" + sceneName + ".png";
        string fullPath = System.IO.Path.Combine(folderPath, screenshotName);
        ScreenCapture.CaptureScreenshot(fullPath);
        
        Debug.Log("Capture: " + fullPath);
        string logPath = System.IO.Path.Combine(folderPath, "All-Logs.txt");
        StreamWriter writer = new StreamWriter(logPath, false);
        writer.Write(m_logs);
        writer.Close();
    }


    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (m_logsQueue.Count > m_maxstack)
            m_logsQueue.Clear();

        m_logs = logString;
        string newString = "\n [" + type + "] : " + m_logs;
        m_logsQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            m_logsQueue.Enqueue(newString);
        }
        m_logs = string.Empty;
        foreach (string mylog in m_logsQueue)
        {
            m_logs += mylog;
        }
    }


    void writeLogs(int level)
    {
        Scene my_scene = SceneManager.GetSceneByBuildIndex(level);
        Debug.Log("#########  Close Level: " + level + " -> " + my_scene.name + "  #########");
        GameObject _sofaContext = GameObject.Find("SofaContext");
        if (_sofaContext == null)
        {
            Debug.LogError("SofaContext not found in scene: " + level + " -> " + my_scene.name);
            return;
        }

        string folderLogs;
        if (WriteRefMode)
            folderLogs = Directory.GetCurrentDirectory() + "/Assets/SofaUnity/Tests/references/";
        else
            folderLogs = Directory.GetCurrentDirectory() + "/Assets/SofaUnity/Tests/logs/";

        string screenshotName = "Snap_" + my_scene.name + ".log";
        string fullPath = System.IO.Path.Combine(folderLogs, screenshotName);

        MeshFilter[] MeshFilters = _sofaContext.GetComponentsInChildren<MeshFilter>();

        SceneTestData sceneData = new SceneTestData(my_scene.name, MeshFilters.Length);
        foreach (MeshFilter mesh in MeshFilters)
        {
            sceneData.AddMeshData(mesh);
        }
        sceneData.WriteData(fullPath);
    }


    void compareScreenshots(string sceneName)
    {
        string folderLogs = Directory.GetCurrentDirectory() + "/Assets/SofaUnity/Tests/logs/";
        string folderRefs = Directory.GetCurrentDirectory() + "/Assets/SofaUnity/Tests/references/";

        string screenshotName = "Snap_" + sceneName + ".png";
        string fullPath = System.IO.Path.Combine(folderLogs, screenshotName);

        Debug.Log("#########  Start compareScreenshots Level: " + screenshotName + "  #########");

        string refPath = folderRefs + screenshotName;
        Debug.Log(screenshotName + " -> " + fullPath);
        Debug.Log(screenshotName + " -> " + refPath);

        Texture2D imgTest = new Texture2D(1, 1);
        imgTest.LoadImage(File.ReadAllBytes(fullPath));

        Texture2D imgRef = new Texture2D(1, 1);
        imgRef.LoadImage(File.ReadAllBytes(refPath));

        if (imgTest.height != imgRef.height || imgTest.width != imgRef.width)
        {
            Debug.LogError(screenshotName + " -> Image size [" + imgTest.width + ", " + imgTest.height + "] differ from ref: [" + imgRef.width + ", " + imgRef.height + "]");
            return;
        }

        for (int x = 0; x< imgTest.width; ++x)
        {
            for (int y = 0; y < imgTest.height; ++y)
            {
                Color pixTest = imgTest.GetPixel(x, y);
                Color pixRef = imgRef.GetPixel(x, y);
                Color pixDiff = pixTest - pixRef;
                if (Mathf.Abs(pixDiff.r) > 0.01f || Mathf.Abs(pixDiff.g) > 0.01f || Mathf.Abs(pixDiff.b) > 0.01f)
                {
                    Debug.LogError(screenshotName + " -> Image pixel differ from ref at [" + x + ", " + y + "]. Test: " + pixTest + " | Ref: " + pixRef + " | Diff: " + pixDiff);
                }
            }
        }

        Debug.Log("#########  End compareScreenshots Level: " + screenshotName + "  #########");
    }
}
