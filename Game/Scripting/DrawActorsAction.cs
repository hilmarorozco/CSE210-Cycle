using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService _videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            CycleA cycleA = (CycleA)cast.GetFirstActor("snake");
            List<Actor> segmentsA = cycleA.GetSegments();

            CycleB cycleB = (CycleB)cast.GetFirstActor("snake");
            List<Actor> segmentsB = cycleB.GetSegments();

            Actor scoreA = cast.GetFirstActor("score");
            Actor scoreB = cast.GetFirstActor("score");

            //Actor food = cast.GetFirstActor("food");
            List<Actor> messages = cast.GetActors("messages");
            
            _videoService.ClearBuffer();
            _videoService.DrawActors(segmentsA);
            _videoService.DrawActors(segmentsB);
            _videoService.DrawActor(scoreA);
            _videoService.DrawActor(scoreB);
            //_videoService.DrawActor(food);
            _videoService.DrawActors(messages);
            _videoService.FlushBuffer();
        }
    }
}