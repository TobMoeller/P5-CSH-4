﻿#pragma checksum "..\..\DbPropertys.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7E1083611EB0D4CB2F6FF172EA8B48747B0C607C5C0F109A24A6D9E711D89D06"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using P5_CSH_4;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace P5_CSH_4 {
    
    
    /// <summary>
    /// DbPropertys
    /// </summary>
    public partial class DbPropertys : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbServer;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbServer;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbDatabase;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbDatabase;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbUid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbUid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbPwd;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPwd;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtConfirm;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\DbPropertys.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/P5-CSH-4;component/dbpropertys.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DbPropertys.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LbServer = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.TbServer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.LbDatabase = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.TbDatabase = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.LbUid = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TbUid = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\DbPropertys.xaml"
            this.TbUid.KeyDown += new System.Windows.Input.KeyEventHandler(this.BtEnter);
            
            #line default
            #line hidden
            return;
            case 7:
            this.LbPwd = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.TbPwd = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\DbPropertys.xaml"
            this.TbPwd.KeyDown += new System.Windows.Input.KeyEventHandler(this.BtEnter);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\DbPropertys.xaml"
            this.BtConfirm.Click += new System.Windows.RoutedEventHandler(this.BtConfirm_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.BtCancel = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\DbPropertys.xaml"
            this.BtCancel.Click += new System.Windows.RoutedEventHandler(this.BtCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

