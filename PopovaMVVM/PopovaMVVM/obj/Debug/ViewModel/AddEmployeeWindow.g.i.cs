﻿#pragma checksum "..\..\..\ViewModel\AddEmployeeWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0EE27631FF75CDEF619BBB7F5BFADF29BE70AE691F554EF84688148BB2F5AD47"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using PopovaMVVM.ViewModel;
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


namespace PopovaMVVM.ViewModel {
    
    
    /// <summary>
    /// AddEmployeeWindow
    /// </summary>
    public partial class AddEmployeeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 32 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LastNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FirstNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MiddleNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GenderComboBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker BirthDatePicker;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker AppointmentDatePicker;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SalaryTextBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PositionComboBox;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddChildButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ChildrenDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/PopovaMVVM;component/viewmodel/addemployeewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
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
            this.LastNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.FirstNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.MiddleNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.GenderComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.BirthDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.AppointmentDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.SalaryTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.PositionComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.AddChildButton = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
            this.AddChildButton.Click += new System.Windows.RoutedEventHandler(this.AddChildButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ChildrenDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 12:
            
            #line 94 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 95 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 11:
            
            #line 83 "..\..\..\ViewModel\AddEmployeeWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteChildButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

