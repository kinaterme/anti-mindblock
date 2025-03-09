using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace antiMindblock.LinuxOS
{
    public class linuxFlipTabletArea
    {
        public void FlipTabletArea(double newRotationValue)
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(homeDirectory, ".config/OpenTabletDriver/settings.json");

            try
            {
                string jsonString = File.ReadAllText(filePath);

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    JsonObject root = JsonSerializer.Deserialize<JsonObject>(jsonString);

                    var profiles = root["Profiles"] as JsonArray;

                    if (profiles != null && profiles.Count > 0)
                    {
                        var firstProfile = profiles[0] as JsonObject;
                        if (firstProfile != null)
                        {
                            var absoluteModeSettings = firstProfile["AbsoluteModeSettings"] as JsonObject;
                            var tabletSettings = absoluteModeSettings?["Tablet"] as JsonObject;

                            if (tabletSettings != null)
                            {
                                tabletSettings["Rotation"] = JsonValue.Create(newRotationValue);

                                string updatedJsonString = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });

                                File.WriteAllText(filePath, updatedJsonString);

                                Console.WriteLine("Rotation value updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Tablet settings not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Profile not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Profiles array is empty or not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            string command = "systemctl --user restart opentabletdriver";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(startInfo);
        }
    }
}