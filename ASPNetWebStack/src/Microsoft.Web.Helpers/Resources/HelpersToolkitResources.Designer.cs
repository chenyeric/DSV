﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class HelpersToolkitResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HelpersToolkitResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Web.Helpers.Resources.HelpersToolkitResources", typeof(HelpersToolkitResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Search Site.
        /// </summary>
        internal static string BingSearch_DefaultSiteSearchText {
            get {
                return ResourceManager.GetString("BingSearch_DefaultSiteSearchText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Search Web.
        /// </summary>
        internal static string BingSearch_DefaultWebSearchText {
            get {
                return ResourceManager.GetString("BingSearch_DefaultWebSearchText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add more files.
        /// </summary>
        internal static string FileUpload_AddMore {
            get {
                return ResourceManager.GetString("FileUpload_AddMore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Upload.
        /// </summary>
        internal static string FileUpload_Upload {
            get {
                return ResourceManager.GetString("FileUpload_Upload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Gravatar image size must be between 1 and 512 pixels..
        /// </summary>
        internal static string Gravatar_InvalidImageSize {
            get {
                return ResourceManager.GetString("Gravatar_InvalidImageSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to LinkShare value {0} is not supported by the Link Share helper. .
        /// </summary>
        internal static string LinkShareValue_NotSupported {
            get {
                return ResourceManager.GetString("LinkShareValue_NotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The captcha cannot be validated because the remote address was not found in the request..
        /// </summary>
        internal static string ReCaptcha_RemoteIPNotFound {
            get {
                return ResourceManager.GetString("ReCaptcha_RemoteIPNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot have a null folder. Try using Themes.GetResourcePath(string fileName) instead if you do not want to specify a folder..
        /// </summary>
        internal static string Themes_FolderCannotBeNull {
            get {
                return ResourceManager.GetString("Themes_FolderCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown theme &apos;{0}&apos;. Ensure that a directory labeled &apos;{0}&apos; exists under the theme directory..
        /// </summary>
        internal static string Themes_InvalidTheme {
            get {
                return ResourceManager.GetString("Themes_InvalidTheme", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must call the &quot;Themes.Initialize&quot; method before you call any other method of the &quot;Themes&quot; class..
        /// </summary>
        internal static string Themes_NotInitialized {
            get {
                return ResourceManager.GetString("Themes_NotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The media file &quot;{0}&quot; does not exist..
        /// </summary>
        internal static string Video_FileDoesNotExist {
            get {
                return ResourceManager.GetString("Video_FileDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &quot;{0}&quot; cannot be set through this argument..
        /// </summary>
        internal static string Video_PropertyCannotBeSet {
            get {
                return ResourceManager.GetString("Video_PropertyCannotBeSet", resourceCulture);
            }
        }
    }
}
