using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace VideoApi.External
{
    public class VideoSplitter
    {
        public async Task SplitVideo(string video)
        {
            await Task.Run(() =>
            {
                // Create a directory to store the chunks if it doesn't exist
                string outputDirectory = Path.Combine(Path.GetDirectoryName(video), "VideoChunks");
                Directory.CreateDirectory(outputDirectory);

                // Get the duration of the input video
                string ffprobeOutput;
                using (Process ffprobeProcess = new Process())
                {
                    ffprobeProcess.StartInfo.FileName = "ffprobe";
                    ffprobeProcess.StartInfo.Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{video}\"";
                    ffprobeProcess.StartInfo.RedirectStandardOutput = true;
                    ffprobeProcess.StartInfo.UseShellExecute = false;
                    ffprobeProcess.StartInfo.CreateNoWindow = true;

                    ffprobeProcess.Start();
                    ffprobeOutput = ffprobeProcess.StandardOutput.ReadToEnd();
                    ffprobeProcess.WaitForExit();
                }

                double duration = double.Parse(ffprobeOutput);

                // Calculate the number of chunks
                int numberOfChunks = (int)Math.Ceiling(duration / 5);

                // Split the video into chunks
                for (int i = 0; i < numberOfChunks; i++)
                {
                    string outputFileName = Path.Combine(outputDirectory, $"video_chunk_{i + 1}.mp4");
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg",
                        Arguments = $"-ss {i * 5} -i \"{video}\" -t 5 -c copy \"{outputFileName}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using (var process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        process.WaitForExit();
                    }
                }
            });
            File.Delete(video);
        }
    }
}
