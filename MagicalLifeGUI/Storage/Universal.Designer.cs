﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MagicalLifeGUI.Storage {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.5.0.0")]
    public sealed partial class Universal : global::System.Configuration.ApplicationSettingsBase {
        
        private static Universal defaultInstance = ((Universal)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Universal())));
        
        public static Universal Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool GameHasRunBefore {
            get {
                return ((bool)(this["GameHasRunBefore"]));
            }
            set {
                this["GameHasRunBefore"] = value;
            }
        }
    }
}