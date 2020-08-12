using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using Harmony;
using System.Collections;
using System.Threading;

namespace AudicaModding
{
    public class ModMenu : MelonPlugin
    {

        static public List<ModPage> modPages = new List<ModPage>();
        public static OptionsMenu optionsMenu = null;


        public static class BuildInfo
        {
            public const string Name = "ModMenu";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "Continuum"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "0.1.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }

        public override void OnApplicationStart()
        {
            var i = HarmonyInstance.Create("ModMenu");
            Hooks.ApplyHooks(i);
        }

        //Adds Mods button to Settings page
        static public void AddModMenuButton(int col)
        {
            optionsMenu.AddButton(col, "Mods", new System.Action(() => {
                GoToMainModPage();
            }), null, "See all installed Mods.");
        }

        //Goes to Mod-MainPage
        public static void GoToMainModPage()
        {
            optionsMenu.ShowPage(OptionsMenu.Page.Gameplay);
            CleanUpPage();
            AddModButtons();
            optionsMenu.screenTitle.text = "Mods";
        }

        //Goes to Mod-Page
        public static void GoToModPage(ModPage modPage)
        {
            optionsMenu.ShowPage(OptionsMenu.Page.Gameplay);
            CleanUpPage();
            AddModPageToModMenu(modPage);
            optionsMenu.screenTitle.text = modPage.modName;
        }


        private static void CleanUpPage()
        {
            Transform optionsTransform = optionsMenu.transform;
            for (int i = 0; i < optionsTransform.childCount; i++)
            {
                Transform child = optionsTransform.GetChild(i);
                if (child.gameObject.name.Contains("(Clone)"))
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            optionsMenu.mRows.Clear();
            optionsMenu.scrollable.ClearRows();
            optionsMenu.scrollable.mRows.Clear();
        }

        public static void RegisterModPage(ModPage modPage)
        {
            modPages.Add(modPage);
        }

        private static void AddModButtons()
        {
            int col = 0;
            foreach(ModPage modPage in modPages)
            {
                var omb = optionsMenu.AddButton(col, modPage.modName, new System.Action(() => {
                    GoToModPage(modPage);
                }), null, modPage.modDescription);
                optionsMenu.scrollable.AddRow(omb.gameObject);
                if (col == 0) col = 1;
                else col = 0;
            }
        }

        public static void AddModPageToModMenu(ModPage modPage)
        {
            int buttonCol = 0;
            foreach(EntryType entryType in modPage.entryType)
            {
                switch (entryType)
                {
                    case EntryType.Header:
                        optionsMenu.AddHeader(0, modPage.headers[0]);
                        modPage.headers.RemoveAt(0);
                        buttonCol = 0;
                        break;
                    case EntryType.Button:
                        ModButton b = modPage.buttons[0];
                        OptionsMenuButton omb = optionsMenu.AddButton(buttonCol, b.label, new System.Action(() => {
                            b.onSelectedDelegate.Invoke();
                        }), null, b.helpText);
                        omb.label.text = b.initialButtonText;
                        modPage.buttons.RemoveAt(0);
                        if (buttonCol == 0) buttonCol = 1;
                        else buttonCol = 0;
                        break;
                    case EntryType.Slider:
                        ModSlider s = modPage.sliders[0];
                        OptionsMenuSlider oms = optionsMenu.AddSlider(buttonCol, s.label, s.numberFormat, s.onAdjustAction, null);
                        oms.label.text = s.initialSliderText;
                        modPage.sliders.RemoveAt(0);
                        if (buttonCol == 0) buttonCol = 1;
                        else buttonCol = 0;
                        break;
                    default:
                        break;
                }

            }
        }

        public class ModPage
        {
            public string modName;
            public string modDescription;
            public ModPage(string _modName, string _modDescription)
            {
                modName = _modName;
                modDescription = _modDescription;
                
            }

            public List<string> headers = new List<string>();
            public List<ModButton> buttons = new List<ModButton>();
            public List<ModSlider> sliders = new List<ModSlider>();
            public List<EntryType> entryType = new List<EntryType>();

            public void AddHeader(string header)
            {
                headers.Add(header);
                entryType.Add(EntryType.Header);                
            }

            public void AddButton(ModButton button)
            {
                buttons.Add(button);
                entryType.Add(EntryType.Button);
            }

            public void AddSlider(ModSlider slider)
            {
                sliders.Add(slider);
                entryType.Add(EntryType.Slider);
            }         
        }

        public delegate void OnSelectedDelegate();

        public class ModButton
        {
            public string initialButtonText;
            public string label;
            public string helpText;
            //public Action onSelectedAction;
            
            public OnSelectedDelegate onSelectedDelegate;

            public ModButton(string _initialButtonText, string _label, string _helpText, OnSelectedDelegate _onSelectedDelegate)
            {
                initialButtonText = _initialButtonText;
                label = _label;
                helpText = _helpText;
                //onSelectedAction = _onSelectedAction;
                onSelectedDelegate = _onSelectedDelegate;
            }
        }

        public class ModSlider
        {
            public string initialSliderText;
            public string label;
            public string numberFormat;
            public Action<float> onAdjustAction;

            public ModSlider(string _initialSliderText, string _label, string _numberFormat, Action<float> _onAdjustAction)
            {
                initialSliderText = _initialSliderText;
                label = _label;
                numberFormat = _numberFormat;
                onAdjustAction = _onAdjustAction;
            }
        }

        public enum EntryType
        {
            Header,
            Button,
            Slider
        }
    }
            
    }

  












