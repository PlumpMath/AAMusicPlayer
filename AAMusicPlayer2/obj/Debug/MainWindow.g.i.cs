﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40D55AF09F6850731880555B0AE57488"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AAMusicPlayer2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Vlc.DotNet.Wpf;


namespace AAMusicPlayer2 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 39 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Vlc.DotNet.Wpf.VlcControl myControl;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label playingLabel;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label timeLabel;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider PositionSlider;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label RemainingLabel;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fullsearch;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox plsearch;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid fulllistgrid;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid playlistgrid;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu MyContextMenu;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider trackWave;
        
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
            System.Uri resourceLocater = new System.Uri("/AAMusicPlayer2;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 6 "..\..\MainWindow.xaml"
            ((AAMusicPlayer2.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.myControl = ((Vlc.DotNet.Wpf.VlcControl)(target));
            return;
            case 3:
            this.playingLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.timeLabel = ((System.Windows.Controls.Label)(target));
            
            #line 43 "..\..\MainWindow.xaml"
            this.timeLabel.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ChangeDisplayTime);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PositionSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 44 "..\..\MainWindow.xaml"
            this.PositionSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.PositionChange);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RemainingLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\MainWindow.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.fullsearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\MainWindow.xaml"
            this.fullsearch.LostFocus += new System.Windows.RoutedEventHandler(this.search_LostFocus);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.fullsearch.GotFocus += new System.Windows.RoutedEventHandler(this.search_GotFocus);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.fullsearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 9:
            this.plsearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\MainWindow.xaml"
            this.plsearch.LostFocus += new System.Windows.RoutedEventHandler(this.search_LostFocus);
            
            #line default
            #line hidden
            
            #line 51 "..\..\MainWindow.xaml"
            this.plsearch.GotFocus += new System.Windows.RoutedEventHandler(this.search_GotFocus);
            
            #line default
            #line hidden
            
            #line 51 "..\..\MainWindow.xaml"
            this.plsearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.PLSearch);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 53 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearS1);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 54 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearS2);
            
            #line default
            #line hidden
            return;
            case 12:
            this.fulllistgrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 14:
            this.playlistgrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 16:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\MainWindow.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 17:
            this.MyContextMenu = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 18:
            
            #line 91 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFile_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 92 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddDir_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 97 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 100 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemSel_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 101 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Crop_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 102 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemAll_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 103 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemDup_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 104 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemMissing_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 105 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PhysicallyRem_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 109 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 112 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectAll_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 113 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SelNone_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 114 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.InvertSel_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 118 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FileInfo_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 119 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 122 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SortDate_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 123 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SortTitle_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 124 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SortArtist_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 125 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SortGenre_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 126 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SortFilename_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 127 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SortReverse_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 131 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 134 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLInsertRepeat_Click);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 135 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLRemoveRepeat_Click);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 136 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLStopAfterCurrent_Click);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 137 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLStopAfterSelected_Click);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 138 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLRemoveStop_Click);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 139 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLDuplicate_Click);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 140 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLFileInfo_Click);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 144 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 147 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLRemDup_Click);
            
            #line default
            #line hidden
            return;
            case 49:
            
            #line 148 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLRemMissing_Click);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 149 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLRemSel_Click);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 150 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLCrop_Click);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 154 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 53:
            
            #line 157 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLMoveAfterCurrent_Click);
            
            #line default
            #line hidden
            return;
            case 54:
            
            #line 158 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLMoveToTop_Click);
            
            #line default
            #line hidden
            return;
            case 55:
            
            #line 159 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLMoveToBottom_Click);
            
            #line default
            #line hidden
            return;
            case 56:
            
            #line 160 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLMoveUp_Click);
            
            #line default
            #line hidden
            return;
            case 57:
            
            #line 161 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLMoveDown_Click);
            
            #line default
            #line hidden
            return;
            case 58:
            
            #line 166 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 59:
            
            #line 169 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLRandom_Click);
            
            #line default
            #line hidden
            return;
            case 60:
            
            #line 170 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLReverse_Click);
            
            #line default
            #line hidden
            return;
            case 61:
            
            #line 171 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLInvertSel_Click);
            
            #line default
            #line hidden
            return;
            case 62:
            
            #line 172 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSelAll_Click);
            
            #line default
            #line hidden
            return;
            case 63:
            
            #line 173 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSelNone_Click);
            
            #line default
            #line hidden
            return;
            case 64:
            
            #line 174 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSortDate_Click);
            
            #line default
            #line hidden
            return;
            case 65:
            
            #line 175 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSortTitle_Click);
            
            #line default
            #line hidden
            return;
            case 66:
            
            #line 176 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSortArtist_Click);
            
            #line default
            #line hidden
            return;
            case 67:
            
            #line 177 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSortGenre_Click);
            
            #line default
            #line hidden
            return;
            case 68:
            
            #line 178 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSortFilename_Click);
            
            #line default
            #line hidden
            return;
            case 69:
            
            #line 182 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCM);
            
            #line default
            #line hidden
            return;
            case 70:
            
            #line 186 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLSaveList_Click);
            
            #line default
            #line hidden
            return;
            case 71:
            
            #line 187 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLLoadList_Click);
            
            #line default
            #line hidden
            return;
            case 72:
            
            #line 188 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PLNewList_Click);
            
            #line default
            #line hidden
            return;
            case 73:
            
            #line 192 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            
            #line 192 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.BackButton2_Click);
            
            #line default
            #line hidden
            
            #line 192 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.BackButton2_Click);
            
            #line default
            #line hidden
            return;
            case 74:
            
            #line 193 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.StopButton_Click);
            
            #line default
            #line hidden
            
            #line 193 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.StopButton2_Click);
            
            #line default
            #line hidden
            
            #line 193 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.StopButton2_Click);
            
            #line default
            #line hidden
            return;
            case 75:
            
            #line 194 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PlayButton_Click);
            
            #line default
            #line hidden
            return;
            case 76:
            
            #line 195 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PauseButton_Click);
            
            #line default
            #line hidden
            return;
            case 77:
            
            #line 196 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.NextButton_Click);
            
            #line default
            #line hidden
            
            #line 196 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.NextButton2_Click);
            
            #line default
            #line hidden
            
            #line 196 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.NextButton2_Click);
            
            #line default
            #line hidden
            return;
            case 78:
            this.trackWave = ((System.Windows.Controls.Slider)(target));
            
            #line 197 "..\..\MainWindow.xaml"
            this.trackWave.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.VolChange);
            
            #line default
            #line hidden
            return;
            case 79:
            
            #line 198 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.ToggleButton)(target)).Click += new System.Windows.RoutedEventHandler(this.Mute_Click);
            
            #line default
            #line hidden
            return;
            case 80:
            
            #line 199 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddToPlaylist2);
            
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
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 13:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 58 "..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.AddToPlayList);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 15:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 74 "..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.PLPlay);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
