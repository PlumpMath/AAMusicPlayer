
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using TagLib;
using Vlc.DotNet.Core;

namespace AAMusicPlayer2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //imports and methods for volume control
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        //variable used to store the current volume using stop with fadeout
        double startvol;
        //counter to tell if you're pressing or holding the stop/forward/back buttons
        int bbcount = 0;
        //variables for mute extension
        IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
        int spkrComponent = MM.MIXERLINE_COMPONENTTYPE_DST_SPEAKERS;
        const int spkrMuteControl = MM.MIXERCONTROL_CONTROLTYPE_MUTE;
        //this variable may be backwards but keeps track of whether the mute is on or not.  I know if you mute/unmute it through system volume control this will be out of sync
        bool muted = true;
        //this variable keeps track of how many files are remaining in the tasks to be added
        int remaining = 0;
        //variable for randomzie method
        private static Random rng = new Random();
        //cancellation tokens and task for cancelling adding songs
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct;
        Task task1;
        //make dataset lists available
        savedlistsEntities1 DBContext = new savedlistsEntities1();
        ObservableCollection<AAList> AALists;
        ObservableCollection<AAPList> AAPLists;
        //don't handle event triggered by TestOnTimeChanged method
        bool ignoreevent = false;
        //displayoptions for the counter
        enum DisplayOptions
        {
            Remaining,
            Elapsed,
            ElapsedAndTotal
        };
        DisplayOptions displayOption = DisplayOptions.ElapsedAndTotal;
        //value in AAPList for stop after and return to start after
        enum After
        {
            Repeat = 0,
            Stop = 1
        }
        //global variable for currently playing object
        AAPList curplay;
        //trying something
        public CollectionViewSource ViewSource;
        public CollectionViewSource ViewSource2;
        //global properties just to set bold on currently playing file
        Setter bold = new Setter(TextBlock.FontWeightProperty, FontWeights.Bold, null);
        Setter normal = new Setter(TextBlock.FontWeightProperty, FontWeights.Normal, null);
        public MainWindow()
        {
            InitializeComponent();

            myControl.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            myControl.MediaPlayer.EndInit();
            //cancellationtoken for the add methods running from a separate thread
            ct = cts.Token;
            //unmute, incase it's muted on startup
            int muteStatus = 0;
            MM.SetVolume(spkrMuteControl, spkrComponent, muteStatus);
            //set volume
            uint CurrVol = 0;
            // At this point, CurrVol gets assigned the volume
            waveOutGetVolume(IntPtr.Zero, out CurrVol);
            // Calculate the volume
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            // Get the volume on a scale of 1 to 10 (to fit the trackbar)
            trackWave.Value = CalcVol / (ushort.MaxValue / 10);
            //load DB.  The first time is to replace a Load() method that I don't have.  The secon time is because if you don't it will add the first song to the top of the list
            ViewSource = new CollectionViewSource();
            ViewSource2 = new CollectionViewSource();
            ViewSource.Source = DBContext.AALists.ToList();
            ViewSource2.Source = DBContext.AAPLists.ToList();
            ViewSource.Source = DBContext.AALists.Local;
            ViewSource2.Source = DBContext.AAPLists.Local;
            fulllistgrid.ItemsSource = ViewSource.View;
            playlistgrid.ItemsSource = ViewSource2.View;
            //just shortcuts
            AALists = DBContext.AALists.Local;
            AAPLists = DBContext.AAPLists.Local;
            //add timechanged event to bind progress
            myControl.MediaPlayer.TimeChanged += TestOnTimeChanged;
            //add media changed event
            myControl.MediaPlayer.MediaChanged += MediaChanged;
            //media finished event
            myControl.MediaPlayer.EndReached += TrackDone;
            ViewSource2.SortDescriptions.Add(new SortDescription("order", ListSortDirection.Ascending));
            ViewSource2.View.Refresh();
            ViewSource.Filter += new FilterEventHandler(SearchFilter);
            ViewSource2.Filter += new FilterEventHandler(SearchFilter2);

        }
        private void SearchFilter(object sender, FilterEventArgs e)
        {
            AAList item = e.Item as AAList;
            if (item != null)
            {
                // Filter out products with price 25 or above
                if (item.displayName.ToUpper().Contains(fullsearch.Text.ToUpper()) || fullsearch.Text == "" || fullsearch.Text == "Search...")
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
        private void SearchFilter2(object sender, FilterEventArgs e)
        {
            AAPList item = e.Item as AAPList;
            if (item != null)
            {
                // Filter out products with price 25 or above
                if (item.displayName.ToUpper().Contains(plsearch.Text.ToUpper()) || plsearch.Text == "" || plsearch.Text == "Search...")
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
        private void TrackDone(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            bbcount = 1;
            NextButton2_Click();
        }
        private void TestOnTimeChanged(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {
            PositionSlider.Dispatcher.BeginInvoke(new Action(() => { ignoreevent = true; PositionSlider.Value = myControl.MediaPlayer.Position * 10; ignoreevent = false; 
            TimeSpan time = new TimeSpan(myControl.MediaPlayer.Time *10000);
            TimeSpan dur = myControl.MediaPlayer.GetCurrentMedia().Duration;
            TimeSpan rem = dur - time;
            switch (displayOption) {
                case DisplayOptions.Elapsed: timeLabel.Content = String.Format("{0:mm\\:ss}", time); break;
                case DisplayOptions.Remaining: timeLabel.Content = String.Format("{0:mm\\:ss}", rem); break;
                case DisplayOptions.ElapsedAndTotal: timeLabel.Content = String.Format("{0:mm\\:ss}/{1:mm\\:ss}", time, dur); break;
            }
            }));
        }
        private void MediaChanged(object sender, VlcMediaPlayerMediaChangedEventArgs e)
        {
            PositionSlider.Dispatcher.BeginInvoke(new Action(() => { PositionSlider.Value = myControl.MediaPlayer.Position * 10; ignoreevent = false; playingLabel.Content = myControl.MediaPlayer.GetCurrentMedia().Title; }));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (curplay == null)
            {
                if (AAPLists.Count != 0) curplay = AAPLists.OrderBy(o => o.order).First();
                else return;
            }
            Play();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            myControl.MediaPlayer.Pause();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            bbcount += 1;
            if (bbcount == 2)
            {//stop with fadeout
                if (myControl.MediaPlayer.IsPlaying)
                {
                    System.Timers.Timer aTimer = new System.Timers.Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(volDown);
                    aTimer.Interval = 50;
                    aTimer.Enabled = true;
                    aTimer.AutoReset = false;
                    startvol = trackWave.Value;
                }
            }
        }
        private void volDown(object source, ElapsedEventArgs e)
        {
            trackWave.Dispatcher.BeginInvoke(new Action(() => { trackWave.Value = trackWave.Value- 0.1;
            if (trackWave.Value < 0.3) { RowBold(); myControl.MediaPlayer.Stop(); trackWave.Value = startvol; timeLabel.Content = ""; playingLabel.Content = ""; }
            else
            {
                System.Timers.Timer cTimer = new System.Timers.Timer();
                cTimer.Elapsed += new ElapsedEventHandler(volDown);
                cTimer.Interval = 50;
                cTimer.Enabled = true;
                cTimer.AutoReset = false;
            }
            }));
        }
        private void StopButton2_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (bbcount == 1) { if (myControl.MediaPlayer.IsPlaying) RowBold(); myControl.MediaPlayer.Stop();
            playingLabel.Dispatcher.BeginInvoke(new Action(() => { playingLabel.Content = ""; timeLabel.Content = ""; }));
            }
            bbcount = 0;
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            bbcount += 1;
            if (bbcount > 1 && myControl.MediaPlayer.IsPlaying) myControl.MediaPlayer.Time = myControl.MediaPlayer.Time + 1500;
        }
        private void NextButton2_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (bbcount == 1)
            {
                if (curplay != null)
                {
                    List<AAPList> sortedlist = new List<AAPList>(AAPLists.OrderBy(o => o.order));
                    if (curplay.after == (int)After.Stop) { RowBold(); return; }
                    else if (curplay.after == (int)After.Repeat) { RowBold(); curplay = sortedlist.First(); if (myControl.MediaPlayer.IsPlaying) Play(); }
                    else if (sortedlist.IndexOf(curplay) == sortedlist.Count - 1) { RowBold(); curplay = null; bbcount = 1; StopButton2_Click(); }
                    else { RowBold();  curplay = sortedlist.ElementAt(sortedlist.IndexOf(curplay) + 1); if (myControl.MediaPlayer.IsPlaying || sender == null) Play(); }
                }
            }
            bbcount = 0;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            bbcount += 1;
            if (bbcount > 1 && myControl.MediaPlayer.Time > 499 && myControl.MediaPlayer.IsPlaying) myControl.MediaPlayer.Time = myControl.MediaPlayer.Time - 1500;
        }
        private void BackButton2_Click(object sender, RoutedEventArgs e)
        {
            if (bbcount == 1)
            {
                if (curplay != null) {
                    List<AAPList> sortedlist = new List<AAPList>(AAPLists.OrderBy(o => o.order));
                    if ((sortedlist.IndexOf(curplay) == 0 || myControl.MediaPlayer.Time > 10000) && (myControl.MediaPlayer.IsPlaying)) Play();
                    else { RowBold();  curplay = sortedlist.ElementAt(sortedlist.IndexOf(curplay) - 1); if (myControl.MediaPlayer.IsPlaying) Play(); }
                }
            }
            bbcount = 0;
        }
        private void ClearS1(object sender, RoutedEventArgs e)
        {
            fullsearch.Text = "Search...";
        }
        private void ClearS2(object sender, RoutedEventArgs e)
        {
            plsearch.Text = "Search...";
        }
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension
            dlg.Multiselect = true;
            dlg.Filter = "Audio Files|*.mp3;*.m4a;*.flac;*.wav;*.wma,*.ogg|All Files (*.*)|*.*";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                remaining += dlg.FileNames.Length;
                if (task1 == null || task1.IsCompleted)
                {
                    cts = new CancellationTokenSource();
                    ct = cts.Token;
                    task1 = new Task(() => taskMethod(ct, dlg.FileNames));
                    task1.Start();
                }
                else task1 = task1.ContinueWith((antecedant) => taskMethod(ct, dlg.FileNames));
                CancelButton.Visibility = System.Windows.Visibility.Visible;
                //when task is complete, save changes to DB, hide cancel button and remaining label
                task1.ContinueWith((antecedant) => { if (task1.IsCompleted) { this.CancelButton.Visibility = System.Windows.Visibility.Hidden; RemainingLabel.Visibility = System.Windows.Visibility.Hidden; DBContext.SaveChanges(); } }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        //this is the method run from the task
        private void taskMethod(CancellationToken ct, string[] fileNames)
        {
            foreach (string filename in fileNames) { if (!ct.IsCancellationRequested) { AddFile(filename); remaining--; } else { remaining = 0; break; } }
        }
        private void AddDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = Directory.EnumerateFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp3") || s.EndsWith(".m4a") || s.EndsWith(".wav") || s.EndsWith(".flac") || s.EndsWith(".wma") || s.EndsWith(".ogg")).ToArray();
                remaining += files.Length;
                if (task1 == null || task1.IsCompleted)
                {
                    cts = new CancellationTokenSource();
                    ct = cts.Token;
                    task1 = new Task(() => taskMethod(ct, files));
                    task1.Start();
                }
                else task1 = task1.ContinueWith((antecedant) => taskMethod(ct, files));
                CancelButton.Visibility = System.Windows.Visibility.Visible;
                //when task is complete, save changes to DB, hide cancel button and remaining label
                task1.ContinueWith((antecedant) => { if (task1.IsCompleted) { this.CancelButton.Visibility = System.Windows.Visibility.Hidden; RemainingLabel.Visibility = System.Windows.Visibility.Hidden; DBContext.SaveChanges(); } }, TaskScheduler.FromCurrentSynchronizationContext());
            }

        }
        private void RemSel_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> templist = new List<AAList>();
            foreach (AAList item in fulllistgrid.SelectedItems.Cast<AAList>()) templist.Add(item);
            foreach (AAList item in templist) AALists.Remove(item);
            templist = null;
            Save();
        }
        private void Crop_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> templist = new List<AAList>();
            var count = fulllistgrid.Items.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridRow row = (DataGridRow)fulllistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && !row.IsSelected) templist.Add((AAList)row.Item);
            }
            foreach (AAList item in templist) AALists.Remove(item);
            templist = null;
            Save();
        }
        private void RemAll_Click(object sender, RoutedEventArgs e)
        {
            AALists.Clear();
            Save();
        }
        private void RemDup_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> SortedList = AALists.OrderBy(o => o.fileAndPath).ToList();
            List<AAList> templist = new List<AAList>();
            for (int i = 1; i < SortedList.Count; i++)
            {
                if (SortedList.ElementAt(i).fileAndPath == SortedList.ElementAt(i - 1).fileAndPath)
                {
                    templist.Add(SortedList.ElementAt(i));
                }
            }
            foreach (AAList item in templist) AALists.Remove(item);
            templist = null;
            SortedList = null;
            Save();
        }
        private void RemMissing_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> templist = new List<AAList>();
            foreach (AAList item in AALists)
            {
                if (!System.IO.File.Exists(item.fileAndPath)) templist.Add(item);
            }
            foreach (AAList item in templist) AALists.Remove(item);
            templist = null;
            Save();
        }
        private void PhysicallyRem_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> templist = new List<AAList>();
            if (System.Windows.MessageBox.Show("Are you sure you want to physically remove these files from your harddrive?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (AAList item in fulllistgrid.SelectedItems.Cast<AAList>())
                {
                    try
                    {
                        System.IO.File.SetAttributes(item.fileAndPath, FileAttributes.Normal);
                        System.IO.File.Delete(item.fileAndPath);
                    }
                    catch (Exception ex) { System.Windows.MessageBox.Show("Error: " + ex.Message); }
                    templist.Add(item);
                }
            }
            foreach (AAList item in templist) AALists.Remove(item);
            templist = null;
            Save();
        }
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            fulllistgrid.SelectAll();
        }
        private void SelNone_Click(object sender, RoutedEventArgs e)
        {
            fulllistgrid.UnselectAll();
        }
        private void InvertSel_Click(object sender, RoutedEventArgs e)
        {
            var count = fulllistgrid.Items.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridRow row = (DataGridRow)fulllistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null) row.IsSelected = !row.IsSelected;
            }
        }
        private void FileInfo_Click(object sender, RoutedEventArgs e)
        {
            AAList item = fulllistgrid.SelectedItems.Cast<AAList>().First();
            if (item != null)
            {
                System.Windows.MessageBox.Show(
                    "Filename: " + item.filename + Environment.NewLine + 
                    "Path: " + item.path + Environment.NewLine + 
                    "Artist: " + item.artist + Environment.NewLine + 
                    "Title: " + item.title + Environment.NewLine + 
                    "Album: " + item.album + Environment.NewLine + 
                    "Genre: " + item.genre + Environment.NewLine
                    );
            }
        }
        private void PLFileInfo_Click(object sender, RoutedEventArgs e)
        {
            AAPList item = playlistgrid.SelectedItems.Cast<AAPList>().First();
            if (item != null)
            {
                System.Windows.MessageBox.Show(
                    "Filename: " + item.filename + Environment.NewLine +
                    "Path: " + item.path + Environment.NewLine +
                    "Artist: " + item.artist + Environment.NewLine +
                    "Title: " + item.title + Environment.NewLine +
                    "Album: " + item.album + Environment.NewLine +
                    "Genre: " + item.genre + Environment.NewLine
                    );
            }
        }
        //sort methods start here
        private void PLMoveToTop_Click(object sender, RoutedEventArgs e)
        {
            plsearch.Text = "Search...";
            int count = playlistgrid.Items.Count;
            int newcount = 0;
            for (int i = 0; i < count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && row.IsSelected)
                {
                    ((AAPList)row.Item).order = newcount++;
                }
            }
            for (int i = 0; i < count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && !row.IsSelected)
                {
                    ((AAPList)row.Item).order = newcount++;
                }
            }
            Save();
        }
        private void PLMoveToBottom_Click(object sender, RoutedEventArgs e)
        {
            plsearch.Text = "Search...";
            int count = playlistgrid.Items.Count;
            int newcount = 0;
            for (int i = 0; i < count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && !row.IsSelected)
                {
                    ((AAPList)row.Item).order = newcount++;
                }
            }
            for (int i = 0; i < count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && row.IsSelected)
                {
                    ((AAPList)row.Item).order = newcount++;
                }
            }
            Save();
        }
        private void PLMoveUp_Click(object sender, RoutedEventArgs e)
        {
            plsearch.Text = "Search...";
            var count = playlistgrid.Items.Count;
            for (int i = 1; i < count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && row.IsSelected)
                {
                    ((AAPList)row.Item).order--;
                    DataGridRow prevrow = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i - 1);
                    ((AAPList)prevrow.Item).order++;
                }
            }
            Save();
        }
        private void PLMoveDown_Click(object sender, RoutedEventArgs e)
        {
            plsearch.Text = "Search...";
            var count = playlistgrid.Items.Count;
            for (int i = 0; i < count - 1; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null && row.IsSelected)
                {
                    ((AAPList)row.Item).order++;
                    DataGridRow nextrow = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i + 1);
                    ((AAPList)nextrow.Item).order--;
                }
            }
            Save();
        }
        private void PLRandom_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> randomlist = new List<AAPList>(AAPLists);
            Shuffle<AAPList>(randomlist);
            int neworder = 0;
            foreach (AAPList item in randomlist) item.order = neworder++;
            Save();
        }
        private void SortDate_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> newlist = new List<AAList>(AALists);
            int neworder = 0;
            foreach (AAList item in newlist.OrderBy(x => x.dateAdded)) item.order = neworder++;
            Save();
        }
        private void SortTitle_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> newlist = new List<AAList>(AALists);
            int neworder = 0;
            foreach (AAList item in newlist.OrderBy(x => x.title)) item.order = neworder++;
            Save();
        }
        private void SortArtist_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> newlist = new List<AAList>(AALists);
            int neworder = 0;
            foreach (AAList item in newlist.OrderBy(x => x.artist)) item.order = neworder++;
            Save();
        }
        private void SortGenre_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> newlist = new List<AAList>(AALists);
            int neworder = 0;
            foreach (AAList item in newlist.OrderBy(x => x.genre)) item.order = neworder++;
            Save();
        }
        private void SortFilename_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> newlist = new List<AAList>(AALists);
            int neworder = 0;
            foreach (AAList item in newlist.OrderBy(x => x.filename)) item.order = neworder++;
            Save();
        }
        private void PLSortDate_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> newlist = new List<AAPList>(AAPLists);
            int neworder = 0;
            foreach (AAPList item in newlist.OrderBy(x => x.dateAdded)) item.order = neworder++;
            Save();
        }
        private void PLSortTitle_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> newlist = new List<AAPList>(AAPLists);
            int neworder = 0;
            foreach (AAPList item in newlist.OrderBy(x => x.title)) item.order = neworder++;
            Save();
        }
        private void PLSortArtist_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> newlist = new List<AAPList>(AAPLists);
            int neworder = 0;
            foreach (AAPList item in newlist.OrderBy(x => x.artist)) item.order = neworder++;
            Save();
        }
        private void PLSortGenre_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> newlist = new List<AAPList>(AAPLists);
            int neworder = 0;
            foreach (AAPList item in newlist.OrderBy(x => x.genre)) item.order = neworder++;
            Save();
        }
        private void PLSortFilename_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> newlist = new List<AAPList>(AAPLists);
            int neworder = 0;
            foreach (AAPList item in newlist.OrderBy(x => x.filename)) item.order = neworder++;
            Save();
        }
        private void SortReverse_Click(object sender, RoutedEventArgs e)
        {
            List<AAList> newlist = new List<AAList>(AALists.OrderByDescending(o => o.order));
            int count = newlist.Count;
            int neworder = 0;
            foreach (AAList item in newlist) item.order = neworder++;
            Save();
        }
        private void PLReverse_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> newlist = new List<AAPList>(AAPLists.OrderByDescending(o => o.order));
            int count = newlist.Count;
            int neworder = 0;
            foreach (AAPList item in newlist) item.order = neworder++;
            Save();
        }
        private void PLMoveAfterCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (curplay != null)
            {
                plsearch.Text = "Search...";
                int count = playlistgrid.Items.Count;
                int newcount = (int)curplay.order + 1;
                for (int i = 0; i < count; i++)
                {
                    DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                    if (row.IsSelected && (AAPList)row.Item != curplay) ((AAPList)row.Item).order = newcount++;
                }
                for (int i = 0; i < count; i++)
                {
                    DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                    if (!row.IsSelected && ((AAPList)row.Item).order > curplay.order) ((AAPList)row.Item).order = newcount++;
                }
                Save();
            }
        }
        //sort methods end here
        private void PLRemoveRepeat_Click(object sender, RoutedEventArgs e)
        {
            foreach (AAPList item in playlistgrid.SelectedItems.Cast<AAPList>()) if (item.after == (int)After.Repeat) item.after = null;
            Save();
        }
        private void PLRemoveStop_Click(object sender, RoutedEventArgs e)
        {
            foreach (AAPList item in playlistgrid.SelectedItems.Cast<AAPList>()) if (item.after == (int)After.Stop) item.after = null;
            Save();
        }
        private void PLStopAfterCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (curplay != null)
            {
                curplay.after = (int)After.Stop;
                Save();
            }
        }
        private void PLStopAfterSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (AAPList item in playlistgrid.SelectedItems.Cast<AAPList>()) item.after = (int)After.Stop;
            Save();
        }
        private void PLDuplicate_Click(object sender, RoutedEventArgs e)
        {
            foreach (AAPList item in playlistgrid.SelectedItems.Cast<AAPList>()) AAPLists.Insert(AAPLists.IndexOf(item), new AAPList(item));
            Save();
        }
        private void PLRemDup_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> SortedList = AAPLists.OrderBy(o => o.fileAndPath).ToList();
            List<AAPList> templist = new List<AAPList>();
            for (int i = 1; i < SortedList.Count; i++)
            {
                if (SortedList.ElementAt(i).fileAndPath == SortedList.ElementAt(i - 1).fileAndPath)
                {
                    templist.Add(SortedList.ElementAt(i));
                }
            }
            foreach (AAPList item in templist) AAPLists.Remove(item);
            templist = null;
            SortedList = null;
            Save();
        }
        private void PLRemMissing_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> templist = new List<AAPList>();
            foreach (AAPList item in AAPLists)
            {
                if (!System.IO.File.Exists(item.fileAndPath)) templist.Add(item);
            }
            foreach (AAPList item in templist) AAPLists.Remove(item);
            templist = null;
            Save();
        }
        private void PLRemSel_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> templist = new List<AAPList>();
            foreach (AAPList item in playlistgrid.SelectedItems.Cast<AAPList>()) templist.Add(item);
            foreach (AAPList item in templist) AAPLists.Remove(item);
            templist = null;
            Save();
        }
        private void PLCrop_Click(object sender, RoutedEventArgs e)
        {
            List<AAPList> templist = new List<AAPList>();
            for (int i = 0; i < playlistgrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (!row.IsSelected) templist.Add((AAPList)row.Item);
            }
            foreach (AAPList item in templist) AAPLists.Remove(item);
            templist = null;
            Save();
        }
        
        private void PLInsertRepeat_Click(object sender, RoutedEventArgs e)
        {
            foreach (AAPList item in playlistgrid.SelectedItems.Cast<AAPList>()) item.after = (int)After.Repeat;
            Save();
        }
        private void PLInvertSel_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < playlistgrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(i);
                row.IsSelected = !row.IsSelected;
            }
        }
        private void PLSelAll_Click(object sender, RoutedEventArgs e)
        {
            playlistgrid.SelectAll();
        }
        private void PLSelNone_Click(object sender, RoutedEventArgs e)
        {
            playlistgrid.UnselectAll();
        }
        private void PLSaveList_Click(object sender, RoutedEventArgs e)
        {
            M3UFile _m3uFile = new M3UFile();
            foreach (AAPList entry in AAPLists) {
                M3UEntry line = new M3UEntry(entry.lengthAsTimeSpan,entry.displayName,entry.pathAsURI);
                _m3uFile.Add(line);
            }
            if (_m3uFile != null)
            {
                using (var sfd = new SaveFileDialog() { Filter = "M3U File (*.m3u)|*.m3u", Title = "Save Playlist" })
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _m3uFile.Save(sfd.FileName);
                    }
                }
            }
        }
        private void PLLoadList_Click(object sender, RoutedEventArgs e)
        {
            M3UFile _m3uFile = new M3UFile();
            using (var ofd = new OpenFileDialog { Filter = "M3U File (*.m3u)|*.m3u", Title = "Open Playlist" })
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        Task task2 = Task.Run(new Action(() =>
                        {
                            _m3uFile.Load(ofd.FileName);
                            foreach (var entry in _m3uFile)
                            {
                                AAPList plist = new AAPList(entry,AAPLists.Count);
                                playlistgrid.Dispatcher.BeginInvoke(new Action(() => AAPLists.Add(plist)));
                            }
                            Save();
                        }));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                    if (_m3uFile != null) {
                        
                    }
                }
            }
        }
        
        private void PLNewList_Click(object sender, RoutedEventArgs e)
        {
            AAPLists.Clear();
            Save();
        }
        private void VolChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Calculate the volume that's being set
            double NewVolume = ((ushort.MaxValue / 10) * trackWave.Value);
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            // Set the volume
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }
        private void PositionChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ignoreevent)  myControl.MediaPlayer.Position = (float)(PositionSlider.Value/ 10);
        }
        private void ChangeDisplayTime(object sender, MouseButtonEventArgs e)
        {
            switch (displayOption)
            {
                case DisplayOptions.Elapsed: displayOption = DisplayOptions.ElapsedAndTotal; break;
                case DisplayOptions.ElapsedAndTotal: displayOption = DisplayOptions.Remaining; break;
                case DisplayOptions.Remaining: displayOption = DisplayOptions.Elapsed; break;
            }
        }
        private void PLSearch(object sender = null, TextChangedEventArgs e = null)
        {
            try
            {
                ViewSource2.View.Refresh();
            }
            catch (Exception ex) { }
        }
        private void Search(object sender = null, TextChangedEventArgs e = null)
        {
            try
            {
                ViewSource.View.Refresh();
            }
            catch (Exception ex) { }
        }
        private void AddToPlayList(object sender = null, MouseButtonEventArgs e = null)
        {
            foreach (AAList item in fulllistgrid.SelectedItems.Cast<AAList>()) { AAPLists.Add(new AAPList(item,AAPLists.Count)); }
            Save();
        }
        private void PLPlay(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            RowBold();
            curplay = row.Item as AAPList;
            Play();
        }
        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            int muteStatus = muted ? 1 : 0;
            MM.SetVolume(spkrMuteControl, spkrComponent, muteStatus);
            muted = !muted;
        }
        //custom add file method
        private void AddFile(string filename)
        {
            if (!System.IO.File.Exists(filename)) return;
            AAList addedsong = new AAList();
            addedsong.path = Path.GetDirectoryName(filename);
            addedsong.filename = Path.GetFileName(filename);
            try
            {
                TagLib.File f = TagLib.File.Create(filename);
                if (f.Tag.Genres.Length != 0) addedsong.genre = f.Tag.Genres.GetValue(0).ToString();
                if (f.Tag.Performers.Length != 0) addedsong.artist = f.Tag.Performers.GetValue(0).ToString();
                if (f.Tag.Title != null) addedsong.title = f.Tag.Title;
            }
            catch (UnsupportedFormatException e) { Console.WriteLine("UnsupportedFormat Exception" + e.Message); }
            addedsong.dateAdded = DateTime.Now;
            addedsong.order = AALists.Count;
            try
            {
                Mp3FileReader naudio = new Mp3FileReader(filename);
                if (naudio != null) addedsong.length = (int)naudio.TotalTime.TotalSeconds;
            }
            catch (InvalidDataException e) { Console.WriteLine("InvalidData Exception " + e.Message); }
            catch (InvalidOperationException e) { Console.WriteLine("InvalidOperation Exception " + e.Message); }
            catch (IOException e) { Console.WriteLine("IO Exception " + e.Message); }
            catch (Exception e) { Console.WriteLine("Some other exception" + e.Message); }
            //these actions called from the UI thread
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => { AALists.Add(addedsong); RemainingLabel.Visibility = Visibility.Visible;  RemainingLabel.Content = remaining + " files remaining"; }));
        }
        //custom play method
        private void Play()
        {       
            //I had to add the 50 millisecond timer here before actually playing the song or it would freeze up playing on the track end event.  stupid hack
            System.Timers.Timer timer1 = new System.Timers.Timer();
            timer1.Elapsed += new ElapsedEventHandler(anotherPlay);
            timer1.Interval = 50;
            timer1.Enabled = true;
            timer1.AutoReset = false;
        }
        private void anotherPlay(object sender, ElapsedEventArgs e)
        {
            Uri songfile = new Uri(curplay.fileAndPath);
            RowBold(true);
            myControl.MediaPlayer.Play(songfile);
        }
        //method used for initilizing VLC Plugin
        private void OnVlcControlNeedsLibDirectory(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"..\..\lib\x86\"));
        }
        //shuffle method for randomizing list
        public static void Shuffle<T>(IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox me = (System.Windows.Controls.TextBox)sender;
            if (me.Text == "Search...") me.Text = "";
        }
        private void search_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox me = (System.Windows.Controls.TextBox)sender;
            if (me.Text == "") me.Text = "Search...";
        }
        //cancel adding files if necessary before closing
        private void closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cts.Cancel();
            e.Cancel = true;
            if (task1 == null || task1.IsCompleted) System.Windows.Application.Current.Shutdown();
            else { 
            System.Timers.Timer timer1 = new System.Timers.Timer();
            timer1.Elapsed += new ElapsedEventHandler(tryclose);
            timer1.Interval = 50;
            timer1.Enabled = true;
            timer1.AutoReset = false;
            }
        }

private void tryclose(object sender, ElapsedEventArgs e)
{
    if (task1 == null || task1.IsCompleted) System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => { System.Windows.Application.Current.Shutdown(); }));
    else
    {
        System.Timers.Timer timer1 = new System.Timers.Timer();
        timer1.Elapsed += new ElapsedEventHandler(tryclose);
        timer1.Interval = 50;
        timer1.Enabled = true;
        timer1.AutoReset = false;
    }
}

        private void Save()
        {
            if (task1 != null && task1.Status != TaskStatus.Running) DBContext.SaveChanges();
            Search();
            PLSearch();
            ViewSource2.SortDescriptions.Add(new SortDescription("order", ListSortDirection.Ascending));
            ViewSource2.View.Refresh();
            ViewSource.SortDescriptions.Add(new SortDescription("order", ListSortDirection.Ascending));
            ViewSource.View.Refresh();
        }
        //a method to take off bold on currently playing file (before stopping)
        private void RowBold(bool on = false)
        {
            if (curplay != null)
            {
                List<AAPList> templist = new List<AAPList>(AAPLists.OrderBy(o => o.order));
                DataGridRow row2 = (DataGridRow)playlistgrid.ItemContainerGenerator.ContainerFromIndex(templist.IndexOf(curplay));
                if (row2 != null) System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Style newStyle = new Style(row2.GetType());
                    if (on) { newStyle.Setters.Add(bold); }
                    else newStyle.Setters.Add(normal);
                    row2.Style = newStyle;
                }));
            }
       }

        private void AddToPlaylist2(object sender, RoutedEventArgs e)
        {
            AddToPlayList();
        }
        //method to open context menu with left click
        private void OpenCM(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            btn.ContextMenu.PlacementTarget = btn;
            btn.ContextMenu.Placement = PlacementMode.Top;
            btn.ContextMenu.IsOpen = true;
        }
    }
}