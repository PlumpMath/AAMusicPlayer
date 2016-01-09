using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAMusicPlayer2
{
    public partial class AAPList
    {
        public AAPList() { }
        public AAPList(AAPList old) {
            artist = old.artist;
            album = old.album;
            title = old.title;
            genre = old.genre;
            filename = old.filename;
            path = old.path;
            length = old.length;
            dateAdded = old.dateAdded;
        }
        public AAPList(M3UEntry entry,int count)
        {
            path = Path.GetDirectoryName(entry.Path.LocalPath);
            filename = Path.GetFileName(entry.Path.LocalPath);
            title = entry.Title;
            length = (int)entry.Duration.TotalSeconds;
            order = count;
        }
        public AAPList(AAList song,int count)
        {
            artist = song.artist;
            album = song.album;
            title = song.title;
            genre = song.genre;
            filename = song.filename;
            path = song.path;
            length = song.length;
            dateAdded = song.dateAdded;
            order = count;
        }
        public string fileAndPath { get { return path + "\\" + filename; } }
        public string displayName { get { if ((title == null) || (artist == null)) return Path.GetFileNameWithoutExtension(filename); else return artist + " - " + title; } }
        public string lenAsString { get {
            if (after == null) return String.Format("{0:mm\\:ss}", lengthAsTimeSpan);
            if (after == 1) return String.Format("{0:mm\\:ss}", lengthAsTimeSpan) + "■";
            return String.Format("{0:mm\\:ss}", lengthAsTimeSpan) + "▲";
            }
        }
        public TimeSpan lengthAsTimeSpan { get { if (length != null) return TimeSpan.FromSeconds((double)length); else return new TimeSpan(0); } }
        public Uri pathAsURI { get { return new Uri(fileAndPath); } }
    }
    public partial class AAList
    {
        public string fileAndPath { get { return path + "\\" + filename; } }
        public string displayName { get { if ((title == null) || (artist == null)) return Path.GetFileNameWithoutExtension(filename); else return artist + " - " + title; } }
        public string lenAsString { get { return String.Format("{0:mm\\:ss}", lengthAsTimeSpan); } }
        public TimeSpan lengthAsTimeSpan { get { if (length != null) return TimeSpan.FromSeconds((double)length); else return new TimeSpan(0); } }
    }
}
