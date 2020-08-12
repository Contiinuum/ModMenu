using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;
using AudicaModding;

[assembly: AssemblyTitle(ModMenu.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(ModMenu.BuildInfo.Company)]
[assembly: AssemblyProduct(ModMenu.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + ModMenu.BuildInfo.Author)]
[assembly: AssemblyTrademark(ModMenu.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(ModMenu.BuildInfo.Version)]
[assembly: AssemblyFileVersion(ModMenu.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonPluginInfo(typeof(ModMenu), ModMenu.BuildInfo.Name, ModMenu.BuildInfo.Version, ModMenu.BuildInfo.Author, ModMenu.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonPluginGame(null, null)]