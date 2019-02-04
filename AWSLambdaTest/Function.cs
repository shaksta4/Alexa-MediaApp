using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.APL;
using Amazon.Lambda.Core;
using Alexa.NET.Response.APL;
using Alexa.NET.APL.Components;
using Alexa.NET.APL.Commands;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaTest
{

    public class Function
    {

        private IDirective GetVideoPlayerDirective(SkillRequest input, string url)
        {
            VideoAppDirective dir = new VideoAppDirective();
            VideoItem item = new VideoItem(url);
            VideoItemMetadata meta = new VideoItemMetadata();
            meta.Title = "Test";

            item.Metadata = meta;
            dir.VideoItem = item;
            
            return dir;
        }

        private List<string> GetMediaToPlay(string id)
        {
            string audioURL = "Unknown audio Url";
            string videoURL = "Unknown video Url";
            string imageURL = "Unknown image Url";
            string mediaTitle = "Unknown title";
            var requestValue = id; // Get ID of request

            switch (requestValue) // Depending on the request ID, play a different track.
            {
                case "10_triggers":
                    mediaTitle = "10 Triggers to help you sleep";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+10+Triggers+to+Help+You+Sleep+%E2%99%A5.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/10+triggers.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+10+Triggers+to+Help+You+Sleep.mp4";
                    break;
                case "20_triggers":
                    mediaTitle = "20 Triggers to help you sleep";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+20+Triggers+To+Help+You+Sleep+%E2%99%A5.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/20+triggers.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+20+Triggers+To+Help+You+Sleep.mp4";
                    break;
                case "100_triggers":
                    mediaTitle = "100 Triggers to help you sleep";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+A-Z+Triggers+to+Help+You+Sleep+%E2%99%A5+2+HOURS.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/100+triggers.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+100+Triggers+To+Help+You+Sleep+4+HOURS.mp4";
                    break;
                case "AZ_triggers":
                    mediaTitle = "A to Z Triggers to help you sleep";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+A-Z+Triggers+to+Help+You+Sleep+%E2%99%A5+2+HOURS.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/AZ+triggers.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+A-Z+Triggers+to+Help+You+Sleep+2+HOURS.mp4";
                    break;
                case "brushing_microphone":
                    mediaTitle = "Brushing the microphone with different brushes";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+Brushing+the+Microphone+With+Different+Brushes.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/brushing+micro.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+Brushing+the+Microphone+With+Different+Brushes.mp4";
                    break;
                case "personal_attention":
                    mediaTitle = "Close up personal attention to help you sleep";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+Close+Up+Personal+Attention+For+You+To+Sleep+%E2%99%A5.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/close+up+personal.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+Close+Up+Personal+Attention+For+You+To+Sleep.mp4";
                    break;
                case "head_massage":
                    mediaTitle = "Relaxing head massage";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+Relaxing+Head+Massage+%E2%99%A5+Face+and+Scalp+Massage.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/head+massage.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+Relaxing+Head+Massage+Face+and+Scalp+Massage.mp4";
                    break;
                case "scalp_massage":
                    mediaTitle = "Relaxing scalp massage";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+Relaxing+Scalp+Massage+%E2%99%A5.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/scalp+massage.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+Relaxing+Scalp+Massage.mp4";
                    break;
                case "tapping_scratching":
                    mediaTitle = "Whispered tapping and scratching";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/ASMR+Whispered+Tapping+and+Scratching.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/tapping+n+scratching.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/ASMR+Whispered+Tapping+and+Scratching.mp4";
                    break;
                case "asmr":
                    mediaTitle = "What is A.S.M.R.";
                    audioURL = "https://s3.amazonaws.com/darling-asmr/What+is+ASMR.m4a";
                    imageURL = "https://s3.amazonaws.com/darling-asmr/Images/What+is+asmr.jpg";
                    videoURL = "https://s3.amazonaws.com/darling-asmr/Videos/What+is+ASMR.mp4";
                    break;
            }

            List<string> myList = new List<string>();
            myList.Add(audioURL);
            myList.Add(videoURL);
            myList.Add(imageURL);
            myList.Add(mediaTitle);

            return myList;
        }

        private string GetErrorMessage(SystemExceptionRequest sysException)
        {

            string message = sysException.Error.Message;
            string reqID = sysException.ErrorCause.requestId;
            string logString = null;

            switch (sysException.Error.Type)
            {
                case ErrorType.InvalidResponse:
                    logString ="The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.DeviceCommunicationError:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.InternalError:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.MediaErrorUnknown:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.InvalidMediaRequest:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.MediaServiceUnavailable:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.InternalServerError:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                case ErrorType.InternalDeviceError:
                    logString = "The error is: " + message + ". And the req ID is: " + reqID;
                    break;
                default:
                    logString = "Unknown Exception";
                    break;
            }

            return logString;
        }

        private Image InitButtonImage(APLSkillRequest input, string url, string id)
        {
            Image myImage = new Image(url);

            if (input.Context.Viewport.Shape is ViewportShape.Round)
            {
                myImage.Width = "75vw"; 
                myImage.Height = "75vh";
            }
            else
            {
                myImage.Width = "33vw";
                myImage.Height = "33vh";
            }
            myImage.Id = id; 

            return myImage;
        }

        private Container CreateTouchableButton(APLSkillRequest input, string url, string id, string title)
        {
            List<string> myArguments = new List<string>();
            myArguments.Add(id);

            Container myContainer = new Container(new APLComponent[]
            {
                new TouchWrapper(InitButtonImage(input, url, id))
                {
                    OnPress = new SendEvent() { Arguments = myArguments } // Send the image id here
                },
                new Text(title) { FontSize = "16" }
            });
            
            return myContainer;
        }

        private Container[] CreateMyButtons(APLSkillRequest input)
        {
            Container[] myButtons = new Container[10];

            myButtons[0] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/10+triggers.jpg", "10_triggers", "Ten Triggers");
            myButtons[1] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/20+triggers.jpg", "20_triggers", "Twenty Triggers");
            myButtons[2] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/100+triggers.jpg", "100_triggers", "100 Triggers");
            myButtons[3] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/AZ+triggers.jpg", "AZ_triggers", "A to Z Triggers");
            myButtons[4] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/brushing+micro.jpg", "brushing_microphone", "Brushing the Microphone");
            myButtons[5] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/close+up+personal.jpg", "personal_attention", "Personal Attention");
            myButtons[6] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/head+massage.jpg", "head_massage", "Relaxing Head Massage");
            myButtons[7] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/scalp+massage.jpg", "scalp_massage", "Relaxing Scalp Massage");
            myButtons[8] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/tapping+n+scratching.jpg", "tapping_scratching", "Whispered Tapping and Scratching");
            myButtons[9] = CreateTouchableButton(input, "https://s3.amazonaws.com/darling-asmr/Images/What+is+asmr.jpg", "asmr", "What is ASMR");

            return myButtons;
        }

        private SkillResponse PlayAudio(string audioURL, string imageURL, string mediaTitle)
        {
            IOutputSpeech innerResponse;

            AudioItemMetadata audioMetaData = new AudioItemMetadata();
            AudioItemSources imageSources = new AudioItemSources();
            imageSources.Sources.Add(new AudioItemSource(imageURL));

            audioMetaData.Art = imageSources;
            audioMetaData.Title = "Darling ASMR videos";
            audioMetaData.Subtitle = "Filler text";

            var audioResponse = ResponseBuilder.AudioPlayerPlay(PlayBehavior.ReplaceAll, audioURL, mediaTitle);
            audioResponse.Response.ShouldEndSession = true;

            innerResponse = new PlainTextOutputSpeech() { Text = "Now playing: " + mediaTitle };

            audioResponse.Response.OutputSpeech = innerResponse;
            return audioResponse;
        }

        private SkillResponse PlayVideo(APLSkillRequest input, string videoURL, string imageURL, string mediaTitle)
        {
            SkillResponse videoResponse = new SkillResponse();
            videoResponse.Response = new ResponseBody();

            videoResponse.Response.Directives.Add(GetVideoPlayerDirective(input, videoURL));
            videoResponse.SessionAttributes = null;
            videoResponse.Response.ShouldEndSession = null;
            videoResponse.Version = "1.0";
            videoResponse.Response.OutputSpeech = new PlainTextOutputSpeech() { Text = "Attempting to play the video" + mediaTitle };
            return videoResponse;
        }
        
        public SkillResponse FunctionHandler(APLSkillRequest input, ILambdaContext context)
        {
            var log = context.Logger;
            bool VideoAppSupport = false;

            if (input.Context.System.Device.IsInterfaceSupported("VideoApp"))
            {
                VideoAppSupport = true;
            }

            new UserEventRequestHandler().AddToRequestConverter();

            IOutputSpeech innerResponse = null;
            var reprompt = new Reprompt();

            string audioURL = "Your audio URL is incorrect or outdated!";
            string videoURL = "Your audio URL is incorrect or outdated!";
            string imageURL = "Your image URL is incorrect or outdated!";
            string mediaTitle = "Unknown title";


            if (input.Request is LaunchRequest)
            {
                // default launch request, let's just let them know what you can do
                log.LogLine($"Default LaunchRequest made");

                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = "Welcome to a.s.m.r. darling. Tell me what you want to listen to";
            }
            else if (input.Request is IntentRequest)
            {
                log.LogLine($"An IntentRequest was made");
                var intentRequest = input.Request as IntentRequest;
                var output = new PlainTextOutputSpeech();
                
                switch (intentRequest.Intent.Name)
                {
                    case "PlayMedia":
                        log.LogLine($"Attempting to play media");
                        string requestId = intentRequest.Intent.Slots["Video.Title"].Resolution.Authorities[0].Values[0].Value.Id;

                        if (intentRequest.DialogState.Equals("COMPLETED"))
                        {
                            log.LogLine($"DialogState is Complete");
                            List<string> myList = GetMediaToPlay(requestId);
                            audioURL = myList[0];
                            videoURL = myList[1];
                            imageURL = myList[2];
                            mediaTitle = myList[3];
                        }

                        //If it's possible to play video, send video response.
                        if (VideoAppSupport)
                        {
                            log.LogLine("Playing Video");
                            return PlayVideo(input, videoURL, imageURL, mediaTitle);
                        }

                        log.LogLine("Playing Audio");
                        return PlayAudio(audioURL, imageURL, mediaTitle);

                    case "AMAZON.HelpIntent":
                        output.Text = "You can ask me to play an audio title. Try asking me to play what is a.s.m.r.";
                        reprompt.OutputSpeech = new PlainTextOutputSpeech() { Text = "Say play what is a.s.m.r." };
                        return ResponseBuilder.Ask(output, reprompt);

                    case "AMAZON.PauseIntent":
                        log.LogLine("Stopping audio");
                        var pauseResponse = ResponseBuilder.AudioPlayerStop();
                        pauseResponse.Response.OutputSpeech = new PlainTextOutputSpeech() { Text = " Stopping the audio " };
                        return pauseResponse;

                    case "AMAZON.StopIntent":
                        log.LogLine("Stopping audio");
                        var stopResponse = ResponseBuilder.AudioPlayerStop();
                        stopResponse.Response.OutputSpeech = new PlainTextOutputSpeech() { Text = " Stopping the audio " };
                        return stopResponse;

                    case "AMAZON.FallbackIntent":
                        log.LogLine("Fallback intent triggered");
                        innerResponse = new PlainTextOutputSpeech() { Text = "Sorry, I didn't get that." };
                        reprompt.OutputSpeech = new PlainTextOutputSpeech() { Text = "What did you want me to do again?" };
                        var fallbackResponse = ResponseBuilder.Ask(innerResponse, reprompt);
                        return fallbackResponse;

                    default:
                        log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                        break;
                }
            }
            else if (input.Request is AudioPlayerRequest)
            {
                var audioRequest = input.Request as AudioPlayerRequest;

                if (audioRequest.AudioRequestType == AudioRequestType.PlaybackStarted)
                {
                    log.LogLine($"PlaybackStarted Triggered ");
                    // respond with Stop or ClearQueue
                    return ResponseBuilder.AudioPlayerClearQueue(ClearBehavior.ClearEnqueued);
                }
                else
                {
                    log.LogLine("This is triggering. DO SOMETHING HERE!");
                    return null;
                }
            }
            else if (input.Request is SystemExceptionRequest)
            {
                var sysException = input.Request as SystemExceptionRequest;
                log.LogLine(GetErrorMessage(sysException));
            }
            else if (input.Request is SessionEndedRequest)
            {
                var sessEndReq = input.Request as SessionEndedRequest;

                switch (sessEndReq.Reason)
                {
                    case Reason.UserInitiated:
                        log.LogLine("User has quit the activity");
                        break;
                    case Reason.Error:
                        log.LogLine("There seems to be an error somewhere: ");
                        break;
                    case Reason.ExceededMaxReprompts:
                        log.LogLine("Too many reprompts!!!");
                        break;
                }
            }
            else if(input.Request is UserEventRequest)
            {
                //CHECK IF BUTTON IS BEING PRESSED ---------------- TO DO IF MORE INTERACTABLE CONTENT IS MADE
                log.LogLine("User event initiated!");
                var userReq = input.Request as UserEventRequest;

                log.LogLine("The argument being sent is:" + userReq.Arguments[0]);

                List<string> myList = GetMediaToPlay(userReq.Arguments[0]);
                audioURL = myList[0];
                videoURL = myList[1];
                imageURL = myList[2];
                mediaTitle = myList[3];

                log.LogLine("Attempting to play: " + mediaTitle);

                return PlayVideo(input, videoURL, imageURL, mediaTitle);
            }

            reprompt.OutputSpeech = new PlainTextOutputSpeech() { Text = "Please tell me what video you wanted to watch." };
            var response = ResponseBuilder.Ask(innerResponse, reprompt);

            //Create response to send if nothing else is triggered.
            SkillResponse defaultResponse = new SkillResponse();
            defaultResponse.Response = response.Response;
            defaultResponse.Version = "1.0";

            //If video player is supported, create a render document as well.
            if (VideoAppSupport)
            {
                var directive = new RenderDocumentDirective()
                {
                    Token = "randomToken",
                    Document = new APLDocument
                    {
                        MainTemplate = new Layout(new[]
                        {
                            new Container(new APLComponent[]{
                                new Sequence( CreateMyButtons(input)
                                ) { ScrollDirection = "horizontal", Numbered = true, Width = "100vw", Height = "80vh", AlignSelf = "Center",  Spacing = "10vw"}
                            })
                        })
                    }
                };

                defaultResponse.Response.Directives.Add(directive);
            }

            return defaultResponse;
        }
    }
}
