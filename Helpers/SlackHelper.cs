using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using SD.Helpers;
using System.Diagnostics;
using SD.Hooks;
using System.Collections.Generic;

class SlackHelper
{


    public static async Task UploadFileToSlack(int passedCount,int failedCount)
    {
    string icon="";
    string message="";

  if (failedCount == 0 && passedCount > 0)
    {
        // All passed
        icon = ":large_green_circle:";
        message = "Test Results: "+passedCount+" Test Cases  Passed";
    }
    else if (failedCount > 0 && passedCount == 0)
    {
        // All failed
        icon = ":red_circle:";
        message = "Test Results: "+failedCount+" Test Cases Failed";
    }
    else if (failedCount > 0 && passedCount > 0)
    {
        // Partially succeeded
        icon = ":large_orange_circle:";
        message = "Test Results: Passed: " + passedCount + ", Failed: "+failedCount+" (Partially Succeeded)";
    }

        var Env=EnvironmentHelper.CurrentStage==Stage.SD_Production?"Production | ":"Stagging | "; //Need Update: if cases from stagging and production has been executed together, this will give incorrect environmemnt message

    // Generate the Slack message
        string slackMessage = $"{icon} {Env} {message} {icon}";

        await ShareTestResultsWithSlack(slackMessage);
        if (failedCount > 0)
        {
            await ShareFileToSlack(EnvironmentHelper.LivingDocfilePath);
        }
        var runId=EnvironmentHelper.CurrentStage==Stage.SD_Production?EnvironmentHelper.ProductionTestRunId:EnvironmentHelper.StaggingTestRunId;
        if (failedCount > 0)
        {
            string screenshotsFolderPath = Path.Combine(Environment.CurrentDirectory,"screenshots",runId.ToString());
            if (Directory.Exists(screenshotsFolderPath))
           {
            foreach (var screenshotPath in Directory.GetFiles(screenshotsFolderPath, "*.png"))
            {
                    // Share each screenshot with Slack
                    await ShareFileToSlack(screenshotPath);
            }
        }
        else
        {
            Console.WriteLine($"Screenshots folder not found: {screenshotsFolderPath}");
        }
        }
    }

    private static async Task ShareFileToSlack(string filePathToSlack)
    {
        try{
        using (HttpClient client = new HttpClient())
        {
            // Set the API endpoint
            string apiUrl = "https://slack.com/api/files.upload";

            // Set the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", EnvironmentHelper.slackToken);

            // Create the content for the file upload
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            using (FileStream fileStream = new FileStream(filePathToSlack, FileMode.Open))
            {
                // Add the file as content
                content.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePathToSlack));

                // Add other form data
                content.Add(new StringContent(EnvironmentHelper.channel), "channels");

                // Make the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"File uploaded successfully. Response: {responseContent}");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
        }
        }
        catch (Exception ex){
            Console.WriteLine("Share File To Slack Error: "+ex.Message);
        }
    }

    public static async Task GenerateLivingDocReport()
    {

try{
string workingDirectory = Environment.CurrentDirectory;
string projectDirectory2 = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        string testAssembly = workingDirectory+"\\SD.dll"; 
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = "livingdoc",
            Arguments = $"test-assembly {testAssembly} -t {workingDirectory}\\TestExecution.json --output {projectDirectory2}\\LivingDoc.html",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = processStartInfo })
        {
            process.Start();

            // Asynchronously read the standard output and error streams
            Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
            Task<string> errorTask = process.StandardError.ReadToEndAsync();

            await Task.WhenAll(outputTask, errorTask);

            // Print the output and error messages
            Console.WriteLine($"LivingDoc Output: {outputTask.Result}");
            Console.WriteLine($"LivingDoc Error: {errorTask.Result}");

            process.WaitForExit();
        }
}
catch (Exception ex){
    Console.WriteLine("Line 141: "+ex.Message);
}

    }

        private static async Task ShareTestResultsWithSlack(string message)
    {
        using (HttpClient client = new HttpClient())
        {
            string slackWebhookUrl = "https://hooks.slack.com/services/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";

            // Set the Content-Type header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Create the JSON payload
            string jsonPayload = $"{{\"text\":\"{message}\"}}";

            // Convert the JSON payload to StringContent
            var content = new StringContent(jsonPayload);

            // Make the POST request to Slack
            HttpResponseMessage response = await client.PostAsync(slackWebhookUrl, content);

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Test results shared with Slack. Response: {responseContent}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }

}
