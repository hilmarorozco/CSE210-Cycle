using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;
        private bool deadCycleA = false;
        private bool deadCycleB = false;
        private int i = 0;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleIncrement(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>

        private void HandleIncrement(Cast cast)
        {
            CycleA cycleA = (CycleA)cast.GetFirstActor("cycleA");
            CycleB cycleB = (CycleB)cast.GetFirstActor("cycleB");

            ScoreA scoreA = (ScoreA)cast.GetFirstActor("scoreA");
            ScoreB scoreB = (ScoreB)cast.GetFirstActor("scoreB");

            // private int increment = 1;
            cycleA.GrowTail(1);
            cycleB.GrowTail(1);            

            
            //Food food = (Food)cast.GetFirstActor("food");
            
            // for (int i = 0; i <= 10; i = i + 1)
            // {
            //     cycleA.GrowTail(1);
            //     cycleB.GrowTail(1);
            // }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            CycleA cycleA = (CycleA)cast.GetFirstActor("cycleA");
            Actor headA = cycleA.GetHead();
            List<Actor> bodyA = cycleA.GetBody();

            CycleB cycleB = (CycleB)cast.GetFirstActor("cycleB");
            Actor headB = cycleB.GetHead();
            List<Actor> bodyB = cycleB.GetBody();

            foreach (Actor segmentA in bodyA)
            {
                // if (segmentA.GetPosition().Equals(headB.GetPosition()))
                // {
                //     _isGameOver = true;
                // }
                if (segmentA.GetPosition().Equals(headA.GetPosition()))
                {
                    _isGameOver = true;
                }
            }

            foreach (Actor segmentB in bodyB) 
            {
                if (segmentB.GetPosition().Equals(headA.GetPosition()))
                {
                    _isGameOver = true;
                }
                // if (segmentB.GetPosition().Equals(headB.GetPosition()))
                // {
                //     _isGameOver = true;
                // }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                CycleA cycleA = (CycleA)cast.GetFirstActor("cycleA");
                List<Actor> segmentsA = cycleA.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");
                CycleB cycleB = (CycleB)cast.GetFirstActor("cycleB");
                List<Actor> segmentsB = cycleB.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segmentsA)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach (Actor segment in segmentsB)
                {
                    segment.SetColor(Constants.WHITE);
                }
                
                
                //food.SetColor(Constants.WHITE);
            }
        }

    }
}