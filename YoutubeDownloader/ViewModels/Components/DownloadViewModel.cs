﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Gress;
using YoutubeDownloader.Models;
using YoutubeDownloader.Services;
using YoutubeExplode.Models;
using PropertyChangedBase = Stylet.PropertyChangedBase;

namespace YoutubeDownloader.ViewModels.Components
{
    public class DownloadViewModel : PropertyChangedBase
    {
        private readonly DownloadService _downloadService;

        private CancellationTokenSource _cancellationTokenSource;

        public Video Video { get; set; }

        public string FilePath { get; set; }

        public string FileName => Path.GetFileName(FilePath);

        public string Format { get; set; }

        public DownloadOption DownloadOption { get; set; }

        public IProgressManager ProgressManager { get; set; }

        public IProgressOperation ProgressOperation { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsSuccessful { get; private set; }

        public bool IsCanceled { get; private set; }

        public bool IsFailed { get; private set; }

        public string FailReason { get; private set; }

        public DownloadViewModel(DownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        public bool CanStart => !IsActive;

        public void Start()
        {
            if (!CanStart)
                return;

            Task.Run(async () =>
            {
                // Create cancellation token source
                _cancellationTokenSource = new CancellationTokenSource();

                // Create progress operation
                ProgressOperation = ProgressManager.CreateOperation();

                try
                {
                    IsSuccessful = false;
                    IsCanceled = false;
                    IsFailed = false;
                    IsActive = true;

                    // If download option is not set - get the best download option
                    if (DownloadOption == null)
                        DownloadOption = await _downloadService.GetBestDownloadOptionAsync(Video.Id, Format);

                    // Download
                    await _downloadService.DownloadVideoAsync(DownloadOption, FilePath, ProgressOperation, _cancellationTokenSource.Token);

                    IsSuccessful = true;
                }
                catch (OperationCanceledException)
                {
                    IsCanceled = true;
                }
                catch (Exception ex)
                {
                    IsFailed = true;
                    FailReason = ex.Message;
                }
                finally
                {
                    IsActive = false;

                    _cancellationTokenSource.Dispose();
                    ProgressOperation.Dispose();
                }
            });
        }

        public bool CanCancel => IsActive && !IsCanceled;

        public void Cancel()
        {
            if (!CanCancel)
                return;

            _cancellationTokenSource.Cancel();
        }

        public bool CanShowFile => IsSuccessful;

        public void ShowFile()
        {
            if (!CanShowFile)
                return;

            // This opens explorer, navigates to the output directory and selects the video file
            Process.Start("explorer", $"/select, \"{FilePath}\"");
        }

        public bool CanOpenFile => IsSuccessful;

        public void OpenFile()
        {
            if (!CanOpenFile)
                return;

            // This opens the video file using the default player
            Process.Start(FilePath);
        }

        public bool CanRestart => !IsActive && !IsSuccessful;

        public void Restart() => Start();
    }
}