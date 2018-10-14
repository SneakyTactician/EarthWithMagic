﻿using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.Util.Reusable;
using ProtoBuf;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeAPI.Visual.Rendering.Animation
{
    /// <summary>
    /// Used to hold the order of an animation sequence.
    /// </summary>
    [ProtoContract]
    public class AnimationSequence
    {
        /// <summary>
        /// Holds a collection of numbers that specify what frame number is to be displayed next.
        /// </summary>
        [ProtoMember(1)]
        public int[] FrameOrder { get; private set; }

        /// <summary>
        /// The target FPS for this animation.
        /// </summary>
        [ProtoMember(2)]
        public int FPS { get; private set; }

        /// <summary>
        /// Used to determine when to change frames.
        /// </summary>
        [ProtoMember(3)]
        private TickTimer FrameTimer;

        [ProtoMember(4)]
        private int CurrentFrame;

        /// <param name="frameOrder">The order the frames should be played in.</param>
        /// <param name="FPS">The target FPS for this animation.</param>
        public AnimationSequence(int[] frameOrder, int FPS)
        {
            this.FrameOrder = frameOrder;
            this.FPS = FPS;
            this.FrameTimer = new TickTimer(RenderInfo.GameFPS / FPS);
        }

        public void ResetToBeginning()
        {
            this.FrameTimer.Reset();
            this.CurrentFrame = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Frame">The position within a sprite sheet that should be played next.</param>
        /// <returns>Returns true if the AnimationSequence has completed, and has been reset.</returns>
        public bool Tick(out int Frame)
        {
            bool done = this.ShiftFrame();

            Frame = this.FrameOrder[this.CurrentFrame];
            return done;
        }

        /// <summary>
        /// Changes which frame is being displayed.
        /// </summary>
        /// <returns>True if the sequence has finished.</returns>
        private bool ShiftFrame()
        {
            if (this.FrameTimer.Allow())
            {
                Log.Debug("Frame before: " + this.CurrentFrame.ToString());
                this.CurrentFrame++;
                Log.Debug("Frame after: " + this.CurrentFrame.ToString());

                if (this.CurrentFrame == this.FrameOrder.Length)
                {
                    this.ResetToBeginning();
                    return true;
                }
            }

            return false;
        }
    }
}
