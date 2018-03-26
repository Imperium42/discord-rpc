using UnityEditor;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Modification of "BuildHelper.cs" by Xblade#4242 ( https://discord.gg/tol )
/// </summary>
[InitializeOnLoad]
public class DiscordBuildHelper
{
    static DiscordBuildHelper()
    {
        EnsureDLL();
    }

    public static bool FileExists(string filename)
    {
        return new FileInfo(filename).Exists;
    }

    /// <summary>
    /// This would imply that the editor machine already has:
    /// 'python', 'pip' and pip's 'click' mod installed - 
    /// and have the script work (I haven't had it work yet)... no thanks.
    /// I removed this call here, anyway. Keeping this here in case someone wants to reference it.
    /// </summary>
    /// <returns></returns>
    //    public static bool RunRpcBuildScript()
    //    {
    //        UnityEngine.Debug.Log("[DiscordBuild] Try to run build script");

    //        Process proc = new Process();
    //#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    //		proc.StartInfo.UseShellExecute = false;
    //		// brew installs cmake in /usr/local/bin, which Unity seems to strip from PATH?
    //		string newPath = proc.StartInfo.EnvironmentVariables["PATH"] + ":/usr/local/bin";
    //		proc.StartInfo.EnvironmentVariables["PATH"] = newPath;
    //#endif
    //        proc.StartInfo.FileName = "python";
    //        proc.StartInfo.Arguments = "build.py unity";
    //        proc.StartInfo.WorkingDirectory = "../..";
    //        proc.Start();
    //        proc.WaitForExit();
    //        return proc.ExitCode == 0;
    //    }

    /// <summary>
    /// https://github.com/discordapp/discord-rpc/blob/master/examples/button-clicker/Assets/Editor/BuildHelper.cs
    /// https://github.com/discordapp/discord-rpc/releases
    /// </summary>
    public static void EnsureDLL()
    {
        UnityEngine.Debug.Log("[DiscordBuild] Checking for DLLs...");

        // ######################################
        // ARCHITECTURE >>
        // /Discord-Include/
        // /Assets/Discord/DiscordRpc.cs
        // /Assets/Discord/Example/main.unity
        // /Assets/Plugins/x86_64/discord-rpc.dll
        // ######################################

        string projectRoot = string.Format("{0}/..", UnityEngine.Application.dataPath);             // +1 up from Assets (no trailing slash/)

        string dirInclude = string.Format("{0}/Discord-Include", projectRoot);                      // Windows: <path to project root dir> (no trailing slash/)
        string dllIncludeWin = string.Format("{0}/win64-dynamic/discord-rpc.dll", dirInclude);      // Seems like this 1 dll works for both 64 AND 32-bit

        string dirAssets = UnityEngine.Application.dataPath;                                        // Windows: <path to project folder>/Assets (no trailing slash/)
        string dirAssetsPlugins = string.Format("{0}/Plugins", dirAssets);                          // @ /Assets/Plugins
        string dirAssetsPlugins_x86 = string.Format("{0}/x86", dirAssetsPlugins);                   // @ /Assets/Plugins/x86
        string dirAssetsPlugins_x86_64 = string.Format("{0}/x86_64", dirAssetsPlugins);             // @ /Assets/Plugins/x86_64

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        // ###############################################################################################################################################
        // (ORIGINAL)
        //string[] dstDirs = { "/Plugins", "../Plugins/x86", "Assets/Plugins/x86_64" }; // Same as linux
        //string[] dstDlls = { "../Plugins/x86/discord-rpc.dll", "../Plugins/x86_64/discord-rpc.dll" };
        //string[] srcDlls = { "../../builds/install/win64-dynamic/bin/discord-rpc.dll", "../../builds/install/win64-dynamic/bin/discord-rpc.dll" };
        // ###############################################################################################################################################

        string dirPlugins_x86 = string.Format("{0}/discord-rpc.dll", dirAssetsPlugins_x86);       // @ /Assets/Plugins/x86/discord-rpc.dll
        string dirPlugins_x86_64 = string.Format("{0}/discord-rpc.dll", dirAssetsPlugins_x86_64); // @ /Assets/Plugins/x86_64/discord-rpc.dll

        string[] dstDirs = { dirAssetsPlugins, dirAssetsPlugins_x86, dirAssetsPlugins_x86_64 };   // @ /Assets/Plugins , @ /Assets/Plugins/x86 , @ /Assets/Plugins/x86_64
        string[] dstDlls = { dirAssetsPlugins_x86, dirAssetsPlugins_x86_64 };                     // @ /Assets/Plugins/x86/discord-rpc.dll , @ /Assets/Plugins/x86_64/discord-rpc.dll
        string[] srcDlls = { dllIncludeWin, dllIncludeWin }; // Same single file for both baths   // @ /Discord-Include/win64-dynamic/

        UnityEngine.Debug.LogFormat("[Discord] Placing DLLs @ '{0}' && '{1}'", srcDlls[0], srcDlls[1]);

#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        // Mac-specific [WIP]
        // ###############################################################################################################################################
        // (ORIGINAL)
        //string[] dstDirs = { "Assets/Plugins" };
        //string[] dstDlls = { "Assets/Plugins/discord-rpc.bundle" };
        //string[] srcDlls = { "../../builds/install/osx-dynamic/lib/libdiscord-rpc.dylib" };
        // ###############################################################################################################################################

        string bundleAssetsPlugins = string.Format("{0}/discord-rpc.bundle", dirAssetsPlugins);   // @ /Assets/Plugins/discord-rpc.bundle
        string dylibIncludeMac = string.Format("{0}/mac-dynamic/libdiscord-rpc.dylib",            // @ /Discord-Include/mac-dynamic/libdiscord-rpc.dylib
            dirInclude);                                                                          // Seems like this 1 dll works for JUST 64 AND 32-bit. Universal...?
        
        string[] dstDirs = { dirAssetsPlugins };      // @ /Assets/Plugins
        string[] dstDlls = { bundleAssetsPlugins };   // @ /Assets/Plugins/discord-rpc.bundle
        string[] srcDlls = { dylibIncludeMac };       // @ /Assets/Discord-Include/mac-dyhnamic/libdiscord-rpc.dylib

        UnityEngine.Debug.Log("[Discord] Placing DLLs @ " + srcDlls[0]);

#else
        // Linux-specific? [WIP]
        // ###############################################################################################################################################
        // (ORIGINAL)
        //string[] dstDirs = { "Assets/Plugins", "Assets/Plugins/x86", "Assets/Plugins/x86_64" }; // Same as win
        //string[] dstDlls = { "Assets/Plugins/x86/discord-rpc.so", "Assets/Plugins/x86_64/discord-rpc.so" };
        //string[] srcDlls = { "../../builds/install/linux-dynamic/bin/discord-rpc.dll", "../../builds/install/win64-dynamic/bin/discord-rpc.dll" };
        // ###############################################################################################################################################

        string soPluginsLinux_x86 = string.Format("{0}/discord-rpc.so", dirAssetsPlugins_x86);          // @ /Assets/Plugins/x86/discord-rpc.so
        string soPluginsLinux_x86_64 = string.Format("{0}/discord-rpc.so", dirAssetsPlugins_x86_64);    // @ /Assets/Plugins/x86_64/discord-rpc.so

        // -----------------------------------------------------------------------------------------------
        // Src file/dir is actually missing from current release. Testing/guessing the file:
        // https://github.com/discordapp/discord-rpc/issues/157
        // Now, eventually you can see the original dstDll path tried to rename it to discord-rpc.so ,
        // So (no pun intended), does this mean that this libdiscord-rpc.so is THE file we're looking for?
        // -----------------------------------------------------------------------------------------------
        string soIncludeLinux = string.Format("{0}/linux-dynamic/libdiscord-rpc.so", dirInclude);       // Seems that this is ONLY the 32-bit one?

        string[] dstDirs = { dirAssetsPlugins, dirAssetsPlugins_x86, dirAssetsPlugins_x86_64 };         // @ /Assets/Plugins , /Assets/Plugins/x86 , /Assets/Plugins/x86_64
        string[] dstDlls = { soPluginsLinux_x86, soPluginsLinux_x86_64 };                               // @ /Assets/Plugins/x86_64/discord-rpc.so , /Assets/Plugins/
        string[] srcDlls = { soIncludeLinux, soIncludeLinux };                                          // Seems the 64-bit one is actually.. the WINDOWS one??
                                                                                                        // Is this right? https://github.com/discordapp/discord-rpc/issues/157
                                                                                                        // EDIT: I don't think it's right. I think it's the same Linux one for both. No way it's Windows.

        UnityEngine.Debug.LogFormat("[Discord] Placing DLLs @ '{0}' && '{1}', srcDlls[0], srcDlls[1]);
#endif

        //Debug.Assert(dstDlls.Length == srcDlls.Length);

        //bool exists = true;
        //foreach (string fname in dstDlls)
        //{
        //    if (!FileExists(fname))
        //    {
        //        exists = false;
        //        break;
        //    }
        //}

        //if (exists)
        //{
        //    UnityEngine.Debug.Log("[DiscordBuild] DLL check successfully passed.");
        //    return; // Success
        //}

        //// Fail >>
        //UnityEngine.Debug.LogError("[DiscordBuild] Cannot find DLLs - for Windows, for example, " +
        //    "place 'discord-rpc.dll' @ '/Assets/Plugins/x86_64/'");

        //// make sure the dll plugins dirs exist
        //foreach (string dirname in dstDirs)
        //{
        //    Directory.CreateDirectory(dirname);
        //}
    }
}