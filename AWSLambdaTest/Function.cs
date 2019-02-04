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
        /*This function takes in a skillrequest and a string,
         *It creates the Video Item, sets its url and adds meta data. And then returns the Directive
         */
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

        /*This function takes in a string which it uses to
         * compares to a number of cases to figure out what video/audio the user wants to play
         * It then sets the image, audio and video URLs the case that's requested.
         * It returns a list holding the audioURL, videoURL, imageURL and title of the media.
         */
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

        /*
         * This function takes in a system exception request. It returns a string.
         * It compares the request type to understand what type of exception is being thrown by the skill. 
         * And returns an appropriate string detailing the exception.
         */
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

        /*
         * This function takes in an APL skill request, and two strings and returns an Image object.
         * It creates an image object based on the url passed in and sets its id
         * based on the id passed in. 
         * It changes the width and height of the image based on whether the
         * videoport is round or not.           
         */
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

        /*
         * This function takes in an APL skill request, and 3 strings and it returns a Container object. 
         * The id of a button is added to a list "myArguments" as a new TouchableButton is created. 
         * Then a container is created which holds a touch wrapper and a text object.
         * The touchwrapper is wrapped around the image object returned by InitButtonImage.
         * It then sends the id of the tapped image as an argument.
         * The text object is the title of each touchable button.
         */
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

        /*
         * This object takes in an APL skill request and returns an array of Containers
         * Each element of this array is a new touchable button returned by the CreateTouchableButton function
         */
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

        /*
         * This function takes in 3 strings and returns a skill response.
         * It creates and sets the metadata for each audio item
         * It then uses the AudioPlayerPlay function to create a 
         * response to play the audio.
         */
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

        /*
         *This function takes in an APL skill request, and 3 strings. 
         * It uses GetVideoPlayerDirective, and adds it to the response directive. 
         * Then any mandatory fields are set (Version, ShouldEndSession) and it 
         * returns the response to play the video.
         */
        private SkillResponse PlayVideo(APLSkillRequest input, string videoURL, string mediaTitle)
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
        
        /*
         * The main function.
         * 
         * Takes in an APL skill request and an ILambdaContext and returns a skill response.
         * 
         * 
         */ 
        public SkillResponse FunctionHandler(APLSkillRequest input, ILambdaContext context)
        {
            var log = context.Logger;
            bool VideoAppSupport = false;

            //Check if devices supports a video app
            if (input.Context.System.Device.IsInterfaceSupported("VideoApp"))
            {
                VideoAppSupport = true;
            }

            //Done to handle user events from the touchable buttons.
            new UserEventRequestHandler().AddToRequestConverter();

            IOutputSpeech innerResponse = null;
            var reprompt = new Reprompt();

            //Default string values
            string audioURL = "Your audio URL is incorrect or outdated!";
            string videoURL = "Your audio URL is incorrect or outdated!";
            string imageURL = "Your image URL is incorrect or outdated!";
            string mediaTitle = "Unknown title";

            //What happens when launching the skill
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
                    //If playing audio/video
                    case "PlayMedia":
                        log.LogLine($"Attempting to play media");
                        //Find requested audio/video
                        string requestId = intentRequest.Intent.Slots["Video.Title"].Resolution.Authorities[0].Values[0].Value.Id;

                        if (intentRequest.DialogState.Equals("COMPLETED"))
                        {
                            log.LogLine($"DialogState is Complete");
                            List<string> myList = GetMediaToPlay(requestId);

                            //Setting URLs for the requested audio/video
                            audioURL = myList[0];
                            videoURL = myList[1];
                            imageURL = myList[2];
                            mediaTitle = myList[3];
                        }

                        //If it's possible to play video, send video response.
                        if (VideoAppSupport)
                        {
                            log.LogLine("Playing Video");
                            return PlayVideo(input, videoURL, mediaTitle);
                        }

                        //Else play audio
                        log.LogLine("Playing Audio");
                        return PlayAudio(audioURL, imageURL, mediaTitle);

                    //If user asks for help   
                    case "AMAZON.HelpIntent":
                        output.Text = "You can ask me to play an audio title. Try asking me to play what is a.s.m.r.";
                        reprompt.OutputSpeech = new PlainTextOutputSpeech() { Text = "Say play what is a.s.m.r." };
                        return ResponseBuilder.Ask(output, reprompt);

                    //If user asks to pause the audio
                    case "AMAZON.PauseIntent":
                        log.LogLine("Stopping audio");
                        var pauseResponse = ResponseBuilder.AudioPlayerStop();
                        pauseResponse.Response.OutputSpeech = new PlainTextOutputSpeech() { Text = " Stopping the audio " };
                        return pauseResponse;

                    //If user asks to stop the video (Pause intent and Stop intent are interchangeable, To Fix)
                    case "AMAZON.StopIntent":
                        log.LogLine("Stopping audio");
                        var stopResponse = ResponseBuilder.AudioPlayerStop();
                        stopResponse.Response.OutputSpeech = new PlainTextOutputSpeech() { Text = " Stopping the audio " };
                        return stopResponse;
                    
                    //If skill does not understand what user has asked for.
                    case "AMAZON.FallbackIntent":
                        log.LogLine("Fallback intent triggered");
                        innerResponse = new PlainTextOutputSpeech() { Text = "Sorry, I didn't get that." };
                        reprompt.OutputSpeech = new PlainTextOutputSpeech() { Text = "What did you want me to do again?" };
                        var fallbackResponse = ResponseBuilder.Ask(innerResponse, reprompt);
                        return fallbackResponse;

                    //Debug to help me figure out what intent is being called
                    default:
                        log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                        break;
                }
            }
            /*
             * Not entirely sure what this request is. I think it's requests made to the audio player mid playback. 
             */
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

            //If exception is thrown, log a message
            else if (input.Request is SystemExceptionRequest)
            {
                var sysException = input.Request as SystemExceptionRequest;
                log.LogLine(GetErrorMessage(sysException));
            }

            //If session is ended, log the reason. Used for debugging. 
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

            //If user presses a button.
            else if(input.Request is UserEventRequest)
            {
                //CHECK IF BUTTON IS BEING PRESSED ---------------- TO DO IF MORE INTERACTABLE CONTENT IS MADE
                log.LogLine("User event initiated!");
                var userReq = input.Request as UserEventRequest;

                log.LogLine("The argument being sent is:" + userReq.Arguments[0]);

                //Set audio, video and image urls as well as media title using GetMediaToPlay
                List<string> myList = GetMediaToPlay(userReq.Arguments[0]);
                audioURL = myList[0];
                videoURL = myList[1];
                imageURL = myList[2];
                mediaTitle = myList[3];

                log.LogLine("Attempting to play: " + mediaTitle);

                //Returns a video response. User is assumed to want to play a video, if menu is available. 
                return PlayVideo(input, videoURL, mediaTitle);
            }

            //Default speech response if nothing is triggered
            reprompt.OutputSpeech = new PlainTextOutputSpeech() { Text = "Please tell me what video you wanted to watch." };
            var response = ResponseBuilder.Ask(innerResponse, reprompt);

            //Create response to send if nothing else is triggered.
            SkillResponse defaultResponse = new SkillResponse();
            defaultResponse.Response = response.Response;
            defaultResponse.Version = "1.0";

            //If video player is supported, create a render document as well. CAN BE SEPERATED INTO A FUNCTION
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
