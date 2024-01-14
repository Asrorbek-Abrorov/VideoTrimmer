using System;
using System.Diagnostics;
using Spectre.Console;

public class VideoTrimmer
{
    public static void TrimVideo(string videoPath, int startSeconds, int endSeconds, string name)
    {
        // Check if FFmpeg is installed
        if (!IsFFmpegInstalled())
        {
            Console.WriteLine("FFmpeg is not installed or added to the system PATH.");
            return;
        }

        // Output video file name
        string outputVideoPath = $"/home/as_abrorov/Documents/{name}.mp4";

        // Generate FFmpeg command
        string command = $"-ss {startSeconds} -i \"{videoPath}\" -to {endSeconds} -c:v copy -c:a copy \"{outputVideoPath}\"";

        // Run FFmpeg command
        Process ffmpegProcess = new Process();
        ffmpegProcess.StartInfo.FileName = "ffmpeg";
        ffmpegProcess.StartInfo.Arguments = command;
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;
        ffmpegProcess.StartInfo.CreateNoWindow = true;
        ffmpegProcess.Start();

        // Wait for the process to finish
        ffmpegProcess.WaitForExit();

        Console.WriteLine("Video trimming completed!");
    }

    private static bool IsFFmpegInstalled()
    {
        // Check if FFmpeg is installed by running "ffmpeg -version" command
        Process ffmpegProcess = new Process();
        ffmpegProcess.StartInfo.FileName = "ffmpeg";
        ffmpegProcess.StartInfo.Arguments = "-version";
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;
        ffmpegProcess.StartInfo.CreateNoWindow = true;
        ffmpegProcess.Start();

        string output = ffmpegProcess.StandardOutput.ReadToEnd();
        ffmpegProcess.WaitForExit();

        return output.Contains("ffmpeg version");
    }

    public static void Main(string[] args)
    {
        string videoPath = "/home/as_abrorov/Downloads/video_2024-01-13_21-14-33.mp4";
        int startSeconds = AnsiConsole.Ask<int>("Enter the starting point in seconds : ");
        int endSeconds = AnsiConsole.Ask<int>("Enter the Ending point in seconds : ");
        string name = AnsiConsole.Ask<string>("Enter a name of the new video(don't enter an extension) : ");
        TrimVideo(videoPath, startSeconds, endSeconds, name);
    }
}