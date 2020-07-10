using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    public static string EXE_NAME = "Build";

    struct BuildArgs
    {
        public string Output
        {
            get
            {
                string o;
                switch (Target)
                {
                    case BuildTarget.StandaloneWindows: o = $"win32/{EXE_NAME}.exe"; break;
                    case BuildTarget.StandaloneWindows64: o = $"win64/{EXE_NAME}.exe"; break;
                    case BuildTarget.WebGL: o = "webgl"; break;
                    default: throw new Exception("No target was specified!");
                }
                return $"{OutputRootDir.TrimEnd('/')}/" + o;
            }
        }
        public string OutputRootDir { get; set; }
        public BuildTarget Target { get; set; }
    }

    private static string[] QueryEnabledScenes()
    {
        return EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
    }

    private static void Build(in BuildArgs args)
    {
        Debug.Log($"Building ${args.Target} ... ");

        var ecode = 0;
        try
        {
            var result = BuildPipeline.BuildPlayer(QueryEnabledScenes(), args.Output, args.Target, BuildOptions.None);
            ecode = result.summary.totalErrors;
        }
        catch (Exception ex)
        {
            Debug.Log($"[ERROR] during build process: {ex.Message}");
            ecode = 1;
        }
#if UNITY_EDITOR
#else
            EditorApplication.Exit(ecode);
#endif
    }
    public static void BuildCLI()
    {
        BuildArgs bArgs = new BuildArgs();
        bArgs.Target = EditorUserBuildSettings.activeBuildTarget;

        var args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length - 1; ++i)
        {
            if (args[i] == "-o")
                bArgs.OutputRootDir = args[i + 1].Trim(new char[] { '\"' });
        }
        Build(bArgs);
    }

    [MenuItem("BuildPipeline/Build Win32")]
    public static void BuildWin32()
    {
        Build(new BuildArgs()
        {
            OutputRootDir = $"{Application.dataPath}/../Build",
            Target = BuildTarget.StandaloneWindows
        });
    }
    [MenuItem("BuildPipeline/Build Win64")]
    public static void BuildWin64()
    {
        Build(new BuildArgs()
        {
            OutputRootDir = $"{Application.dataPath}/../Build",
            Target = BuildTarget.StandaloneWindows64
        });
    }
    [MenuItem("BuildPipeline/Build WebGL")]
    public static void BuildWebGL()
    {
        Build(new BuildArgs()
        {
            OutputRootDir = $"{Application.dataPath}/../Build",
            Target = BuildTarget.WebGL
        });
    }
    [MenuItem("BuildPipeline/Build All")]
    public static void BuildAll()
    {
        BuildWin32();
        BuildWin64();
        BuildWebGL();
    }
}