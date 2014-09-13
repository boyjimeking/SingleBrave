using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// Create a Sublime Text 2 project from a Unity project
// Includes folders and file types of your choosing
// Includes all assemblies for autocompletion in CompleteSharp package
public class SyncSublimeText2 : Editor
{
    //
    // SublimeText2プロジェクトにインクルードするディレクトリ
    //
    private static string[] sIncludeFolders = new[] {
        "Scripts",
        "NGUI",
        "Plugins",
        //
        // 追加でインクルードする場合はここに追記する
        //
    };

    //
    // SublimeText2プロジェクトにインクルードするファイル拡張子
    //
    private static string[] sIncludeExtensions = new[] {
        "cs",
        "js",
        "txt",
        "shader",
        "json",
        "mm",
        "m",
        // "cginc",
        // "xml",

        //
        // 追加でインクルードする場合はここに追記する
        //
    };

    private const string TEMPLATE   =
@"{
    ""folders"": [
__FOLDERS__
    ],

    ""settings"": {
        ""tab_size"": 4,
        ""translate_tabs_to_spaces"": true,
        ""default_encoding"": ""UTF-8 with BOM"",
        ""default_line_ending"": ""windows"",
        ""completesharp_assemblies"": [
__COMPLETESHARP_ASSEMBLIES__
        ]
    }
}";

    private const string TEMPLATE_FOLDERS   =
@"        {
            ""file_include_patterns"": [
__FILE_INCLUDE_PATTERNS__
            ],
            ""path"": __PATH__
        }";

    [MenuItem("Assets/Sync Sublime Text 2 Project")]
    private static void syncST2Project() {
        string text = TEMPLATE;
        text = text.Replace("__FOLDERS__", makeFoldersString("\t\t"));
        text = text.Replace("__COMPLETESHARP_ASSEMBLIES__", makeCompletesharpAssembliesString("\t\t\t"));

        // Output file location
        string outFolder = Application.dataPath.Substring(0, Application.dataPath.Length - "/Assets".Length);
        // Get folder name for current project
        string projectFolderName = Path.GetFileName(outFolder);

        File.WriteAllText(outFolder + "/" + projectFolderName + ".sublime-project", text);
    }

    /**
     * makeFoldersString
     */
    private static string makeFoldersString(string indent = "") {
        string fileIncludePatterns = makeFileIncludePatternsString(indent + "\t");

        string str = "";
        for (int i = 0, il = sIncludeFolders.Length; i < il; i++) {
            string folders = TEMPLATE_FOLDERS;
            folders = folders.Replace("__FILE_INCLUDE_PATTERNS__", fileIncludePatterns);
            folders = folders.Replace("__PATH__", "\"" + Application.dataPath + "/" + sIncludeFolders[i] + "/" + "\"");

            if (i < sIncludeFolders.Length - 1) {
                folders += ",\n";
            }

            str += folders;
        }

        return str;
    }

    /**
     * makeFileIncludePatternsString
     */
    private static string makeFileIncludePatternsString(string indent = "") {
        string str = "";
        for (int i = 0, il = sIncludeExtensions.Length; i < il; i++) {
            str += (indent + "\t") + string.Format("\"*.{0}\"", sIncludeExtensions[i]);
            if (i < sIncludeExtensions.Length - 1) {
                str += ",\n";
            }
        }

        return str;
    }

    /**
     * makeCompletesharpAssembliesString
     */
    private static string makeCompletesharpAssembliesString(string indent = "") {
        List<string> assemblies = new List<string>();
        // Unity***.dll
        if (Application.platform == RuntimePlatform.OSXEditor) {
            assemblies.Add(EditorApplication.applicationContentsPath + "/Frameworks/Managed/UnityEngine.dll");
            assemblies.Add(EditorApplication.applicationContentsPath + "/Frameworks/Managed/UnityEditor.dll");
        } else {
            assemblies.Add(EditorApplication.applicationContentsPath + "/Managed/UnityEngine.dll");
            assemblies.Add(EditorApplication.applicationContentsPath + "/Managed/UnityEditor.dll");
        }
        // アプリdll
        assemblies.Add(Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp.dll");
        assemblies.Add(Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp-Editor.dll");
        // その他dll
        assemblies.AddRange(Directory.GetFiles(Application.dataPath, "*.dll", SearchOption.AllDirectories));

        string str = "";
        for (int i = 0, il = assemblies.Count; i < il; i++) {
            str += indent + string.Format("\"{0}\"", assemblies[i]);
            if (i < assemblies.Count - 1) {
                str += ",\n";
            }
        }

        return str;
    }
}