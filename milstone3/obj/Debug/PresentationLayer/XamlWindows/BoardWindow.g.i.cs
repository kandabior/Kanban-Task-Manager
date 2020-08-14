﻿#pragma checksum "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E4CEB57E3349ECC22C54EBF34A5AFBC20B413E85"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using milstone3;


namespace milstone3 {
    
    
    /// <summary>
    /// BoardWindow
    /// </summary>
    public partial class BoardWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BoardGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTerm;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewColumnTitle;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid columnOrder;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteColumn;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetMaxTask;
        
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
            System.Uri resourceLocater = new System.Uri("/milstone3;component/presentationlayer/xamlwindows/boardwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
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
            this.BoardGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 14 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            this.BoardGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 16 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Edit_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTask_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 18 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddColumn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SearchTerm = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.NewColumnTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 22 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 23 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Change_State_Next);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 24 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Change_State_Back);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 25 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Change_State);
            
            #line default
            #line hidden
            return;
            case 12:
            this.columnOrder = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 13:
            this.DeleteColumn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            this.DeleteColumn.Click += new System.Windows.RoutedEventHandler(this.DeleteColumn_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.SetMaxTask = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            this.SetMaxTask.Click += new System.Windows.RoutedEventHandler(this.SetMaxTask_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 33 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 34 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 35 "..\..\..\..\PresentationLayer\XamlWindows\BoardWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

