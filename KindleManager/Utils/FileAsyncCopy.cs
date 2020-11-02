using System.ComponentModel;
using System.IO;

//https://gist.github.com/szunyog/52390c6f8a61615dfc01

namespace KindleManager.Utils
{
    // Minimal async file copy using background worker

    public class FileAsyncCopy
    {
        private string _source;
        private string _target;
        BackgroundWorker _worker;
        public FileAsyncCopy(string source, string target)
        {
            if (!File.Exists(source))
                throw new FileNotFoundException(string.Format(@"Source file was not found. FileName: {0}", source));

            _source = source;
            _target = target;
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = false;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += DoWork;
        }


        private void DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _source;
            int bufferSize = 1024 * 512;
            using (FileStream inStream = new FileStream(_source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (FileStream fileStream = new FileStream(_target, FileMode.OpenOrCreate, FileAccess.Write))
            {
                int bytesRead = -1;
                var totalReads = 0;
                var totalBytes = inStream.Length;
                byte[] bytes = new byte[bufferSize];
                int prevPercent = 0;

                while ((bytesRead = inStream.Read(bytes, 0, bufferSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                    totalReads += bytesRead;
                    int percent = System.Convert.ToInt32(((decimal)totalReads / (decimal)totalBytes) * 100);
                    if (percent != prevPercent)
                    {
                        _worker.ReportProgress(percent);
                        prevPercent = percent;
                    }
                }
            }
        }

        public event ProgressChangedEventHandler ProgressChanged
        {
            add { _worker.ProgressChanged += value; }
            remove { _worker.ProgressChanged -= value; }
        }

        public event RunWorkerCompletedEventHandler Completed
        {
            add { _worker.RunWorkerCompleted += value; }
            remove { _worker.RunWorkerCompleted -= value; }
        }
        public void StartAsync()
        {
            _worker.RunWorkerAsync();
        }
    }



}
