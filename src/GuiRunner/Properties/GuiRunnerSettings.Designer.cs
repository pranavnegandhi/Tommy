﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Notadesigner.Pomodour.App.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
    internal sealed partial class GuiRunnerSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static GuiRunnerSettings defaultInstance = ((GuiRunnerSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GuiRunnerSettings())));
        
        public static GuiRunnerSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AutoAdvance {
            get {
                return ((bool)(this["AutoAdvance"]));
            }
            set {
                this["AutoAdvance"] = value;
            }
        }
    }
}
