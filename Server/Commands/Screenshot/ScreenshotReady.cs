﻿using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class ScreenshotReady : Command {
        Texture2D screenshot;
        Vector2 size;

        public ScreenshotReady(Texture2D screenshot, Vector2 size) {
            this.screenshot = screenshot;
            this.size = size;
        }

        public override string Execute() {
            int width = (int)size.x;
            int height = (int)size.y;

            var heightDifference = screenshot.height - height;
            var widthDifference = screenshot.width - width;
            if (heightDifference >= 0 || widthDifference >= 0) {
                if (heightDifference > widthDifference) {
                    width = height * screenshot.width / screenshot.height;
                } else {
                    height = width * screenshot.height / screenshot.width;
                }
            }
            string[] fullResponse = new string[5];

            fullResponse[0] = Newtonsoft.Json.JsonConvert.SerializeObject(new Vector2(screenshot.width, screenshot.height), new Newtonsoft.Json.JsonSerializerSettings {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            TextureScale.Bilinear(screenshot, width, height);
            screenshot.Apply(true);
            screenshot.Compress(false);
            screenshot.Apply(false);


            var screenshotSerialized = screenshot.GetRawTextureData();
            TestRunner.Instance.LogMessage(screenshotSerialized.LongLength + " size after Unity Compression");
            TestRunner.Instance.LogMessage(System.DateTime.Now + " Start Compression");
            var screenshotCompressed = TestRunner.CompressScreenshot(screenshotSerialized);
            Debug.Log(System.DateTime.Now + " Finished Compression");
            var length = screenshotCompressed.LongLength;
            fullResponse[1] = length.ToString();

            var format = screenshot.format;
            fullResponse[2] = format.ToString();

            var newSize = new Vector3(screenshot.width, screenshot.height);
            fullResponse[3] = Newtonsoft.Json.JsonConvert.SerializeObject(newSize, new Newtonsoft.Json.JsonSerializerSettings {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            Debug.Log(System.DateTime.Now + " Serialize screenshot");
            fullResponse[4] = Newtonsoft.Json.JsonConvert.SerializeObject(screenshotCompressed, new Newtonsoft.Json.JsonSerializerSettings {
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii
            });

            TestRunner.Instance.LogMessage(System.DateTime.Now + " Finished Serialize Screenshot Start serialize response");
            TestRunner.Instance.LogMessage(System.DateTime.Now + " Finished send Response");
            Object.Destroy(screenshot);
            TestRunner.Instance.destroyHightlight = true;
            return Newtonsoft.Json.JsonConvert.SerializeObject(fullResponse);
        }
    }
}
